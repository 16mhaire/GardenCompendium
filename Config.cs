using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GardenCompendium
{
    public static class Config
    {
        public static ApiKeys ApiKeys { get; private set; }
        public static ApiUrls ApiUrls { get; private set; }
        private static string configFile = "C:/Users/techp/source/repos/GardenCompendium/config.json";

        static Config()
        {
            LoadConfig();
        }

        private static void LoadConfig()
        {
            string configJson = File.ReadAllText(configFile);
            
            var configData = JsonConvert.DeserializeObject<ConfigData>(configJson);

            ApiKeys = configData.ApiKeys;
            ApiUrls = configData.ApiUrls;
        }

        private class ConfigData
        {
            public ApiKeys ApiKeys { get; set; }
            public ApiUrls ApiUrls { get; set; }
        }
    }

    public class ApiKeys
    {
        public string PerenualApiKey { get; set; }
        public string RapidApiKey { get; set; }
    }

    public class ApiUrls
    {
        public string PerenualUrl { get; set; }
        public string RapidUrl { get; set; }
    }
}
