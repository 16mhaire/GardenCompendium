using System.Collections.ObjectModel;
using System.ComponentModel;


namespace GardenCompendium
{
    public partial class MainPage : ContentPage
    {
        private string _zipCode;
        public string ZipCode
        {
            get => _zipCode;
            set
            {
                if (_zipCode != value)
                {
                    _zipCode = value;
                    OnPropertyChanged(nameof(ZipCode));
                }
            }
        }
        public ObservableCollection<Plant> Plants { get; set; }
        public MainPage()
        {
            InitializeComponent();

            Plants = new ObservableCollection<Plant>();
            MyPlantsListView.ItemsSource = Plants;
            BindingContext = this;
        }

        private async void OnNavigateToAddressEntryPageClicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new AddressEntryPage(this));
        }
        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

    }


}
