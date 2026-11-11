using System.Collections;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Imaging;
using System.Runtime.InteropServices;

namespace Lumina.Forms;

/// <summary>Specifies the color depth of a native image list.</summary>
public enum ColorDepth
{
    Depth4Bit = 4,
    Depth8Bit = 8,
    Depth16Bit = 16,
    Depth24Bit = 24,
    Depth32Bit = 32,
}
/// <summary>Owns image data and a native image list independently of its consuming controls.</summary>
public sealed partial class ImageList : Component
{
    private NativeImageListHandle? _handle;
    private Size _imageSize = new(16, 16);
    private ColorDepth _colorDepth = ColorDepth.Depth32Bit;
    private Color _transparentColor = Color.Transparent;
    private bool _disposed;

    public ImageList() => Images = new ImageCollection(this);
    public ImageList(IContainer container) : this()
    {
        ArgumentNullException.ThrowIfNull(container);
        container.Add(this);
    }

    [Browsable(false), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public nint Handle => EnsureHandle().DangerousGetHandle();
    [Browsable(false), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public bool HandleCreated => _handle is { IsInvalid: false, IsClosed: false };
    [Localizable(true)]
    public Size ImageSize
    {
        get => _imageSize;
        set
        {
            if (value.Width < 1 || value.Width > 256 || value.Height < 1 || value.Height > 256)
                throw new ArgumentOutOfRangeException(nameof(value));
            if (_imageSize == value) return;
            ObjectDisposedException.ThrowIf(_disposed, this);
            _imageSize = value;
            RecreateNativeHandle();
        }
    }
    public ColorDepth ColorDepth
    {
        get => _colorDepth;
        set
        {
            if (!Enum.IsDefined(value)) throw new InvalidEnumArgumentException(nameof(value), (int)value, typeof(ColorDepth));
            if (_colorDepth == value) return;
            ObjectDisposedException.ThrowIf(_disposed, this);
            _colorDepth = value;
            RecreateNativeHandle();
        }
    }
    public Color TransparentColor
    {
        get => _transparentColor;
        set
        {
            ObjectDisposedException.ThrowIf(_disposed, this);
            if (_transparentColor == value) return;
            _transparentColor = value;
            RecreateNativeHandle();
        }
    }
    [Bindable(true), DefaultValue(null), Localizable(false), TypeConverter(typeof(StringConverter))]
    public object? Tag { get; set; }
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    public ImageCollection Images { get; }
    [Browsable(false)]
    public event EventHandler? RecreateHandle;
    internal event EventHandler? ImagesChanged;

    public void Draw(Graphics g, Point pt, int index) => Draw(g, pt.X, pt.Y, index);
    public void Draw(Graphics g, int x, int y, int index) => Draw(g, x, y, _imageSize.Width, _imageSize.Height, index);
    public void Draw(Graphics g, int x, int y, int width, int height, int index)
    {
        ArgumentNullException.ThrowIfNull(g);
        Images.ValidateIndex(index);
        NativeImageListHandle handle = EnsureHandle();
        nint dc = g.GetHdc();
        try
        {
            if (Native.DrawEx(handle, index, dc, x, y, width, height, 0xFFFFFFFF, 0xFFFFFFFF, 1) == 0)
                throw new Win32Exception(Marshal.GetLastWin32Error());
        }
        finally { g.ReleaseHdc(dc); }
    }
    public override string ToString() => $"{GetType().FullName} Images.Count: {Images.Count}, ImageSize: {ImageSize}";

    protected override void Dispose(bool disposing)
    {
        if (disposing && !_disposed)
        {
            _disposed = true;
            NativeImageListHandle? old = _handle;
            _handle = null;
            Images.ClearOriginals();
            // Consumers unbind in Disposed while the previous native handle is still valid.
            try { base.Dispose(true); }
            finally { old?.Dispose(); }
            return;
        }
        base.Dispose(disposing);
    }

    private NativeImageListHandle EnsureHandle()
    {
        ObjectDisposedException.ThrowIf(_disposed, this);
        if (!OperatingSystem.IsWindows()) throw new PlatformNotSupportedException("Native image lists require Windows.");
        if (HandleCreated) return _handle!;
        NativeImageListHandle result = new(Native.Create(_imageSize.Width, _imageSize.Height, (uint)_colorDepth | 1u, Images.Count, 4));
        if (result.IsInvalid) { result.Dispose(); throw new Win32Exception(Marshal.GetLastWin32Error()); }
        try
        {
            for (int index = 0; index < Images.Count; index++) AddNative(result, Images.GetEntry(index));
            _handle = result;
            Images.ReleaseOriginals();
            return result;
        }
        catch { result.Dispose(); throw; }
    }

    private void RecreateNativeHandle()
    {
        if (!HandleCreated) return;
        List<Image> snapshots = [];
        for (int index = 0; index < Images.Count; index++) snapshots.Add(Images[index]);
        NativeImageListHandle? old = _handle;
        _handle = null;
        Images.SetOriginals(snapshots);
        try
        {
            _ = EnsureHandle();
            RecreateHandle?.Invoke(this, EventArgs.Empty);
            ImagesChanged?.Invoke(this, EventArgs.Empty);
        }
        finally { old?.Dispose(); }
    }

    private void AddNative(NativeImageListHandle handle, ImageEntry entry)
    {
        using Bitmap bitmap = CreateBitmap(entry);
        nint nativeBitmap = bitmap.GetHbitmap(Color.FromArgb(0));
        try
        {
            if (Native.Add(handle, nativeBitmap, 0) < 0)
                throw new InvalidOperationException("The native image list could not add an image.");
        }
        finally { _ = Native.DeleteObject(nativeBitmap); }
    }

    private Bitmap CreateBitmap(ImageEntry entry)
    {
        Image original = entry.Original ?? throw new InvalidOperationException("The source image is unavailable.");
        Bitmap bitmap = new(_imageSize.Width, _imageSize.Height, PixelFormat.Format32bppArgb);
        try
        {
            using (Graphics graphics = Graphics.FromImage(bitmap))
                graphics.DrawImage(original, new Rectangle(Point.Empty, _imageSize));
            Color transparent = entry.TransparentColor ?? TransparentColor;
            if (transparent.A > 0) bitmap.MakeTransparent(transparent);
            return bitmap;
        }
        catch { bitmap.Dispose(); throw; }
    }

    internal sealed class ImageEntry
    {
        internal string Key = string.Empty;
        internal Image? Identity;
        internal Image? Original;
        internal Color? TransparentColor;
        internal void ReleaseOriginal() { Original?.Dispose(); Original = null; }
    }

    /// <summary>Provides the indexed and keyed images. Returned images are independent caller-owned copies.</summary>
    public sealed class ImageCollection : IList
    {
        private readonly ImageList _owner;
        private readonly List<ImageEntry> _entries = [];
        internal ImageCollection(ImageList owner) => _owner = owner;
        public int Count => _entries.Count;
        public bool Empty => Count == 0;
        public bool IsReadOnly => false;
        bool IList.IsFixedSize => false;
        bool ICollection.IsSynchronized => false;
        object ICollection.SyncRoot => this;

        public Image this[int index]
        {
            get
            {
                ValidateIndex(index);
                nint iconHandle = Native.GetIcon(_owner.EnsureHandle(), index, 1);
                if (iconHandle == 0) throw new InvalidOperationException("The native image list could not retrieve an image.");
                try
                {
                    Icon icon = Icon.FromHandle(iconHandle);
                    using Icon copy = (Icon)icon.Clone();
                    return copy.ToBitmap();
                }
                finally { _ = Native.DestroyIcon(iconHandle); }
            }
            set
            {
                ValidateIndex(index);
                ArgumentNullException.ThrowIfNull(value);
                NativeImageListHandle handle = _owner.EnsureHandle();
                using Bitmap bitmap = _owner.CreateBitmap(new ImageEntry { Original = value });
                nint nativeBitmap = bitmap.GetHbitmap(Color.FromArgb(0));
                try
                {
                    if (Native.Replace(handle, index, nativeBitmap, 0) == 0)
                        throw new InvalidOperationException("The native image list could not replace an image.");
                }
                finally { _ = Native.DeleteObject(nativeBitmap); }
                _owner.ImagesChanged?.Invoke(_owner, EventArgs.Empty);
            }
        }
        public Image? this[string key] => IndexOfKey(key) is int index && index >= 0 ? this[index] : null;
        public StringCollection Keys
        {
            get
            {
                StringCollection result = new();
                foreach (ImageEntry entry in _entries) result.Add(entry.Key);
                return result;
            }
        }

        public void Add(Image value) => AddCore(null, value, null);
        public int Add(Image value, Color transparentColor) => AddCore(null, value, transparentColor);
        public void Add(Icon value)
        {
            ArgumentNullException.ThrowIfNull(value);
            using Bitmap bitmap = value.ToBitmap();
            Add(bitmap);
        }
        public void Add(string key, Image image) => AddCore(key, image, null);
        public void Add(string key, Icon icon)
        {
            ArgumentNullException.ThrowIfNull(icon);
            using Bitmap bitmap = icon.ToBitmap();
            Add(key, bitmap);
        }
        public void AddRange(Image[] images)
        {
            ArgumentNullException.ThrowIfNull(images);
            foreach (Image image in images) Add(image);
        }
        public int AddStrip(Image value)
        {
            ArgumentNullException.ThrowIfNull(value);
            Size size = _owner.ImageSize;
            if (value.Width == 0 || value.Width % size.Width != 0 || value.Height != size.Height)
                throw new ArgumentException("The strip must contain complete images of the configured size.", nameof(value));
            int first = Count;
            for (int x = 0; x < value.Width; x += size.Width)
            {
                using Bitmap frame = new(size.Width, size.Height, PixelFormat.Format32bppArgb);
                using (Graphics g = Graphics.FromImage(frame))
                    g.DrawImage(value, new Rectangle(Point.Empty, size), new Rectangle(x, 0, size.Width, size.Height), GraphicsUnit.Pixel);
                Add(frame);
            }
            return first;
        }
        public void Clear()
        {
            ObjectDisposedException.ThrowIf(_owner._disposed, _owner);
            if (_owner.HandleCreated && Native.Remove(_owner._handle!, -1) == 0)
                throw new InvalidOperationException("The native image list could not clear its images.");
            ClearOriginals();
            _owner.ImagesChanged?.Invoke(_owner, EventArgs.Empty);
        }
        public bool Contains(Image image) => IndexOf(image) >= 0;
        public int IndexOf(Image image)
        {
            for (int index = 0; index < _entries.Count; index++)
                if (ReferenceEquals(_entries[index].Identity, image)) return index;
            return -1;
        }
        public void Remove(Image image)
        {
            int index = IndexOf(image);
            if (index >= 0) RemoveAt(index);
        }
        public bool ContainsKey(string key) => IndexOfKey(key) >= 0;
        public int IndexOfKey(string key)
        {
            if (string.IsNullOrEmpty(key)) return -1;
            for (int index = 0; index < _entries.Count; index++)
                if (string.Equals(_entries[index].Key, key, StringComparison.OrdinalIgnoreCase)) return index;
            return -1;
        }
        public void RemoveAt(int index)
        {
            ValidateIndex(index);
            if (Native.Remove(_owner.EnsureHandle(), index) == 0)
                throw new InvalidOperationException("The native image list could not remove an image.");
            _entries[index].ReleaseOriginal();
            _entries.RemoveAt(index);
            _owner.ImagesChanged?.Invoke(_owner, EventArgs.Empty);
        }
        public void RemoveByKey(string key)
        {
            int index = IndexOfKey(key);
            if (index >= 0) RemoveAt(index);
        }
        public void SetKeyName(int index, string name)
        {
            ValidateIndex(index);
            _entries[index].Key = name ?? string.Empty;
            _owner.ImagesChanged?.Invoke(_owner, EventArgs.Empty);
        }
        public IEnumerator GetEnumerator()
        {
            Image[] images = new Image[Count];
            try
            {
                for (int index = 0; index < images.Length; index++) images[index] = this[index];
                return images.GetEnumerator();
            }
            catch { foreach (Image? image in images) image?.Dispose(); throw; }
        }
        private int AddCore(string? key, Image image, Color? transparentColor)
        {
            ArgumentNullException.ThrowIfNull(image);
            ObjectDisposedException.ThrowIf(_owner._disposed, _owner);
            ImageEntry entry = new() { Key = key ?? string.Empty, Identity = image, Original = (Image)image.Clone(), TransparentColor = transparentColor };
            try
            {
                int index = Count;
                if (_owner.HandleCreated)
                {
                    _owner.AddNative(_owner._handle!, entry);
                    entry.ReleaseOriginal();
                }
                _entries.Add(entry);
                _owner.ImagesChanged?.Invoke(_owner, EventArgs.Empty);
                return index;
            }
            catch { if (!_entries.Contains(entry)) entry.ReleaseOriginal(); throw; }
        }
        internal void ValidateIndex(int index)
        {
            if ((uint)index >= (uint)Count) throw new ArgumentOutOfRangeException(nameof(index));
        }
        internal ImageEntry GetEntry(int index) => _entries[index];
        internal void ReleaseOriginals() { foreach (ImageEntry entry in _entries) entry.ReleaseOriginal(); }
        internal void ClearOriginals() { ReleaseOriginals(); _entries.Clear(); }
        internal void SetOriginals(IReadOnlyList<Image> originals)
        {
            if (originals.Count != _entries.Count) throw new InvalidOperationException("The image snapshot does not match the collection.");
            for (int index = 0; index < _entries.Count; index++) _entries[index].Original = originals[index];
        }
        object? IList.this[int index] { get => this[index]; set => this[index] = value as Image ?? throw new ArgumentException("The value must be an Image.", nameof(value)); }
        int IList.Add(object? value) => AddCore(null, value as Image ?? throw new ArgumentException("The value must be an Image.", nameof(value)), null);
        bool IList.Contains(object? value) => value is Image image && Contains(image);
        int IList.IndexOf(object? value) => value is Image image ? IndexOf(image) : -1;
        void IList.Insert(int index, object? value) => throw new NotSupportedException("ImageList does not support insertion.");
        void IList.Remove(object? value) { if (value is Image image) Remove(image); }
        void ICollection.CopyTo(Array array, int index)
        {
            Image[] images = new Image[Count];
            try
            {
                for (int position = 0; position < images.Length; position++) images[position] = this[position];
                images.CopyTo(array, index);
            }
            catch { foreach (Image? image in images) image?.Dispose(); throw; }
        }
    }

    private sealed class NativeImageListHandle : SafeHandle
    {
        internal NativeImageListHandle(nint value) : base(0, true) => SetHandle(value);
        public override bool IsInvalid => handle == 0;
        protected override bool ReleaseHandle() => Native.Destroy(handle) != 0;
    }
    private static partial class Native
    {
        [LibraryImport("comctl32", EntryPoint = "ImageList_Create")]
        internal static partial nint Create(int cx, int cy, uint flags, int initial, int grow);
        [LibraryImport("comctl32", EntryPoint = "ImageList_Destroy")]
        internal static partial int Destroy(nint imageList);
        [LibraryImport("comctl32", EntryPoint = "ImageList_Add")]
        internal static partial int Add(NativeImageListHandle imageList, nint bitmap, nint mask);
        [LibraryImport("comctl32", EntryPoint = "ImageList_Replace")]
        internal static partial int Replace(NativeImageListHandle imageList, int index, nint bitmap, nint mask);
        [LibraryImport("comctl32", EntryPoint = "ImageList_Remove")]
        internal static partial int Remove(NativeImageListHandle imageList, int index);
        [LibraryImport("comctl32", EntryPoint = "ImageList_GetIcon")]
        internal static partial nint GetIcon(NativeImageListHandle imageList, int index, uint flags);
        [LibraryImport("comctl32", EntryPoint = "ImageList_DrawEx")]
        internal static partial int DrawEx(NativeImageListHandle imageList, int index, nint dc, int x, int y, int dx, int dy, uint background, uint foreground, uint flags);
        [LibraryImport("gdi32", EntryPoint = "DeleteObject")]
        internal static partial int DeleteObject(nint handle);
        [LibraryImport("user32", EntryPoint = "DestroyIcon")]
        internal static partial int DestroyIcon(nint handle);
    }
}
