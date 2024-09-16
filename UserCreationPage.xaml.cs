namespace GardenCompendium;

public partial class UserCreationPage : ContentPage
{
	User user;
	public UserCreationPage()
	{
		InitializeComponent();
	}

	private async void OnSubmitClicked(object sender, EventArgs e)
	{
		user = new User(FirstNameEntry.Text, LastNameEntry.Text, PostalCodeEntry.Text);
		await UserService.SaveUserAsync(user);
		await Navigation.PushAsync(new MainPage());
    }
}