using PdfSharp.Maui.Platforms.MacCatalyst;
using ImageSource = MigraDocCore.DocumentObjectModel.MigraDoc.DocumentObjectModel.Shapes.ImageSource;

// ReSharper disable once CheckNamespace
namespace PdfSharp.Maui.Utils;

internal static partial class PdfImageSourceProvider
{
    static partial void RegisterImageSourceImpl(Func<string, Task<Stream>> fileSystemStreamProvider)
    {
        ImageSource.ImageSourceImpl = new MacImageSource(fileSystemStreamProvider);
    }
}