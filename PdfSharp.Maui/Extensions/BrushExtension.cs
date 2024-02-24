namespace PdfSharp.Maui.Extensions;

public static class BrushExtension
{
    /// <summary>
    ///     Creates a solid XBrush from a Maui Brush
    /// </summary>
    /// <param name="brush"></param>
    /// <returns>XSolidBrush</returns>
    public static XBrush ToXBrush(this Brush brush)
    {
        return brush switch
        {
            LinearGradientBrush gradientBrush when gradientBrush.GradientStops.Any() => new XLinearGradientBrush(
                gradientBrush.StartPoint.ToXPoint(), gradientBrush.EndPoint.ToXPoint(),
                gradientBrush.GradientStops.First().Color.ToXColor(),
                gradientBrush.GradientStops.Last().Color.ToXColor()),
            SolidColorBrush solidColorBrush => new XSolidBrush(solidColorBrush.Color.ToXColor()),
            _ => new XSolidBrush(XColors.Transparent)
        };
    }

    /// <summary>
    ///     Creates a XPen from a Brush with a given width
    /// </summary>
    /// <param name="brush">Color</param>
    /// <param name="with">Pen Width</param>
    /// <returns>XPen</returns>
    public static XPen ToXPen(this Brush brush, double with)
    {
        var color = brush switch
        {
            LinearGradientBrush gradientBrush when gradientBrush.GradientStops.Any() => gradientBrush.GradientStops
                .First()
                .Color.ToXColor(),
            SolidColorBrush solidColorBrush => solidColorBrush.Color.ToXColor(),
            _ => XColors.Black
        };

        return new XPen(color, with);
    }

    /// <summary>
    ///     Creates a XColor from Maui Color
    /// </summary>
    /// <param name="brush"></param>
    /// <returns>XColor</returns>
    public static XColor ToXColor(this Brush brush)
    {
        var color = brush switch
        {
            LinearGradientBrush gradientBrush when gradientBrush.GradientStops.Any() => gradientBrush.GradientStops
                .First()
                .Color.ToXColor(),
            SolidColorBrush solidColorBrush => solidColorBrush.Color.ToXColor(),
            _ => XColors.Black
        };

        return color;
    }

    /// <summary>
    /// Checks whether Brush has a color value
    /// It does not support RadialGradientBrush
    /// </summary>
    /// <param name="brush"></param>
    /// <returns>Brush has any color rather than transparent</returns>
    public static bool IsDefault(this XBrush brush)
    {
        return brush switch
        {
            null => true,
            XSolidBrush solidBrush => solidBrush.Color == XColors.Transparent,
            XLinearGradientBrush => false,
            _ => true
        };
    }

    /// <summary>
    /// Checks whether Brush has a color value
    /// It does not support RadialGradientBrush
    /// </summary>
    /// <param name="brush"></param>
    /// <returns>Brush has any color rather than transparent</returns>
    public static bool IsNotDefault(this XBrush brush)
    {
        return !brush.IsDefault();
    }
}
