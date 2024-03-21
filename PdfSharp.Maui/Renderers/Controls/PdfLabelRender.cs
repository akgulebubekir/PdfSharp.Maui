namespace PdfSharp.Maui.Renderers.Controls;

[PdfRenderer(ViewType = typeof(Label))]
public class PdfLabelRenderer : PdfRendererBase<Label>
{
    protected override void CreateLayout(XGraphics page, Label view, XRect bounds, double scaleFactor)
    {
        var font = new XFont(view.FontFamily ?? GlobalFontSettings.DefaultFontName,
            view.FontSize * scaleFactor, view.FontAttributes.ToXFontStyle(), XPdfFontOptions.UnicodeDefault);

        if (HasBackgroundColor())
        {
            page.DrawRectangle(GetProperty<XBrush>(ViewProperties.BackgroundColor), bounds);
        }

        if (!string.IsNullOrEmpty(view.Text))
        {
            page.DrawFormattedString(view.TransformedText(), font, GetProperty<XBrush>(ViewProperties.TextColor), bounds,
                view.HorizontalTextAlignment.ToXStringFormat());
        }
    }

    protected override void CreateUniformLayoutParameters(XGraphics page, Label view, XRect bounds, double scaleFactor)
    {
        Properties.Add(ViewProperties.TextColor, XBrushes.Black);
        Properties.Add(ViewProperties.BackgroundColor, XBrushes.White);
    }

    protected override void CreateAndroidLayoutParameters(XGraphics page, Label view, XRect bounds, double scaleFactor)
    {
        Properties.Add(ViewProperties.TextColor, view.TextColor.ToXBrush());
        Properties.Add(ViewProperties.BackgroundColor, view.GetBackgroundBrush(bounds));
    }

    protected override void CreateIosLayoutParameters(XGraphics page, Label view, XRect bounds, double scaleFactor)
    {
        Properties.Add(ViewProperties.TextColor, view.TextColor.ToXBrush());
        Properties.Add(ViewProperties.BackgroundColor, view.GetBackgroundBrush(bounds));
    }

    protected override void CreateWindowsLayoutParameters(XGraphics page, Label view, XRect bounds, double scaleFactor)
    {
        Properties.Add(ViewProperties.TextColor, view.TextColor.ToXBrush());
        Properties.Add(ViewProperties.BackgroundColor, view.GetBackgroundBrush(bounds));
    }
}
