namespace PdfSharp.Maui.Renderers.Layouts;

[PdfRenderer(ViewType = typeof(ScrollView))]
public class PdfScrollViewRenderer : PdfRendererBase<ScrollView>
{
    protected override void CreateLayout(XGraphics page, ScrollView view, XRect bounds, double scaleFactor)
    {
        var background = view.GetBackgroundBrush(bounds);
        if (background.IsNotDefault())
        {
            page.DrawRectangle(background, bounds);
        }
    }
}
