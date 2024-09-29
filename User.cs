using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Reflection.Emit;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace GardenCompendium
{
    public class User : INotifyPropertyChanged
    {
        private string _firstName;
        private string _lastName;
        private string? _zipCode;
        private string? _zone;
        private ObservableCollection<Plant>? _plants;

        public string FirstName { get; set; }
        public string LastName { get; set; }

        public string? ZipCode 
        {  get { return _zipCode; }
            set 
            { 
                if (_zipCode != value) 
                { 
                    _zipCode = value; 
                    OnPropertyChanged();
                } 
            } 
        }
        public string? Zone { get { return _zone; } set { _zone = value; } }

        public ObservableCollection<Plant> Plants 
        { 
            get 
            { 
                return _plants ??= new ObservableCollection<Plant>(); 
            } 
            set 
            {
                if (_plants != value)
                {
                    _plants = value;
                    OnPropertyChanged();
                }

            } 
        }

        public User(string firstName, string lastName, string zipCode) 
        { 
            _firstName = firstName;
            _lastName = lastName;
            _zipCode = zipCode;
            _plants = new ObservableCollection<Plant>();
        }

        public async Task InitZoneAsync()
        {
            Zone = await AddressService.GetZoneAsync(ZipCode);
        }

        public async void AddPlantToUser(Plant plant)
        {
            Plants.Add(plant);
            await UserService.Instance.SaveUserAsync(this);
        }

        public async void DelPlantFromUser(Plant plant)
        {
            Plants.Remove(plant);
            await UserService.Instance.SaveUserAsync(this);
        }

        public event PropertyChangedEventHandler? PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string? propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
