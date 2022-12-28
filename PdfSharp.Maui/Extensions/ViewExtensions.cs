namespace PdfSharp.Maui.Extensions;

public static class ViewExtensions
{
    /// <summary>
    /// Checks if View has any Background
    /// </summary>
    /// <param name="view">Maui.Controls.View</param>
    /// <returns></returns>
    public static bool HasBackground(this View view)
    {
        return view.BackgroundColor.IsNotDefault() || !view.Background.IsEmpty;
    }

    /// <summary>
    /// Gets background brush from a view whether if its Gradient brush or solid color
    /// SolidColorBrush and LinearGradientBrush are supported,RadialGradientBrush is not supported.
    /// </summary>
    /// <param name="view">View to get background</param>
    /// <param name="bounds">Bound in pdf to calculate linear gradient</param>
    /// <returns>XBrush</returns>
    public static XBrush GetBackgroundBrush(this View view, XRect bounds)
    {
        if (view.Background is { IsEmpty: false })
        {
            return view.Background switch
            {
                LinearGradientBrush gradientBrush when gradientBrush.GradientStops.Any() => new XLinearGradientBrush(
                    gradientBrush.StartPoint.ProjectFromXRect(bounds),
                    gradientBrush.EndPoint.ProjectFromXRect(bounds),
                    gradientBrush.GradientStops.First().Color.ToXColor(),
                    gradientBrush.GradientStops.Last().Color.ToXColor()),
                SolidColorBrush solidColorBrush => new XSolidBrush(solidColorBrush.Color.ToXColor()),
                _ => new XSolidBrush(XColors.Transparent)
            };
        }

        if (view.BackgroundColor != null && view.BackgroundColor.IsNotDefault())
        {
            return view.BackgroundColor.ToXBrush();
        }

        return XBrushes.Transparent;
    }

    /// <summary>
    /// Gets background brush from a page whether if its Gradient brush or solid color
    /// SolidColorBrush and LinearGradientBrush are supported,RadialGradientBrush is not supported.
    /// </summary>
    /// <param name="page">Page to get background</param>
    /// <returns>XBrush</returns>
    public static XBrush GetBackgroundBrush(this Page page)
    {
        return !page.Background.IsEmpty ? page.Background.ToXBrush() : page.BackgroundColor.ToXBrush();
    }
}