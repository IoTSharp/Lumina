using System.Collections;
using System.Globalization;
using System.Reflection;
using System.Runtime.ExceptionServices;
using System.Runtime.InteropServices;
using System.Text;
using Lumina.Forms;
using Microsoft.Win32;
using Xunit;

namespace Lumina.Tests;

[Collection(ControlFocusCollection.Name)]
public sealed class InputLanguageTests
{
    [Fact]
    public void LanguageIdentityUsesTheEntireHandleAndTheStandardHash()
    {
        InputLanguage first = CreateLanguage(0x04090409);
        InputLanguage equal = CreateLanguage(0x04090409);
        InputLanguage differentLayout = CreateLanguage(0x08090409);

        Assert.Equal((nint)0x04090409, first.Handle);
        Assert.Equal(first, equal);
        Assert.NotSame(first, equal);
        Assert.NotEqual(first, differentLayout);
        Assert.False(first.Equals(null));
        Assert.False(first.Equals(first.Handle));
        Assert.Equal(0x04090409, first.GetHashCode());
        if (nint.Size == 8)
        {
            InputLanguage upperBits = CreateLanguage(unchecked((nint)0x104090409L));
            Assert.NotEqual(first, upperBits);
            Assert.Equal(first.GetHashCode(), upperBits.GetHashCode());
        }
    }

    [Fact]
    public void CultureUsesTheLanguageRatherThanThePhysicalKeyboard()
    {
        InputLanguage frenchKeyboardForEnglish = CreateLanguage(0x040c0409);
        Assert.Equal(CultureInfo.GetCultureInfo("en-US"), frenchKeyboardForEnglish.Culture);
        Assert.NotSame(frenchKeyboardForEnglish.Culture, frenchKeyboardForEnglish.Culture);
        Assert.Equal(CultureInfo.GetCultureInfo("ja-JP"), CreateLanguage(0x04110411).Culture);
    }

    [Fact]
    public void CollectionPreservesIdentityOrderNullQueriesAndReadOnlyBaseContract()
    {
        InputLanguage first = CreateLanguage(0x04090409);
        InputLanguage second = CreateLanguage(0x04110411);
        InputLanguageCollection collection = CreateCollection([first, second]);

        Assert.IsAssignableFrom<ReadOnlyCollectionBase>(collection);
        Assert.False(collection is IList);
        Assert.Equal(2, collection.Count);
        Assert.Same(first, collection[0]);
        Assert.Same(second, collection[1]);
        Assert.True(collection.Contains(CreateLanguage(first.Handle)));
        Assert.Equal(1, collection.IndexOf(CreateLanguage(second.Handle)));
        Assert.False(collection.Contains(null));
        Assert.Equal(-1, collection.IndexOf(null));
        Assert.False(collection.Contains(CreateLanguage(0x040c040c)));
        Assert.Equal(new object[] { first, second }, collection.Cast<object>().ToArray());
        Assert.Empty(typeof(InputLanguage).GetConstructors());
        Assert.Empty(typeof(InputLanguageCollection).GetConstructors());
    }

    [Fact]
    public void CollectionCopiesBothTypedAndICollectionArraysAndKeepsBclBoundaries()
    {
        InputLanguage language = CreateLanguage(0x04090409);
        InputLanguageCollection collection = CreateCollection([language]);
        var typed = new InputLanguage[3];
        collection.CopyTo(typed, 1);
        Assert.Null(typed[0]);
        Assert.Same(language, typed[1]);
        Assert.Null(typed[2]);

        object[] untyped = new object[2];
        ((ICollection)collection).CopyTo(untyped, 1);
        Assert.Same(language, untyped[1]);
        Assert.Throws<ArgumentNullException>(() => collection.CopyTo(null!, 0));
        Assert.Throws<ArgumentOutOfRangeException>(() => collection.CopyTo(typed, -1));
        Assert.Throws<ArgumentException>(() => collection.CopyTo(Array.Empty<InputLanguage>(), 0));
        Assert.Throws<ArgumentOutOfRangeException>(() => collection[-1]);
        Assert.Throws<ArgumentOutOfRangeException>(() => collection[1]);
    }

    [Fact]
    public void FromCultureRejectsNullBeforeEnumeratingNativeLayouts()
        => Assert.Equal("culture", Assert.Throws<ArgumentNullException>(() => InputLanguage.FromCulture(null!)).ParamName);

    [Fact]
    public void CurrentAndApplicationEntryPointsReadTheActualThreadLayout()
        => OnSta(() =>
        {
            nint native = GetKeyboardLayout(0);
            InputLanguage first = InputLanguage.CurrentInputLanguage;
            InputLanguage second = InputLanguage.CurrentInputLanguage;
            Assert.NotEqual((nint)0, native);
            Assert.Equal(native, first.Handle);
            Assert.Equal(native, Application.CurrentInputLanguage.Handle);
            Assert.Equal(first, second);
            Assert.NotSame(first, second);
        });

    [Fact]
    public void DefaultAndInstalledLanguagesReadNativeSystemState()
        => OnSta(() =>
        {
            nint expectedDefault = 0;
            Assert.True(SystemParametersInfoW(0x0059, 0, ref expectedDefault, 0));
            Assert.Equal(expectedDefault, InputLanguage.DefaultInputLanguage.Handle);

            int count = GetKeyboardLayoutList(0, null);
            Assert.InRange(count, 1, 1024);
            nint[] handles = new nint[count];
            Assert.Equal(count, GetKeyboardLayoutList(count, handles));
            InputLanguageCollection languages = InputLanguage.InstalledInputLanguages;
            Assert.Equal(handles, languages.Cast<InputLanguage>().Select(language => language.Handle).ToArray());
            Assert.NotSame(languages, InputLanguage.InstalledInputLanguages);
        });

    [Fact]
    public void SettingInstalledLanguageActivatesNativeStateAndNullRestoresTheDefault()
        => OnSta(() =>
        {
            InputLanguage original = InputLanguage.CurrentInputLanguage;
            InputLanguage target = InputLanguage.InstalledInputLanguages.Cast<InputLanguage>()
                .FirstOrDefault(language => !language.Equals(original)) ?? original;
            try
            {
                InputLanguage.CurrentInputLanguage = target;
                Assert.Equal(target.Handle, GetKeyboardLayout(0));
                Assert.Equal(target, Application.CurrentInputLanguage);
                Application.CurrentInputLanguage = original;
                Assert.Equal(original.Handle, GetKeyboardLayout(0));
                InputLanguage.CurrentInputLanguage = null;
                Assert.Equal(InputLanguage.DefaultInputLanguage.Handle, GetKeyboardLayout(0));
                Application.CurrentInputLanguage = null;
                Assert.Equal(InputLanguage.DefaultInputLanguage.Handle, GetKeyboardLayout(0));
            }
            finally
            {
                InputLanguage.CurrentInputLanguage = original;
            }
        });

    [Fact]
    public void UnrecognizedLayoutThrowsWithoutChangingTheThreadLanguage()
        => OnSta(() =>
        {
            nint original = GetKeyboardLayout(0);
            InputLanguage invalid = CreateLanguage(unchecked((nint)0x77773333));
            ArgumentException error = Assert.Throws<ArgumentException>(() => InputLanguage.CurrentInputLanguage = invalid);
            Assert.Equal("value", error.ParamName);
            Assert.Equal(original, GetKeyboardLayout(0));
        });

    [Fact]
    public void FromCultureFindsTheFirstInstalledMatchAndReturnsNullForAnAbsentCulture()
        => OnSta(() =>
        {
            InputLanguage[] installed = InputLanguage.InstalledInputLanguages.Cast<InputLanguage>().ToArray();
            foreach (InputLanguage language in installed)
            {
                InputLanguage expected = installed.First(candidate => candidate.Culture.Equals(language.Culture));
                Assert.Equal(expected, InputLanguage.FromCulture(language.Culture));
            }

            Assert.Null(InputLanguage.FromCulture(CultureInfo.InvariantCulture));
        });

    [Fact]
    public void LayoutNameUsesTheCurrentNativeLayoutIdentifierWithoutActivatingAnotherLanguage()
        => OnSta(() =>
        {
            nint original = GetKeyboardLayout(0);
            var identifier = new StringBuilder(9);
            Assert.True(GetKeyboardLayoutNameW(identifier));
            using RegistryKey? key = Registry.LocalMachine.OpenSubKey(
                @"SYSTEM\CurrentControlSet\Control\Keyboard Layouts\" + identifier);
            Assert.NotNull(key);
            var name = new StringBuilder(2048);
            string? expected = RegLoadMUIStringW(key.Handle.DangerousGetHandle(), "Layout Display Name", name,
                4096, out _, 0, null) == 0 ? name.ToString() : key.GetValue("Layout Text") as string;

            Assert.NotNull(expected);
            Assert.Equal(expected, InputLanguage.CurrentInputLanguage.LayoutName);
            Assert.Equal(original, GetKeyboardLayout(0));
            GC.KeepAlive(key);
        });

    [Fact]
    public void MissingLayoutUsesTheFixedUnknownLayoutFallback()
        => OnSta(() => Assert.Equal("Unknown layout", CreateLanguage(0x12341234).LayoutName));

    [Fact]
    public void ActivationChangesOnlyTheCallingThread()
        => OnSta(() =>
        {
            nint ownerLayout = GetKeyboardLayout(0);
            InputLanguage target = InputLanguage.InstalledInputLanguages.Cast<InputLanguage>()
                .FirstOrDefault(language => language.Handle != ownerLayout) ?? InputLanguage.CurrentInputLanguage;
            using var activated = new ManualResetEventSlim();
            using var release = new ManualResetEventSlim();
            Exception? failure = null;
            var worker = new Thread(() =>
            {
                InputLanguage original = InputLanguage.CurrentInputLanguage;
                try
                {
                    Application.CurrentInputLanguage = target;
                    Assert.Equal(target.Handle, GetKeyboardLayout(0));
                    activated.Set();
                    Assert.True(release.Wait(TimeSpan.FromSeconds(5)), "The owner did not release the input-language worker.");
                }
                catch (Exception exception) { failure = exception; }
                finally
                {
                    try { InputLanguage.CurrentInputLanguage = original; }
                    catch (Exception exception)
                    {
                        failure = failure is null ? exception : new AggregateException(failure, exception);
                    }
                    finally { activated.Set(); }
                }
            }) { IsBackground = true };
            worker.SetApartmentState(ApartmentState.STA);
            worker.Start();
            try
            {
                Assert.True(activated.Wait(TimeSpan.FromSeconds(5)), "The input-language worker did not activate its layout.");
                Assert.Equal(ownerLayout, GetKeyboardLayout(0));
            }
            finally
            {
                release.Set();
                Assert.True(worker.Join(TimeSpan.FromSeconds(5)), "The input-language worker did not finish.");
            }
            if (failure is not null) { ExceptionDispatchInfo.Capture(failure).Throw(); }
        });

    private static InputLanguage CreateLanguage(nint handle)
        => (InputLanguage)typeof(InputLanguage).GetConstructor(BindingFlags.Instance | BindingFlags.NonPublic,
            null, [typeof(nint)], null)!.Invoke([handle]);

    private static InputLanguageCollection CreateCollection(InputLanguage[] languages)
        => (InputLanguageCollection)typeof(InputLanguageCollection).GetConstructor(BindingFlags.Instance | BindingFlags.NonPublic,
            null, [typeof(InputLanguage[])], null)!.Invoke([languages]);

    [DllImport("user32.dll")]
    private static extern nint GetKeyboardLayout(uint threadId);

    [DllImport("user32.dll")]
    private static extern int GetKeyboardLayoutList(int count, [Out] nint[]? layouts);

    [DllImport("user32.dll", CharSet = CharSet.Unicode)]
    [return: MarshalAs(UnmanagedType.Bool)]
    private static extern bool GetKeyboardLayoutNameW(StringBuilder name);

    [DllImport("user32.dll", SetLastError = true)]
    [return: MarshalAs(UnmanagedType.Bool)]
    private static extern bool SystemParametersInfoW(uint action, uint parameter, ref nint value, uint flags);

    [DllImport("advapi32.dll", CharSet = CharSet.Unicode)]
    private static extern int RegLoadMUIStringW(nint key, string value, StringBuilder output,
        uint bufferBytes, out uint bytesRequired, uint flags, string? directory);

    private static void OnSta(Action action)
    {
        if (!OperatingSystem.IsWindows()) { return; }
        Exception? failure = null;
        var thread = new Thread(() =>
        {
            try { action(); }
            catch (Exception exception) { failure = exception; }
        }) { IsBackground = true };
        thread.SetApartmentState(ApartmentState.STA);
        thread.Start();
        Assert.True(thread.Join(TimeSpan.FromSeconds(20)), "The input-language test STA did not complete.");
        if (failure is not null) { ExceptionDispatchInfo.Capture(failure).Throw(); }
    }
}
