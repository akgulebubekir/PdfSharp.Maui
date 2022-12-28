using PdfSharp.Maui.Delegates;

namespace PdfSharp.Maui.Renderers.Collections;

public class PdfCollectionViewRenderer : PdfRendererBase<CollectionView>
{
    protected override void CreateLayout(XGraphics page, CollectionView view, XRect bounds,
        double scaleFactor)
    {
        var collectionViewDelegate =
            (PdfCollectionViewRendererDelegate)view.GetValue(PdfRendererAttributes.ListViewRendererDelegateProperty);

        var offset = bounds.TopLeft;

        if (view.HeaderTemplate != null)
        {
            var headerBound = new XRect(bounds.X + offset.X,
                bounds.Y + offset.Y,
                offset.X + bounds.Width,
                offset.Y + collectionViewDelegate.GetHeaderHeight(view) * scaleFactor);

            collectionViewDelegate.DrawHeader(view, page, headerBound, scaleFactor);
            offset.Y += headerBound.Height;
        }

        for (var section = 0; section < collectionViewDelegate.GetNumberOfSections(view); section++)
        {
            if (view.GroupHeaderTemplate != null)
            {
                var groupHeaderBounds = new XRect(bounds.X + offset.X,
                    bounds.Y + offset.Y,
                    offset.X + bounds.Width,
                    offset.Y + collectionViewDelegate.GetGroupHeaderHeight(view, section) * scaleFactor);

                collectionViewDelegate.DrawGroupHeader(view, section, page, groupHeaderBounds, scaleFactor);
                offset.Y += groupHeaderBounds.Height;
            }

            for (var row = 0; row < collectionViewDelegate.GetNumberOfRowsInSection(view, section); row++)
            {
                var rowBounds = new XRect(bounds.X + offset.X,
                    bounds.Y + offset.Y,
                    offset.X + bounds.Width,
                    offset.Y + collectionViewDelegate.GetCellHeight(view, section, row) * scaleFactor);

                collectionViewDelegate.DrawCell(view, section, row, page, bounds, scaleFactor);
                offset.Y += rowBounds.Height;
            }
        }

        if (view.FooterTemplate != null)
        {
            var footerBound = new XRect(bounds.X + offset.X,
                bounds.Y + offset.Y,
                offset.X + bounds.Width,
                offset.Y + collectionViewDelegate.GetFooterHeight(view) * scaleFactor);

            collectionViewDelegate.DrawFooter(view, page, footerBound, scaleFactor);
            offset.Y += footerBound.Height;
        }
    }
}