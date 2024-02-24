namespace PdfSharp.Maui.Renderers.Shapes;

using Microsoft.Maui.Controls.Shapes;


[PdfRenderer(ViewType = typeof(Line))]
public class PdfLineRenderer : PdfRendererBase<Line>
{
    protected override void CreateLayout(XGraphics page, Line view, XRect bounds, double scaleFactor)
    {
        var background = view.GetBackgroundBrush(bounds);
        if (background.IsNotDefault())
        {
            page.DrawRectangle(view.GetBackgroundBrush(bounds), bounds);
        }

        page.DrawLine(view.Stroke.ToXPen(view.StrokeThickness * scaleFactor),
            bounds.X + (view.X1 * scaleFactor),
            bounds.Y + (view.Y1 * scaleFactor),
            bounds.X + (view.X2 * scaleFactor),
            bounds.Y + (view.Y2 * scaleFactor));
    }
}
