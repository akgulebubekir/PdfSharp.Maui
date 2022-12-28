using PdfSharp.Maui.Delegates;

namespace PdfSharp.Maui.Renderers.Collections;

public class PdfListViewRenderer : PdfRendererBase<ListView>
{
    protected override void CreateLayout(XGraphics page, ListView view, XRect bounds,
        double scaleFactor)
    {
        var listViewDelegate =
            (PdfListViewRendererDelegate)view.GetValue(PdfRendererAttributes.ListViewRendererDelegateProperty);

        var offset = bounds.TopLeft;

        if (view.HeaderTemplate != null)
        {
            var headerBound = new XRect(bounds.X + offset.X,
                bounds.Y + offset.Y,
                offset.X + bounds.Width,
                offset.Y + listViewDelegate.GetHeaderHeight(view) * scaleFactor);

            listViewDelegate.DrawHeader(view, page, headerBound, scaleFactor);
            offset.Y += headerBound.Height;
        }

        for (var section = 0; section < listViewDelegate.GetNumberOfSections(view); section++)
        {
            if (view.GroupHeaderTemplate != null)
            {
                var groupHeaderBounds = new XRect(bounds.X + offset.X,
                    bounds.Y + offset.Y,
                    offset.X + bounds.Width,
                    offset.Y + listViewDelegate.GetGroupHeaderHeight(view, section) * scaleFactor);

                listViewDelegate.DrawGroupHeader(view, section, page, groupHeaderBounds, scaleFactor);
                offset.Y += groupHeaderBounds.Height;
            }

            for (var row = 0; row < listViewDelegate.GetNumberOfRowsInSection(view, section); row++)
            {
                var rowBounds = new XRect(bounds.X + offset.X,
                    bounds.Y + offset.Y,
                    offset.X + bounds.Width,
                    offset.Y + listViewDelegate.GetCellHeight(view, section, row) * scaleFactor);

                listViewDelegate.DrawCell(view, section, row, page, bounds, scaleFactor);
                offset.Y += rowBounds.Height;
            }
        }

        if (view.FooterTemplate != null)
        {
            var footerBound = new XRect(bounds.X + offset.X,
                bounds.Y + offset.Y,
                offset.X + bounds.Width,
                offset.Y + listViewDelegate.GetFooterHeight(view) * scaleFactor);

            listViewDelegate.DrawFooter(view, page, footerBound, scaleFactor);
            offset.Y += footerBound.Height;
        }
    }
}