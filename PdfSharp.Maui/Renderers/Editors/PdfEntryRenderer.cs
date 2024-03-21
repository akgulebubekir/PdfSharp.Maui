namespace PdfSharp.Maui.Renderers.Editors;

[PdfRenderer(ViewType = typeof(Entry))]
public class PdfEntryRenderer : PdfRendererBase<Entry>
{
    protected override void CreateLayout(XGraphics page, Entry view, XRect bounds,
        double scaleFactor)
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
                    GetProperty<double>(ViewProperties.BorderThickness)),
                GetProperty<XRect>(ViewProperties.BorderLocation), new XSize(cornerRadius, cornerRadius));
        }

        if (!string.IsNullOrEmpty(view.Text))
        {
            page.DrawFormattedString(view.TransformedText(), font, GetProperty<XBrush>(ViewProperties.TextColor),
                bounds.WithMargin(5 * scaleFactor),
                view.HorizontalTextAlignment.ToXStringFormat());
        }
        else if (!string.IsNullOrEmpty(view.Placeholder))
        {
            page.DrawFormattedString(view.Placeholder, font, GetProperty<XBrush>(ViewProperties.PlaceholderColor),
                bounds.WithMargin(5 * scaleFactor),
                view.HorizontalTextAlignment.ToXStringFormat());
        }
    }

    protected override void CreateUniformLayoutParameters(XGraphics page, Entry view, XRect bounds, double scaleFactor)
    {
        Properties.Add(ViewProperties.TextColor, XBrushes.Black);
        Properties.Add(ViewProperties.PlaceholderColor, XBrushes.Gray);
        Properties.Add(ViewProperties.BackgroundColor, XBrushes.White);
        Properties.Add(ViewProperties.BorderColor, XColors.LightGray);
        Properties.Add(ViewProperties.BorderLocation, bounds);
        Properties.Add(ViewProperties.CornerRadius, scaleFactor);
        Properties.Add(ViewProperties.BorderThickness, scaleFactor);
    }

    protected override void CreateAndroidLayoutParameters(XGraphics page, Entry view, XRect bounds, double scaleFactor)
    {
        Properties.Add(ViewProperties.TextColor, view.TextColor.ToXBrush());
        Properties.Add(ViewProperties.BackgroundColor, view.GetBackgroundBrush(bounds));
        Properties.Add(ViewProperties.PlaceholderColor,
            view.PlaceholderColor.IsNotDefault() ? view.PlaceholderColor.ToXBrush() : XBrushes.Gray);

        Properties.Add(ViewProperties.CornerRadius, 0.0);
    }

    protected override void CreateIosLayoutParameters(XGraphics page, Entry view, XRect bounds, double scaleFactor)
    {
        Properties.Add(ViewProperties.TextColor, view.TextColor.ToXBrush());
        Properties.Add(ViewProperties.BackgroundColor, view.GetBackgroundBrush(bounds));
        Properties.Add(ViewProperties.PlaceholderColor,
            view.PlaceholderColor.IsNotDefault() ? view.PlaceholderColor.ToXBrush() : XBrushes.Gray);
        Properties.Add(ViewProperties.CornerRadius, 2 * scaleFactor);
        Properties.Add(ViewProperties.BorderThickness, scaleFactor);
        Properties.Add(ViewProperties.BorderColor, XColors.LightGray);
        Properties.Add(ViewProperties.BorderLocation, bounds);
    }

    protected override void CreateWindowsLayoutParameters(XGraphics page, Entry view, XRect bounds, double scaleFactor)
    {
        Properties.Add(ViewProperties.TextColor, view.TextColor.ToXBrush());
        Properties.Add(ViewProperties.BackgroundColor, view.GetBackgroundBrush(bounds));
        Properties.Add(ViewProperties.PlaceholderColor,
            view.PlaceholderColor.IsNotDefault() ? view.PlaceholderColor.ToXBrush() : XBrushes.Gray);
        Properties.Add(ViewProperties.CornerRadius, scaleFactor);

        Properties.Add(ViewProperties.BorderThickness, scaleFactor);
        Properties.Add(ViewProperties.BorderColor, XColors.LightGray);
        Properties.Add(ViewProperties.BorderLocation,
            new XRect(bounds.BottomLeft, new XSize(bounds.Width, scaleFactor)));
    }
}
