namespace PdfSharp.Maui.Renderers.Shapes;

using Microsoft.Maui.Controls.Shapes;


[PdfRenderer(ViewType = typeof(Rectangle))]
public class PdfRectangleRenderer : PdfShapeRendererBase<Rectangle>
{
    protected override void CreateLayout(XGraphics page, Rectangle view, XRect bounds,
        double scaleFactor)
    {
        var path = GetPath(view.GetPath(), view.Bounds.ToXRect(), bounds);
        page.DrawRectangle(view.GetBackgroundBrush(bounds), bounds);
        if (view.Fill?.IsEmpty == false)
        {
            page.DrawPath(view.Fill.ToXBrush(), path);
        }

        page.DrawPath(new XPen(view.Stroke.ToXColor(), view.StrokeThickness * scaleFactor), path);
    }
}
