using PdfSharp.Maui.Delegates;

namespace PdfSharp.Maui;

public class PdfRendererAttributes : BindableObject
{
    public static readonly BindableProperty ShouldRenderProperty =
        BindableProperty.CreateAttached(nameof(ShouldRender), typeof(bool), typeof(PdfRendererAttributes), true);

    public static readonly BindableProperty ListViewRendererDelegateProperty =
        BindableProperty.CreateAttached(nameof(ListViewRendererDelegate), typeof(PdfListViewRendererDelegate),
            typeof(PdfRendererAttributes), new PdfListViewRendererDelegate());

    public static readonly BindableProperty CollectionViewRendererDelegateProperty =
        BindableProperty.CreateAttached(nameof(CollectionViewRendererDelegate),
            typeof(PdfCollectionViewRendererDelegate),
            typeof(PdfRendererAttributes), new PdfCollectionViewRendererDelegate());

    public bool ShouldRender
    {
        get => (bool)GetValue(ShouldRenderProperty);
        set => SetValue(ShouldRenderProperty, value);
    }

    public PdfListViewRendererDelegate ListViewRendererDelegate
    {
        get => (PdfListViewRendererDelegate)GetValue(ListViewRendererDelegateProperty);
        set => SetValue(ListViewRendererDelegateProperty, value);
    }

    public PdfCollectionViewRendererDelegate CollectionViewRendererDelegate
    {
        get => (PdfCollectionViewRendererDelegate)GetValue(CollectionViewRendererDelegateProperty);
        set => SetValue(CollectionViewRendererDelegateProperty, value);
    }

    public static bool ShouldRenderView(BindableObject bindable)
    {
        return (bool)bindable.GetValue(ShouldRenderProperty);
    }
}