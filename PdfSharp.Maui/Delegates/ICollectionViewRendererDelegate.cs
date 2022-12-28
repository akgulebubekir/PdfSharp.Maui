namespace PdfSharp.Maui.Delegates;

internal interface ICollectionViewRendererDelegate<in T> where T : View
{
    #region Items Calculation

    int GetNumberOfSections(T view);
    int GetNumberOfRowsInSection(T view, int section);

    #endregion

    #region Size calculation

    double GetHeaderHeight(T view);
    double GetGroupHeaderHeight(T view, int section);
    double GetFooterHeight(T view);
    double GetCellHeight(T view, int section, int index);
    double GetTotalHeight(T view);

    #endregion

    #region Drawing

    void DrawHeader(T view, XGraphics page, XRect bounds, double scaleFactor);
    void DrawFooter(T view, XGraphics page, XRect bounds, double scaleFactor);

    void DrawGroupHeader(T view, int section, XGraphics page, XRect bounds,
        double scaleFactor);

    void DrawCell(T view, int section, int row, XGraphics page, XRect bounds,
        double scaleFactor);

    #endregion
}