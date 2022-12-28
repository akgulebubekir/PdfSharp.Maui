using PdfSharp.Maui.Attributes;

namespace PdfSharp.Maui.Renderers.Editors;

[PdfRenderer(ViewType = typeof(Stepper))]
internal class PdfStepperRenderer : PdfRendererBase<Stepper>
{
    protected override void CreateLayout(XGraphics page, Stepper view, XRect bounds, double scaleFactor)
    {
        var font = new XFont(GlobalFontSettings.DefaultFontName, 14 * scaleFactor);
        var cornerRadius = GetProperty<double>(StepperProperties.ButtonCornerRadius);
        var buttonSize = GetProperty<XSize>(StepperProperties.ButtonSize);
        var buttonSpace = GetProperty<double>(StepperProperties.ButtonSpace);

        var minusButtonLocation =
            new XRect(
                new XPoint(bounds.X + (bounds.Width - 2 * buttonSize.Width - buttonSpace) / 2,
                    bounds.Y + (bounds.Height - buttonSize.Height) / 2), buttonSize);
        var plusButtonLocation =
            new XRect(new XPoint(minusButtonLocation.Right + buttonSpace / 2, minusButtonLocation.Y), buttonSize);

        if (HasProperty(ViewProperties.BackgroundColor))
        {
            page.DrawRectangle(GetProperty<XBrush>(ViewProperties.BackgroundColor), bounds);
        }

        page.DrawRoundedRectangle(GetProperty<XBrush>(StepperProperties.ButtonBackgroundColor), minusButtonLocation,
            new XSize(cornerRadius, cornerRadius));
        page.DrawRoundedRectangle(GetProperty<XBrush>(StepperProperties.ButtonBackgroundColor), plusButtonLocation,
            new XSize(cornerRadius, cornerRadius));

        page.DrawString("-", font, GetProperty<XBrush>(ViewProperties.TextColor), minusButtonLocation,
            XStringFormats.Center);
        page.DrawString("+", font, GetProperty<XBrush>(ViewProperties.TextColor), plusButtonLocation,
            XStringFormats.Center);
    }

    protected override void CreateUniformLayoutParameters(XGraphics page, Stepper view, XRect bounds,
        double scaleFactor)
    {
        Properties.Add(StepperProperties.ButtonBackgroundColor, XBrushes.LightGray);
        Properties.Add(StepperProperties.ButtonSize, new XSize(bounds.Width * 0.4, bounds.Height * 0.8));
        Properties.Add(StepperProperties.ButtonCornerRadius, 2 * scaleFactor);
        Properties.Add(StepperProperties.ButtonSpace, 0.06 * bounds.Width);

        Properties.Add(ViewProperties.TextColor, XBrushes.Black);
        Properties.Add(ViewProperties.BackgroundColor, XBrushes.White);
    }

    protected override void CreateAndroidLayoutParameters(XGraphics page, Stepper view, XRect bounds,
        double scaleFactor)
    {
        Properties.Add(StepperProperties.ButtonBackgroundColor, XBrushes.LightGray);
        Properties.Add(StepperProperties.ButtonCornerRadius, scaleFactor);
        Properties.Add(StepperProperties.ButtonSize, new XSize(bounds.Width * 0.45, bounds.Height * 0.8));
        Properties.Add(StepperProperties.ButtonSpace, 0.1 * bounds.Width);

        Properties.Add(ViewProperties.TextColor, XBrushes.Black);
        Properties.Add(ViewProperties.BackgroundColor, view.GetBackgroundBrush(bounds));
    }

    protected override void CreateIosLayoutParameters(XGraphics page, Stepper view, XRect bounds, double scaleFactor)
    {
        Properties.Add(StepperProperties.ButtonBackgroundColor, view.GetBackgroundBrush(bounds));
        Properties.Add(StepperProperties.ButtonCornerRadius, scaleFactor);
        Properties.Add(StepperProperties.ButtonSize, new XSize(bounds.Width * 0.45, bounds.Height * 0.9));
        Properties.Add(StepperProperties.ButtonSpace, 0.1 * bounds.Width);

        Properties.Add(ViewProperties.TextColor, XBrushes.Black);
        Properties.Add(ViewProperties.BackgroundColor, view.GetBackgroundBrush(bounds));
  }

    protected override void CreateWindowsLayoutParameters(XGraphics page, Stepper view, XRect bounds,
        double scaleFactor)
    {
        Properties.Add(StepperProperties.ButtonBackgroundColor,
            view.HasBackground() ? view.GetBackgroundBrush(bounds) : XBrushes.LightGray);
        Properties.Add(StepperProperties.ButtonCornerRadius, 3 * scaleFactor);
        Properties.Add(StepperProperties.ButtonSize, new XSize(bounds.Width * 0.4, bounds.Height * 0.8));
        Properties.Add(StepperProperties.ButtonSpace, 0.04 * bounds.Width);

        Properties.Add(ViewProperties.TextColor, XBrushes.White);
        Properties.Add(ViewProperties.BackgroundColor, XBrushes.Transparent);
    }

    private static class StepperProperties
    {
        public const string ButtonBackgroundColor = nameof(ButtonBackgroundColor);
        public const string ButtonCornerRadius = nameof(ButtonCornerRadius);
        public const string ButtonSize = nameof(ButtonSize);
        public const string ButtonSpace = nameof(ButtonSpace);
    }
}