using PdfSharp.Drawing;
using PdfSharp.Fonts;
using PdfSharp.Maui.Extensions;
using PdfSharp.Maui.Sample.Models;

namespace PdfSharp.Maui.Sample;

internal class CustomListViewRendererDelegate : Delegates.PdfListViewRendererDelegate
{
    private readonly XBrush _defaultTextColor =
        Application.Current?.RequestedTheme == AppTheme.Dark ? XBrushes.White : XBrushes.Black;

    public override void DrawCell(ListView view, int section, int row, XGraphics page, XRect bounds,
        double scaleFactor)
    {
        var boldFont = new XFont(GlobalFontSettings.DefaultFontName, 14 * scaleFactor, XFontStyleEx.Bold);
        var italicFont = new XFont(GlobalFontSettings.DefaultFontName, 14 * scaleFactor, XFontStyleEx.Italic);
        var itemSource = view.ItemsSource as IEnumerable<IGrouping<string, City>>;
        var item = itemSource.ElementAt(section).ElementAt(row);

        page.DrawString(item.Name, boldFont, _defaultTextColor,
            bounds.WithMargin(new Thickness(bounds.Width * 0.05, 0, 0, bounds.Height / 2)), XStringFormats.CenterLeft);
        page.DrawString(item.Country, italicFont, _defaultTextColor,
            bounds.WithMargin(new Thickness(bounds.Width * 0.05, bounds.Height / 2, 0, 0)), XStringFormats.CenterLeft);
    }

    public override int GetNumberOfSections(ListView view)
    {
        return ((IEnumerable<IGrouping<string, City>>)view.ItemsSource).Count();
    }

    public override double GetTotalHeight(ListView listView)
    {
        return listView.Bounds.Height;
    }

    public override void DrawGroupHeader(ListView view, int section, XGraphics page, XRect bounds, double scaleFactor)
    {
        var font = new XFont(GlobalFontSettings.DefaultFontName, 14 * scaleFactor, XFontStyleEx.Bold);
        var itemSource = view.ItemsSource as IEnumerable<IGrouping<string, City>>;
        page.DrawString(itemSource.ElementAt(section).Key, font, _defaultTextColor, bounds, XStringFormats.CenterLeft);
    }
}