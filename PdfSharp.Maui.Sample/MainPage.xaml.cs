using PdfSharp.Maui.Sample.Models;
using Path = System.IO.Path;

namespace PdfSharp.Maui.Sample;

[XamlCompilation(XamlCompilationOptions.Compile)]
public partial class MainPage
{
    public MainPage()
    {
        InitializeComponent();
        _datePicker.Date = DateTime.Now;
        _datePicker2.Date = DateTime.Now;
        picker.SelectedIndex = 0;

        var cities = new List<City>
        {
            new() { Name = "Stockholm", Country = "Sweden", Continent = "Europa" },
            new() { Name = "Paris", Country = "France", Continent = "Europa" },
            new() { Name = "Barcelona", Country = "Spain", Continent = "Europa" },
            new() { Name = "Tokyo", Country = "Japan", Continent = "Asia" },
            new() { Name = "İstanbul", Country = "Turkey", Continent = "Asia" },
            new() { Name = "Jakarta", Country = "Indonesia", Continent = "Asia" },
            new() { Name = "Nairobi", Country = "Kenya", Continent = "Africa" },
            new() { Name = "Cairo", Country = "Egypt", Continent = "Africa" }
        };
        _listView.ItemsSource = cities.GroupBy(t => t.Continent);
        _collectionView.ItemsSource = cities;
    }

    private void GeneratePDF(object sender, EventArgs e)
    {
        var pdfManager = new PdfManager();
        var pdf = pdfManager.GeneratePdfFromView(Content, PageOrientation.Portrait, PageSize.A4, PdfStyle.PlatformSpecific);

        var path = DeviceInfo.Platform == DevicePlatform.Android ? "/storage/emulated/0/Download" : Path.GetTempPath();
        Utils.PdfSave.Save(pdf, "SinglePage.pdf", path);


        Application.Current.MainPage.DisplayAlert(
            "Success",
            $"Your PDF generated at {path}",
            "OK");
    }
}