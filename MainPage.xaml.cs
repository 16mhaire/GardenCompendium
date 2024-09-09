using System.Collections.ObjectModel;
using System.ComponentModel;


namespace GardenCompendium
{
    public partial class MainPage : ContentPage
    {   
        public ObservableCollection<Plant> Plants { get; set; }
        public MainPage()
        {
            InitializeComponent();

            Plants = new ObservableCollection<Plant>();
            MyPlantsCollectionView.ItemsSource = Plants;
            BindingContext = this;
        }

        private async void OnNavigateToAddressEntryPageClicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new AddressEntryPage(this));
        }
       /* public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
       */
    }


}
