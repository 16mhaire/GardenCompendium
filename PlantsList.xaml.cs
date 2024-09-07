using System.Numerics;

namespace GardenCompendium;

public partial class PlantsList : ContentPage
{
	public PlantsList()
	{
		InitializeComponent();

        Task<List<Plant>> plants = PlantService.GetPlantAsync(Config.ApiUrls.PerenualUrl);

    }
}