namespace PdfSharp.Maui.Extensions;

public static class LayoutOptionsExtension
{
    /// <summary>
    /// Converts TextAlignment to  <see cref="XStringAlignment"/>
    /// </summary>
    /// <param name="alignment">alignment</param>
    /// <returns></returns>
    public static XStringAlignment ToXStringAlignment(this TextAlignment alignment)
    {
        return alignment switch
        {
            TextAlignment.Start => XStringAlignment.Near,
            TextAlignment.Center => XStringAlignment.Center,
            TextAlignment.End => XStringAlignment.Far,
            _ => XStringAlignment.Near
        };
    }

    /// <summary>
    /// Converts TextAlignment to <see cref="XLineAlignment"/>
    /// </summary>
    /// <param name="alignment">alignment</param>
    /// <returns></returns>
    public static XLineAlignment ToXLineAlignment(this TextAlignment alignment)
    {
        return alignment switch
        {
            TextAlignment.Start => XLineAlignment.Near,
            TextAlignment.Center => XLineAlignment.Center,
            TextAlignment.End => XLineAlignment.Far,
            _ => XLineAlignment.BaseLine
        };
    }

    /// <summary>
    /// Converts TextAlignment into <see cref="XStringFormat"/>
    /// </summary>
    /// <param name="alignment"></param>
    /// <returns></returns>
    public static XStringFormat ToXStringFormat(this TextAlignment alignment)
    {
        return new XStringFormat
        {
            Alignment = alignment.ToXStringAlignment(),
            LineAlignment = alignment.ToXLineAlignment()
        };
    }
}