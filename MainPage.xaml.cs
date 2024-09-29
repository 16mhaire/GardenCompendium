using System.Collections.ObjectModel;
using System.Collections.Generic;
using System.ComponentModel;
using Microsoft.Maui.Layouts;


namespace GardenCompendium
{
    public partial class MainPage : ContentPage
    {
        public User user { get; set; }
        public ObservableCollection<Plant> plants { get; set; }
        public MainPage()
        {
            InitializeComponent();
            BindingContext = this;

            plants = new ObservableCollection<Plant>();

            // Subscribe to PlantDeleted message
            MessagingCenter.Subscribe<UserPlantsList, Plant>(this, "PlantDeleted", (sender, deletedPlant) =>
            {
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

        private void OnDragStarting(object sender, DragStartingEventArgs e)
        {
            // sender is DragGestureRecognizer, so get the image it's attached to
            var dragGestureRecognizer = sender as DragGestureRecognizer;
            var image = dragGestureRecognizer?.Parent as Microsoft.Maui.Controls.Image; // or your custom Image class

            if (image != null)
            {
                var plant = image.BindingContext as Plant;

                if (plant != null)
                {
                    e.Data.Properties.Add("Plant", plant);
                }
            }
        }

        private void OnDrop(object sender, DropEventArgs e)
        {
            if (e.Data.Properties.ContainsKey("Plant"))
            {
                var droppedPlant = e.Data.Properties["Plant"] as Plant;

                if (droppedPlant != null)
                {
                    // Add the plant's image to the AbsoluteLayout at the drop position
                    var position = e.GetPosition(GardenLayout) ?? new Point(0, 0);
                    AddPlantImageToLayout(droppedPlant, position.X, position.Y);
                } 
            }
        }

        private void AddPlantImageToLayout(Plant plant, double x, double y)
        {
            var plantImage = new Microsoft.Maui.Controls.Image
            {
                Source = plant.DefaultImage?.RegularUrl,
                WidthRequest = 100,
                HeightRequest = 100,
                IsVisible = true,
                Opacity = 1
            };

            var tapGestureRecognizer = new TapGestureRecognizer();
            tapGestureRecognizer.Tapped += (s, e) =>
            {
                DisplayAlert("Plant Info", $"Plant: Clicked", "OK");
            };
            plantImage.GestureRecognizers.Add(tapGestureRecognizer);

            // Add the plant image to the AbsoluteLayout
            AbsoluteLayout.SetLayoutBounds(plantImage, new Rect(x, y, 100, 100));
            AbsoluteLayout.SetLayoutFlags(plantImage, AbsoluteLayoutFlags.None);

            GardenLayout.Children.Add(plantImage);
        }
    }
}
