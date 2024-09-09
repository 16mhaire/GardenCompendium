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
        private string _zipCode;
        private string _zone;
        private List<Plant> _plants;
        
        public string ZipCode 
        {  get { return _zipCode; }
            set { if (_zipCode != value) { _zipCode = value; } } }
        public string Zone { get { return _zone; } set { _zone = value; } }

        public List<Plant> Plants { get { return _plants; } set { _plants = value; } }

        public User() 
        { 
        
        }
    }
}
