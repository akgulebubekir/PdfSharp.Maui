namespace PdfSharp.Maui.Renderers.Layouts;

[PdfRenderer(ViewType = typeof(Frame))]
public class PdfFrameRenderer : PdfRendererBase<Frame>
{
    protected override void CreateLayout(XGraphics page, Frame view, XRect bounds, double scaleFactor)
    {
        var cornerRadius = GetProperty<double>(ViewProperties.CornerRadius);

        if (HasBackgroundColor())
        {
            page.DrawRoundedRectangle(GetProperty<XBrush>(ViewProperties.BackgroundColor), bounds,
                new XSize(cornerRadius, cornerRadius));
        }

        page.DrawRoundedRectangle(new XPen(GetProperty<XColor>(ViewProperties.BorderColor),
            GetProperty<double>(ViewProperties.BorderThickness)), bounds, new XSize(cornerRadius, cornerRadius));
    }

    protected override void CreateUniformLayoutParameters(XGraphics page, Frame view, XRect bounds, double scaleFactor)
    {
        Properties.Add(ViewProperties.BackgroundColor, XBrushes.White);
        Properties.Add(ViewProperties.BorderColor, XColors.Gray);
        Properties.Add(ViewProperties.BorderThickness, 2 * scaleFactor);
        Properties.Add(ViewProperties.CornerRadius, 4 * scaleFactor);
    }

    protected override void CreateAndroidLayoutParameters(XGraphics page, Frame view, XRect bounds, double scaleFactor)
    {
        Properties.Add(ViewProperties.BackgroundColor, view.GetBackgroundBrush(bounds));
        Properties.Add(ViewProperties.BorderColor, view.BorderColor.ToXColor());
        Properties.Add(ViewProperties.BorderThickness, 2 * scaleFactor);
        Properties.Add(ViewProperties.CornerRadius, view.CornerRadius * scaleFactor);
    }

    protected override void CreateIosLayoutParameters(XGraphics page, Frame view, XRect bounds, double scaleFactor)
    {
        Properties.Add(ViewProperties.BackgroundColor, view.GetBackgroundBrush(bounds));
        Properties.Add(ViewProperties.BorderColor, view.BorderColor.ToXColor());
        Properties.Add(ViewProperties.BorderThickness, 2 * scaleFactor);
        Properties.Add(ViewProperties.CornerRadius, view.CornerRadius * scaleFactor);
    }

    protected override void CreateWindowsLayoutParameters(XGraphics page, Frame view, XRect bounds, double scaleFactor)
    {
        Properties.Add(ViewProperties.BackgroundColor, view.GetBackgroundBrush(bounds));
        Properties.Add(ViewProperties.BorderColor, view.BorderColor.ToXColor());
        Properties.Add(ViewProperties.BorderThickness, 2 * scaleFactor);
        Properties.Add(ViewProperties.CornerRadius, view.CornerRadius * scaleFactor);
    }
}
