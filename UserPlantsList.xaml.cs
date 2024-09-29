using System.Collections.ObjectModel;

namespace GardenCompendium;
public partial class UserPlantsList : ContentPage
{
    User _user = UserService.Instance.CurrentUser;
    public UserPlantsList()
    {
        InitializeComponent();
        BindingContext = this;
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
        /*await UserService.Instance.GetUserAsync();
        User user = UserService.Instance.CurrentUser;*/
        if (_user != null)
        {
            UserPlants = new ObservableCollection<Plant>(_user.Plants);
            OnPropertyChanged(nameof(UserPlants));
        }
        else
        {
            _user = UserService.Instance.CurrentUser;
            UserPlants = new ObservableCollection<Plant>(_user.Plants);
            OnPropertyChanged(nameof(UserPlants));
        }
    }

    public event EventHandler<Plant> PlantDeleted;
    private async void OnDeletePlantClicked(object sender, EventArgs e)
    {
        // Get the plant from the button's CommandParameter
        var plantToDelete = (sender as Button)?.BindingContext as Plant;
        if (plantToDelete != null)
        {
            UserPlants.Remove(plantToDelete);
            if (_user != null)
            {
                // Use the delete method from the User class
                _user.DelPlantFromUser(plantToDelete);

                // Notify other parts of the app about the deletion
                MessagingCenter.Send(this, "PlantDeleted", plantToDelete);
            }
        }
    }
}
