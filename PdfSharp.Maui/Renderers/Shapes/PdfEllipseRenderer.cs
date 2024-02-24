namespace PdfSharp.Maui.Renderers.Shapes;

using Microsoft.Maui.Controls.Shapes;

[PdfRenderer(ViewType = typeof(Ellipse))]
public class PdfEllipseRenderer : PdfRendererBase<Ellipse>
{
    protected override void CreateLayout(XGraphics page, Ellipse view, XRect bounds,
        double scaleFactor)
    {
        var background = view.GetBackgroundBrush(bounds);
        if (background.IsNotDefault())
        {
            page.DrawRectangle(view.GetBackgroundBrush(bounds), bounds);
        }
        page.DrawEllipse(view.Stroke.ToXPen(view.StrokeThickness * scaleFactor), bounds);
        page.DrawEllipse(view.Fill.ToXBrush(), bounds);
    }
}
