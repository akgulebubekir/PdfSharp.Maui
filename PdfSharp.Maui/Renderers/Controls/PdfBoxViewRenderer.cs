using PdfSharp.Maui.Attributes;

namespace PdfSharp.Maui.Renderers.Controls;

[PdfRenderer(ViewType = typeof(BoxView))]
public class PdfBoxViewRenderer : PdfRendererBase<BoxView>
{
    protected override void CreateLayout(XGraphics page, BoxView view, XRect bounds, double scaleFactor)
    {
        var background = view.GetBackgroundBrush(bounds);
        if (background.IsNotDefault())
        {
            page.DrawRectangle(view.GetBackgroundBrush(bounds), bounds);
        }

        page.DrawPath(view.Color.ToXBrush(), ConstructPathWithCornerRadius(bounds, view.CornerRadius, scaleFactor));
    }

    private static XGraphicsPath ConstructPathWithCornerRadius(XRect pageBounds, CornerRadius radius,
        double scaleFactor)
    {
        var p11 = new XPoint(pageBounds.X, pageBounds.Top + radius.TopLeft * scaleFactor);
        var p12 = new XPoint(pageBounds.X + radius.TopLeft * scaleFactor, pageBounds.Top);
        var p21 = new XPoint(pageBounds.Right - radius.TopRight * scaleFactor, pageBounds.Top);
        var p22 = new XPoint(pageBounds.Right, pageBounds.Top + radius.TopRight * scaleFactor);
        var p31 = new XPoint(pageBounds.Right, pageBounds.Bottom - radius.BottomRight * scaleFactor);
        var p32 = new XPoint(pageBounds.Right - radius.BottomRight * scaleFactor, pageBounds.Bottom);
        var p41 = new XPoint(pageBounds.Left + radius.BottomLeft * scaleFactor, pageBounds.Bottom);
        var p42 = new XPoint(pageBounds.Left, pageBounds.Bottom - radius.BottomLeft * scaleFactor);
        var path = new XGraphicsPath();

        if (radius.TopLeft > 0)
        {
            path.AddArc(p11, p12, new XSize(radius.TopLeft * scaleFactor, radius.TopLeft * scaleFactor), 90, false,
                XSweepDirection.Clockwise);
        }

        path.AddLine(p12, p21);
        if (radius.TopRight > 0)
        {
            path.AddArc(p21, p22, new XSize(radius.TopRight * scaleFactor, radius.TopRight * scaleFactor), 90, false,
                XSweepDirection.Clockwise);
        }

        path.AddLine(p22, p31);

        if (radius.BottomRight > 0)
        {
            path.AddArc(p31, p32, new XSize(radius.BottomRight * scaleFactor, radius.BottomRight * scaleFactor), 90,
                false,
                XSweepDirection.Clockwise);
        }

        path.AddLine(p32, p41);
        if (radius.BottomLeft > 0)
        {
            path.AddArc(p41, p42, new XSize(radius.BottomLeft * scaleFactor, radius.BottomLeft * scaleFactor), 90,
                false,
                XSweepDirection.Clockwise);
        }

        path.AddLine(p42, p11);

        return path;
    }
}