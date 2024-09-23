using System.Collections.ObjectModel;

namespace GardenCompendium;
public partial class UserPlantsList : ContentPage
{
    public UserPlantsList()
    {
        InitializeComponent();
        BindingContext = this;
        //LoadUserPlants();
    }
    public ObservableCollection<Plant> UserPlants { get; set; }

    protected override async void OnAppearing()
    {
        base.OnAppearing();

        // Fetch the user data and force the UI to update
        await LoadUserPlants();
    }

    private async Task LoadUserPlants()
    {
        User user = await UserService.GetUserAsync();
        if (user != null)
        {
            UserPlants = new ObservableCollection<Plant>(user.Plants);
            OnPropertyChanged(nameof(UserPlants));
           
        }
    }
}
