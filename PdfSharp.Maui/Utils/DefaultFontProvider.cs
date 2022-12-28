using System.Reflection;
using PdfSharp.Maui.Contracts;

namespace PdfSharp.Maui.Utils;

public class DefaultFontProvider : IFontResolver
{
    public DefaultFontProvider(ICustomFontProvider fontProvider)
    {
        _fontProvider = fontProvider;
    }

    public string DefaultFontName => "OpenSans";

    #region Fields

    private readonly ICustomFontProvider _fontProvider;

    public static readonly string[] DefaultFontFiles =
    {
        "OpenSans-Regular.ttf",
        "OpenSans-Bold.ttf",
        "OpenSans-Italic.ttf",
        "OpenSans-BoldItalic.ttf"
    };

    #endregion

    #region IFontResolver implementation

    public byte[] GetFont(string faceName)
    {
        if (DefaultFontFiles.Contains(faceName) || _fontProvider == null)
        {
            var assembly = typeof(DefaultFontProvider).GetTypeInfo().Assembly;
            var stream = assembly.GetManifestResourceStream($"PdfSharp.Maui.DefaultFonts.{faceName}");
            using var reader = new StreamReader(stream!);
            using var memStream = new MemoryStream();
            reader.BaseStream.CopyTo(memStream);
            var bytes = memStream.ToArray();

            return bytes;
        }

        return _fontProvider.GetFont(faceName);
    }

    public FontResolverInfo ResolveTypeface(string familyName, bool isBold, bool isItalic)
    {
        string fontName;
        if (familyName == DefaultFontName || _fontProvider == null)
        {
            fontName = DefaultFontFiles[Convert.ToInt32(isBold) + 2 * Convert.ToInt32(isItalic)];
        }
        else
        {
            fontName = _fontProvider.ProvideFont(familyName, isBold, isItalic);
        }

        return new FontResolverInfo(fontName);
    }

    #endregion
}