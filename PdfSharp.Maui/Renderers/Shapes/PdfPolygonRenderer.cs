using Microsoft.Maui.Controls.Shapes;
using PdfSharp.Maui.Attributes;

namespace PdfSharp.Maui.Renderers.Shapes;

[PdfRenderer(ViewType = typeof(Polygon))]
public class PdfPolygonRenderer : PdfShapeRendererBase<Polygon>
{
    protected override void CreateLayout(XGraphics page, Polygon view, XRect bounds, double scaleFactor)
    {
        var path = GetPath(view.GetPath(), view.Bounds.ToXRect(), bounds);
        var background = view.GetBackgroundBrush(bounds);
        if (background.IsNotDefault())
        {
            page.DrawRectangle(view.GetBackgroundBrush(bounds), bounds);
        }

        if (view.Fill?.IsEmpty == false)
        {
            page.DrawPath(view.Fill.ToXBrush(), path);
        }

        page.DrawPath(new XPen(view.Stroke.ToXColor(), view.StrokeThickness * scaleFactor), path);
    }
}