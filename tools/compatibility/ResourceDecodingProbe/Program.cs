using System.Drawing;
using System.Globalization;
using System.Reflection;
using System.Resources;
using System.Security.Cryptography;
#if WINDOWS
using BitmapResourceManager = System.Resources.ResourceManager;
using BitmapComponentResourceManager = System.ComponentModel.ComponentResourceManager;
#else
using BitmapResourceManager = Lumina.Forms.Resources.ResourceManager;
using BitmapComponentResourceManager = Lumina.Forms.Resources.ComponentResourceManager;
#endif

namespace ResourceDecodingProbe;

internal static class Program
{
    private static readonly CultureInfo s_neutral = CultureInfo.InvariantCulture;
    private static readonly CultureInfo s_fallback = CultureInfo.GetCultureInfo("zh-CN");

    [STAThread]
    private static void Main()
    {
        Assembly demoAssembly = typeof(NativeFormsDemo.frmMain).Assembly;
        var componentResources = new BitmapComponentResourceManager(typeof(NativeFormsDemo.frmMain));
        var propertyResources = new BitmapResourceManager("NativeFormsDemo.Properties.Resources", demoAssembly);
        var rawComponentResources = new System.Resources.ResourceManager("NativeFormsDemo.frmMain", demoAssembly);
        var rawPropertyResources = new System.Resources.ResourceManager("NativeFormsDemo.Properties.Resources", demoAssembly);

        string[] imageNames =
        [
            "toolStripDropDownButton1.Image", "toolStripSplitButton1.Image",
            "toolStripSplitButton2.Image", "toolStripButton1.Image"
        ];
        foreach (string name in imageNames)
        {
            CheckBitmap(componentResources, rawComponentResources, name, 16, 16,
                "9A394B5161F014ED97365BE4D7B71BDCEDDCF1017C5C18E91A339B9D32C88121");
        }
        CheckBitmap(propertyResources, rawPropertyResources, "SplashScreen", 620, 300,
            "1C415731F697D99BD163754816E0B7A8E3181F3E6CB31C4FB4B8CB3B9DB74E09");
        Bitmap typedSplash = NativeFormsDemo.Properties.Resources.SplashScreen;
        Require(typedSplash.Width == 620 && typedSplash.Height == 300, "The actual strongly typed SplashScreen property failed.");
        Require(GetPixelHash(typedSplash) == GetPixelHash((Bitmap)propertyResources.GetObject("SplashScreen", s_neutral)!),
            "The strongly typed SplashScreen property differs from GetObject.");
        bool typedRepeatedIdentity = ReferenceEquals(typedSplash, NativeFormsDemo.Properties.Resources.SplashScreen);
        Require(!typedRepeatedIdentity, "The WinForms strongly typed Bitmap path decodes a new instance on every lookup.");
        Console.WriteLine("TypedBitmap SplashScreen repeatedIdentity=" + typedRepeatedIdentity + " " + GetPixelHash(typedSplash));

        CheckPrimitiveSemantics(new BitmapResourceManager(typeof(ProbeResources)));
        CheckPrimitiveSemantics(new BitmapComponentResourceManager(typeof(ProbeResources)));
        var missingResources = new BitmapResourceManager("ResourceDecodingProbe.DoesNotExist", typeof(Program).Assembly);
        ExpectException<MissingManifestResourceException>(() => missingResources.GetObject("missing"));
        componentResources.ReleaseAllResources();
        propertyResources.ReleaseAllResources();
        rawComponentResources.ReleaseAllResources();
        rawPropertyResources.ReleaseAllResources();
        Console.WriteLine("PASS 5 actual Demo Bitmap resources; primitive, culture, identity, release and error semantics.");
    }

    private static void CheckBitmap(System.Resources.ResourceManager resources, System.Resources.ResourceManager rawResources,
        string name, int width, int height, string sourceHash)
    {
        object? value = resources.GetObject(name, s_neutral);
        Require(value is Bitmap, name + " did not decode to Bitmap.");
        var bitmap = (Bitmap)value!;
        Require(bitmap.Width == width && bitmap.Height == height, name + " has unexpected dimensions.");
        bool repeatedIdentity = ReferenceEquals(bitmap, resources.GetObject(name, s_neutral));
        bool fallbackIdentity = ReferenceEquals(bitmap, resources.GetObject(name, s_fallback));
        Require(!repeatedIdentity, name + " must decode a new Bitmap on repeated lookup, matching WinForms.");
        Require(!fallbackIdentity, name + " must decode a new Bitmap during culture fallback, matching WinForms.");
        string pixelHash = GetPixelHash(bitmap);

#if !WINDOWS
        // The BCL reader must see built-in bytes, so successful image decoding cannot be explained
        // by a rooted ImageConverter or the custom-resource reader.
        object? rawValue = rawResources.GetObject(name, s_neutral);
        Require(rawValue is byte[], name + " is still a custom serialized object.");
        Require(Convert.ToHexString(SHA256.HashData((byte[])rawValue!)) == sourceHash,
            name + " differs from the original checked-in image bytes.");
#endif

        resources.ReleaseAllResources();
        var reloaded = resources.GetObject(name, s_neutral) as Bitmap;
        Require(reloaded is not null && !ReferenceEquals(bitmap, reloaded), name + " did not reload after ReleaseAllResources.");
        Require(GetPixelHash(reloaded!) == pixelHash, name + " changed after resource reload.");
        // Force access after resource reload and GC: Bitmap retains its decoding stream.
        GC.Collect();
        GC.WaitForPendingFinalizers();
        Require(GetPixelHash(bitmap) == pixelHash, name + " lost its image data after resource reload.");
        Console.WriteLine($"Bitmap {name} {width}x{height} repeatedIdentity={repeatedIdentity} fallbackIdentity={fallbackIdentity} {pixelHash}");
    }

    private static void CheckPrimitiveSemantics(System.Resources.ResourceManager resources)
    {
        Require(resources.GetString("Greeting", s_neutral) == "neutral", "Neutral string lookup failed.");
        Require((string?)resources.GetObject("Greeting", s_neutral) == "neutral", "GetObject string lookup changed.");
        Require(resources.GetString("Greeting", CultureInfo.GetCultureInfo("fr-FR")) == "français", "Parent-culture lookup failed.");
        Require(resources.GetString("Greeting", s_fallback) == "neutral", "Neutral-culture fallback failed.");
        object? payload = resources.GetObject("Payload", s_neutral);
        Require(payload is byte[] bytes && bytes.AsSpan().SequenceEqual(new byte[] { 1, 2, 3 }), "Unmarked byte[] was converted.");
        bool repeatedPayloadIdentity = ReferenceEquals(payload, resources.GetObject("Payload", s_neutral));
        bool fallbackPayloadIdentity = ReferenceEquals(payload, resources.GetObject("Payload", CultureInfo.GetCultureInfo("fr-FR")));
        var baseline = new System.Resources.ResourceManager(typeof(ProbeResources));
        object? baselinePayload = baseline.GetObject("Payload", s_neutral);
        Require(repeatedPayloadIdentity == ReferenceEquals(baselinePayload, baseline.GetObject("Payload", s_neutral)),
            "Repeated byte[] identity differs from the standard resource manager.");
        Require(fallbackPayloadIdentity == ReferenceEquals(baselinePayload, baseline.GetObject("Payload", CultureInfo.GetCultureInfo("fr-FR"))),
            "Fallback byte[] identity differs from the standard resource manager.");
        baseline.ReleaseAllResources();
        Console.WriteLine($"Payload repeatedIdentity={repeatedPayloadIdentity} fallbackIdentity={fallbackPayloadIdentity}");
        Require(resources.GetObject("missing", s_neutral) is null, "Missing resource did not return null.");
        ExpectException<ArgumentNullException>(() => resources.GetObject(null!));
        ExpectException<InvalidOperationException>(() => resources.GetString("Payload", s_neutral));
        resources.IgnoreCase = true;
        Require((string?)resources.GetObject("greeting", s_neutral) == "neutral", "IgnoreCase behavior changed.");
        resources.ReleaseAllResources();
    }

    private static string GetPixelHash(Bitmap bitmap)
    {
        byte[] pixels = new byte[checked(bitmap.Width * bitmap.Height * 4)];
        int index = 0;
        for (int y = 0; y < bitmap.Height; y++)
        {
            for (int x = 0; x < bitmap.Width; x++)
            {
                Color color = bitmap.GetPixel(x, y);
                pixels[index++] = color.A;
                pixels[index++] = color.R;
                pixels[index++] = color.G;
                pixels[index++] = color.B;
            }
        }
        return Convert.ToHexString(SHA256.HashData(pixels));
    }

    private static void ExpectException<TException>(Action action) where TException : Exception
    {
        try
        {
            action();
        }
        catch (TException)
        {
            return;
        }
        throw new InvalidOperationException("Expected " + typeof(TException).Name + ".");
    }

    private static void Require(bool condition, string message)
    {
        if (!condition)
        {
            throw new InvalidOperationException(message);
        }
    }
}

// A standard ComponentResourceManager(Type) base name without creating a component instance.
internal sealed class ProbeResources
{
}
