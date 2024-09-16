using System.Collections.ObjectModel;
using System.Collections.Generic;
using System.ComponentModel;
using Microsoft.Maui.Layouts;


namespace GardenCompendium
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
            AddPlantToLayout("Rose", 200, 200);
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
    }
}
