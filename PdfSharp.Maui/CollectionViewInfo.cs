using PdfSharp.Maui.Delegates;

namespace PdfSharp.Maui;

internal class CollectionViewInfo : ViewInfo
{
    public int Section { get; set; }

    public int Row { get; set; }

    public CollectionItemType ItemType { get; set; }
}

internal class CollectionViewInfo<T> : CollectionViewInfo where T : View
{
    public ICollectionViewRendererDelegate<T> CollectionViewDelegate { get; set; }
}