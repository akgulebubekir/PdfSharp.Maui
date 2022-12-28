using ImageSource = MigraDocCore.DocumentObjectModel.MigraDoc.DocumentObjectModel.Shapes.ImageSource;

namespace PdfSharp.Maui.Platforms.iOS;

public class IosImageSource : ImageSource
{
    private readonly Func<string, Task<Stream>> _fileSystemStreamProvider;

    public IosImageSource(Func<string, Task<Stream>> fileSystemStreamProvider)
    {
        _fileSystemStreamProvider = fileSystemStreamProvider;
    }

    protected override IImageSource FromBinaryImpl(string name, Func<byte[]> imageSource, int? quality = 75)
    {
        return new IosImageSourceImpl(name, () => new MemoryStream(imageSource.Invoke()), quality);
    }

    protected override IImageSource FromFileImpl(string path, int? quality = 75)
    {
        return new IosImageSourceImpl(path, () => _fileSystemStreamProvider(path).Result, quality);
    }

    protected override IImageSource FromStreamImpl(string name, Func<Stream> imageStream, int? quality = 75)
    {
        return new IosImageSourceImpl(name, imageStream, quality);
    }
}