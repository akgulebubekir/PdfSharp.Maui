namespace PdfSharp.Maui.Attributes;

/// <summary>
/// Attribute to fetch PdfRenderers.
/// All custom user renderers should use this attribute for their renderers.
/// </summary>
[AttributeUsage(AttributeTargets.Class)]
public class PdfRendererAttribute : Attribute
{
    public Type ViewType { get; set; }
}