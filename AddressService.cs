using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GardenCompendium
{
    public static class AddressService
    {
        public static async Task<string> GetZoneAsync(string postalCode)
        {
            /*var client = new HttpClient();
            var request = new HttpRequestMessage
            {
                Method = HttpMethod.Get,
                RequestUri = new Uri("https://plant-hardiness-zone.p.rapidapi.com/zipcodes/" + postalCode),
                Headers =
    {
        { "x-rapidapi-key", Config.ApiKeys.RapidApiKey },
        { "x-rapidapi-host", Config.ApiUrls.RapidUrl },
    },
            };
            using (var response = await client.SendAsync(request))
            {
                response.EnsureSuccessStatusCode();
                string zoneString = await response.Content.ReadAsStringAsync();
                var zoneData = JsonConvert.DeserializeObject<ZoneInfo>(zoneString);
                if (zoneData != null)
                {
                    return zoneData.Zone;
                }
                else
                {
                    return null;
                }*/
            return "7";
            }
        }

    


    public class ZoneInfo()
    {
        [JsonProperty("hardiness_zone")]
        public string Zone { get; set; }
        [JsonProperty("zipcode")]
        public int Zipcode { get; set; }
    }
}
