using Windows.Foundation;
using Windows.Graphics.Imaging;
using Windows.Storage.Streams;
using ImageSource = MigraDocCore.DocumentObjectModel.MigraDoc.DocumentObjectModel.Shapes.ImageSource;

namespace PdfSharp.Maui.Platforms.Windows;

internal class WindowsImageSourceImpl : ImageSource.IImageSource
{
    private readonly Func<IRandomAccessStream> _getRas;
    private readonly int _quality;

    public WindowsImageSourceImpl(string name, Func<IRandomAccessStream> getRas, int? quality)
    {
        Name = name;
        _getRas = getRas;
        _quality = quality ?? 75;
        using var ras = getRas.Invoke();
        var decoder = GetDecoder(ras);
        Width = (int)decoder.OrientedPixelWidth;
        Height = (int)decoder.OrientedPixelHeight;
    }

    public int Width { get; }
    public int Height { get; }
    public string Name { get; }

    public void SaveAsJpeg(MemoryStream ms, CancellationToken ct)
    {
        var tcs = new TaskCompletionSource<object>();
        ct.Register(() => { tcs.TrySetCanceled(); });
        using var ras = _getRas.Invoke();
        var decoder = GetDecoder(ras);
        var task = Task.Run(async () =>
        {
            ct.ThrowIfCancellationRequested();
            var encoder = await BitmapEncoder.CreateAsync(BitmapEncoder.JpegEncoderId, ms.AsRandomAccessStream(),
                new BitmapPropertySet
                {
                    { "ImageQuality", new BitmapTypedValue(Convert.ToSingle(_quality) / 100, PropertyType.Single) }
                });
            ct.ThrowIfCancellationRequested();
            encoder.SetPixelData(decoder.BitmapPixelFormat, decoder.BitmapAlphaMode, decoder.OrientedPixelWidth,
                decoder.OrientedPixelHeight, decoder.DpiX, decoder.DpiY,
                (await decoder.GetPixelDataAsync()).DetachPixelData());
            ct.ThrowIfCancellationRequested();
            await encoder.FlushAsync();
        }, ct);
        Task.WaitAny(task, tcs.Task);
        tcs.TrySetCanceled();
        ct.ThrowIfCancellationRequested();
        if (task.IsFaulted)
        {
            throw task.Exception;
        }
    }

    private static BitmapDecoder GetDecoder(IRandomAccessStream ras)
    {
        return Task.Run(async () => await BitmapDecoder.CreateAsync(ras)).Result;
    }
}