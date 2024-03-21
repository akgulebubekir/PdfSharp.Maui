namespace PdfSharp.Maui.Renderers.Editors;

[PdfRenderer(ViewType = typeof(TimePicker))]
internal class PdfTimePickerRenderer : PdfRendererBase<TimePicker>
{
    protected override void CreateLayout(XGraphics page, TimePicker view, XRect bounds, double scaleFactor)
    {
        var font = new XFont(GlobalFontSettings.DefaultFontName, 14 * scaleFactor);
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

        page.DrawFormattedString(GetProperty<string>(TimePickerProperties.TimeText), font,
            GetProperty<XBrush>(ViewProperties.TextColor), bounds, XStringFormats.Center);
    }

    protected override void CreateUniformLayoutParameters(XGraphics page, TimePicker view, XRect bounds,
        double scaleFactor)
    {
        var hour = view.Time.Hours == 12 ? 12 : view.Time.Hours % 12;
        var indicator = view.Time.Hours >= 12 ? "pm" : "am";
        Properties.Add(TimePickerProperties.TimeText, $"{hour} {view.Time.Minutes} {indicator}");
        Properties.Add(ViewProperties.TextColor, XBrushes.Black);
        Properties.Add(ViewProperties.BackgroundColor, XBrushes.White);
        Properties.Add(ViewProperties.BorderColor, XColors.Gray);
        Properties.Add(ViewProperties.BorderThickness, scaleFactor);
        Properties.Add(ViewProperties.CornerRadius, scaleFactor);
    }

    protected override void CreateAndroidLayoutParameters(XGraphics page, TimePicker view, XRect bounds,
        double scaleFactor)
    {
        var hour = view.Time.Hours == 12 ? 12 : view.Time.Hours % 12;
        var indicator = view.Time.Hours >= 12 ? "pm" : "am";
        Properties.Add(TimePickerProperties.TimeText, $"{hour} : {view.Time.Minutes} {indicator}");
        Properties.Add(ViewProperties.TextColor, view.TextColor.ToXBrush());
        Properties.Add(ViewProperties.BackgroundColor, view.GetBackgroundBrush(bounds));
        Properties.Add(ViewProperties.BorderColor, XColors.Transparent);
        Properties.Add(ViewProperties.BorderThickness, 0.0);
        Properties.Add(ViewProperties.CornerRadius, 0.0);
    }

    protected override void CreateIosLayoutParameters(XGraphics page, TimePicker view, XRect bounds, double scaleFactor)
    {
        var hour = view.Time.Hours == 12 ? 12 : view.Time.Hours % 12;
        var indicator = view.Time.Hours >= 12 ? "PM" : "AM";
        Properties.Add(TimePickerProperties.TimeText, $"{hour}:{view.Time.Minutes} {indicator}");
        Properties.Add(ViewProperties.TextColor, view.TextColor.ToXBrush());
        Properties.Add(ViewProperties.BackgroundColor, view.GetBackgroundBrush(bounds));
        Properties.Add(ViewProperties.BorderColor, XColors.Gray);
        Properties.Add(ViewProperties.BorderThickness, scaleFactor);
        Properties.Add(ViewProperties.CornerRadius, 2 * scaleFactor);
    }

    protected override void CreateWindowsLayoutParameters(XGraphics page, TimePicker view, XRect bounds,
        double scaleFactor)
    {
        var hour = view.Time.Hours == 12 ? 12 : view.Time.Hours % 12;
        var indicator = view.Time.Hours >= 12 ? "PM" : "AM";
        Properties.Add(TimePickerProperties.TimeText, $"{hour}    {view.Time.Minutes}    {indicator}");
        Properties.Add(ViewProperties.TextColor, view.TextColor.ToXBrush());
        Properties.Add(ViewProperties.BackgroundColor, view.GetBackgroundBrush(bounds));
        Properties.Add(ViewProperties.BorderColor, XColors.LightGray);
        Properties.Add(ViewProperties.BorderThickness, scaleFactor);
        Properties.Add(ViewProperties.CornerRadius, 2 * scaleFactor);
    }

    private static class TimePickerProperties
    {
        public const string TimeText = nameof(TimeText);
    }
}
