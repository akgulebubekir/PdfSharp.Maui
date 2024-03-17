namespace PdfSharp.Maui.Utils;

public static class ImageUtils
{
    public static XImage ToXImage(this ImageSource imageSource)
    {
        if (imageSource == null)
        {
            return null;
        }

        return imageSource switch
        {
            FileImageSource f => GetImage(f).Result,
            UriImageSource u => XImage.FromFile(u.Uri.AbsolutePath),
            StreamImageSource s => XImage.FromStream(s.Stream.Invoke(new CancellationToken()).Result),
            _ => throw new ArgumentException("Image.Source")
        };
    }

    private static async Task<XImage> GetImage(FileImageSource imageSource)
    {
        using var imageStream = await ImageLoading.LoadImageStreamAsync(imageSource.File, CancellationToken.None);
        if (imageStream == null)
        {
            return null;
        }

#if ANDROID
        if (imageSource.File.EndsWith(".png", StringComparison.InvariantCultureIgnoreCase))
        {
            using var bitmap = Android.Graphics.BitmapFactory.DecodeStream(imageStream);
            using var outputStream = new MemoryStream();
            _ = bitmap.Compress(Android.Graphics.Bitmap.CompressFormat.Jpeg, 100, outputStream);

            return XImage.FromStream(outputStream);
        }
#endif
        return XImage.FromStream(imageStream);

    }
}
