using System;
using System.Globalization;
using System.Text.Json.Serialization;
using Microsoft.Maui.Controls;
using Newtonsoft.Json;
using CsvHelper;
using CsvHelper.Configuration;
using System.Security.Cryptography.X509Certificates;
using System.Collections.ObjectModel;

namespace GardenCompendium;

public partial class AddressEntryPage : ContentPage
{
    private User? _user;

    public AddressEntryPage() // Constructor
    {
        InitializeComponent();
    }

    public AddressEntryPage(User user)
    {
        InitializeComponent();
    }

    private async void OnSubmitClicked(object sender, EventArgs e)
    {
        string postalCode = PostalCodeEntry.Text;

        // Validate the input
        if (
            string.IsNullOrWhiteSpace(postalCode))
        {
            await DisplayAlert("Error", "Please provide a valid postal code.", "OK");
            return;
        }
        // We only want to use the geocode api sparingly, we only have 150 free queries per month.
        //string plantZone = await getZoneAsync(postalCode);
        string plantZone = "7";

        // Generate a 1 plant suitable for this zone
        string perenualKey = Config.ApiKeys.PerenualApiKey;
        string perenualURL = $"https://perenual.com/api/species-list?key={perenualKey}&hardiness={plantZone}";
        List<Plant> plants = await PlantService.GetPlantAsync(perenualURL);
        //ObservableCollection<Plant> observablePlants = new ObservableCollection<Plant>(plants);

        if (plants != null)
        {
            await DisplayAlert("Plant Aquired", plants[0].Name, "OK");
            
            await Navigation.PopAsync();
        }
        else
        {
            await DisplayAlert("Error", "Failed to fetch plant data.", "OK");
        }
    }
}

