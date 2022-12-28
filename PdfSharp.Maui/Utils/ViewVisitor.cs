using PdfSharp.Maui.Delegates;


namespace PdfSharp.Maui.Utils
{
  internal class ViewVisitor
    {
        private readonly View _view;
        private readonly double _scaleFactor;
        private readonly List<ViewInfo> _visitedViews;

        public ViewVisitor(View view, double scaleFactor)
        {
            _view = view;
            _scaleFactor = scaleFactor;
            _visitedViews = new List<ViewInfo>();
        }

        internal List<ViewInfo> Visit()
        {
            _visitedViews.Clear();
            VisitView(_view, new XPoint(0, 0));
            return _visitedViews;
        }

        private void VisitView(View view, XPoint pageOffset)
        {
            if (!PdfRendererAttributes.ShouldRenderView(view))
            {
                return;
            }

            var newOffset = new XPoint(
                pageOffset.X + view.Bounds.X * _scaleFactor,
                pageOffset.Y + view.Bounds.Y * _scaleFactor);

            var bounds = new XRect(newOffset,
                new XSize(view.Bounds.Width * _scaleFactor,
                    view.Bounds.Height * _scaleFactor));
            _visitedViews.Add(new ViewInfo { View = view, Offset = newOffset, Bounds = bounds });

            switch (view)
            {
                case ListView listView:
                {
                    PopulateListView(listView, newOffset, bounds);
                    break;
                }
                case CollectionView collectionView:
                {
                    PopulateCollectionView(collectionView, newOffset, bounds);
                    break;
                }
                case Layout layout:
                {
                    foreach (var v in layout.Children)
                    {
                        VisitView(v as View, newOffset);
                    }

                    break;
                }
                case Frame { Content: { } } frame:
                    VisitView(frame.Content, newOffset);
                    break;
                case Border { Content: { } content }:
                    VisitView(content, newOffset);
                    break;
                case RefreshView { Content: not null } refreshView:
                    VisitView(refreshView.Content, newOffset);
                    break;
                case ContentView { Content: { } } contentView:
                    VisitView(contentView.Content, newOffset);
                    break;
                case ScrollView { Content: { } } scrollView:
                    VisitView(scrollView.Content, newOffset);
                    break;
            }
        }

        #region Collection & ListView handling

        private void PopulateListView(ListView listView, XPoint newOffset, XRect bounds)
        {
            var listViewDelegate =
                (PdfListViewRendererDelegate)listView.GetValue(PdfRendererAttributes
                    .ListViewRendererDelegateProperty);
            var totalHeight = listViewDelegate.GetTotalHeight(listView) * _scaleFactor;
            var usedHeight = 0.0;
            var listOffset = newOffset;
            //Get Headers
            if (listView.HeaderTemplate != null)
            {
                var headerHeight = listViewDelegate.GetHeaderHeight(listView) * _scaleFactor;
                _visitedViews.Add(new CollectionViewInfo<ListView>
                {
                    ItemType = CollectionItemType.Header,
                    CollectionViewDelegate = listViewDelegate,
                    View = listView,
                    Offset = listOffset,
                    Bounds = new XRect(0, 0, bounds.Width * _scaleFactor, headerHeight)
                });
                listOffset.Y += headerHeight;
                usedHeight += headerHeight;
            }

            for (var section = 0; section < listViewDelegate.GetNumberOfSections(listView); section++)
            {
                if (listView.GroupHeaderTemplate != null)
                {
                    var groupHeaderHeight = listViewDelegate.GetGroupHeaderHeight(listView, section) * _scaleFactor;
                    _visitedViews.Add(new CollectionViewInfo<ListView>
                    {
                        ItemType = CollectionItemType.GroupHeader,
                        CollectionViewDelegate = listViewDelegate,
                        View = listView,
                        Section = section,
                        Offset = listOffset,
                        Bounds = new XRect(0, 0, bounds.Width * _scaleFactor, groupHeaderHeight)
                    });

                    listOffset.Y += groupHeaderHeight;
                    usedHeight += groupHeaderHeight;

                    if (usedHeight >= totalHeight)
                    {
                        break;
                    }
                }

                //Get Rows
                for (var row = 0; row < listViewDelegate.GetNumberOfRowsInSection(listView, section); row++)
                {
                    var rowHeight = listViewDelegate.GetCellHeight(listView, section, row) * _scaleFactor;
                    _visitedViews.Add(new CollectionViewInfo<ListView>
                    {
                        ItemType = CollectionItemType.Cell,
                        CollectionViewDelegate = listViewDelegate,
                        View = listView,
                        Row = row,
                        Section = section,
                        Offset = listOffset,
                        Bounds = new XRect(listOffset.X, listOffset.Y, bounds.Width * _scaleFactor,
                            rowHeight)
                    });

                    listOffset.Y += rowHeight;
                    usedHeight += rowHeight;

                    if (usedHeight >= totalHeight)
                    {
                        break;
                    }
                }
            }

            //Get Footers
            if (listView.FooterTemplate != null)
            {
                var footerHeight = listViewDelegate.GetFooterHeight(listView) * _scaleFactor;
                if (usedHeight + footerHeight <= totalHeight)
                {
                    _visitedViews.Add(new CollectionViewInfo<ListView>
                    {
                        ItemType = CollectionItemType.Footer,
                        CollectionViewDelegate = listViewDelegate,
                        View = listView,
                        Offset = listOffset,
                        Bounds = new XRect(0, 0, 0, footerHeight)
                    });

                    listOffset.Y += footerHeight;
                }
            }
        }

        private void PopulateCollectionView(CollectionView collectionView, XPoint newOffset, XRect bounds)
        {
            var collectionViewRendererDelegate =
                (PdfCollectionViewRendererDelegate)collectionView.GetValue(PdfRendererAttributes
                    .CollectionViewRendererDelegateProperty);
            var totalHeight = collectionViewRendererDelegate.GetTotalHeight(collectionView) * _scaleFactor;
            var usedHeight = 0.0;
            var listOffset = newOffset;
            //Get Headers
            if (collectionView.HeaderTemplate != null)
            {
                var headerHeight = collectionViewRendererDelegate.GetHeaderHeight(collectionView) * _scaleFactor;

                _visitedViews.Add(new CollectionViewInfo<CollectionView>
                {
                    ItemType = CollectionItemType.Header,
                    CollectionViewDelegate = collectionViewRendererDelegate,
                    View = collectionView,
                    Offset = listOffset,
                    Bounds = new XRect(0, 0, bounds.Width * _scaleFactor, headerHeight)
                });
                listOffset.Y += headerHeight;
                usedHeight += headerHeight;
            }

            for (var section = 0;
                 section < collectionViewRendererDelegate.GetNumberOfSections(collectionView);
                 section++)
            {
                if (collectionView.GroupHeaderTemplate != null)
                {
                    var groupHeaderHeight =
                        collectionViewRendererDelegate.GetGroupHeaderHeight(collectionView, section) * _scaleFactor;
                    _visitedViews.Add(new CollectionViewInfo<CollectionView>
                    {
                        ItemType = CollectionItemType.GroupHeader,
                        CollectionViewDelegate = collectionViewRendererDelegate,
                        View = collectionView,
                        Section = section,
                        Offset = listOffset,
                        Bounds = new XRect(0, 0, bounds.Width * _scaleFactor, groupHeaderHeight)
                    });

                    listOffset.Y += groupHeaderHeight;
                    usedHeight += groupHeaderHeight;

                    if (usedHeight >= totalHeight)
                    {
                        break;
                    }
                }

                //Get Rows
                for (var row = 0;
                     row < collectionViewRendererDelegate.GetNumberOfRowsInSection(collectionView, section);
                     row++)
                {
                    var rowHeight = collectionViewRendererDelegate.GetCellHeight(collectionView, section, row) *
                                    _scaleFactor;
                    _visitedViews.Add(new CollectionViewInfo<CollectionView>
                    {
                        ItemType = CollectionItemType.Cell,
                        CollectionViewDelegate = collectionViewRendererDelegate,
                        View = collectionView,
                        Row = row,
                        Section = section,
                        Offset = listOffset,
                        Bounds = new XRect(listOffset.X, listOffset.Y, bounds.Width * _scaleFactor, rowHeight)
                    });

                    listOffset.Y += rowHeight;
                    usedHeight += rowHeight;

                    if (usedHeight >= totalHeight)
                    {
                        break;
                    }
                }
            }

            //Get Footers
            if (collectionView.FooterTemplate != null)
            {
                var footerHeight = collectionViewRendererDelegate.GetFooterHeight(collectionView) * _scaleFactor;
                if (usedHeight + footerHeight <= totalHeight)
                {
                    _visitedViews.Add(new CollectionViewInfo<CollectionView>
                    {
                        ItemType = CollectionItemType.Footer,
                        CollectionViewDelegate = collectionViewRendererDelegate,
                        View = collectionView,
                        Offset = listOffset,
                        Bounds = new XRect(0, 0, 0, footerHeight)
                    });

                    listOffset.Y += footerHeight;
                }
            }
        }

        #endregion
    }
}
