using ImageSource = MigraDocCore.DocumentObjectModel.MigraDoc.DocumentObjectModel.Shapes.ImageSource;

namespace PdfSharp.Maui.Platforms.Windows;

public class WindowsImageSource : ImageSource
{
    private readonly Func<string, Task<Stream>> _fileSystemStreamProvider;

    public WindowsImageSource(Func<string, Task<Stream>> fileSystemStreamProvider)
    {
        _fileSystemStreamProvider = fileSystemStreamProvider;
    }

    protected override IImageSource FromBinaryImpl(string name, Func<byte[]> imageSource, int? quality = 75)
    {
        return new WindowsImageSourceImpl(name,
            () => new MemoryStream(imageSource.Invoke()).AsRandomAccessStream(), quality);
    }

    protected override IImageSource FromFileImpl(string path, int? quality = 75)
    {
        return new WindowsImageSourceImpl(path, () => _fileSystemStreamProvider(path).Result.AsRandomAccessStream(),
            quality);
    }

    protected override IImageSource FromStreamImpl(string name, Func<Stream> imageStream, int? quality = 75)
    {
        return new WindowsImageSourceImpl(name,
            () => imageStream.Invoke().AsRandomAccessStream(), quality);
    }
}