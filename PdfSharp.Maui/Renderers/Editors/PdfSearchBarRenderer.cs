namespace PdfSharp.Maui.Renderers.Editors;

using System.Reflection;

[PdfRenderer(ViewType = typeof(SearchBar))]
public class PdfSearchBarRenderer : PdfRendererBase<SearchBar>
{
    protected override void CreateLayout(XGraphics page, SearchBar view, XRect bounds, double scaleFactor)
    {
        var font = new XFont(view.FontFamily ?? GlobalFontSettings.DefaultFontName,
            view.FontSize * scaleFactor);
        var cornerRadius = GetProperty<double>(ViewProperties.CornerRadius);
        var themeName = Application.Current?.RequestedTheme == AppTheme.Dark ? "dark" : "light";
        var searchIconName = HasProperty(SearchBarProperties.ImageSourcePlatform)
            ? $"{GetProperty<string>(SearchBarProperties.ImageSourcePlatform)}_{themeName}"
            : "windows_light";

        var searchIcon = XImage.FromStream(
            typeof(PdfSearchBarRenderer).GetTypeInfo().Assembly.GetManifestResourceStream(
                $"PdfSharp.Maui.Icons.search_{searchIconName}.png"));


        if (HasBackgroundColor())
        {
            page.DrawRoundedRectangle(GetProperty<XBrush>(ViewProperties.BackgroundColor), bounds,
                new XSize(cornerRadius, cornerRadius));
        }

        page.DrawRoundedRectangle(new XPen(GetProperty<XColor>(ViewProperties.BorderColor), 1.5 * scaleFactor),
            GetProperty<XRect>(ViewProperties.BorderLocation), new XSize(cornerRadius, cornerRadius));

        page.DrawImage(searchIcon, GetProperty<XRect>(ViewProperties.ImageLocation));

        if (!string.IsNullOrEmpty(view.Text))
        {
            page.DrawFormattedString(view.TransformedText(), font, GetProperty<XBrush>(ViewProperties.TextColor),
                GetProperty<XRect>(ViewProperties.TextLocation), new XStringFormat
                {
                    Alignment = XStringAlignment.Near,
                    LineAlignment = XLineAlignment.Center
                });
        }
        else if (!string.IsNullOrEmpty(view.Placeholder))
        {
            page.DrawFormattedString(view.Placeholder, font, GetProperty<XBrush>(ViewProperties.PlaceholderColor),
                GetProperty<XRect>(ViewProperties.TextLocation), new XStringFormat
                {
                    Alignment = XStringAlignment.Near,
                    LineAlignment = XLineAlignment.Center
                });
        }
    }

    protected override void CreateUniformLayoutParameters(XGraphics page, SearchBar view, XRect bounds,
        double scaleFactor)
    {
        Properties.Add(ViewProperties.ImageLocation,
            new XRect(bounds.BottomRight.WithMargin(-0.9 * bounds.Height),
                new XSize(0.8 * bounds.Height, 0.8 * bounds.Height)));
        Properties.Add(ViewProperties.TextLocation,
            new XRect(new XPoint(bounds.X + (5 * scaleFactor), bounds.Y),
                new XSize(bounds.Width - bounds.Height, bounds.Height)));
        Properties.Add(ViewProperties.TextColor, XBrushes.Black);
        Properties.Add(ViewProperties.PlaceholderColor, XBrushes.Gray);
        Properties.Add(ViewProperties.BackgroundColor, XBrushes.White);
        Properties.Add(ViewProperties.BorderThickness, scaleFactor);
        Properties.Add(ViewProperties.BorderColor, XColors.LightGray);
        Properties.Add(ViewProperties.BorderLocation, bounds);
        Properties.Add(ViewProperties.CornerRadius, scaleFactor);
    }

    protected override void CreateAndroidLayoutParameters(XGraphics page, SearchBar view, XRect bounds,
        double scaleFactor)
    {
        Properties.Add(SearchBarProperties.ImageSourcePlatform, "android");
        Properties.Add(ViewProperties.ImageLocation,
            new XRect(bounds.Location.WithMargin(0.2 * bounds.Height),
                new XSize(0.6 * bounds.Height, 0.6 * bounds.Height)));
        Properties.Add(ViewProperties.TextLocation,
            new XRect(new XPoint(bounds.X + bounds.Height, bounds.Y),
                new XSize(bounds.Width - bounds.Height, bounds.Height)));
        Properties.Add(ViewProperties.TextColor,
            view.TextColor.IsDefault() ? XBrushes.Black : view.TextColor.ToXBrush());
        Properties.Add(ViewProperties.PlaceholderColor,
            view.PlaceholderColor.IsDefault() ? XBrushes.Gray : view.PlaceholderColor.ToXBrush());
        Properties.Add(ViewProperties.BackgroundColor, view.GetBackgroundBrush(bounds));
        Properties.Add(ViewProperties.BorderColor, XColors.LightGray);
        Properties.Add(ViewProperties.BorderLocation,
            new XRect(new XPoint(bounds.Left, bounds.Bottom - 3 * scaleFactor),
                new XSize(bounds.Width - bounds.Height, 1 * scaleFactor)));
        Properties.Add(ViewProperties.CornerRadius, 0.0);
    }

    protected override void CreateIosLayoutParameters(XGraphics page, SearchBar view, XRect bounds, double scaleFactor)
    {
        Properties.Add(SearchBarProperties.ImageSourcePlatform, "ios");
        Properties.Add(ViewProperties.ImageLocation,
            new XRect(bounds.Location.WithMargin(0.2 * bounds.Height),
                new XSize(0.6 * bounds.Height, 0.6 * bounds.Height)));
        Properties.Add(ViewProperties.TextLocation,
            new XRect(new XPoint(bounds.X + bounds.Height, bounds.Y),
                new XSize(bounds.Width - bounds.Height, bounds.Height)));
        Properties.Add(ViewProperties.TextColor,
            view.TextColor.IsDefault() ? XBrushes.Black : view.TextColor.ToXBrush());
        Properties.Add(ViewProperties.PlaceholderColor,
            view.PlaceholderColor.IsDefault() ? XBrushes.Gray : view.PlaceholderColor.ToXBrush());

        Properties.Add(ViewProperties.BackgroundColor,
            view.HasBackground() ? XBrushes.White : view.GetBackgroundBrush(bounds));

        Properties.Add(ViewProperties.BorderColor, XColors.LightGray);
        Properties.Add(ViewProperties.BorderLocation, bounds.WithMargin(new Thickness(bounds.Height * 0.1)));
        Properties.Add(ViewProperties.CornerRadius, 2 * scaleFactor);
    }

    protected override void CreateWindowsLayoutParameters(XGraphics page, SearchBar view, XRect bounds,
        double scaleFactor)
    {
        Properties.Add(SearchBarProperties.ImageSourcePlatform, "windows");
        Properties.Add(ViewProperties.ImageLocation,
            new XRect(bounds.BottomRight.WithMargin(-bounds.Height),
                new XSize(0.8 * bounds.Height, 0.8 * bounds.Height)));

        Properties.Add(ViewProperties.TextLocation,
            new XRect(bounds.TopLeft, new XSize(bounds.Width - bounds.Height, bounds.Height)));
        Properties.Add(ViewProperties.TextColor,
            view.TextColor.IsDefault() ? XBrushes.Black : view.TextColor.ToXBrush());
        Properties.Add(ViewProperties.PlaceholderColor,
            view.PlaceholderColor.IsDefault() ? XBrushes.Gray : view.PlaceholderColor.ToXBrush());

        Properties.Add(ViewProperties.BackgroundColor, view.GetBackgroundBrush(bounds));
        Properties.Add(ViewProperties.BorderColor, XColors.LightGray);
        Properties.Add(ViewProperties.BorderLocation,
            new XRect(new XPoint(bounds.Left, bounds.Bottom - 3 * scaleFactor),
                new XSize(bounds.Width - bounds.Height, 1 * scaleFactor)));

        Properties.Add(ViewProperties.CornerRadius, scaleFactor);
    }

    private static class SearchBarProperties
    {
        public const string ImageSourcePlatform = nameof(ImageSourcePlatform);
    }
}
