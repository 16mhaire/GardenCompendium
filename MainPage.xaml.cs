using System.Collections.ObjectModel;
using System.Collections.Generic;
using System.ComponentModel;
using Microsoft.Maui.Layouts;


namespace GardenCompendium
{
    public partial class MainPage : ContentPage
    {
        //public ObservableCollection<Plant> UserPlants { get; set; }
        public User user { get; set; }
        public ObservableCollection<Plant> plants { get; set; }
        public MainPage()
        {
            InitializeComponent();
            BindingContext = this;

            plants = new ObservableCollection<Plant>();

            AddPlantToLayout("Corn", 200, 200);

            // Subscribe to PlantDeleted message
            MessagingCenter.Subscribe<UserPlantsList, Plant>(this, "PlantDeleted", (sender, deletedPlant) =>
            {
                //user?.Plants.Remove(deletedPlant); // Directly remove from User.Plants
                plants.Remove(deletedPlant);
            });
            OnAppearing();
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();

            await UserService.Instance.GetUserAsync();
            user = UserService.Instance.CurrentUser;

            // Ensure binding is kept live, but no need to recreate the collection
            if (user != null && user.Plants != null)
            {
                // Synchronize the UI collection with User.Plants
                plants.Clear();  // Clear the existing collection
                foreach (var plant in user.Plants)
                {
                    plants.Add(plant);  // Add all plants from User.Plants to the UI collection
                }
                OnPropertyChanged(nameof(plants));  // Notify the UI to refresh
            }
        }
        private void AddPlantToLayout(string plantName, double x, double y)
        {
            var plantLabel = new Label
            {
                Text = plantName,
                BackgroundColor = Colors.LightYellow,
                WidthRequest = 100,
                HeightRequest = 50
            };

            var tapGestureRecognizer = new TapGestureRecognizer();
            tapGestureRecognizer.Tapped += (s, e) =>
            {
                DisplayAlert("Plant Info", $"Plant: {plantName}", "OK");
            };
            plantLabel.GestureRecognizers.Add(tapGestureRecognizer);

            AbsoluteLayout.SetLayoutBounds(plantLabel, new Rect(x, y, 100, 50));
            AbsoluteLayout.SetLayoutFlags(plantLabel, AbsoluteLayoutFlags.None);

            GardenLayout.Children.Add(plantLabel);
        }
        /*private async Task LoadUserPlants()
        {
            User user = await UserService.GetUserAsync();
            if (user != null)
            {
                UserPlants = user.Plants;

            }
        }*/
        private void OnDragStarting(object sender, DragStartingEventArgs e)
        {
            // Get the plant bound to the image and pass it in drag data
            var plant = (sender as VisualElement)?.BindingContext as Plant;

            if (plant != null)
            {
                e.Data.Properties.Add("Plant", plant);
            }
        }

        private void OnDrop(object sender, DropEventArgs e)
        {
            if (e.Data.Properties.ContainsKey("Plant"))
            {
                var droppedPlant = e.Data.Properties["Plant"] as Plant;

                // Add the plant's image to the AbsoluteLayout at the drop position
                var position = e.GetPosition(GardenLayout) ?? new Point(0,0);

                AddPlantImageToLayout(droppedPlant, position.X, position.Y);
            }
        }

        private void AddPlantImageToLayout(Plant plant, double x, double y)
        {
            var plantImage = new Microsoft.Maui.Controls.Image
            {
                Source = plant.DefaultImage?.RegularUrl,
                WidthRequest = 100,
                HeightRequest = 100
            };

            // Add the plant image to the AbsoluteLayout
            AbsoluteLayout.SetLayoutBounds(plantImage, new Rect(x, y, 100, 100));
            AbsoluteLayout.SetLayoutFlags(plantImage, AbsoluteLayoutFlags.None);

            GardenLayout.Children.Add(plantImage);
        }
    }
}
