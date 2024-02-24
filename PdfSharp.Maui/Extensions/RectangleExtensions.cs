namespace PdfSharp.Maui.Extensions;

using Microsoft.Maui.Controls.Shapes;

public static class RectangleExtensions
{
    /// <summary>
    /// Creates XRect from <c>Rectangle</c>
    /// </summary>
    /// <param name="rect"></param>
    /// <returns></returns>
    public static XRect ToXRect(this Rectangle rect)
    {
        return new XRect(rect.X, rect.Y, rect.Width, rect.Height);
    }

    /// <summary>
    /// Creates new XRect with specified margin
    /// </summary>
    /// <param name="rect"></param>
    /// <param name="thickness"></param>
    /// <returns></returns>
    public static XRect WithMargin(this XRect rect, Thickness thickness)
    {
        return new XRect(rect.X + thickness.Left, rect.Y + thickness.Top, rect.Width - thickness.HorizontalThickness,
            rect.Height - thickness.VerticalThickness);
    }

    /// <summary>
    /// Creates new XRect from <c>Rect</c>
    /// </summary>
    /// <param name="rect"></param>
    /// <returns></returns>
    public static XRect ToXRect(this Rect rect)
    {
        return new XRect(rect.X, rect.Y, rect.Width, rect.Height);
    }
}
