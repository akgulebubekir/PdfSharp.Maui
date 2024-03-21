namespace PdfSharp.Maui.Extensions;

using Microsoft.Maui.Controls;
using PdfSharp.Drawing.Layout;

public static class TextExtensions
{
    /// <summary>
    ///Converts FontAttributes into XFontStyle
    /// </summary>
    /// <param name="attributes"></param>
    /// <returns></returns>
    public static XFontStyleEx ToXFontStyle(this FontAttributes attributes)
    {
        return attributes switch
        {
            FontAttributes.None => XFontStyleEx.Regular,
            FontAttributes.Bold => XFontStyleEx.Bold,
            FontAttributes.Italic => XFontStyleEx.Italic,
            FontAttributes.Bold | FontAttributes.Italic => XFontStyleEx.BoldItalic,
            _ => XFontStyleEx.Regular
        };
    }

    /// <summary>
    /// Transforms the Text of the InputView according to the TextTransform property
    /// </summary>
    /// <param name="label">InputView to transform its text</param>
    /// <returns>transformed string</returns>
    public static string TransformedText(this InputView inputView)
    {
        return inputView.Text?.TransformedText(inputView.TextTransform);
    }

    /// <summary>
    /// Transforms the Text of the Button according to the TextTransform property
    /// </summary>
    /// <param name="label">Button to transform its text</param>
    /// <returns>transformed string</returns>
    public static string TransformedText(this Button button)
    {
        return button.Text?.TransformedText(button.TextTransform);
    }

    /// <summary>
    /// Transforms the Text of the Label according to the TextTransform property
    /// </summary>
    /// <param name="label">label to transform its text</param>
    /// <returns>transformed string</returns>
    public static string TransformedText(this Label label)
    {
        return label.Text?.TransformedText(label.TextTransform);
    }

    /// <summary>
    /// Transforms the text according to the TextTransform property
    /// </summary>
    /// <param name="text">string to transform</param>
    /// <param name="transform">transform</param>
    /// <returns>transformed string</returns>
    public static string TransformedText(this string text, TextTransform transform)
    {
        return transform switch
        {
            TextTransform.Uppercase => text?.ToUpperInvariant(),
            TextTransform.Lowercase => text?.ToLowerInvariant(),
            _ => text
        };
    }

    /// <summary>
    /// Wraps the string if it does not fit the bounds as one line.
    /// </summary>
    /// <param name="page">PDF page</param>
    /// <param name="text">string to draw</param>
    /// <param name="font">font</param>
    /// <param name="brush">text brush</param>
    /// <param name="bounds">text bounds</param>
    /// <param name="stringFormat">text alignment</param>
    public static void DrawFormattedString(this XGraphics page, string text, XFont font, XBrush brush, XRect bounds, XStringFormat stringFormat)
    {
        if (page.MeasureString(text, font).Width > bounds.Width)
        {
            var formatter = new XTextFormatter(page);
            formatter.DrawString(text, font, brush, bounds, XStringFormats.TopLeft);
        }
        else
        {
            page.DrawString(text, font, brush, bounds, stringFormat);
        }
    }
}
