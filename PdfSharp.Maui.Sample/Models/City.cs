namespace PdfSharp.Maui.Sample.Models;

internal class City
{
    public string Name { get; init; }
    public string Country { get; init; }
    public string Continent { get; init; }

    public override string ToString()
    {
        return Name;
    }
}