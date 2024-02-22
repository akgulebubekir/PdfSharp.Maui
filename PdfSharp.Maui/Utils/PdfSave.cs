using PdfSharp.Pdf;

namespace PdfSharp.Maui.Utils;

public static class PdfSave
{
    /// <summary>
    /// Saves PDF into specified location as file
    /// </summary>
    /// <param name="document">Pdf document</param>
    /// <param name="fileName">file name</param>
    /// <param name="path">path. Default value is System's temp path</param>
    public static void Save(PdfDocument document, string fileName, string path = default)
    {
        var location = Path.Join(path ?? Path.GetTempPath(), fileName);

        document.Save(location);
        document.Close();
    }
}