namespace PdfSharp.Maui.Renderers.Controls;

[PdfRenderer(ViewType = typeof(Button))]
public class PdfButtonRenderer : PdfRendererBase<Button>
{
    protected override void CreateLayout(XGraphics page, Button view, XRect bounds, double scaleFactor)
    {
        var font = new XFont(view.FontFamily ?? GlobalFontSettings.DefaultFontName,
            view.FontSize * scaleFactor);
        var cornerRadius = GetProperty<double>(ViewProperties.CornerRadius);

        if (HasBackgroundColor())
        {
            page.DrawRoundedRectangle(GetProperty<XBrush>(ViewProperties.BackgroundColor), bounds,
                new XSize(cornerRadius, cornerRadius));
        }

        if (HasProperty(ViewProperties.BorderColor))
        {
            page.DrawRoundedRectangle(
                new XPen(GetProperty<XColor>(ViewProperties.BorderColor),
                    GetProperty<double>(ViewProperties.BorderThickness)), bounds,
                new XSize(cornerRadius, cornerRadius));
        }

        if (!string.IsNullOrEmpty(view.Text))
        {
            page.DrawFormattedString(view.TransformedText(), font, GetProperty<XBrush>(ViewProperties.TextColor), bounds,
                XStringFormats.Center);
        }
    }

    protected override void CreateUniformLayoutParameters(XGraphics page, Button view, XRect bounds, double scaleFactor)
    {
        Properties.Add(ViewProperties.BackgroundColor, XBrushes.Gray);
        Properties.Add(ViewProperties.TextColor, XBrushes.White);
        Properties.Add(ViewProperties.CornerRadius, 4 * scaleFactor);
    }

    protected override void CreateAndroidLayoutParameters(XGraphics page, Button view, XRect bounds, double scaleFactor)
    {
        Properties.Add(ViewProperties.BackgroundColor, view.GetBackgroundBrush(bounds));
        Properties.Add(ViewProperties.TextColor, view.TextColor.ToXBrush());
        Properties.Add(ViewProperties.BorderColor, view.BorderColor.ToXColor());
        Properties.Add(ViewProperties.BorderThickness, view.BorderWidth * scaleFactor);
        Properties.Add(ViewProperties.CornerRadius, view.CornerRadius * scaleFactor);
    }

    protected override void CreateIosLayoutParameters(XGraphics page, Button view, XRect bounds, double scaleFactor)
    {
        Properties.Add(ViewProperties.BackgroundColor, view.GetBackgroundBrush(bounds));
        Properties.Add(ViewProperties.TextColor, view.TextColor.ToXBrush());
        Properties.Add(ViewProperties.BorderColor, view.BorderColor.ToXColor());
        Properties.Add(ViewProperties.BorderThickness, view.BorderWidth * scaleFactor);
        Properties.Add(ViewProperties.CornerRadius, view.CornerRadius * scaleFactor);
    }

    protected override void CreateWindowsLayoutParameters(XGraphics page, Button view, XRect bounds, double scaleFactor)
    {
        Properties.Add(ViewProperties.BackgroundColor, view.GetBackgroundBrush(bounds));
        Properties.Add(ViewProperties.TextColor, view.TextColor.ToXBrush());
        Properties.Add(ViewProperties.BorderColor, view.BorderColor.ToXColor());
        Properties.Add(ViewProperties.BorderThickness, view.BorderWidth * scaleFactor);
        Properties.Add(ViewProperties.CornerRadius, view.CornerRadius * scaleFactor);
    }
}
