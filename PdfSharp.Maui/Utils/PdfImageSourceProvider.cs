namespace PdfSharp.Maui.Utils;

internal static partial class PdfImageSourceProvider
{
    internal static void RegisterImageSource()
    {
        RegisterImageSourceImpl(FileSystem.OpenAppPackageFileAsync);
    }

    static partial void RegisterImageSourceImpl(Func<string, Task<Stream>> fileSystemStreamProvider);
}