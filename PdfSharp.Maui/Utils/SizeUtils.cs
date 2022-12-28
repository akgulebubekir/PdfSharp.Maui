using ArgumentOutOfRangeException = System.ArgumentOutOfRangeException;

namespace PdfSharp.Maui.Utils;

public class SizeUtils
{
    /// <summary>
    ///     Calculates page size in point
    /// </summary>
    /// <param name="pageSize"> Predefined page sizes</param>
    /// <param name="orientation">Page orientation</param>
    /// <returns>Page size in point.</returns>
    /// <exception cref="System.ArgumentOutOfRangeException"></exception>
    public static XSize GetPageSize(PageSize pageSize, PageOrientation orientation = PageOrientation.Portrait)
    {
        var size = PageSizeConverter.ToSize(pageSize);

        return orientation switch
        {
            PageOrientation.Portrait => size,
            PageOrientation.Landscape => new XSize(size.Height, size.Width),
            _ => throw new ArgumentOutOfRangeException(nameof(orientation), orientation, null)
        };
    }

    /// <summary>
    ///     Calculates amount of page that can be written in point
    /// </summary>
    /// <param name="pageSize">Predefined page size</param>
    /// <param name="orientation">Page orientation</param>
    /// <returns>Amount of page that can be written in point</returns>
    public static XRect GetAvailablePageSize(PageSize pageSize, PageOrientation orientation)
    {
        var size = GetPageSize(pageSize);
        if (orientation == PageOrientation.Landscape)
        {
            size = new XSize(size.Height, size.Width);
        }

        var margin = (size.Width + size.Height) * 0.04;
        return new XRect(margin, margin, size.Width - 2 * margin, size.Height - margin);
    }
}