PdfSharp.Maui
======================

**PdfSharp.Maui** is a Microsoft.Maui library for **converting any Maui.View into PDF**.
It uses[PdfSharpCore](https://github.com/groege/PdfSharpCore) which is based on [PdfSharp](http://www.pdfsharp.net/).


[![NuGet](https://img.shields.io/badge/nuget-v1.0.2-blue.svg?style=plastic)](https://www.nuget.org/packages/PdfSharp.Maui)


### Demo
By following ([Demo](https://github.com/akgulebubekir/PDFSharp.Maui/tree/master/Demo)) folder, you will see screenshots, and PDF view generated from.


### Usage
> - **Init** : `var pdfManager = new PdfManager()`
> - **Generate** : `var pdf = pdfManager.GeneratePdfFromView(view, PageOrientation.Landscape, PageSize.A4, PdfStyleUniversal)`
> - **Save** :  `Utils.PdfSave.Save(pdf, "name.pdf","location")`


### Features
> - Universal style and Platform Specific(WYSIWYG) Pdf rendering
> - Custom Fonts (You should provide Font while creating `PdfManager`)
> - Image rendering (`FileImageSource`'s must be copied under `Resources/Raw` folder ). It also supports `png` images.
> - Custom renderer (Write your own renderer for your own custom view) 
> - Paper size & orientation support
> - Do not render option : by using `pdf:PdfRendererAttributes.ShouldRender="False"` you can ignore the view from rendering
> - Gradient background color support (`LinearGradientBrush` and `SolidColorBrush`)

### Limitations
> - Images renders only Jpeg format (It converts PNG to JPEG automatically)
> - CustomFont provider can be set only once (PdfSharp limitation)
> - It does not support `RadialGradientBrush`


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
