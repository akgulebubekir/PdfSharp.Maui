using PdfSharp.Maui.Attributes;

namespace PdfSharp.Maui.Renderers.Controls;

[PdfRenderer(ViewType = typeof(IndicatorView))]
internal class PdfIndicatorViewRenderer : PdfRendererBase<IndicatorView>
{
    //TODO implement
    protected override void CreateLayout(XGraphics page, IndicatorView view, XRect bounds,
        double scaleFactor)
    {
        if (HasBackgroundColor())
        {
            page.DrawRectangle(view.Background.ToXBrush(), bounds);
        }
    }
}