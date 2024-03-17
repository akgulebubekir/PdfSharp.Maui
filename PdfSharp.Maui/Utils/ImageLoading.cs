namespace PdfSharp.Maui.Utils;

#pragma warning disable IDE0005 // Using directive is unnecessary.
using System;
using Microsoft.Maui.Platform;
using System.Threading.Tasks;
#pragma warning restore IDE0005 // Using directive is unnecessary.

/// <summary>
/// source: https://gist.github.com/mattleibow/c7c162e984d56557a72eb8edc4a459a0#file-loadmauiimageasstream-cs
/// </summary>
internal static class ImageLoading
{
#pragma warning disable CS1998 // Async method lacks 'await' operators and will run synchronously
    public static async Task<Stream> LoadImageStreamAsync(string file, CancellationToken cancellationToken)
#pragma warning restore CS1998 // Async method lacks 'await' operators and will run synchronously
    {
        if (Path.IsPathRooted(file) && File.Exists(file))
        {
            return File.OpenRead(file);
        }
#if ANDROID
        var context = Android.App.Application.Context;
        var resources = context.Resources;

        var resourceId = context.GetDrawableId(file);
        if (resourceId > 0)
        {
            var imageUri = new Android.Net.Uri.Builder()
                .Scheme(Android.Content.ContentResolver.SchemeAndroidResource)
                .Authority(resources.GetResourcePackageName(resourceId))
                .AppendPath(resources.GetResourceTypeName(resourceId))
                .AppendPath(resources.GetResourceEntryName(resourceId))
                .Build();

            var stream = context.ContentResolver.OpenInputStream(imageUri);
            if (stream is not null)
            {
                return stream;
            }
        }
#elif WINDOWS
        try
        {
            var sf = await Windows.Storage.StorageFile.GetFileFromApplicationUriAsync(new Uri("ms-appx:///" + file));
            if (sf is not null)
            {
                var stream = await sf.OpenStreamForReadAsync();
                if (stream is not null)
                {
                    return stream;
                }
            }
        }
        catch
        {
        }

        if (AppInfo.PackagingModel == AppPackagingModel.Packaged)
        {
            var uri = new Uri("ms-appx:///" + file);
            var sf = await Windows.Storage.StorageFile.GetFileFromApplicationUriAsync(uri);
            var stream = await sf.OpenStreamForReadAsync();
            if (stream is not null)
            {
                return stream;
            }
        }
        else
        {
            var root = AppContext.BaseDirectory;
            file = Path.Combine(root, file);
            if (File.Exists(file))
            {
                return File.OpenRead(file);
            }
        }
#elif IOS || MACCATALYST
		var root = Foundation.NSBundle.MainBundle.BundlePath;
#if MACCATALYST || MACOS
		root = Path.Combine(root, "Contents", "Resources");
#endif
		file = Path.Combine(root, file);
		if (File.Exists(file))
			return File.OpenRead(file);
#endif

        return null;
    }
}
