using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.Json;

namespace GardenCompendium
{
    public class UserService
    {
        private User _user;
        public User CurrentUser => _user;
        // Static variable to hold the single instance of UserService
        private static UserService _instance;

        // Lock object for thread safety
        private static readonly object _lock = new object();

        // Filepath for user data
        public static readonly string filePath = Path.Combine(FileSystem.AppDataDirectory, "user.json");

        // Private constructor to prevent instatiation from another class
        private UserService() { }

        // Public method to get the singleton instance
        public static UserService Instance
        {
            get
            {
                // Double-check locking to ensure thread safety
                if (_instance == null)
                {
                    lock (_lock)
                    {
                        if (_instance == null)
                        {
                            _instance = new UserService();
                        }
                    }
                }
                return _instance;
            }
        }
        public async Task SaveUserAsync(User user)
        {
            if (File.Exists(filePath))
            {
                File.Delete(filePath);
            }
            string json = JsonSerializer.Serialize(user);
            await File.WriteAllTextAsync(filePath, json);
        }

        public async Task<User> GetUserAsync()
        {
            if (File.Exists(filePath))
            {
                string json = await File.ReadAllTextAsync(filePath);
                _user = JsonSerializer.Deserialize<User>((json));
            }
            return null;
        }
    }
}
