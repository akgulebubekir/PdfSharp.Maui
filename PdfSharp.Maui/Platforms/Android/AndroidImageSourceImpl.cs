using Android.Graphics;
using Android.Media;
using static Android.Graphics.Bitmap;
using static Android.Graphics.BitmapFactory;
using ImageSource = MigraDocCore.DocumentObjectModel.MigraDoc.DocumentObjectModel.Shapes.ImageSource;
using Stream = System.IO.Stream;

namespace PdfSharp.Maui.Platforms.Android;

internal class AndroidImageSourceImpl : ImageSource.IImageSource
{
    private readonly int _quality;

    public AndroidImageSourceImpl(string name, Func<Stream> streamSource, int? quality)
    {
        Name = name;
        _quality = quality ?? 75;
        using var stream = streamSource.Invoke();
        Orientation = Orientation.Normal;
        
        Bitmap = DecodeStream(stream, null, null);
        Width = Orientation is Orientation.Normal or Orientation.Rotate180
            ? Bitmap.Width
            : Bitmap.Height;
        Height = Orientation is Orientation.Normal or Orientation.Rotate180
            ? Bitmap.Height
            : Bitmap.Width;
    }

    internal Bitmap Bitmap { get; set; }
    internal Stream Stream { get; set; }
    private Orientation Orientation { get; }

    public int Width { get; }
    public int Height { get; }
    public string Name { get; }

    public void SaveAsJpeg(MemoryStream ms, CancellationToken ct)
    {
        var tcs = new TaskCompletionSource<object>();
        ct.Register(() => { tcs.TrySetCanceled(); });
        var task = Task.Run(() =>
        {
            var mx = new Matrix();
            ct.ThrowIfCancellationRequested();
            switch (Orientation)
            {
                case Orientation.Rotate90:
                    mx.PostRotate(90);
                    break;
                case Orientation.Rotate180:
                    mx.PostRotate(180);
                    break;
                case Orientation.Rotate270:
                    mx.PostRotate(270);
                    break;
                default:
                    ct.ThrowIfCancellationRequested();
                    Bitmap.Compress(CompressFormat.Jpeg, _quality, ms);
                    ct.ThrowIfCancellationRequested();
                    return;
            }

            ct.ThrowIfCancellationRequested();
            using (var flip = CreateBitmap(Bitmap, 0, 0, Bitmap.Width, Bitmap.Height, mx, true))
            {
                flip.Compress(CompressFormat.Jpeg, _quality, ms);
            }

            ct.ThrowIfCancellationRequested();
        }, ct);
        Task.WaitAny(task, tcs.Task);
        tcs.TrySetCanceled();
        ct.ThrowIfCancellationRequested();
        if (task.IsFaulted)
        {
            throw task.Exception;
        }
    }
}