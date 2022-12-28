using PdfSharp.Maui.Attributes;

namespace PdfSharp.Maui.Renderers.Editors;

[PdfRenderer(ViewType = typeof(CheckBox))]
public class PdfCheckBoxRenderer : PdfRendererBase<CheckBox>
{
    protected override void CreateLayout(XGraphics page, CheckBox view, XRect bounds, double scaleFactor)
    {
        var checkPath = new[]
        {
            new XPoint(bounds.X + bounds.Width * 0.2, bounds.Center.Y + bounds.Height * 0.1),
            new XPoint(bounds.Center.X - bounds.Width * 0.1, bounds.Center.Y + bounds.Height * 0.25),
            new XPoint(bounds.X + bounds.Width * 0.75, bounds.Y + bounds.Height * 0.25)
        };

        if (GetProperty<CheckboxShape>(CheckBoxProperties.Shape) == CheckboxShape.Circle)
        {
            var circleBounds = new XRect(bounds.X + bounds.Height * 0.1, bounds.Y + bounds.Height * 0.1,
                bounds.Height * 0.8, bounds.Height * 0.8);
            page.DrawEllipse(GetProperty<XBrush>(ViewProperties.BackgroundColor), circleBounds);
            page.DrawEllipse(
                new XPen(GetProperty<XColor>(ViewProperties.BorderColor),
                    GetProperty<double>(ViewProperties.BorderThickness)), circleBounds);
        }
        else
        {
            var cornerRadius = GetProperty<double>(ViewProperties.CornerRadius);
            page.DrawRoundedRectangle(GetProperty<XBrush>(ViewProperties.BackgroundColor), bounds,
                new XSize(cornerRadius, cornerRadius));
            page.DrawRoundedRectangle(
                new XPen(GetProperty<XColor>(ViewProperties.BorderColor),
                    GetProperty<double>(ViewProperties.BorderThickness)),
                bounds, new XSize(cornerRadius, cornerRadius));
        }

        if (view.IsChecked)
        {
            page.DrawLines(new XPen(GetProperty<XColor>(ViewProperties.TickColor), 3 * scaleFactor), checkPath);
        }
    }

    protected override void CreateUniformLayoutParameters(XGraphics page, CheckBox view, XRect bounds,
        double scaleFactor)
    {
        Properties.Add(CheckBoxProperties.Shape, CheckboxShape.Square);
        Properties.Add(ViewProperties.BorderColor, XColors.Black);
        Properties.Add(ViewProperties.BackgroundColor, XBrushes.White);
        Properties.Add(ViewProperties.BorderThickness, 2 * scaleFactor);
        Properties.Add(ViewProperties.TickColor, XColors.Black);
        Properties.Add(ViewProperties.CornerRadius, 2 * scaleFactor);
    }

    protected override void CreateAndroidLayoutParameters(XGraphics page, CheckBox view, XRect bounds,
        double scaleFactor)
    {
        Properties.Add(CheckBoxProperties.Shape, CheckboxShape.Square);
        Properties.Add(ViewProperties.BorderColor,
            view.Color.IsDefault() ? XColor.FromArgb(32, 17, 83) : view.Color.ToXColor());
        Properties.Add(ViewProperties.BackgroundColor,
            new XSolidBrush(view.IsChecked ? GetProperty<XColor>(ViewProperties.BorderColor) : XColors.Transparent));
        Properties.Add(ViewProperties.BorderThickness, 2 * scaleFactor);
        Properties.Add(ViewProperties.TickColor,
            Application.Current?.UserAppTheme == AppTheme.Dark ? XColors.Black : XColors.White);
        Properties.Add(ViewProperties.CornerRadius, scaleFactor);
    }

    protected override void CreateIosLayoutParameters(XGraphics page, CheckBox view, XRect bounds, double scaleFactor)
    {
        Properties.Add(CheckBoxProperties.Shape, CheckboxShape.Circle);
        Properties.Add(ViewProperties.BorderColor,
            view.Color.IsDefault() ? XColor.FromArgb(32, 17, 83) : view.Color.ToXColor());
        Properties.Add(ViewProperties.BackgroundColor,
            new XSolidBrush(view.IsChecked ? GetProperty<XColor>(ViewProperties.BorderColor) : XColors.Transparent));
        Properties.Add(ViewProperties.BorderThickness, 2 * scaleFactor);
        Properties.Add(ViewProperties.TickColor,
            Application.Current?.UserAppTheme == AppTheme.Dark ? XColors.White : XColors.Black);
        Properties.Add(ViewProperties.CornerRadius, scaleFactor);
    }

    protected override void CreateWindowsLayoutParameters(XGraphics page, CheckBox view, XRect bounds,
        double scaleFactor)
    {
        Properties.Add(CheckBoxProperties.Shape, CheckboxShape.Square);
        Properties.Add(ViewProperties.BackgroundColor, view.IsChecked ? view.Color.ToXBrush() : XBrushes.Transparent);
        Properties.Add(ViewProperties.BorderColor, view.IsChecked ? view.Color.ToXColor() : XColors.LightGray);
        Properties.Add(ViewProperties.BorderThickness, 2 * scaleFactor);
        Properties.Add(ViewProperties.TickColor,
            Application.Current?.UserAppTheme == AppTheme.Dark ? XColors.Black : XColors.White);
        Properties.Add(ViewProperties.CornerRadius, 3 * scaleFactor);
    }


    private static class CheckBoxProperties
    {
        public const string Shape = nameof(Shape);
    }

    private enum CheckboxShape
    {
        Square,
        Circle
    }
}