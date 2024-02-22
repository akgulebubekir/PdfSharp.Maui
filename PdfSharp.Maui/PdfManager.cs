using System.Reflection;
using PdfSharp.Maui.Attributes;
using PdfSharp.Maui.Contracts;
using PdfSharp.Pdf;

namespace PdfSharp.Maui;

public class PdfManager
{
    private readonly Dictionary<Type, Type> _renderers;

    /// <summary>
    /// 
    /// </summary>
    /// <param name="customFontProvider">Provides custom fonts for the PDF. Default font is "OpenSans"</param>
    /// <param name="rendererAssemblies">Provides user's custom renderer assemblies If user have your own renderer that you want layout yourself.</param>
    public PdfManager(ICustomFontProvider customFontProvider = null, IList<Assembly> rendererAssemblies = null)
    {
        // ReSharper disable once ConstantNullCoalescingCondition
        GlobalFontSettings.FontResolver ??= new DefaultFontProvider(customFontProvider);

        _renderers = new Dictionary<Type, Type>();
        rendererAssemblies ??= new List<Assembly>();
        var thisAssembly = typeof(PdfManager).GetTypeInfo().Assembly;

        if (!rendererAssemblies.Contains(thisAssembly))
        {
            rendererAssemblies.Add(thisAssembly);
        }

        foreach (var assembly in rendererAssemblies)
        {
            foreach (var typeInfo in assembly.DefinedTypes)
            {
                if (typeInfo.IsDefined(typeof(PdfRendererAttribute), false))
                {
                    var rInfo = typeInfo.GetCustomAttribute<PdfRendererAttribute>();
                    _renderers[rInfo!.ViewType] = typeInfo.AsType();
                }
            }
        }
    }

    /// <summary>
    /// Creates Pdf from a View
    /// </summary>
    /// <param name="view">View to create Pdf</param>
    /// <param name="orientation">Page orientation</param>
    /// <param name="size">Page paper size i.e: A4</param>
    /// <param name="style">Pdf style. <c>Uniform</c> style creates same result on all platforms, <c>PlatformSpecific</c> creates as it looks in current platform</param>
    /// <param name="resizeToFit">Resize view to fit page</param>
    /// <returns>PdfDocument</returns>
    /// <exception cref="InvalidOperationException"></exception>
    public PdfDocument GeneratePdfFromView(View view, PageOrientation orientation = PageOrientation.Landscape,
        PageSize size = PageSize.A4, PdfStyle style = PdfStyle.Uniform, bool resizeToFit = true)
    {
        var generator = new PdfGenerator(view, _renderers, orientation, size, style, resizeToFit);
        return generator.Generate();
    }
}