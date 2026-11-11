using System;

namespace Lumina.Forms;

[Flags]
public enum AnchorStyles
{
    None = 0,
    Top = 1,
    Bottom = 2,
    Left = 4,
    Right = 8
}

public enum AutoSizeMode
{
    GrowAndShrink = 0,
    GrowOnly = 1
}

public enum CheckState
{
    Unchecked = 0,
    Checked = 1,
    Indeterminate = 2
}

public enum CloseReason
{
    None = 0,
    WindowsShutDown = 1,
    MdiFormClosing = 2,
    UserClosing = 3,
    TaskManagerClosing = 4,
    FormOwnerClosing = 5,
    ApplicationExitCall = 6
}

public enum FormStartPosition
{
    Manual = 0,
    CenterScreen = 1
}

public enum FormWindowState
{
    Normal = 0,
    Minimized = 1,
    Maximized = 2
}

public enum HighDpiMode
{
    DpiUnaware = 0,
    SystemAware = 1,
    PerMonitor = 2,
    PerMonitorV2 = 3,
    DpiUnawareGdiScaled = 4
}

public enum HorizontalAlignment
{
    Left = 0,
    Right = 1,
    Center = 2
}

public enum ScrollBars
{
    None = 0,
    Horizontal = 1,
    Vertical = 2,
    Both = 3
}
