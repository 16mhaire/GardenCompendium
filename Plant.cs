using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GardenCompendium
{
    public static class PlantService
    {
        private static readonly HttpClient _httpClient = new HttpClient();
        public static async Task<List<Plant>> GetPlantAsync(string url)
        {
            try
            {
                var response = await _httpClient.GetAsync(url);
                response.EnsureSuccessStatusCode();

                string jsonResponse = await response.Content.ReadAsStringAsync();
                var plantData = JsonConvert.DeserializeObject<PlantData>(jsonResponse);

                return plantData.Data.Count > 0 ? plantData.Data : null;
            }
            catch (Exception ex)
            {
                return null;
            }
        }
    }
        


    public class Plant
    {
        [JsonProperty("id")]
        public int Id { get; set; }

        [JsonProperty("common_name")]
        public string Name { get; set; }

        [JsonProperty("scientific_name")]
        public List<string> ScientificName { get; set; }

        [JsonProperty("other_name")]
        public List<string> OtherName { get; set; }

        [JsonProperty("cycle")]
        public string Cycle { get; set; }

        [JsonProperty("watering")]
        public string Watering { get; set; }

        [JsonProperty("sunlight")]
        public List<string> Sunlight { get; set; }

        [JsonProperty("default_image")]
        public Image DefaultImage { get; set; }

        private async Task<List<Plant>> getPlantAsync(string url)
        {
            using (HttpClient client = new HttpClient())
            {
                var response = await client.GetAsync(url);
                response.EnsureSuccessStatusCode();

                string jsonResponse = await response.Content.ReadAsStringAsync();
                var plantData = JsonConvert.DeserializeObject<PlantData>(jsonResponse);

                if (plantData != null)
                {
                    return plantData.Data;
                }

                return null;
            }
        }
    }

    public class PlantData
    {
        [JsonProperty("data")]
        public List<Plant> Data { get; set; }
    }

    public class DefaultImage
    {
        [JsonProperty("image_id")]
        public int ImageId { get; set; }

        [JsonProperty("license")]
        public int License { get; set; }

        [JsonProperty("license_name")]
        public string LicenseName { get; set; }

        [JsonProperty("license_url")]
        public string LicenseUrl { get; set; }

        [JsonProperty("original_url")]
        public string OriginalUrl { get; set; }

        [JsonProperty("regular_url")]
        public string RegularUrl { get; set; }

        [JsonProperty("medium_url")]
        public string MediumUrl { get; set; }

        [JsonProperty("small_url")]
        public string SmallUrl { get; set; }

        [JsonProperty("thumbnail")]
        public string Thumbnail { get; set; }
    }
}
