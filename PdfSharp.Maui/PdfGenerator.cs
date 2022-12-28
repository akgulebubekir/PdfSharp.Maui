using PdfSharp.Maui.Renderers;
using PdfSharpCore.Pdf;

namespace PdfSharp.Maui;

internal class PdfGenerator
{
    private readonly double _scaleFactor;
    private readonly XRect _availablePageSize;
    private readonly PageOrientation _orientation;
    private readonly PageSize _pageSize;
    private readonly PdfStyle _style;
    private readonly View _rootView;
    private readonly Dictionary<Type, Type> _renderers;
    private readonly XBrush _bgColor;

    public PdfGenerator(View view, Dictionary<Type, Type> renderers, PageOrientation orientation, PageSize pageSize,
        PdfStyle style, bool resizeToFit)
    {
        _pageSize = pageSize;
        _style = style;
        _orientation = orientation;
        _rootView = view;
        _renderers = renderers;
        _availablePageSize = SizeUtils.GetAvailablePageSize(pageSize, orientation);
        _scaleFactor = resizeToFit ? _availablePageSize.Width / view.Bounds.Width : 1;
        _bgColor = style == PdfStyle.Uniform ? XBrushes.Transparent : GetBackgroundColor(view);
    }

    public PdfDocument Generate()
    {
        var visitor = new ViewVisitor(_rootView, _scaleFactor);
        return CreatePdf(visitor.Visit());
    }

    private static XBrush GetBackgroundColor(View rootView)
    {
        while (rootView.Parent != null)
        {
            if (rootView.Parent is View viewParent)
            {
                rootView = viewParent;
            }

            if (rootView.HasBackground())
            {
                return rootView.GetBackgroundBrush(rootView.Bounds.ToXRect());
            }

            if (rootView.Parent is Page page)
            {
                return page.GetBackgroundBrush();
            }
        }

        return XBrushes.Transparent;
    }

    private PdfDocument CreatePdf(List<ViewInfo> viewInfos)
    {
        var document = new PdfDocument();
        viewInfos = viewInfos.OrderBy(x => x.Offset.Y).ToList();
        var pageIndex = 1;
        while (viewInfos.Any())
        {
            var page = document.AddPage();
            page.Orientation = _orientation;
            page.Size = _pageSize;
            var graphics = XGraphics.FromPdfPage(page, XGraphicsUnit.Point);
            if (_style == PdfStyle.PlatformSpecific)
            {
                graphics.DrawRectangle(_bgColor,
                    new XRect(new XPoint(0, 0), SizeUtils.GetPageSize(_pageSize, _orientation)));
            }

            var adjustedYOffset = (pageIndex-1)*_availablePageSize.Height- viewInfos.First().Offset.Y;
            foreach (var viewInfo in viewInfos.Where(x =>
                             x.Offset.Y + x.Bounds.Bottom * _scaleFactor < pageIndex * _availablePageSize.Height)
                         .ToList())
            {
                    DrawView(viewInfo, pageIndex, adjustedYOffset, graphics);
                viewInfos.Remove(viewInfo);
            }

            pageIndex++;
        }
        
        return document;
    }

    private void DrawView(ViewInfo viewInfo, int pageNumber, double adjustedYOffset, XGraphics gfx)
    {
        var rList = _renderers.LastOrDefault(x => x.Key == viewInfo.View.GetType());

        switch (viewInfo)
        {
            case CollectionViewInfo<ListView> lvInfo:
                DrawListView(pageNumber, adjustedYOffset, gfx, lvInfo);
                break;
            case CollectionViewInfo<CollectionView> cvInfo:
                DrawCollectionView(pageNumber, adjustedYOffset, gfx, cvInfo);
                break;
            //Draw all other Views
            default:
            {
                if (rList.Value != null && viewInfo.Bounds.Width > 0 && viewInfo.View.Height > 0 &&
                    viewInfo.View.IsVisible)
                {
                    var renderer = (PdfRendererBase)Activator.CreateInstance(rList.Value);
                    var desiredBounds = new XRect(viewInfo.Offset.X + _availablePageSize.X,
                        viewInfo.Offset.Y + adjustedYOffset + _availablePageSize.Y - (pageNumber-1) * _availablePageSize.Height,
                        viewInfo.Bounds.Width,
                        viewInfo.Bounds.Height);

                    renderer?.CreateLayout(gfx, viewInfo.View, desiredBounds, _style, _scaleFactor);
                }

                break;
            }
        }
    }

    private void DrawListView(int pageNumber, double adjustedYOffset, XGraphics gfx, CollectionViewInfo<ListView> lvInfo)
    {
        var desiredBounds = new XRect(lvInfo.Offset.X + _availablePageSize.X,
            lvInfo.Offset.Y + adjustedYOffset + _availablePageSize.Y - (pageNumber - 1) * _availablePageSize.Height,
            lvInfo.Bounds.Width,
            lvInfo.Bounds.Height);
        switch (lvInfo.ItemType)
        {
            case CollectionItemType.Cell:
                lvInfo.CollectionViewDelegate.DrawCell(lvInfo.View as ListView, lvInfo.Section, lvInfo.Row,
                    gfx,
                    desiredBounds, _scaleFactor);
                break;
            case CollectionItemType.GroupHeader:
                lvInfo.CollectionViewDelegate.DrawGroupHeader(lvInfo.View as ListView, lvInfo.Section, gfx,
                    desiredBounds, _scaleFactor);
                break;
            case CollectionItemType.Header:
                lvInfo.CollectionViewDelegate.DrawHeader(lvInfo.View as ListView, gfx, desiredBounds,
                    _scaleFactor);
                break;
            case CollectionItemType.Footer:
                lvInfo.CollectionViewDelegate.DrawFooter(lvInfo.View as ListView, gfx, desiredBounds,
                    _scaleFactor);
                break;
        }
    }
private void DrawCollectionView(int pageNumber, double adjustedYOffset, XGraphics gfx, CollectionViewInfo<CollectionView> cvInfo)
    {
        var desiredBounds = new XRect(cvInfo.Offset.X + _availablePageSize.X,
            cvInfo.Offset.Y + adjustedYOffset + _availablePageSize.Y - (pageNumber - 1) * _availablePageSize.Height,
            cvInfo.Bounds.Width,
            cvInfo.Bounds.Height);
        switch (cvInfo.ItemType)
        {
            case CollectionItemType.Cell:
                cvInfo.CollectionViewDelegate.DrawCell(cvInfo.View as CollectionView, cvInfo.Section,
                    cvInfo.Row, gfx,
                    desiredBounds, _scaleFactor);
                break;
            case CollectionItemType.GroupHeader:
                cvInfo.CollectionViewDelegate.DrawGroupHeader(cvInfo.View as CollectionView, cvInfo.Section,
                    gfx,
                    desiredBounds, _scaleFactor);
                break;
            case CollectionItemType.Header:
                cvInfo.CollectionViewDelegate.DrawHeader(cvInfo.View as CollectionView, gfx, desiredBounds,
                    _scaleFactor);
                break;
            case CollectionItemType.Footer:
                cvInfo.CollectionViewDelegate.DrawFooter(cvInfo.View as CollectionView, gfx, desiredBounds,
                    _scaleFactor);
                break;
        }
    }

}