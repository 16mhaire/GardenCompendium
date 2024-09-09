namespace GardenCompendium
{
    public partial class AppShell : Shell
    {
        public AppShell()
        {
            InitializeComponent();
        }

        private async void OnAddAddressButtonClicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new AddressEntryPage());
        }
    }
}
