namespace PdfSharp.Maui.Contracts;

/// <summary>
/// Font provider interface for custom PDF fonts
/// </summary>
public interface ICustomFontProvider
{
    /// <summary>
    /// Font file in ttf format as byte array
    /// </summary>
    /// <param name="faceName">font name</param>
    /// <returns>byte array</returns>
    byte[] GetFont(string faceName);

    /// <summary>
    /// Gets font file name by attributes
    /// </summary>
    /// <param name="fontName"></param>
    /// <param name="isItalic"></param>
    /// <param name="isBold"></param>
    /// <returns>Font file name</returns>
    string ProvideFont(string fontName, bool isItalic, bool isBold);
}