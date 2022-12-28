namespace PdfSharp.Maui.Extensions;

public static class TextExtensions
{
    /// <summary>
    ///Converts FontAttributes into XFontStyle
    /// </summary>
    /// <param name="attributes"></param>
    /// <returns></returns>
    public static XFontStyle ToXFontStyle(this FontAttributes attributes)
    {
        return attributes switch
        {
            FontAttributes.None => XFontStyle.Regular,
            FontAttributes.Bold => XFontStyle.Bold,
            FontAttributes.Italic => XFontStyle.Italic,
            FontAttributes.Bold | FontAttributes.Italic => XFontStyle.Italic | XFontStyle.Bold,
            _ => XFontStyle.Regular
        };
    }
}