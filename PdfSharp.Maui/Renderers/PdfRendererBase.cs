namespace PdfSharp.Maui.Renderers;

public abstract class PdfRendererBase<T> : PdfRendererBase where T : View
{
    protected readonly Dictionary<string, object> Properties = new();

    internal override void CreateLayout(XGraphics page, object view, XRect bounds, PdfStyle style, double scaleFactor)
    {
        if (style == PdfStyle.Uniform)
        {
            CreateUniformLayoutParameters(page, (T)view, bounds, scaleFactor);
        }
        else if (DeviceInfo.Platform == DevicePlatform.Android)
        {
            CreateAndroidLayoutParameters(page, (T)view, bounds, scaleFactor);
        }
        else if (DeviceInfo.Platform == DevicePlatform.iOS || DeviceInfo.Platform == DevicePlatform.macOS ||
                 DeviceInfo.Platform == DevicePlatform.MacCatalyst || DeviceInfo.Platform == DevicePlatform.tvOS ||
                 DeviceInfo.Platform == DevicePlatform.watchOS)
        {
            CreateIosLayoutParameters(page, (T)view, bounds, scaleFactor);
        }
        else if (DeviceInfo.Platform == DevicePlatform.WinUI)
        {
            CreateWindowsLayoutParameters(page, (T)view, bounds, scaleFactor);
        }

        CreateLayout(page, (T)view, bounds, scaleFactor);
    }

    protected bool HasBackgroundColor()
    {
        return HasProperty(ViewProperties.BackgroundColor) &&
               GetProperty<XBrush>(ViewProperties.BackgroundColor).IsNotDefault();
    }

    protected T1 GetProperty<T1>(string name)
    {
        return (T1)Properties[name];
    }

    protected bool HasProperty(string key)
    {
        return Properties.ContainsKey(key);
    }

    /// <summary>
    ///     Draws the control into the page
    /// </summary>
    /// <param name="page">Pdf page</param>
    /// <param name="view">Maui control to reach its properties</param>
    /// <param name="bounds">control bounds in the page(in millimeters)</param>
    /// <param name="scaleFactor">bound to millimeter ratio</param>
    protected abstract void CreateLayout(XGraphics page, T view, XRect bounds, double scaleFactor);

    protected virtual void CreateAndroidLayoutParameters(XGraphics page, T view, XRect bounds, double scaleFactor)
    { }

    protected virtual void CreateIosLayoutParameters(XGraphics page, T view, XRect bounds, double scaleFactor)
    { }

    protected virtual void CreateWindowsLayoutParameters(XGraphics page, T view, XRect bounds, double scaleFactor)
    { }

    protected virtual void CreateUniformLayoutParameters(XGraphics page, T view, XRect bounds, double scaleFactor)
    { }
}

public abstract class PdfRendererBase
{
    internal abstract void CreateLayout(XGraphics page, object view, XRect bounds, PdfStyle style, double scaleFactor);
}