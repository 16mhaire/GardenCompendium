using System;
using System.Globalization;
using System.Text.Json.Serialization;
using Microsoft.Maui.Controls;
using Newtonsoft.Json;
using CsvHelper;
using CsvHelper.Configuration;
using System.Security.Cryptography.X509Certificates;

namespace GardenCompendium;

public partial class AddressEntryPage : ContentPage
{
    private MainPage _mainPage;

    public AddressEntryPage(MainPage mainPage)
    {
        InitializeComponent();
        _mainPage = mainPage;
    }

    private async void OnSubmitClicked(object sender, EventArgs e)
    {
        string postalCode = PostalCodeEntry.Text;

        // Validate the input
        if (
            string.IsNullOrWhiteSpace(postalCode))
        {
            await DisplayAlert("Error", "Please fill in all the fields.", "OK");
            return;
        }

        getZoneAsync(postalCode);

        var plantZone = 7;
        // Generate a 1 plant suitable for this zone
        string perenualKey = "sk-u1y166986d6db54465954";
        string perenualURL = $"https://perenual.com/api/species-list?key={perenualKey}&hardiness={plantZone}";
        Plant plant = await PlantService.GetPlantAsync(perenualURL);

        if (plant != null)
        {
            await DisplayAlert("Plant Aquired", plant.Name, "OK");
            _mainPage.Plants.Add(plant);
            _mainPage.ZipCode = postalCode;

            await Navigation.PopAsync();
        }
        else
        {
            await DisplayAlert("Error", "Failed to fetch plant data.", "OK");
        }
    }


    private async Task<string> getZoneAsync(string postalCode)
    {
        var client = new HttpClient();
        var request = new HttpRequestMessage
        {
            Method = HttpMethod.Get,
            RequestUri = new Uri("https://plant-hardiness-zone.p.rapidapi.com/zipcodes/" + postalCode),
            Headers =
    {
        { "x-rapidapi-key", "17357bfdcbmsh1784dad2b3680abp14d5f2jsnd6ae13cfd017" },
        { "x-rapidapi-host", "plant-hardiness-zone.p.rapidapi.com" },
    },
        };
        using (var response = await client.SendAsync(request))
        {
            response.EnsureSuccessStatusCode();
            var body = await response.Content.ReadAsStringAsync();
            await DisplayAlert("Zone Info", body, "OK");
        }
        return null;
    }
}

