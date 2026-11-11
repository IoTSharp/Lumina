using System.ComponentModel;
using System.Drawing;
using System.Globalization;
using Lumina.Forms;
using Xunit;

namespace Lumina.Tests;

[Collection(ControlFocusCollection.Name)]
public sealed class MaskedTextBoxModelTests
{
    [Fact]
    public void EmptyMaskUsesUnrestrictedTextAndNeverExposesThePrivateProvider()
    {
        using var scope = new ModelScope();
        using var control = new MaskedTextBox();
        Assert.Equal(typeof(TextBoxBase), typeof(MaskedTextBox).BaseType);
        Assert.Equal(string.Empty, control.Mask);
        Assert.Null(control.MaskedTextProvider);
        control.Text = "AB12";
        control.Mask = "LL-00";
        Assert.Equal("AB-12", control.Text);
        MaskedTextProvider snapshot = Assert.IsType<MaskedTextProvider>(control.MaskedTextProvider);
        snapshot.Clear();
        Assert.Equal("AB-12", control.Text);
        control.Mask = null;
        Assert.Equal("AB12", control.Text);
        Assert.Null(control.MaskedTextProvider);
        Assert.False(control.IsHandleCreated);
        Assert.Throws<ArgumentException>(() => new MaskedTextBox(""));
    }

    [Fact]
    public void PasswordAndPromptVisibilityDoNotChangeDataOrDuplicateTextChanged()
    {
        using var scope = new ModelScope();
        using var control = new MaskedTextBox("00-00");
        int changes = 0;
        control.TextChanged += (_, _) => changes++;
        control.Text = "12";
        Assert.Equal("12-", control.Text);
        Assert.Equal(5, control.TextLength);
        Assert.Equal(1, changes);
        control.PasswordChar = '*';
        control.HidePromptOnLeave = true;
        Assert.Equal("12-", control.Text);
        Assert.Equal(1, changes);
        control.TextMaskFormat = MaskFormat.ExcludePromptAndLiterals;
        Assert.Equal("12", control.Text);
        Assert.Equal(2, changes);
        control.Text = "12";
        Assert.Equal(2, changes);
        Assert.False(control.IsHandleCreated);
    }

    [Fact]
    public void RejectedCharactersRespectPartialAndAtomicReplacementPolicies()
    {
        using var scope = new ModelScope();
        using var control = new MaskedTextBox("00-00");
        var failures = new List<(int Position, MaskedTextResultHint Hint)>();
        control.MaskInputRejected += (_, e) => failures.Add((e.Position, e.RejectionHint));
        control.Text = "1x234";
        Assert.Equal("12-34", control.Text);
        Assert.Equal([(1, MaskedTextResultHint.DigitExpected)], failures);
        control.RejectInputOnFirstFailure = true;
        control.Text = "9x";
        Assert.Equal("12-34", control.Text);
        Assert.Equal(2, failures.Count);
        Assert.False(control.IsHandleCreated);
    }

    [Fact]
    public void SelectionUsesDisplayPositionsAndKeyboardUsesProviderInsertion()
    {
        using var scope = new ModelScope();
        using var control = new HookMaskedTextBox("00-00") { Text = "1234" };
        control.TextMaskFormat = MaskFormat.ExcludePromptAndLiterals;
        control.Select(1, 3);
        Assert.Equal("2-3", control.SelectedText);
        control.CutCopyMaskFormat = MaskFormat.ExcludePromptAndLiterals;
        Assert.Equal("23", control.SelectedText);
        control.SelectedText = "98";
        Assert.Equal("1984", control.Text);
        control.Select(0, 0);
        control.PressKey(Keys.Insert);
        Assert.True(control.IsOverwriteMode);
        control.PressCharacter('7');
        Assert.Equal("7984", control.Text);
        control.PressKey(Keys.Back);
        Assert.Equal("984", control.Text);
        Assert.False(control.IsHandleCreated);
    }

    [Fact]
    public void TypeValidationRequiresCompletionUsesFormatProviderAndPrecedesValidating()
    {
        using var scope = new ModelScope();
        using var control = new HookMaskedTextBox("000") { ValidatingType = typeof(int), Text = "12" };
        TypeValidationEventArgs? result = null;
        var order = new List<string>();
        control.TypeValidationCompleted += (_, e) => { result = e; order.Add("type"); };
        control.Validating += (_, _) => order.Add("validating");
        Assert.Null(control.ValidateText());
        Assert.False(result!.IsValidInput);
        control.Text = "123";
        order.Clear();
        Assert.Equal(123, control.ValidateText());
        Assert.True(result.IsValidInput);
        order.Clear();
        control.ValidateFocus();
        Assert.Equal(["type", "validating"], order);
        control.Mask = "";
        control.FormatProvider = CultureInfo.GetCultureInfo("fr-FR").NumberFormat;
        control.ValidatingType = typeof(decimal);
        control.Text = "1,5";
        Assert.Equal(1.5m, control.ValidateText());
        control.ValidatingType = typeof(DayOfWeek);
        control.Text = "Friday";
        Assert.Equal(DayOfWeek.Friday, control.ValidateText());
        control.ValidatingType = typeof(MaskedTextBoxModelTests);
        Assert.Null(control.ValidateText());
        Assert.False(result.IsValidInput);
        Assert.Contains("AOT-safe", result.Message);
        Assert.False(control.IsHandleCreated);
    }

    private sealed class HookMaskedTextBox(string mask) : MaskedTextBox(mask)
    {
        public void PressCharacter(char character) => OnKeyPress(new KeyPressEventArgs(character));
        public void PressKey(Keys key) => OnKeyDown(new KeyEventArgs(key));
        public void ValidateFocus() => OnValidating(new CancelEventArgs());
        protected override Size DefaultSize => new(100, 20);
    }

    private sealed class ModelScope : IDisposable
    {
        private readonly bool _autoInstall = WindowsFormsSynchronizationContext.AutoInstall;
        public ModelScope() => WindowsFormsSynchronizationContext.AutoInstall = false;
        public void Dispose() => WindowsFormsSynchronizationContext.AutoInstall = _autoInstall;
    }
}
