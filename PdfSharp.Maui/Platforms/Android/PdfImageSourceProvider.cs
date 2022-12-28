using PdfSharp.Maui.Platforms.Android;
using ImageSource = MigraDocCore.DocumentObjectModel.MigraDoc.DocumentObjectModel.Shapes.ImageSource;

namespace PdfSharp.Maui.Utils;

internal static partial class PdfImageSourceProvider
{
    static partial void RegisterImageSourceImpl(Func<string, Task<Stream>> fileSystemStreamProvider)
    {
        ImageSource.ImageSourceImpl = new AndroidImageSource(fileSystemStreamProvider);
    }
}