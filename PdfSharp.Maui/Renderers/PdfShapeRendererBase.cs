using Microsoft.Maui.Controls.Shapes;

namespace PdfSharp.Maui.Renderers;

public abstract class PdfShapeRendererBase<T> : PdfRendererBase<T> where T : Shape
{
    protected static XGraphicsPath GetPath(PathF path, XRect controlBounds, XRect bounds)
    {
        var gPath = new XGraphicsPath();
        for (var i = 0; i < path.Count - 1; i++)
        {
            var p1 = TransformPoint(path[i].ToXPoint(), controlBounds, bounds);
            var p2 = TransformPoint(path[i + 1].ToXPoint(), controlBounds, bounds);
            gPath.AddLine(p1, p2);
        }

        if (path.Closed)
        {
            var pFirst = TransformPoint(path[0].ToXPoint(), controlBounds, bounds);
            var pLast = TransformPoint(path[^1].ToXPoint(), controlBounds, bounds);

            gPath.AddLine(pFirst, pLast);
        }

        return gPath;
    }

    protected static XPoint TransformPoint(XPoint point, XRect bound, XRect transformBounds)
    {
        var xRatio = point.X == 0 ? 0 : (point.X - bound.X) / bound.Width;
        var yRatio = point.Y == 0 ? 0 : (point.Y - bound.Y) / bound.Height;

        return new XPoint(transformBounds.X + transformBounds.Width * xRatio,
            transformBounds.Y + transformBounds.Height * yRatio);
    }
}