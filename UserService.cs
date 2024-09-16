using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.Json;

namespace GardenCompendium
{
    public static class UserService
    {
        public static readonly string filePath = Path.Combine(FileSystem.AppDataDirectory, "user.json");

        public static async Task SaveUserAsync(User user)
        {
            string json = JsonSerializer.Serialize(user);
            await File.WriteAllTextAsync(filePath, json);
        }

        public static async Task<User> GetUserAsync()
        {
            if (File.Exists(filePath))
            {
                string json = await File.ReadAllTextAsync(filePath);
                return JsonSerializer.Deserialize<User>((json));
            }
            return null;
        }
    }
}
