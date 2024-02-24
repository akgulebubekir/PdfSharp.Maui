namespace PdfSharp.Maui.Renderers.Controls;

[PdfRenderer(ViewType = typeof(ProgressBar))]
public class PdfProgressBarRenderer : PdfRendererBase<ProgressBar>
{
    protected override void CreateLayout(XGraphics page, ProgressBar view, XRect bounds, double scaleFactor)
    {
        var emptyLineThickness = GetProperty<double>(ProgressBarProperties.EmptyLineThickness);
        var progressLineThickness = GetProperty<double>(ProgressBarProperties.ProgressLineThickness);
        var cornerRadius = GetProperty<double>(ViewProperties.CornerRadius);

        if (HasBackgroundColor())
        {
            page.DrawRectangle(GetProperty<XBrush>(ViewProperties.BackgroundColor), bounds);
        }

        page.DrawRectangle(GetProperty<XBrush>(ProgressBarProperties.EmptyLineColor),
            new XRect(new XPoint(bounds.X, bounds.Y + ((bounds.Height - emptyLineThickness) / 2)),
                new XSize(bounds.Width, emptyLineThickness)));
        page.DrawRoundedRectangle(GetProperty<XBrush>(ProgressBarProperties.ProgressLineColor),
            new XRect(new XPoint(bounds.X, bounds.Y + ((bounds.Height - progressLineThickness) / 2)),
                new XSize(bounds.Width * view.Progress, progressLineThickness)), new XSize(cornerRadius, cornerRadius));
    }

    protected override void CreateUniformLayoutParameters(XGraphics page, ProgressBar view, XRect bounds,
        double scaleFactor)
    {
        Properties.Add(ViewProperties.CornerRadius, 0.0);
        Properties.Add(ViewProperties.BackgroundColor, XBrushes.White);
        Properties.Add(ProgressBarProperties.ProgressLineColor, XBrushes.Black);
        Properties.Add(ProgressBarProperties.ProgressLineThickness, bounds.Height * 0.4);
        Properties.Add(ProgressBarProperties.EmptyLineColor, XBrushes.LightGray);
        Properties.Add(ProgressBarProperties.EmptyLineThickness, bounds.Height * 0.4);
    }

    protected override void CreateAndroidLayoutParameters(XGraphics page, ProgressBar view, XRect bounds,
        double scaleFactor)
    {
        Properties.Add(ViewProperties.CornerRadius, 0.0);
        Properties.Add(ViewProperties.BackgroundColor, view.GetBackgroundBrush(bounds));
        Properties.Add(ProgressBarProperties.ProgressLineColor,
            view.ProgressColor.IsNotDefault()
                ? view.ProgressColor.ToXBrush()
                : new XSolidBrush(XColor.FromArgb(32, 17, 83)));
        Properties.Add(ProgressBarProperties.ProgressLineThickness, bounds.Height * 0.2);
        Properties.Add(ProgressBarProperties.EmptyLineColor, XBrushes.LightGray);
        Properties.Add(ProgressBarProperties.EmptyLineThickness, bounds.Height * 0.2);
    }

    protected override void CreateIosLayoutParameters(XGraphics page, ProgressBar view, XRect bounds,
        double scaleFactor)
    {
        Properties.Add(ViewProperties.CornerRadius, 0.0);
        Properties.Add(ViewProperties.BackgroundColor, XBrushes.Transparent);
        Properties.Add(ProgressBarProperties.ProgressLineColor,
            view.ProgressColor.IsNotDefault()
                ? view.ProgressColor.ToXBrush()
                : new XSolidBrush(XColor.FromArgb(32, 17, 83)));
        Properties.Add(ProgressBarProperties.ProgressLineThickness, bounds.Height * 0.8);
        Properties.Add(ProgressBarProperties.EmptyLineColor, XBrushes.LightGray);
        Properties.Add(ProgressBarProperties.EmptyLineThickness, bounds.Height * 0.8);
    }

    protected override void CreateWindowsLayoutParameters(XGraphics page, ProgressBar view, XRect bounds,
        double scaleFactor)
    {
        Properties.Add(ViewProperties.BackgroundColor, XBrushes.Transparent);
        Properties.Add(ProgressBarProperties.ProgressLineColor,
            view.ProgressColor.IsNotDefault() ? view.ProgressColor.ToXBrush() : XBrushes.White);
        Properties.Add(ProgressBarProperties.ProgressLineThickness, 4 * scaleFactor);
        Properties.Add(ProgressBarProperties.EmptyLineColor, XBrushes.LightGray);
        Properties.Add(ProgressBarProperties.EmptyLineThickness, scaleFactor);
        Properties.Add(ViewProperties.CornerRadius, 2 * scaleFactor);
    }

    private static class ProgressBarProperties
    {
        public const string ProgressLineColor = nameof(ProgressLineColor);
        public const string EmptyLineColor = nameof(EmptyLineColor);
        public const string ProgressLineThickness = nameof(ProgressLineThickness);
        public const string EmptyLineThickness = nameof(EmptyLineThickness);
    }
}
