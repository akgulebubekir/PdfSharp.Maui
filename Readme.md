PdfSharp.Maui
======================

**PdfSharp.Maui** is a Microsoft.Maui library for **converting any Maui.View into PDF**.
It uses[PdfSharp](https://github.com/empira/PDFsharp).


[![NuGet](https://img.shields.io/badge/nuget-v1.0.5-blue.svg?style=plastic)](https://www.nuget.org/packages/PdfSharp.Maui)
[![FOSSA Status](https://app.fossa.com/api/projects/git%2Bgithub.com%2Fakgulebubekir%2FPdfSharp.Maui.svg?type=shield)](https://app.fossa.com/projects/git%2Bgithub.com%2Fakgulebubekir%2FPdfSharp.Maui?ref=badge_shield)


### Demo
By following ([Demo](https://github.com/akgulebubekir/PDFSharp.Maui/tree/master/Demo)) folder, you will see screenshots, and PDF view generated from.


### Usage
> - **Init** : `var pdfManager = new PdfManager()`
> - **Generate** : `var pdf = pdfManager.GeneratePdfFromView(view, PageOrientation.Landscape, PageSize.A4, PdfStyleUniversal)`
> - **Save** :  `Utils.PdfSave.Save(pdf, "name.pdf","location")`


### Features
> - Universal style and Platform Specific(WYSIWYG) Pdf rendering
> - Custom Fonts (You should provide Font while creating `PdfManager`)
> - Image rendering. (It also supports `png` images)
> - Custom renderer (Write your own renderer for your own custom view) 
> - Paper size & orientation support
> - Do not render option : by using `pdf:PdfRendererAttributes.ShouldRender="False"` you can ignore the view from rendering
> - Gradient background color support (`LinearGradientBrush` and `SolidColorBrush`)

### Limitations
> - It does not support `RadialGradientBrush` yet.
> - `png` images will be converted into `jpeg` in android.

### ListView & CollectionView Rendering
> Due to template style of these view library only renders as basic listview. Therefore you should implement your own delegate classes derived from `PdfListViewRendererDelegate` or `PdfCollectionViewRendererDelegate`.  You will find the sample list view delegate at `PdfSharp.Maui.Sample/CustomListViewRendererDelegate.cs`


### Custom PDF Renderer
> Its possible to write your own renderer, for your own custom view. You should mark your class with `PdfRenderer` attribute, then you have to create PdfManager by giving the assembly.


```cs
[PdfRenderer(ViewType = typeof(YourView))]
	public class PdfCustomViewRenderer : PdfRendererBase<YourView>
	{
		public override void CreatePDFLayout(XGraphics page, YourView view, XRect bounds, double scaleFactor)
		{
			XFont font = new XFont(GlobalFontSettings.DefaultFontName, 14 * scaleFactor);

			page.DrawRectangle(view.GetBackgroundBrush(), bounds);
			page.DrawString("Sample text in the page", font, XBrushes.Black, bounds, XStringFormats.Center);
		}
	}
```


## License
[![FOSSA Status](https://app.fossa.com/api/projects/git%2Bgithub.com%2Fakgulebubekir%2FPdfSharp.Maui.svg?type=large)](https://app.fossa.com/projects/git%2Bgithub.com%2Fakgulebubekir%2FPdfSharp.Maui?ref=badge_large)