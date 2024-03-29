namespace PdfSharp.Maui.Renderers.Controls;

[PdfRenderer(ViewType = typeof(ImageButton))]
public class PdfImageButtonRenderer : PdfRendererBase<ImageButton>
{
    protected override void CreateLayout(XGraphics page, ImageButton view, XRect bounds, double scaleFactor)
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
                    GetProperty<double>(ViewProperties.BorderThickness)), bounds,
                new XSize(cornerRadius, cornerRadius));
        }

        var xImage = view.Source.ToXImage();

        var desiredBounds = bounds;
        var imageAspectRatio = xImage.Size.Width / xImage.Size.Height;
        var boundsAspectRatio = bounds.Width / bounds.Height;
        if (boundsAspectRatio > imageAspectRatio)
        {
            desiredBounds.Width = bounds.Height;
        }
        else
        {
            desiredBounds.Height = bounds.Width;
        }

        var centeredBounds = new XRect(bounds.X + ((bounds.Width - desiredBounds.Width) / 2),
            bounds.Y + ((bounds.Height - desiredBounds.Height) / 2), desiredBounds.Width, desiredBounds.Height);
        page.DrawImage(xImage, centeredBounds);
    }

    protected override void CreateUniformLayoutParameters(XGraphics page, ImageButton view, XRect bounds,
        double scaleFactor)
    {
        Properties.Add(ViewProperties.BackgroundColor, XBrushes.Gray);
        Properties.Add(ViewProperties.CornerRadius, 4 * scaleFactor);
    }

    protected override void CreateAndroidLayoutParameters(XGraphics page, ImageButton view, XRect bounds,
        double scaleFactor)
    {
        Properties.Add(ViewProperties.BackgroundColor, view.GetBackgroundBrush(bounds));
        Properties.Add(ViewProperties.BorderColor, view.BorderColor.ToXColor());
        Properties.Add(ViewProperties.BorderThickness, view.BorderWidth * scaleFactor);
        Properties.Add(ViewProperties.CornerRadius, view.CornerRadius * scaleFactor);
    }

    protected override void CreateIosLayoutParameters(XGraphics page, ImageButton view, XRect bounds,
        double scaleFactor)
    {
        Properties.Add(ViewProperties.BackgroundColor, view.GetBackgroundBrush(bounds));
        Properties.Add(ViewProperties.BorderColor, view.BorderColor.ToXColor());
        Properties.Add(ViewProperties.BorderThickness, view.BorderWidth * scaleFactor);
        Properties.Add(ViewProperties.CornerRadius, view.CornerRadius * scaleFactor);
    }

    protected override void CreateWindowsLayoutParameters(XGraphics page, ImageButton view, XRect bounds,
        double scaleFactor)
    {
        Properties.Add(ViewProperties.BackgroundColor, view.GetBackgroundBrush(bounds));
        Properties.Add(ViewProperties.BorderColor, view.BorderColor.ToXColor());
        Properties.Add(ViewProperties.BorderThickness, view.BorderWidth * scaleFactor);
        Properties.Add(ViewProperties.CornerRadius, view.CornerRadius * scaleFactor);
    }
}
