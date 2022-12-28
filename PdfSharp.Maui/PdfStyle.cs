namespace PdfSharp.Maui;

public enum PdfStyle
{
    /// <summary>
    ///     Ignores how it looks on different platforms, and create same PDF on any platforms
    /// </summary>
    Uniform,

    /// <summary>
    ///     Controls will be drawn as it looks on UI and user will get different output n different platform
    /// </summary>
    PlatformSpecific
}