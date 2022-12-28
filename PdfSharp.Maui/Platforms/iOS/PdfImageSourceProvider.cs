using PdfSharp.Maui.Platforms.iOS;

namespace PdfSharp.Maui.Utils;
using ImageSource = MigraDocCore.DocumentObjectModel.MigraDoc.DocumentObjectModel.Shapes.ImageSource;

internal static partial class PdfImageSourceProvider
{
    static partial void RegisterImageSourceImpl(Func<string, Task<Stream>> fileSystemStreamProvider)
    {
        ImageSource.ImageSourceImpl = new IosImageSource(fileSystemStreamProvider);
    }

}