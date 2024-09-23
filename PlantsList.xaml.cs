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

    private async void OnAddToUserPlantsClicked(object sender, EventArgs e)
    {
        var button = (Button)sender;
        var selectedPlant = (Plant)button.CommandParameter;

        User user = await UserService.GetUserAsync();

        if (user.Plants == null)
        {
            user.Plants = new ObservableCollection<Plant>();
        }

        if (user.Plants.Contains(selectedPlant))
        {
            await DisplayAlert("Alert", "This plant is already in your garden!", "Ok");
            return;
        }

        user.Plants.Add(selectedPlant);

        await UserService.SaveUserAsync(user);

        OnPropertyChanged(nameof(user.Plants));

        await DisplayAlert("Success", $"{selectedPlant.Name} has been added to your plant list!", "OK");
    }
}