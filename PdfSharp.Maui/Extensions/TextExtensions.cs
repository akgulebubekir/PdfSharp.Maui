namespace PdfSharp.Maui.Extensions;

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
}
