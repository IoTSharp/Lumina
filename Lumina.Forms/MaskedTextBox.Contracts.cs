using System.ComponentModel;

namespace Lumina.Forms;

/// <summary>Defines how a masked text box formats its displayed or copied value.</summary>
public enum MaskFormat
{
    ExcludePromptAndLiterals = 0,
    IncludePrompt = 1,
    IncludeLiterals = 2,
    IncludePromptAndLiterals = 3
}

/// <summary>Defines the insertion mode used by a masked text box.</summary>
public enum InsertKeyMode
{
    Default = 0,
    Insert = 1,
    Overwrite = 2
}

/// <summary>Provides data for the <see cref="MaskedTextBox.MaskInputRejected"/> event.</summary>
public sealed class MaskInputRejectedEventArgs : EventArgs
{
    public MaskInputRejectedEventArgs(int position, MaskedTextResultHint rejectionHint)
    {
        Position = position;
        RejectionHint = rejectionHint;
    }

    public int Position { get; }

    public MaskedTextResultHint RejectionHint { get; }
}

/// <summary>Represents a handler for rejected masked input.</summary>
public delegate void MaskInputRejectedEventHandler(object? sender, MaskInputRejectedEventArgs e);

/// <summary>Provides data for the <see cref="MaskedTextBox.TypeValidationCompleted"/> event.</summary>
public sealed class TypeValidationEventArgs : EventArgs
{
    public TypeValidationEventArgs(Type validatingType, bool isValidInput, object? returnValue, string? message)
    {
        ArgumentNullException.ThrowIfNull(validatingType);
        ValidatingType = validatingType;
        IsValidInput = isValidInput;
        ReturnValue = returnValue;
        Message = message;
    }

    public bool Cancel { get; set; }

    public bool IsValidInput { get; }

    public string? Message { get; }

    public object? ReturnValue { get; }

    public Type ValidatingType { get; }
}

/// <summary>Represents a handler for masked text type validation.</summary>
public delegate void TypeValidationEventHandler(object? sender, TypeValidationEventArgs e);
