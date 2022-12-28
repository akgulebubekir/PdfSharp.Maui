using System.Collections;

namespace PdfSharp.Maui.Delegates;

public class PdfCollectionViewRendererDelegate : ICollectionViewRendererDelegate<CollectionView>
{
    #region Items Calculation

    /// <summary>
    /// Number of sections in <see cref="CollectionView"/>. If 'IsGrouped' is false, it will be 1 by default.
    /// </summary>
    /// <param name="view">CollectionView</param>
    /// <returns>Number of sections</returns>
    public virtual int GetNumberOfSections(CollectionView view)
    {
        if (!view.IsGrouped)
        {
            return 1;
        }

        return view.ItemsSource?.Cast<object>().Count() ?? 1;
    }

    /// <summary>
    /// Numbers of sections per group.If 'IsGrouped' is false, it will be equal to number of items in the <see cref="CollectionView"/> 
    /// </summary>
    /// <param name="view"><see cref="CollectionView"/></param>
    /// <param name="section">Section</param>
    /// <returns>># of rows in section</returns>
    public virtual int GetNumberOfRowsInSection(CollectionView view, int section)
    {
        if (!view.IsGrouped)
        {
            return view.ItemsSource?.Cast<object>().Count() ?? 0;
        }

        var sectionItems = view.ItemsSource?.Cast<IEnumerable>().ElementAt(section);

        return sectionItems?.Cast<object>().Count() ?? 0;
    }

    #endregion

    #region Size calculation

    /// <summary>
    /// Header height of the <see cref="CollectionView"/>
    /// Default value is 44
    /// </summary>
    /// <param name="view">CollectionView</param>
    /// <returns>Height</returns>
    public virtual double GetHeaderHeight(CollectionView view)
    {
        return view.HeaderTemplate == null ? 0 : 44;
    }

    /// <summary>
    /// Group header height for the <see cref="CollectionView"/> of given section.
    /// Default value is 44
    /// </summary>
    /// <param name="view">CollectionView</param>
    /// <param name="section">Section number</param>
    /// <returns>Height</returns>
    public virtual double GetGroupHeaderHeight(CollectionView view, int section)
    {
        return view.GroupHeaderTemplate != null ? 44 : 0;
    }

    /// <summary>
    /// Footer height.
    /// Default value is 44
    /// </summary>
    /// <param name="view">CollectionView</param>
    /// <returns>Height</returns>
    public virtual double GetFooterHeight(CollectionView view)
    {
        return view.FooterTemplate == null ? 0 : 44;
    }

    /// <summary>
    /// Cell height per given index and section
    /// </summary>
    /// <param name="view"><see cref="CollectionView"/></param>
    /// <param name="section">Section</param>
    /// <param name="index">Index</param>
    /// <returns>Height</returns>
    public virtual double GetCellHeight(CollectionView view, int section, int index)
    {
        return 44;
    }

    /// <summary>
    /// Total height of the CollectionView.
    /// </summary>
    /// <param name="view">CollectionView</param>
    /// <returns>Total height</returns>
    public virtual double GetTotalHeight(CollectionView view)
    {
        return view.Bounds.Height;
    }

    #endregion

    #region Drawing

    /// <summary>
    /// Draws header of the CollectionView into given page.
    /// </summary>
    /// <param name="view">CollectionView</param>
    /// <param name="page">Pdf Page</param>
    /// <param name="bounds">Bounds of the header area</param>
    /// <param name="scaleFactor">Scale factor to convert Maui units to Pdf units.</param>
    public virtual void DrawHeader(CollectionView view, XGraphics page, XRect bounds, double scaleFactor)
    { }

    /// <summary>
    /// Draws footer of the CollectionView into given page.
    /// </summary>
    /// <param name="view">CollectionView</param>
    /// <param name="page">Pdf Page</param>
    /// <param name="bounds">Bounds of the footer area</param>
    /// <param name="scaleFactor">Scale factor to convert Maui units to Pdf units.</param>
    public virtual void DrawFooter(CollectionView view, XGraphics page, XRect bounds, double scaleFactor)
    { }

    /// <summary>
    /// Draws group header of the CollectionView into given page.
    /// </summary>
    /// <param name="view">CollectionView</param>
    /// <param name="section">Section</param>
    /// <param name="page">Pdf Page</param>
    /// <param name="bounds">Bounds of the group header area</param>
    /// <param name="scaleFactor">Scale factor to convert Maui units to Pdf units.</param>
    public virtual void DrawGroupHeader(CollectionView view, int section, XGraphics page, XRect bounds,
        double scaleFactor)
    {
        if (view.ItemsSource is IEnumerable<IGrouping<string, object>> dataSource)
        {
            var headerText = dataSource.ElementAt(section).Key;

            var font = new XFont(GlobalFontSettings.DefaultFontName, 14 * scaleFactor);
            var textColor = Application.Current?.RequestedTheme == AppTheme.Dark ? XBrushes.White : XBrushes.Black;
            page.DrawString(headerText, font, textColor, bounds,
                XStringFormats.CenterLeft);
        }
    }

    /// <summary>
    /// Draws the cell in the CollectionView into given page.
    /// </summary>
    /// <param name="view">CollectionView</param>
    /// <param name="section">Section</param>
    /// <param name="row">Row index</param>
    /// <param name="page">Pdf Page</param>
    /// <param name="bounds">Bounds of the cell area</param>
    /// <param name="scaleFactor">Scale factor to convert Maui units to Pdf units.</param>
    public virtual void DrawCell(CollectionView view, int section, int row, XGraphics page, XRect bounds,
        double scaleFactor)
    {
        object bindingContext;
        if (!view.IsGrouped)
        {
            bindingContext = view.ItemsSource?.Cast<object>().ElementAt(row);
        }
        else
        {
            var groundSource = view.ItemsSource?.Cast<IEnumerable>().ElementAt(section);
            bindingContext = groundSource?.Cast<object>().ElementAt(row);
        }

        if (bindingContext != null)
        {
            var font = new XFont(GlobalFontSettings.DefaultFontName, 14 * scaleFactor);
            var textColor = Application.Current?.RequestedTheme == AppTheme.Dark ? XBrushes.White : XBrushes.Black;
            page.DrawString(bindingContext.ToString(), font, textColor, bounds,
                XStringFormats.CenterLeft);
        }
    }

    #endregion
}