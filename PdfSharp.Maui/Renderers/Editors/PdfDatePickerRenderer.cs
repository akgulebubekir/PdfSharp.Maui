namespace PdfSharp.Maui.Renderers.Editors;

using System.Globalization;

[PdfRenderer(ViewType = typeof(DatePicker))]
public class PdfDatePickerRenderer : PdfRendererBase<DatePicker>
{
    protected override void CreateLayout(XGraphics page, DatePicker view, XRect bounds,
        double scaleFactor)
    {
        var font = new XFont(GlobalFontSettings.DefaultFontName, view.FontSize * scaleFactor);
        var format = view.Format;
        var cornerRadius = GetProperty<double>(ViewProperties.CornerRadius);

        if (string.IsNullOrEmpty(format))
        {
            format = CultureInfo.CurrentCulture.DateTimeFormat.ShortDatePattern;
        }

        if (HasBackgroundColor())
        {
            page.DrawRoundedRectangle(GetProperty<XBrush>(ViewProperties.BackgroundColor), bounds,
                new XSize(cornerRadius, cornerRadius));
        }

        page.DrawRoundedRectangle(new XPen(GetProperty<XColor>(ViewProperties.BorderColor), scaleFactor), bounds,
            new XSize(cornerRadius, cornerRadius));

        page.DrawString(view.Date.ToString(format), font,
            GetProperty<XBrush>(ViewProperties.TextColor), bounds,
            new XStringFormat
            {
                Alignment = XStringAlignment.Center,
                LineAlignment = XLineAlignment.Center
            });
    }

    protected override void CreateUniformLayoutParameters(XGraphics page, DatePicker view, XRect bounds,
        double scaleFactor)
    {
        Properties.Add(ViewProperties.BackgroundColor, XBrushes.White);
        Properties.Add(ViewProperties.TextColor, XBrushes.Black);
        Properties.Add(ViewProperties.CornerRadius, scaleFactor);
        Properties.Add(ViewProperties.BorderColor, XColors.LightGray);
    }

    protected override void CreateAndroidLayoutParameters(XGraphics page, DatePicker view, XRect bounds,
        double scaleFactor)
    {
        Properties.Add(ViewProperties.BackgroundColor, view.GetBackgroundBrush(bounds));
        Properties.Add(ViewProperties.TextColor,
            view.TextColor.IsNotDefault() ? view.TextColor.ToXBrush() : XBrushes.DarkGray);
        Properties.Add(ViewProperties.CornerRadius, 0.0);
        Properties.Add(ViewProperties.BorderColor, XColors.LightSlateGray);
    }

    protected override void CreateWindowsLayoutParameters(XGraphics page, DatePicker view, XRect bounds,
        double scaleFactor)
    {
        Properties.Add(ViewProperties.TextColor,
            view.TextColor.IsNotDefault() ? view.TextColor.ToXBrush() : XBrushes.Gray);
        Properties.Add(ViewProperties.BackgroundColor, view.GetBackgroundBrush(bounds));
        Properties.Add(ViewProperties.CornerRadius, 2 * scaleFactor);
        Properties.Add(ViewProperties.BorderColor, XColors.LightGray);
    }

    protected override void CreateIosLayoutParameters(XGraphics page, DatePicker view, XRect bounds, double scaleFactor)
    {
        Properties.Add(ViewProperties.BackgroundColor, view.GetBackgroundBrush(bounds));
        Properties.Add(ViewProperties.TextColor,
            view.TextColor.IsNotDefault() ? view.TextColor.ToXBrush() : XBrushes.DarkGray);
        Properties.Add(ViewProperties.CornerRadius, 2 * scaleFactor);
        Properties.Add(ViewProperties.BorderColor, XColors.LightGray);
        Properties.Add(ViewProperties.BorderThickness, scaleFactor);
    }
}
