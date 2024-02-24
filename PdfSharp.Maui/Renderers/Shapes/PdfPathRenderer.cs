namespace PdfSharp.Maui.Renderers.Shapes;

using Path = Microsoft.Maui.Controls.Shapes.Path;

[PdfRenderer(ViewType = typeof(Path))]
public class PdfPathRenderer : PdfShapeRendererBase<Path>
{
    protected override void CreateLayout(XGraphics page, Path view, XRect bounds, double scaleFactor)
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
