namespace PdfSharp.Maui.Renderers.Editors;

[PdfRenderer(ViewType = typeof(Switch))]
public class PdfSwitchRenderer : PdfRendererBase<Switch>
{
    protected override void CreateLayout(XGraphics page, Switch view, XRect bounds, double scaleFactor)
    {
        var cornerRadius = GetProperty<double>(ViewProperties.CornerRadius);
        var baseLocation = GetProperty<XRect>(SwitchProperties.BaseLocation);
        var thumbWith = GetProperty<double>(SwitchProperties.ThumbWidth);
        var thumbXOffset = GetProperty<double>(SwitchProperties.ThumbBaseXOffset);

        if (HasBackgroundColor())
        {
            page.DrawRoundedRectangle(GetProperty<XBrush>(ViewProperties.BackgroundColor), bounds,
                new XSize(cornerRadius, cornerRadius));
        }

        page.DrawRoundedRectangle(GetProperty<XBrush>(SwitchProperties.BaseColor), baseLocation,
            new XSize(baseLocation.Height, baseLocation.Height));
        page.DrawRoundedRectangle(
            new XPen(GetProperty<XColor>(SwitchProperties.BaseBorderColor),
                GetProperty<double>(SwitchProperties.BaseBorderThickness)), baseLocation,
            new XSize(baseLocation.Height, baseLocation.Height));

        page.DrawEllipse(GetProperty<XBrush>(SwitchProperties.ThumbColor),
            view.IsToggled
                ? baseLocation.Right + thumbXOffset - thumbWith + scaleFactor
                : baseLocation.X - thumbXOffset,
            bounds.Y + ((bounds.Height - thumbWith) / 2), thumbWith, thumbWith);

        if (GetProperty<bool>(SwitchProperties.HasStateText))
        {
            var text = view.IsToggled ? "On" : "Off";
            var font = new XFont(GlobalFontSettings.DefaultFontName, 14 * scaleFactor);
            var textLocation = new XRect(new XPoint(baseLocation.Right + (4 * scaleFactor), bounds.Y),
                new XSize(bounds.Width - baseLocation.Width, bounds.Height));

            page.DrawFormattedString(text, font, GetProperty<XBrush>(ViewProperties.TextColor), textLocation,
                XStringFormats.CenterLeft);
        }
    }

    protected override void CreateUniformLayoutParameters(XGraphics page, Switch view, XRect bounds, double scaleFactor)
    {
        Properties.Add(ViewProperties.BackgroundColor, XBrushes.White);
        Properties.Add(SwitchProperties.HasStateText, false);
        Properties.Add(SwitchProperties.ThumbColor, XBrushes.Black);
        Properties.Add(SwitchProperties.ThumbWidth, bounds.Height * 0.33);
        Properties.Add(SwitchProperties.BaseLocation,
            new XRect(new XPoint(bounds.X + (bounds.Height * 0.2), bounds.Y + (bounds.Height * 0.3)),
                new XSize(bounds.Height, bounds.Height * 0.4)));
        Properties.Add(SwitchProperties.BaseColor, XBrushes.White);
        Properties.Add(SwitchProperties.BaseBorderColor, XColors.Black);
        Properties.Add(SwitchProperties.BaseBorderThickness, scaleFactor);
        Properties.Add(SwitchProperties.ThumbBaseXOffset, -3 * scaleFactor);
        Properties.Add(ViewProperties.CornerRadius, 0.0);
    }

    protected override void CreateAndroidLayoutParameters(XGraphics page, Switch view, XRect bounds, double scaleFactor)
    {
        Properties.Add(ViewProperties.BackgroundColor, view.GetBackgroundBrush(bounds));
        Properties.Add(SwitchProperties.HasStateText, false);
        Properties.Add(SwitchProperties.ThumbColor,
            view.ThumbColor.IsNotDefault() ? view.ThumbColor.ToXBrush() :
            view.IsToggled ? new XSolidBrush(XColor.FromArgb(32, 17, 83)) : XBrushes.DarkGray);
        Properties.Add(SwitchProperties.ThumbWidth, bounds.Height * 0.4);
        Properties.Add(SwitchProperties.BaseLocation,
            new XRect(new XPoint(bounds.X + (0.1 * bounds.Height), bounds.Y + (bounds.Height * 0.35)),
                new XSize(bounds.Height * 0.8, bounds.Height * 0.3)));
        Properties.Add(SwitchProperties.ThumbBaseXOffset, bounds.Height * 0.1);
        Properties.Add(SwitchProperties.BaseColor,
            view.IsToggled && view.OnColor.IsNotDefault() ? view.OnColor.ToXBrush() :
            view.IsToggled ? XBrushes.LightGray : XBrushes.Gray);
        Properties.Add(ViewProperties.CornerRadius, 0.0);
        Properties.Add(SwitchProperties.BaseBorderColor, XColors.Transparent);
        Properties.Add(SwitchProperties.BaseBorderThickness, 0.0);
    }

    protected override void CreateIosLayoutParameters(XGraphics page, Switch view, XRect bounds, double scaleFactor)
    {
        Properties.Add(ViewProperties.BackgroundColor, view.GetBackgroundBrush(bounds));
        Properties.Add(SwitchProperties.HasStateText, false);
        Properties.Add(SwitchProperties.ThumbColor,
            view.ThumbColor.IsNotDefault() ? view.ThumbColor.ToXBrush() :
            view.IsToggled ? new XSolidBrush(XColor.FromArgb(32, 17, 83)) : XBrushes.DarkGray);
        Properties.Add(SwitchProperties.ThumbWidth, bounds.Height * 0.92);
        Properties.Add(SwitchProperties.BaseLocation,
            new XRect(bounds.Location, new XSize(bounds.Height * 1.6, bounds.Height)));
        Properties.Add(SwitchProperties.ThumbBaseXOffset, -4 * scaleFactor);
        Properties.Add(SwitchProperties.BaseColor,
            view.IsToggled && view.OnColor.IsNotDefault() ? view.OnColor.ToXBrush() : XBrushes.LightGray);
        Properties.Add(ViewProperties.CornerRadius, scaleFactor);
        Properties.Add(SwitchProperties.BaseBorderColor, view.IsToggled ? view.ThumbColor.ToXColor() : XColors.White);
        Properties.Add(SwitchProperties.BaseBorderThickness, scaleFactor);
    }

    protected override void CreateWindowsLayoutParameters(XGraphics page, Switch view, XRect bounds, double scaleFactor)
    {
        Properties.Add(ViewProperties.BackgroundColor, view.GetBackgroundBrush(bounds));
        Properties.Add(SwitchProperties.HasStateText, true);
        Properties.Add(ViewProperties.TextColor, XBrushes.White);
        Properties.Add(SwitchProperties.ThumbColor,
            view.ThumbColor.IsNotDefault() ? view.ThumbColor.ToXBrush() :
            view.IsToggled ? XBrushes.White : XBrushes.DarkGray);
        Properties.Add(SwitchProperties.ThumbWidth, bounds.Height * 0.35);
        Properties.Add(SwitchProperties.BaseLocation,
            new XRect(new XPoint(bounds.X + (3 * scaleFactor), bounds.Y + (bounds.Height * 0.275)),
                new XSize(bounds.Height * 0.9, bounds.Height * 0.45)));
        Properties.Add(SwitchProperties.ThumbBaseXOffset, -2 * scaleFactor);
        Properties.Add(SwitchProperties.BaseColor,
            view.IsToggled && view.OnColor.IsNotDefault() ? view.OnColor.ToXBrush() : XBrushes.LightGray);
        Properties.Add(ViewProperties.CornerRadius, 3 * scaleFactor);
        Properties.Add(SwitchProperties.BaseBorderColor, view.IsToggled ? view.ThumbColor.ToXColor() : XColors.White);
        Properties.Add(SwitchProperties.BaseBorderThickness, scaleFactor);
    }

    private static class SwitchProperties
    {
        public const string ThumbColor = nameof(ThumbColor);
        public const string ThumbWidth = nameof(ThumbWidth);
        public const string ThumbBaseXOffset = nameof(ThumbBaseXOffset);
        public const string HasStateText = nameof(HasStateText);
        public const string BaseColor = nameof(BaseColor);
        public const string BaseLocation = nameof(BaseLocation);
        public const string BaseBorderColor = nameof(BaseBorderColor);
        public const string BaseBorderThickness = nameof(BaseBorderThickness);
    }
}
