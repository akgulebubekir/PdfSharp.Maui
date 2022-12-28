using PdfSharp.Maui.Attributes;

namespace PdfSharp.Maui.Renderers.Layouts;

[PdfRenderer(ViewType = typeof(ContentView))]
public class PdfContentViewRenderer : PdfRendererBase<ContentView>
{
    protected override void CreateLayout(XGraphics page, ContentView view, XRect bounds,
        double scaleFactor)
    {
        var background = view.GetBackgroundBrush(bounds);
        if (background.IsNotDefault())
        {
            page.DrawRectangle(background, bounds);
        }
    }
}