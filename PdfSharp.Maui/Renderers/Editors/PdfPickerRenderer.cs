using PdfSharp.Maui.Attributes;

namespace PdfSharp.Maui.Renderers.Editors;

[PdfRenderer(ViewType = typeof(Picker))]
public class PdfPickerRenderer : PdfRendererBase<Picker>
{
    protected override void CreateLayout(XGraphics page, Picker view, XRect bounds, double scaleFactor)
    {
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

        if (view.SelectedItem != null)
        {
            var font = new XFont(GlobalFontSettings.DefaultFontName, 14 * scaleFactor);
            page.DrawString(view.SelectedItem.ToString(), font, GetProperty<XBrush>(ViewProperties.TextColor), bounds,
                new XStringFormat
                {
                    Alignment = XStringAlignment.Center,
                    LineAlignment = XLineAlignment.Center
                });
        }
    }

    protected override void CreateUniformLayoutParameters(XGraphics page, Picker view, XRect bounds, double scaleFactor)
    {
        Properties.Add(ViewProperties.BackgroundColor, XBrushes.White);
        Properties.Add(ViewProperties.BorderColor, XColors.Black);
        Properties.Add(ViewProperties.TextColor, XBrushes.Black);
        Properties.Add(ViewProperties.BorderLocation, bounds);
        Properties.Add(ViewProperties.BorderThickness, scaleFactor);
        Properties.Add(ViewProperties.CornerRadius, 2 * scaleFactor);
    }

    protected override void CreateAndroidLayoutParameters(XGraphics page, Picker view, XRect bounds, double scaleFactor)
    {
        Properties.Add(ViewProperties.BackgroundColor, view.GetBackgroundBrush(bounds));
        Properties.Add(ViewProperties.TextColor, view.TextColor.ToXBrush());
        Properties.Add(ViewProperties.CornerRadius, 0.0);
    }

    protected override void CreateIosLayoutParameters(XGraphics page, Picker view, XRect bounds, double scaleFactor)
    {
        Properties.Add(ViewProperties.BackgroundColor, view.GetBackgroundBrush(bounds));
        Properties.Add(ViewProperties.TextColor, view.TextColor.ToXBrush());
        Properties.Add(ViewProperties.CornerRadius, 3 * scaleFactor);
    }

    protected override void CreateWindowsLayoutParameters(XGraphics page, Picker view, XRect bounds, double scaleFactor)
    {
        Properties.Add(ViewProperties.BackgroundColor, view.GetBackgroundBrush(bounds));
        Properties.Add(ViewProperties.TextColor, view.TextColor.ToXBrush());
        Properties.Add(ViewProperties.CornerRadius, 2 * scaleFactor);
    }
}