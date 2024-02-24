namespace PdfSharp.Maui.Renderers.Editors;

[PdfRenderer(ViewType = typeof(Editor))]
public class PdfEditorRenderer : PdfRendererBase<Editor>
{
    protected override void CreateLayout(XGraphics page, Editor view, XRect bounds, double scaleFactor)
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
            page.DrawString(view.Text, font, GetProperty<XBrush>(ViewProperties.TextColor),
                bounds.WithMargin(5 * scaleFactor),
                new XStringFormat
                {
                    Alignment = XStringAlignment.Near,
                    LineAlignment = XLineAlignment.Near
                });
        }
        else if (!string.IsNullOrEmpty(view.Placeholder))
        {
            page.DrawString(view.Placeholder, font, GetProperty<XBrush>(ViewProperties.PlaceholderColor),
                bounds.WithMargin(5 * scaleFactor),
                new XStringFormat
                {
                    Alignment = XStringAlignment.Near,
                    LineAlignment = XLineAlignment.Near
                });
        }
    }

    protected override void CreateUniformLayoutParameters(XGraphics page, Editor view, XRect bounds, double scaleFactor)
    {
        Properties.Add(ViewProperties.BackgroundColor, XBrushes.White);
        Properties.Add(ViewProperties.PlaceholderColor, XBrushes.Gray);
        Properties.Add(ViewProperties.TextColor, XBrushes.Black);
        Properties.Add(ViewProperties.CornerRadius, 0.0);
        Properties.Add(ViewProperties.BorderThickness, scaleFactor);
        Properties.Add(ViewProperties.BorderColor, XColors.LightGray);
        Properties.Add(ViewProperties.BorderLocation, bounds);
    }

    protected override void CreateAndroidLayoutParameters(XGraphics page, Editor view, XRect bounds, double scaleFactor)
    {
        Properties.Add(ViewProperties.TextColor,
            view.TextColor.IsNotDefault() ? view.TextColor.ToXBrush() : XBrushes.Black);
        Properties.Add(ViewProperties.PlaceholderColor,
            view.PlaceholderColor.IsNotDefault() ? view.PlaceholderColor.ToXBrush() : XBrushes.Gray);
        Properties.Add(ViewProperties.BackgroundColor,
            view.HasBackground() ? view.GetBackgroundBrush(bounds) : XBrushes.White);
        Properties.Add(ViewProperties.CornerRadius, 0.0);
    }

    protected override void CreateIosLayoutParameters(XGraphics page, Editor view, XRect bounds, double scaleFactor)
    {
        Properties.Add(ViewProperties.TextColor,
            view.TextColor.IsNotDefault() ? view.TextColor.ToXBrush() : XBrushes.Black);
        Properties.Add(ViewProperties.PlaceholderColor,
            view.PlaceholderColor.IsNotDefault() ? view.PlaceholderColor.ToXBrush() : XBrushes.Gray);
        Properties.Add(ViewProperties.BackgroundColor,
            view.HasBackground() ? view.GetBackgroundBrush(bounds) : XBrushes.White);
        Properties.Add(ViewProperties.CornerRadius, scaleFactor);
    }

    protected override void CreateWindowsLayoutParameters(XGraphics page, Editor view, XRect bounds, double scaleFactor)
    {
        Properties.Add(ViewProperties.TextColor,
            view.TextColor.IsNotDefault() ? view.TextColor.ToXBrush() : XBrushes.Black);
        Properties.Add(ViewProperties.PlaceholderColor,
            view.PlaceholderColor.IsNotDefault() ? view.PlaceholderColor.ToXBrush() : XBrushes.Gray);
        Properties.Add(ViewProperties.BackgroundColor,
            view.HasBackground() ? view.GetBackgroundBrush(bounds) : XBrushes.White);
        Properties.Add(ViewProperties.CornerRadius, scaleFactor);
        Properties.Add(ViewProperties.BorderThickness, scaleFactor);
        Properties.Add(ViewProperties.BorderColor, XColors.LightGray);
        Properties.Add(ViewProperties.BorderLocation,
            new XRect(bounds.BottomLeft, new XSize(bounds.Width, scaleFactor)));
    }
}
