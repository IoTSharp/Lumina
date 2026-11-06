namespace Lumina.Forms;

/// <summary>
/// Specifies identifiers that indicate the result of a common dialog box.
/// </summary>
public enum DialogResult
{
    /// <summary>
    /// Nothing is returned from the dialog box.
    /// </summary>
    None = 0,

    /// <summary>
    /// The dialog box return value is OK.
    /// </summary>
    OK = 1,

    /// <summary>
    /// The dialog box return value is Cancel.
    /// </summary>
    Cancel = 2,

    /// <summary>
    /// The dialog box return value is Abort.
    /// </summary>
    Abort = 3,

    /// <summary>
    /// The dialog box return value is Retry.
    /// </summary>
    Retry = 4,

    /// <summary>
    /// The dialog box return value is Ignore.
    /// </summary>
    Ignore = 5,

    /// <summary>
    /// The dialog box return value is Yes.
    /// </summary>
    Yes = 6,

    /// <summary>
    /// The dialog box return value is No.
    /// </summary>
    No = 7,

    /// <summary>
    /// The dialog box return value is Try Again.
    /// </summary>
    TryAgain = 10,

    /// <summary>
    /// The dialog box return value is Continue.
    /// </summary>
    Continue = 11,
}
