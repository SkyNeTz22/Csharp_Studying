using ShopApp.MVVM.Model;
using System;
using System.Collections.ObjectModel;
using System.Net.Http;

namespace ShopApp.MVVM.ViewModel
{
    internal class MainViewModel
    {
        public ObservableCollection<User> Users { get; set; }

        public MainViewModel() {
            Users = new ObservableCollection<User>();
            // Call the async method to populate the Users collection
            InitializeUsersAsync();
        }

        private async void InitializeUsersAsync()
        {
            try
            {
                // Call the async method to get users from the API
                var users = await Core.UsersManager.GetAllUsers();

                // Update the Users collection once data is retrieved
                foreach (var user in users)
                {
                    Users.Add(user);
                }
            }
            catch (Exception ex)
            {
                // Handle any exceptions that may occur during the API call
                Console.WriteLine($"An error occurred: {ex.Message}");
            }
        }
    }
}
