﻿namespace GardenCompendium
{
    public partial class AppShell : Shell
    {
        public AppShell()
        {
            InitializeComponent();
            SetLaunchPageAsync();
        }

        private async void OnAddAddressButtonClicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new AddressEntryPage());
        }

        private async void SetLaunchPageAsync()
        {
            await UserService.Instance.GetUserAsync();
            User user = UserService.Instance.CurrentUser;

            if (user != null)
            {
                await Navigation.PushAsync(new MainPage());// User exists, navigate to main app shell
            }
            else
            {
                await Navigation.PushAsync(new UserCreationPage()); // No user, navigate to user creation page
            }
        }
    }
}
