using System.Diagnostics.CodeAnalysis;
using System.Drawing;
using System.Reflection;
using System.Runtime.ExceptionServices;
using Lumina.Forms;
using Xunit;

namespace Lumina.Tests;

public sealed class ControlAppearanceTests
{
    [Fact]
    public void Defaults_AreEffectiveValuesWithoutEstablishingLocalProperties()
        => OnSta(() =>
        {
            using var first = new Control();
            using var second = new Control();
            Assert.Same(Control.DefaultFont, first.Font);
            Assert.Same(first.Font, second.Font);
            Assert.Equal(SystemColors.Control, first.BackColor);
            Assert.Equal(SystemColors.ControlText, first.ForeColor);
            foreach (string property in new[] { "Font", "BackColor", "ForeColor" })
            {
                Assert.False(ShouldSerialize(first, property));
            }
            Assert.False(first.IsHandleCreated);
        });

    [Fact]
    public void EqualExplicitFonts_PreserveTheFirstBorrowedReference()
        => OnSta(() =>
        {
            using var first = new Font("Segoe UI", 12);
            using var equal = (Font)first.Clone();
            using var control = new Control();
            int changes = 0;
            control.FontChanged += (_, _) => changes++;
            control.Font = first;
            control.Font = equal;
            Assert.Same(first, control.Font);
            Assert.Equal(1, changes);
            control.ResetFont();
            Assert.False(ShouldSerialize(control, "Font"));
            Assert.True(first.Height > 0);
        });

    [Fact]
    public void FontEqualToInheritedValue_EstablishesLocalStateWithoutAnEvent()
        => OnSta(() =>
        {
            using var inherited = new Font("Segoe UI", 12);
            using var local = (Font)inherited.Clone();
            using var next = new Font("Segoe UI", 16);
            using var parent = new Control { Font = inherited };
            using var child = new Control(parent, "child");
            int changes = 0;
            child.FontChanged += (_, _) => changes++;
            child.Font = local;
            Assert.Same(local, child.Font);
            Assert.True(ShouldSerialize(child, "Font"));
            Assert.Equal(0, changes);
            parent.Font = next;
            Assert.Same(local, child.Font);
            Assert.Equal(0, changes);
            child.ResetFont();
            Assert.Same(next, child.Font);
            Assert.Equal(1, changes);
        });

    [Fact]
    public void ResetFont_RaisesChangeWhenTheRestoredInheritedValueIsEqual()
        => OnSta(() =>
        {
            using var font = new Font("Segoe UI", 12);
            using var parent = new Control { Font = font };
            using var child = new Control(parent, "child") { Font = font };
            List<string> trace = [];
            child.FontChanged += (_, _) => trace.Add("font");
            child.Layout += (_, e) => trace.Add("child:" + e.AffectedProperty);
            parent.Layout += (_, e) => trace.Add("parent:" + e.AffectedProperty);
            child.ResetFont();
            child.ResetFont();
            Assert.Equal(new[] { "font", "child:Font", "parent:Font" }, trace);
            Assert.Same(font, child.Font);
        });

    [Theory]
    [InlineData(true)]
    [InlineData(false)]
    public void EqualInheritedColors_BlockParentChangesUntilReset(bool background)
        => OnSta(() =>
        {
            using var parent = new Control();
            using var child = new Control(parent, "child");
            SetColor(parent, background, Color.Red);
            int changes = 0;
            if (background) { child.BackColorChanged += (_, _) => changes++; }
            else { child.ForeColorChanged += (_, _) => changes++; }
            SetColor(child, background, Color.Red);
            Assert.Equal(0, changes);
            Assert.True(ShouldSerialize(child, background ? "BackColor" : "ForeColor"));
            SetColor(parent, background, Color.Blue);
            Assert.Equal(Color.Red, GetColor(child, background));
            Assert.Equal(0, changes);
            if (background) { child.ResetBackColor(); }
            else { child.ResetForeColor(); }
            Assert.Equal(Color.Blue, GetColor(child, background));
            Assert.Equal(1, changes);
        });

    [Fact]
    public void ColorEquality_PreservesKnownColorIdentityInsteadOfComparingArgbOnly()
        => OnSta(() =>
        {
            using var control = new Control { BackColor = Color.Red };
            int changes = 0;
            control.BackColorChanged += (_, _) => changes++;
            control.BackColor = Color.FromArgb(Color.Red.ToArgb());
            Assert.Equal(1, changes);
            Assert.False(control.BackColor.IsKnownColor);
            control.BackColor = Color.Red;
            Assert.Equal(2, changes);
            Assert.True(control.BackColor.IsKnownColor);
        });

    [Fact]
    public void TransparentBackColor_IsRejectedBeforeReadingTheVirtualGetter()
        => OnSta(() =>
        {
            using var control = new GetterControl();
            control.BackReads = 0;
            Assert.Throws<ArgumentException>(() => control.BackColor = Color.Transparent);
            Assert.Throws<ArgumentException>(() => control.BackColor = Color.FromArgb(128, 1, 2, 3));
            Assert.Equal(0, control.BackReads);
            Assert.False(ShouldSerialize(control, "BackColor"));
            control.ForeColor = Color.Transparent;
            Assert.Equal(Color.Transparent, control.ForeColor);
        });

    [Fact]
    public void ParentFontInheritance_UsesStoredFontWhileColorsUseVirtualGetters()
        => OnSta(() =>
        {
            using var local = new Font("Segoe UI", 11);
            using var displayed = new Font("Segoe UI", 17);
            using var parent = new GetterControl
            {
                Font = local,
                DisplayFont = displayed,
                DisplayBack = Color.AliceBlue,
                DisplayFore = Color.DarkGreen,
            };
            using var child = new Control(parent, "child");
            Assert.Same(displayed, parent.Font);
            Assert.Same(local, child.Font);
            Assert.Equal(Color.AliceBlue, child.BackColor);
            Assert.Equal(Color.DarkGreen, child.ForeColor);
            parent.DisplayBack = Color.Transparent;
            Assert.Equal(Control.DefaultBackColor, child.BackColor);
            parent.DisplayBack = Color.Empty;
            Assert.Equal(Color.Empty, child.BackColor);
        });

    [Fact]
    public void Reparenting_RaisesParentThenFontForeAndBackInOrder()
        => OnSta(() =>
        {
            using var font = new Font("Segoe UI", 13);
            using var parent = new Control { Font = font, BackColor = Color.Beige, ForeColor = Color.Maroon };
            using var child = new Control();
            List<string> trace = [];
            child.ParentChanged += (_, _) => trace.Add("parent");
            child.FontChanged += (_, _) => trace.Add("font");
            child.ForeColorChanged += (_, _) => trace.Add("fore");
            child.BackColorChanged += (_, _) => trace.Add("back");
            parent.Controls.Add(child);
            Assert.Equal(new[] { "parent", "font", "fore", "back" }, trace);
            trace.Clear();
            parent.Controls.Remove(child);
            Assert.Equal(new[] { "parent", "font", "fore", "back" }, trace);
        });

    [Fact]
    public void FontEvents_VisitDescendantsBeforeTheOwningLayout()
        => OnSta(() =>
        {
            using var font = new Font("Segoe UI", 13);
            using var parent = new Control();
            using var child = new Control(parent, "child");
            using var nested = new Control(child, "nested");
            List<string> trace = [];
            parent.FontChanged += (_, _) => trace.Add("parent-font");
            child.FontChanged += (_, _) => trace.Add("child-font");
            nested.FontChanged += (_, _) => trace.Add("nested-font");
            parent.Layout += (_, e) => trace.Add("parent-layout:" + e.AffectedProperty);
            child.Layout += (_, e) => trace.Add("child-layout:" + e.AffectedProperty);
            nested.Layout += (_, e) => trace.Add("nested-layout:" + e.AffectedProperty);
            parent.Font = font;
            Assert.Equal(new[] { "parent-font", "child-font", "nested-font", "nested-layout:Font", "child-layout:Font", "parent-layout:Font" }, trace);
        });

    [Fact]
    public void ParentNotifications_ReuseArgumentsAndRespectLocalState()
        => OnSta(() =>
        {
            using var control = new ExposedControl();
            var args = new EventArgs();
            List<EventArgs> received = [];
            control.FontChanged += (_, e) => received.Add(e);
            control.BackColorChanged += (_, e) => received.Add(e);
            control.ForeColorChanged += (_, e) => received.Add(e);
            control.RaiseParentFont(args);
            control.RaiseParentBack(args);
            control.RaiseParentFore(args);
            Assert.Equal(3, received.Count);
            Assert.All(received, value => Assert.Same(args, value));
            control.Font = Control.DefaultFont;
            control.BackColor = Control.DefaultBackColor;
            control.ForeColor = Control.DefaultForeColor;
            received.Clear();
            control.RaiseParentFont(args);
            control.RaiseParentBack(args);
            control.RaiseParentFore(args);
            Assert.Empty(received);
        });

    [Fact]
    public void SkippingBaseNotification_StopsEventsAndDescendantNotifications()
        => OnSta(() =>
        {
            using var font = new Font("Segoe UI", 14);
            using var parent = new ExposedControl { SkipBase = true };
            using var child = new Control(parent, "child");
            int changes = 0;
            parent.FontChanged += (_, _) => changes++;
            child.FontChanged += (_, _) => changes++;
            parent.Font = font;
            Assert.Same(font, child.Font);
            Assert.Equal(0, changes);
            parent.SkipBase = false;
            parent.RaiseFont(EventArgs.Empty);
            Assert.Equal(2, changes);
        });

    [Fact]
    public void ThrowingFontEvent_PreservesOriginalExceptionAndBalancesParentLayout()
        => OnSta(() =>
        {
            using var font = new Font("Segoe UI", 13);
            using var parent = new Control();
            using var child = new Control(parent, "child");
            using var nested = new Control(child, "nested");
            var expected = new InvalidOperationException("font callback");
            EventHandler throwing = (_, _) => throw expected;
            int nestedChanges = 0;
            List<string?> parentReasons = [];
            child.FontChanged += throwing;
            nested.FontChanged += (_, _) => nestedChanges++;
            parent.Layout += (_, e) => parentReasons.Add(e.AffectedProperty);
            Assert.Same(expected, Assert.Throws<InvalidOperationException>(() => child.Font = font));
            Assert.Same(font, child.Font);
            Assert.Equal(0, nestedChanges);
            Assert.Equal(new[] { "Font" }, parentReasons);
            child.FontChanged -= throwing;
            child.ResetFont();
            Assert.Equal(1, nestedChanges);
        });

    [Fact]
    public void GetterExceptions_KeepTheStandardMutationBoundary()
        => OnSta(() =>
        {
            using var font = new Font("Segoe UI", 12);
            using var control = new GetterControl();
            var expected = new InvalidOperationException("getter failure");
            control.FontFailure = expected;
            Assert.Same(expected, Assert.Throws<InvalidOperationException>(() => control.Font = font));
            Assert.False(ShouldSerialize(control, "Font"));
            control.FontFailure = null;
            control.Font = font;
            control.FontFailure = expected;
            control.Font = font;
            control.FontFailure = null;
            control.BackReads = 0;
            control.ThrowBackRead = 2;
            control.BackFailure = expected;
            Assert.Same(expected, Assert.Throws<InvalidOperationException>(() => control.BackColor = Color.Red));
            control.ThrowBackRead = 0;
            Assert.Equal(Color.Red, control.BackColor);
            Assert.True(ShouldSerialize(control, "BackColor"));
        });

    [Fact]
    public void FontHeight_UsesParentCacheAndResetsOnInheritedFontChanges()
        => OnSta(() =>
        {
            using var font = new Font("Segoe UI", 15);
            using var parent = new ExposedControl();
            using var child = new ExposedControl { Parent = parent };
            parent.HeightCache = 37;
            Assert.Equal(37, child.HeightCache);
            child.HeightCache = -2;
            Assert.Equal(-2, child.HeightCache);
            parent.Font = font;
            Assert.Equal(font.Height, parent.HeightCache);
            Assert.Equal(font.Height, child.HeightCache);
            child.HeightCache = 79;
            child.Font = font;
            Assert.Equal(79, child.HeightCache);
            child.ResetFont();
            Assert.Equal(font.Height, child.HeightCache);
        });

    [Fact]
    public void FontChanges_ClearOwnAndParentPreferredSizeCaches()
        => OnSta(() =>
        {
            using var font = new Font("Segoe UI", 14);
            using var parent = new Panel();
            using var child = new Panel { Parent = parent, AutoSize = true };
            using var leaf = new MeasuringControl { Parent = child, AutoSize = true };
            Size parentBefore = parent.PreferredSize;
            Size childBefore = child.PreferredSize;
            int measures = leaf.Measures;
            _ = parent.PreferredSize;
            _ = child.PreferredSize;
            Assert.Equal(measures, leaf.Measures);
            child.Font = font;
            Assert.NotEqual(parentBefore, parent.PreferredSize);
            Assert.NotEqual(childBefore, child.PreferredSize);
            Assert.True(leaf.Measures > measures);
        });

    [Fact]
    public void Disposal_PreservesBorrowedFontsAndAllowsManagedAppearanceChanges()
        => OnSta(() =>
        {
            using var font = new Font("Segoe UI", 14);
            var control = new Control { Font = font };
            control.Dispose();
            Assert.True(font.Height > 0);
            int changed = 0;
            control.FontChanged += (_, _) => changed++;
            control.BackColorChanged += (_, _) => changed++;
            control.ForeColorChanged += (_, _) => changed++;
            control.ResetFont();
            control.BackColor = Color.Red;
            control.ForeColor = Color.Blue;
            Assert.Equal(3, changed);
            Assert.False(control.IsHandleCreated);
        });

    [Fact]
    public void DisposingHierarchy_SuppressesBaseAppearanceNotifications()
        => OnSta(() =>
        {
            using var font = new Font("Segoe UI", 14);
            using var parent = new ExposedControl();
            using var child = new Control(parent, "child");
            int changed = 0;
            child.FontChanged += (_, _) => changed++;
            child.BackColorChanged += (_, _) => changed++;
            child.ForeColorChanged += (_, _) => changed++;
            parent.DisposingCallback = () =>
            {
                child.Font = font;
                child.BackColor = Color.Red;
                child.ForeColor = Color.Blue;
            };
            parent.Dispose();
            Assert.Equal(0, changed);
        });

    [Fact]
    public void Resets_DispatchThroughVirtualSetters()
        => OnSta(() =>
        {
            using var control = new ResetControl();
            Control view = control;
            view.ResetFont();
            view.ResetBackColor();
            view.ResetForeColor();
            Assert.Equal(new[] { "font:null", "back:True", "fore:True" }, control.Calls);
        });

    private static bool ShouldSerialize(Control control, string name)
    {
        MethodInfo method = typeof(Control).GetMethod("ShouldSerialize" + name, BindingFlags.NonPublic | BindingFlags.Instance)!;
        Assert.True(method.IsAssembly);
        Assert.True(method.IsVirtual);
        return (bool)method.Invoke(control, null)!;
    }

    private static void SetColor(Control control, bool background, Color value)
    {
        if (background) { control.BackColor = value; }
        else { control.ForeColor = value; }
    }

    private static Color GetColor(Control control, bool background) => background ? control.BackColor : control.ForeColor;

    private static void OnSta(Action action)
    {
        Exception? failure = null;
        var thread = new Thread(() =>
        {
            WindowsFormsSynchronizationContext.AutoInstall = false;
            try { action(); }
            catch (Exception exception) { failure = exception; }
        }) { IsBackground = true };
        thread.SetApartmentState(ApartmentState.STA);
        thread.Start();
        Assert.True(thread.Join(TimeSpan.FromSeconds(15)), "The appearance contract check timed out.");
        if (failure is not null) { ExceptionDispatchInfo.Capture(failure).Throw(); }
    }

    private sealed class GetterControl : Control
    {
        internal Font? DisplayFont { get; init; }
        internal Color? DisplayBack { get; set; }
        internal Color? DisplayFore { get; init; }
        internal Exception? FontFailure { get; set; }
        internal Exception? BackFailure { get; set; }
        internal int BackReads { get; set; }
        internal int ThrowBackRead { get; set; }

        [AllowNull]
        public override Font Font
        {
            get => FontFailure is { } failure ? throw failure : DisplayFont ?? base.Font;
            set => base.Font = value;
        }
        public override Color BackColor
        {
            get
            {
                BackReads++;
                if (BackReads == ThrowBackRead && BackFailure is { } failure) { throw failure; }
                return DisplayBack ?? base.BackColor;
            }
            set => base.BackColor = value;
        }
        public override Color ForeColor { get => DisplayFore ?? base.ForeColor; set => base.ForeColor = value; }
    }

    private sealed class ExposedControl : Control
    {
        internal bool SkipBase { get; set; }
        internal Action? DisposingCallback { get; set; }
        internal int HeightCache { get => FontHeight; set => FontHeight = value; }
        internal void RaiseFont(EventArgs args) => OnFontChanged(args);
        internal void RaiseParentFont(EventArgs args) => OnParentFontChanged(args);
        internal void RaiseParentBack(EventArgs args) => OnParentBackColorChanged(args);
        internal void RaiseParentFore(EventArgs args) => OnParentForeColorChanged(args);
        protected override void OnFontChanged(EventArgs e) { if (!SkipBase) { base.OnFontChanged(e); } }
        protected override void OnDisposing() { DisposingCallback?.Invoke(); base.OnDisposing(); }
    }

    private sealed class MeasuringControl : Control
    {
        internal int Measures { get; private set; }
        public override Size GetPreferredSize(Size proposedConstraints)
        {
            Measures++;
            return new Size(Font.Height * 3, Font.Height);
        }
    }

    private sealed class ResetControl : Control
    {
        internal List<string> Calls { get; } = [];
        [AllowNull]
        public override Font Font { get => base.Font; set => Calls.Add("font:" + (value is null ? "null" : "value")); }
        public override Color BackColor { get => base.BackColor; set => Calls.Add("back:" + value.IsEmpty); }
        public override Color ForeColor { get => base.ForeColor; set => Calls.Add("fore:" + value.IsEmpty); }
    }
}
