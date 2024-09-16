using System.Collections.ObjectModel;

namespace GardenCompendium;
public partial class UserPlantsList : ContentPage
{
    public UserPlantsList()
    {
        InitializeComponent();
        LoadUserPlants();
    }
    public ObservableCollection<Plant> UserPlants { get; set; }

    private async void LoadUserPlants()
    {
        User user = await UserService.GetUserAsync();
        if (user != null)
        {
            UserPlants = new ObservableCollection<Plant>(user.Plants);
            BindingContext = this;
        }
    }
}
