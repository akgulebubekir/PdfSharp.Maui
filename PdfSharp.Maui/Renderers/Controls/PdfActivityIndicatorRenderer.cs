namespace PdfSharp.Maui.Renderers.Controls;

[PdfRenderer(ViewType = typeof(ActivityIndicator))]
public class PdfActivityIndicatorRenderer : PdfRendererBase<ActivityIndicator>
{
    protected override void CreateLayout(XGraphics page, ActivityIndicator view, XRect bounds, double scaleFactor)
    {
        if (HasBackgroundColor())
        {
            page.DrawRectangle(GetProperty<XBrush>(ViewProperties.BackgroundColor), bounds);
        }

        if (view.IsRunning)
        {
            page.DrawArc(
                new XPen(GetProperty<XColor>(ActivityIndicatorProperties.RingColor),
                    GetProperty<double>(ActivityIndicatorProperties.RingThickness)),
                GetProperty<XRect>(ActivityIndicatorProperties.RingLocation), 0,
                GetProperty<int>(ActivityIndicatorProperties.RingAngle));
        }
    }

    protected override void CreateUniformLayoutParameters(XGraphics page, ActivityIndicator view, XRect bounds,
        double scaleFactor)
    {
        Properties.Add(ViewProperties.BackgroundColor, XBrushes.White);
        Properties.Add(ActivityIndicatorProperties.RingColor, XColors.Gray);
        Properties.Add(ActivityIndicatorProperties.RingAngle, 270);
        Properties.Add(ActivityIndicatorProperties.RingThickness, 5 * scaleFactor);
        Properties.Add(ActivityIndicatorProperties.RingLocation,
            new XRect(new XPoint(bounds.X + ((bounds.Width - bounds.Height) / 2), bounds.Y),
                new XSize(bounds.Height - (3 * scaleFactor), bounds.Height - (3 * scaleFactor))));
    }

    protected override void CreateAndroidLayoutParameters(XGraphics page, ActivityIndicator view, XRect bounds,
        double scaleFactor)
    {
        Properties.Add(ViewProperties.BackgroundColor, view.GetBackgroundBrush(bounds));
        Properties.Add(ActivityIndicatorProperties.RingColor, view.Color.ToXColor());
        Properties.Add(ActivityIndicatorProperties.RingAngle, 270);
        Properties.Add(ActivityIndicatorProperties.RingThickness, 5 * scaleFactor);
        Properties.Add(ActivityIndicatorProperties.RingLocation,
            new XRect(new XPoint(bounds.X + ((bounds.Width - bounds.Height) / 2), bounds.Y),
                new XSize(bounds.Height - (3 * scaleFactor), bounds.Height - (3 * scaleFactor))));
    }

    protected override void CreateIosLayoutParameters(XGraphics page, ActivityIndicator view, XRect bounds,
        double scaleFactor)
    {
        Properties.Add(ViewProperties.BackgroundColor, view.GetBackgroundBrush(bounds));
        Properties.Add(ActivityIndicatorProperties.RingColor, view.Color.ToXColor());
        Properties.Add(ActivityIndicatorProperties.RingAngle, 240);
        Properties.Add(ActivityIndicatorProperties.RingThickness, 5 * scaleFactor);
        Properties.Add(ActivityIndicatorProperties.RingLocation,
            new XRect(new XPoint(bounds.X + ((bounds.Width - bounds.Height) / 2), bounds.Y),
                new XSize(bounds.Height - (3 * scaleFactor), bounds.Height - (3 * scaleFactor))));
    }

    protected override void CreateWindowsLayoutParameters(XGraphics page, ActivityIndicator view, XRect bounds,
        double scaleFactor)
    {
        Properties.Add(ViewProperties.BackgroundColor, view.GetBackgroundBrush(bounds));
        Properties.Add(ActivityIndicatorProperties.RingColor, view.Color.ToXColor());
        Properties.Add(ActivityIndicatorProperties.RingAngle, 240);
        Properties.Add(ActivityIndicatorProperties.RingThickness, 4 * scaleFactor);
        Properties.Add(ActivityIndicatorProperties.RingLocation,
            new XRect(new XPoint(bounds.X + ((bounds.Width - bounds.Height) / 2), bounds.Y),
                new XSize(bounds.Height - (3 * scaleFactor), bounds.Height - (3 * scaleFactor))));
    }

    private static class ActivityIndicatorProperties
    {
        public const string RingThickness = nameof(RingThickness);
        public const string RingColor = nameof(RingColor);
        public const string RingAngle = nameof(RingAngle);
        public const string RingLocation = nameof(RingLocation);
    }
}
