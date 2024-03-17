namespace PdfSharp.Maui.Renderers.Controls;

[PdfRenderer(ViewType = typeof(Image))]
public class PdfImageRenderer : PdfRendererBase<Image>
{
    protected override void CreateLayout(XGraphics page, Image view, XRect bounds, double scaleFactor)
    {
        var background = view.GetBackgroundBrush(bounds);
        if (background.IsNotDefault())
        {
            page.DrawRectangle(background, bounds);
        }

        if (view.Source == null)
        {
            return;
        }

        page.DrawImage(view.Source.ToXImage(), bounds);
    }
}
