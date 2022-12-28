namespace PdfSharp.Maui.Extensions;

public static class ColorExtension
{
    /// <summary>
    ///     Creates a XColor from  Maui color
    /// </summary>
    /// <param name="color"></param>
    /// <returns>XColor</returns>
    public static XColor ToXColor(this Color color)
    {
        if (color == default)
        {
            return XColors.Transparent;
        }

        return XColor.FromArgb(
            (int)(color.Alpha * 255),
            (int)(color.Red * 255),
            (int)(color.Green * 255),
            (int)(color.Blue * 255));
    }

    /// <summary>
    ///     Creates a solid XBrush from Maui color
    /// </summary>
    /// <param name="color"></param>
    /// <returns>XSolidBrush</returns>
    public static XBrush ToXBrush(this Color color)
    {
        return new XSolidBrush(color.ToXColor());
    }
}