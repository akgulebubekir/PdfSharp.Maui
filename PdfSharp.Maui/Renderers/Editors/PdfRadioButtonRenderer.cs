namespace PdfSharp.Maui.Renderers.Editors;

[PdfRenderer(ViewType = typeof(RadioButton))]
public class PdfRadioButtonRenderer : PdfRendererBase<RadioButton>
{
    public static readonly XStringFormat DefaultTextFormat = new()
    {
        LineAlignment = XLineAlignment.Center,
        Alignment = XStringAlignment.Near
    };

    protected override void CreateLayout(XGraphics page, RadioButton view, XRect bounds,
        double scaleFactor)
    {
        var font = new XFont(view.FontFamily ?? GlobalFontSettings.DefaultFontName,
            view.FontSize * scaleFactor);
        var cornerRadius = GetProperty<double>(ViewProperties.CornerRadius);

        if (HasProperty(ViewProperties.BackgroundColor))
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

        var outerRadius = GetProperty<double>(RadioButtonProperties.OuterCircleRadius);
        page.DrawEllipse(
            new XPen(GetProperty<XColor>(RadioButtonProperties.OuterCircleColor),
                GetProperty<double>(RadioButtonProperties.OuterCircleThickness)),
            bounds.X + (4 * scaleFactor), bounds.Center.Y - outerRadius, 2 * outerRadius, 2 * outerRadius);

        if (HasProperty(RadioButtonProperties.InnerCircleColor))
        {
            var innerRadius = GetProperty<double>(RadioButtonProperties.InnerCircleRadius);
            page.DrawEllipse(new XSolidBrush(GetProperty<XColor>(RadioButtonProperties.InnerCircleColor)),
                bounds.X + (4 * scaleFactor) + outerRadius - innerRadius, bounds.Center.Y - innerRadius, 2 * innerRadius,
                2 * innerRadius);
        }

        if (view.Content is string text)
        {
            page.DrawString(text, font, GetProperty<XBrush>(ViewProperties.TextColor),
                new XRect(new XPoint(bounds.X + bounds.Height, bounds.Y),
                    new XSize(bounds.Width - (1.5 * bounds.Height), bounds.Height)), DefaultTextFormat);
        }
    }


    protected override void CreateUniformLayoutParameters(XGraphics page, RadioButton view, XRect bounds,
        double scaleFactor)
    {
        Properties.Add(ViewProperties.BorderColor, XColors.White);
        Properties.Add(ViewProperties.BorderThickness, 0.0);
        Properties.Add(ViewProperties.BackgroundColor, XBrushes.White);
        Properties.Add(ViewProperties.TextColor, XBrushes.Black);
        Properties.Add(ViewProperties.CornerRadius, 0.0);
        if (view.IsChecked)
        {
            Properties.Add(RadioButtonProperties.OuterCircleColor, XColors.Black);
            Properties.Add(RadioButtonProperties.OuterCircleRadius, bounds.Height * 0.37);
            Properties.Add(RadioButtonProperties.OuterCircleThickness, 2 * scaleFactor);
            Properties.Add(RadioButtonProperties.InnerCircleColor, XColors.Black);
            Properties.Add(RadioButtonProperties.InnerCircleRadius, bounds.Height * 0.2);
        }
        else
        {
            Properties.Add(RadioButtonProperties.OuterCircleColor, XColors.Black);
            Properties.Add(RadioButtonProperties.OuterCircleRadius, bounds.Height * 0.37);
            Properties.Add(RadioButtonProperties.OuterCircleThickness, 2 * scaleFactor);
        }
    }


    protected override void CreateAndroidLayoutParameters(XGraphics page, RadioButton view, XRect bounds,
        double scaleFactor)
    {
        Properties.Add(ViewProperties.BorderColor, view.BorderColor.ToXColor());
        Properties.Add(ViewProperties.BorderThickness, scaleFactor * view.BorderWidth);
        Properties.Add(ViewProperties.BackgroundColor, view.GetBackgroundBrush(bounds));
        Properties.Add(ViewProperties.TextColor, view.TextColor.ToXBrush());
        Properties.Add(ViewProperties.CornerRadius, 0.0);
        if (view.IsChecked)
        {
            Properties.Add(RadioButtonProperties.OuterCircleColor, XColor.FromArgb(32, 17, 83));
            Properties.Add(RadioButtonProperties.OuterCircleRadius, bounds.Height * 0.37);
            Properties.Add(RadioButtonProperties.OuterCircleThickness, 2 * scaleFactor);
            Properties.Add(RadioButtonProperties.InnerCircleColor, XColor.FromArgb(32, 17, 83));
            Properties.Add(RadioButtonProperties.InnerCircleRadius, bounds.Height * 0.2);
        }
        else
        {
            Properties.Add(RadioButtonProperties.OuterCircleColor, XColors.Black);
            Properties.Add(RadioButtonProperties.OuterCircleRadius, bounds.Height * 0.37);
            Properties.Add(RadioButtonProperties.OuterCircleThickness, scaleFactor);
        }
    }

    protected override void CreateIosLayoutParameters(XGraphics page, RadioButton view, XRect bounds,
        double scaleFactor)
    {
        Properties.Add(ViewProperties.BorderColor, view.BorderColor.ToXColor());
        Properties.Add(ViewProperties.BorderThickness, scaleFactor * view.BorderWidth);
        Properties.Add(ViewProperties.BackgroundColor, view.GetBackgroundBrush(bounds));
        Properties.Add(ViewProperties.TextColor, view.TextColor.ToXBrush());
        Properties.Add(ViewProperties.CornerRadius, 2 * scaleFactor);
        if (view.IsChecked)
        {
            Properties.Add(RadioButtonProperties.OuterCircleColor, XColor.FromArgb(32, 17, 83));
            Properties.Add(RadioButtonProperties.OuterCircleRadius, bounds.Height * 0.37);
            Properties.Add(RadioButtonProperties.OuterCircleThickness, 2 * scaleFactor);
            Properties.Add(RadioButtonProperties.InnerCircleColor, XColor.FromArgb(32, 17, 83));
            Properties.Add(RadioButtonProperties.InnerCircleRadius, bounds.Height * 0.2);
        }
        else
        {
            Properties.Add(RadioButtonProperties.OuterCircleColor, XColors.Black);
            Properties.Add(RadioButtonProperties.OuterCircleRadius, bounds.Height * 0.37);
            Properties.Add(RadioButtonProperties.OuterCircleThickness, scaleFactor);
        }
    }

    protected override void CreateWindowsLayoutParameters(XGraphics page, RadioButton view, XRect bounds,
        double scaleFactor)
    {
        Properties.Add(ViewProperties.BorderColor, view.BorderColor.ToXColor());
        Properties.Add(ViewProperties.BorderThickness, scaleFactor * view.BorderWidth);
        Properties.Add(ViewProperties.BackgroundColor, view.GetBackgroundBrush(bounds));
        Properties.Add(ViewProperties.TextColor, view.TextColor.ToXBrush());
        Properties.Add(ViewProperties.CornerRadius, scaleFactor);

        if (view.IsChecked)
        {
            Properties.Add(RadioButtonProperties.OuterCircleColor, XColor.FromArgb(53, 219, 217));
            Properties.Add(RadioButtonProperties.OuterCircleRadius, bounds.Height * 0.25);
            Properties.Add(RadioButtonProperties.OuterCircleThickness, bounds.Height * 0.2);
            Properties.Add(RadioButtonProperties.InnerCircleColor, XColors.Black);
            Properties.Add(RadioButtonProperties.InnerCircleRadius, bounds.Height * 0.2);
        }
        else
        {
            Properties.Add(RadioButtonProperties.OuterCircleColor, XColors.LightGray);
            Properties.Add(RadioButtonProperties.OuterCircleRadius, bounds.Height * 0.32);
            Properties.Add(RadioButtonProperties.OuterCircleThickness, scaleFactor);
        }
    }

    private static class RadioButtonProperties
    {
        public const string OuterCircleColor = nameof(OuterCircleColor);
        public const string InnerCircleColor = nameof(InnerCircleColor);
        public const string OuterCircleRadius = nameof(OuterCircleRadius);
        public const string InnerCircleRadius = nameof(InnerCircleRadius);
        public const string OuterCircleThickness = nameof(OuterCircleThickness);
    }
}
