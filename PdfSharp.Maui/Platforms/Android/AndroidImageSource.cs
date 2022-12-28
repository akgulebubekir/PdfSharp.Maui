using ImageSource = MigraDocCore.DocumentObjectModel.MigraDoc.DocumentObjectModel.Shapes.ImageSource;

namespace PdfSharp.Maui.Platforms.Android;

public class AndroidImageSource : ImageSource
{
    private readonly Func<string, Task<Stream>> _fileSystemStreamProvider;

    public AndroidImageSource(Func<string, Task<Stream>> fileSystemStreamProvider)
    {
        _fileSystemStreamProvider = fileSystemStreamProvider;
    }

    protected override IImageSource FromBinaryImpl(string name, Func<byte[]> imageSource, int? quality = 75)
    {
        return new AndroidImageSourceImpl(name, () => new MemoryStream(imageSource.Invoke()), quality);
    }

    protected override IImageSource FromFileImpl(string path, int? quality = 75)
    {
        return new AndroidImageSourceImpl(path, () => _fileSystemStreamProvider(path).Result, quality);
    }

    protected override IImageSource FromStreamImpl(string name, Func<Stream> imageStream, int? quality = 75)
    {
        return new AndroidImageSourceImpl(name, imageStream, quality);
    }
}