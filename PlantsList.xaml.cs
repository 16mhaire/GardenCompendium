using System.Numerics;
using System.Collections.ObjectModel;
using System.ComponentModel;

namespace GardenCompendium;

public partial class PlantsList : ContentPage, INotifyPropertyChanged
{
	public ObservableCollection<Plant> Plants { get; set; }

    public PlantsList()
	{
		InitializeComponent();

		//Task<List<Plant>> plants = PlantService.GetPlantAsync(Config.ApiUrls.PerenualUrl);
		Plants = new ObservableCollection<Plant>();
		BindingContext = this;
		LoadPlants();
    }

	private async void LoadPlants()
	{
        Task<List<Plant>> plantTask = PlantService.GetPlantAsync(Config.ApiUrls.PerenualUrl);
		List<Plant>	plants = await plantTask;

		foreach (Plant plant in plants)
		{
            if (plant.DefaultImage != null)
            {
                System.Diagnostics.Debug.WriteLine($"Image URL: {plant.DefaultImage.RegularUrl}");
            }
            Plants.Add(plant);
		}
    }

    public event PropertyChangedEventHandler PropertyChanged;
    protected void OnPropertyChanged(string propertyName)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}