using System.Drawing;

namespace Lumina.Forms;

/// <summary>
/// Provides a client-area container and shared layout suspension for scrollable controls.
/// </summary>
public class ScrollableControl : Control
{
    private int _layoutSuspendCount;
    private bool _layoutPending;

    /// <inheritdoc />
    public override Rectangle DisplayRectangle => new(
        Padding.Left,
        Padding.Top,
        Math.Max(0, ClientSize.Width - Padding.Horizontal),
        Math.Max(0, ClientSize.Height - Padding.Vertical));

    /// <inheritdoc />
    public override void SuspendLayout()
    {
        _layoutSuspendCount++;
    }

    /// <inheritdoc />
    public override void ResumeLayout(bool performLayout)
    {
        if (_layoutSuspendCount > 0)
        {
            _layoutSuspendCount--;
        }

        if (_layoutSuspendCount == 0)
        {
            bool pending = _layoutPending;
            _layoutPending = false;
            if (performLayout && pending)
            {
                PerformLayout();
            }
        }
    }

    /// <inheritdoc />
    public override void PerformLayout()
    {
        if (!DeferLayout())
        {
            foreach (Control child in ChildControls)
            {
                child.PerformLayout();
            }

            base.PerformLayout();
        }
    }

    private protected bool DeferLayout()
    {
        if (_layoutSuspendCount == 0)
        {
            return false;
        }

        _layoutPending = true;
        return true;
    }
}
