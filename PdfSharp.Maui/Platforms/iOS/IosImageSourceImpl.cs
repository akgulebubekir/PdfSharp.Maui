using CoreGraphics;
using Foundation;
using UIKit;
using ImageSource = MigraDocCore.DocumentObjectModel.MigraDoc.DocumentObjectModel.Shapes.ImageSource;

namespace PdfSharp.Maui.Platforms.iOS;

internal class IosImageSourceImpl : ImageSource.IImageSource
{
    private readonly UIImage _image;


    public IosImageSourceImpl(string name, Func<Stream> streamSource, int? quality)
    {
        Name = name;
        using var stream = streamSource.Invoke();
        _image = UIImage.LoadFromData(NSData.FromStream(stream)!);
        var size = _image?.Size ?? new CGSize(0, 0);

        Width = (int)size.Width;
        Height = (int)size.Height;
    }

    public int Width { get; }
    public int Height { get; }
    public string Name { get; }

    public void SaveAsJpeg(MemoryStream ms, CancellationToken ct)
    {
        var jpg = _image.AsJPEG();
        ms.Write(jpg.ToArray(), 0, (int)jpg.Length);
        ms.Seek(0, SeekOrigin.Begin);
    }
}