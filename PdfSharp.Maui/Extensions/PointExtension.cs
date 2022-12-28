namespace PdfSharp.Maui.Extensions;

public static class PointExtension
{
    /// <summary>
    /// Creates <see cref="XPoint"/> from Point 
    /// </summary>
    /// <param name="point">point</param>
    /// <returns>XPoint</returns>
    public static XPoint ToXPoint(this Point point)
    {
        return new XPoint(point.X, point.Y);
    }


    /// <summary>
    /// Creates <see cref="XPoint"/> from Point 
    /// </summary>
    /// <param name="point">point</param>
    /// <returns>XPoint</returns>
    public static XPoint ToXPoint(this PointF point)
    {
        return new XPoint(point.X, point.Y);
    }

    /// <summary>
    /// Creates <see cref="XPoint"/> from PointF by multiplying with scale factors 
    /// </summary>
    /// <param name="point">point</param>
    /// <param name="xScale">Scale factor for x axis</param>
    /// <param name="yScale">Scale factor for y axis</param>
    /// <returns>XPoint</returns>
    public static XPoint ToXPoint(this PointF point, double xScale, double yScale)
    {
        return new XPoint(point.X * xScale, point.Y * yScale);
    }

    /// <summary>
    /// Creates <see cref="XPoint"/> from Point by adding given margin
    /// </summary>
    /// <param name="point">point</param>
    /// <param name="margin">Value to add for both X and Y axis</param>
    /// <returns>XPoint</returns>
    public static XPoint WithMargin(this XPoint point, double margin)
    {
        return new XPoint(point.X + margin, point.Y + margin);
    }

    /// <summary>
    /// Creates <see cref="XPoint"/> from Point 
    /// </summary>
    /// <param name="point">point</param>
    /// <param name="xMargin">Value to add to X axis</param>
    /// <param name="yMargin">Value to add to Y axis</param>
    /// <returns>XPoint</returns>
    public static XPoint WithMargin(this XPoint point, double xMargin, double yMargin)
    {
        return new XPoint(point.X + xMargin, point.Y + yMargin);
    }

    /// <summary>
    /// Creates <see cref="XPoint"/> by projecting given point in given rectangle 
    /// </summary>
    /// <param name="point">point</param>
    /// <param name="rect">Rectangle to project</param>
    /// <returns>XPoint</returns>
    public static XPoint ProjectFromXRect(this Point point, XRect rect)
    {
        return new XPoint(rect.X + rect.Width * point.X, rect.Y + rect.Height * point.Y);
    }
}