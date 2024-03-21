namespace PdfSharp.Maui.Delegates;

using System.Collections;

public class PdfListViewRendererDelegate : ICollectionViewRendererDelegate<ListView>
{
    #region Items Calculation

    /// <summary>
    /// Number of sections in <see cref="ListView"/>. If 'IsGroupingEnabled' is false, it will be 1 by default.
    /// </summary>
    /// <param name="view">ListView</param>
    /// <returns>Number of sections</returns>
    public virtual int GetNumberOfSections(ListView view)
    {
        if (!view.IsGroupingEnabled)
        {
            return 1;
        }

        return view.ItemsSource?.Cast<object>().Count() ?? 1;
    }

    /// <summary>
    /// Numbers of sections per group.If 'IsGroupingEnabled' is false, it will be equal to number of items in the <see cref="ListView"/> 
    /// </summary>
    /// <param name="view"><see cref="ListView"/></param>
    /// <param name="section">Section</param>
    /// <returns>># of rows in section</returns>
    public virtual int GetNumberOfRowsInSection(ListView view, int section)
    {
        if (!view.IsGroupingEnabled)
        {
            return view.ItemsSource?.Cast<object>().Count() ?? 0;
        }

        var sectionItems = view.ItemsSource?.Cast<IEnumerable>().ElementAt(section);

        return sectionItems?.Cast<object>().Count() ?? 0;
    }

    #endregion

    #region Size calculation

    /// <summary>
    /// Header height of the <see cref="ListView"/>
    /// Default value is 44
    /// </summary>
    /// <param name="view">ListView</param>
    /// <returns>Height</returns>
    public virtual double GetHeaderHeight(ListView view)
    {
        return view.HeaderTemplate == null ? 0 : 44;
    }

    /// <summary>
    /// Group header height for the <see cref="ListView"/> of given section.
    /// Default value is 44
    /// </summary>
    /// <param name="view">ListView</param>
    /// <param name="section">Section number</param>
    /// <returns>Height</returns>
    public virtual double GetGroupHeaderHeight(ListView view, int section)
    {
        return view.GroupHeaderTemplate != null ? 44 : 0;
    }

    /// <summary>
    /// Footer height.
    /// Default value is 44
    /// </summary>
    /// <param name="view">ListView</param>
    /// <returns>Height</returns>
    public virtual double GetFooterHeight(ListView view)
    {
        return view.FooterTemplate == null ? 0 : 44;
    }

    /// <summary>
    /// Cell height per given index and section
    /// </summary>
    /// <param name="view"><see cref="ListView"/></param>
    /// <param name="section">Section</param>
    /// <param name="index">Index</param>
    /// <returns>Height</returns>
    public virtual double GetCellHeight(ListView view, int section, int index)
    {
        return view.RowHeight > 0 ? view.RowHeight : 44;
    }

    /// <summary>
    /// Total height of the ListView.
    /// listView.Bounds.Height is default value.
    /// </summary>
    /// <param name="view">ListView</param>
    /// <returns>Total height</returns>
    public virtual double GetTotalHeight(ListView view)
    {
        return view.Bounds.Height;
    }

    #endregion

    #region Drawing

    /// <summary>
    /// Draws header of the ListView into given page.
    /// </summary>
    /// <param name="view">ListView</param>
    /// <param name="page">Pdf Page</param>
    /// <param name="bounds">Bounds of the header area</param>
    /// <param name="scaleFactor">Scale factor to convert Maui units to Pdf units.</param>
    public virtual void DrawHeader(ListView view, XGraphics page, XRect bounds, double scaleFactor)
    { }

    /// <summary>
    /// Draws footer of the ListView into given page.
    /// </summary>
    /// <param name="view">ListView</param>
    /// <param name="page">Pdf Page</param>
    /// <param name="bounds">Bounds of the footer area</param>
    /// <param name="scaleFactor">Scale factor to convert Maui units to Pdf units.</param>
    public virtual void DrawFooter(ListView view, XGraphics page, XRect bounds, double scaleFactor)
    { }

    /// <summary>
    /// Draws group header of the ListView into given page.
    /// </summary>
    /// <param name="view">ListView</param>
    /// <param name="section">Section</param>
    /// <param name="page">Pdf Page</param>
    /// <param name="bounds">Bounds of the group header area</param>
    /// <param name="scaleFactor">Scale factor to convert Maui units to Pdf units.</param>
    public virtual void DrawGroupHeader(ListView view, int section, XGraphics page, XRect bounds, double scaleFactor)
    {
        if (view.ItemsSource is IEnumerable<IGrouping<string, object>> dataSource)
        {
            var headerText = dataSource.ElementAt(section).Key;

            var font = new XFont(GlobalFontSettings.DefaultFontName, 14 * scaleFactor);
            var textColor = Application.Current?.RequestedTheme == AppTheme.Dark ? XBrushes.White : XBrushes.Black;
            page.DrawFormattedString(headerText, font, textColor, bounds,
                XStringFormats.CenterLeft);
        }
    }


    /// <summary>
    /// Draws the cell in the ListView into given page.
    /// </summary>
    /// <param name="view">ListView</param>
    /// <param name="section">Section</param>
    /// <param name="row">Row index</param>
    /// <param name="page">Pdf Page</param>
    /// <param name="bounds">Bounds of the cell area</param>
    /// <param name="scaleFactor">Scale factor to convert Maui units to Pdf units.</param>
    public virtual void DrawCell(ListView view, int section, int row, XGraphics page, XRect bounds,
        double scaleFactor)
    {
        object bindingContext;
        if (!view.IsGroupingEnabled)
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
            page.DrawFormattedString(bindingContext.ToString(), font, textColor, bounds,
                XStringFormats.CenterLeft);
        }
    }

    #endregion
}
