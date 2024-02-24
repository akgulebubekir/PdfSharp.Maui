namespace PdfSharp.Maui.Renderers.Controls;

[PdfRenderer(ViewType = typeof(Image))]
public class PdfImageRenderer : PdfRendererBase<Image>
{
    protected override void CreateLayout(XGraphics page, Image view, XRect bounds, double scaleFactor)
    {
        var background = view.GetBackgroundBrush(bounds);
        if (background.IsNotDefault())
        {
            page.DrawRectangle(background, bounds);
        }

        if (view.Source == null)
        {
            return;
        }

        var img = view.Source switch
        {
            FileImageSource f => XImage.FromStream(FileSystem.OpenAppPackageFileAsync(f.File).Result),
            UriImageSource u => XImage.FromFile(u.Uri.AbsolutePath),
            StreamImageSource s => XImage.FromStream(s.Stream.Invoke(new CancellationToken()).Result),
            _ => throw new ArgumentException("Image.Source")
        };

        page.DrawImage(img, bounds);
    }
}
