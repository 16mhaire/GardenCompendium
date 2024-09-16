using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;

namespace GardenCompendium
{
    public class User
    {
        private string _firstName;
        private string _lastName;
        private string? _zipCode;
        private string? _zone;
        private List<Plant>? _plants;

        public string FirstName { get; set; }
        public string LastName { get; set; }

        public string? ZipCode 
        {  get { return _zipCode; }
            set { if (_zipCode != value) { _zipCode = value; } } }
        public string? Zone { get { return _zone; } set { _zone = value; } }

        public List<Plant> Plants { get { return _plants; } set { _plants = value; } }

        public User(string firstName, string lastName, string zipCode) 
        { 
            _firstName = firstName;
            _lastName = lastName;
            _zipCode = zipCode;
            _plants = new List<Plant>();

            string zoneTask = AddressService.GetZoneAsync(zipCode).Result;
        }

        public async void AddPlantToUser(Plant plant)
        {
            Plants.Add(plant);
            await UserService.SaveUserAsync(this);
        }
    }
}
