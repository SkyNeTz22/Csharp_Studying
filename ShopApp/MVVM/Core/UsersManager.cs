using Newtonsoft.Json;
using ShopApp.MVVM.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace ShopApp.MVVM.Core
{
    internal class UsersManager
    {
        public static async Task<ObservableCollection<User>> GetAllUsers()
        {
            using (HttpClient client = new HttpClient())
            {
                try
                {
                    // Define the API endpoint URL
                    string apiUrl = "https://localhost:7147/Users"; // Replace with your API URL

                    // Make the GET request to the API
                    HttpResponseMessage response = await client.GetAsync(apiUrl);

                    // Check if the request was successful (status code 200)
                    if (response.IsSuccessStatusCode)
                    {
                        // Read the response content as a string
                        string apiResponse = await response.Content.ReadAsStringAsync();
                        List<User> userList = JsonConvert.DeserializeObject<List<User>>(apiResponse);

                        // Convert the list to an ObservableCollection and return it
                        return new ObservableCollection<User>(userList);
                    }
                    else
                    {
                        Console.WriteLine($"HTTP Error: {response.StatusCode}");
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"An error occurred: {ex.Message}");
                    // Handle the exception here, you can return an empty ObservableCollection or throw the exception as needed.
                }
            }

            // Return an empty ObservableCollection if something went wrong
            return new ObservableCollection<User>();
        }

        public static async Task<Dictionary<string, string>> GetPassByEmail(string email)
        {
            using (HttpClient client = new HttpClient())
            {
               try
                {
                    // Define the API endpoint URL
                    string apiUrl = $"https://localhost:7147/Users/GetUserPass/{email}"; // Replace with your API URL

                    // Make the GET request to the API
                    HttpResponseMessage response = await client.GetAsync(apiUrl);

                    // Check if the request was successful (status code 200)
                    if (response.IsSuccessStatusCode)
                    {
                        // Read the response content as a string
                        string apiResponse = await response.Content.ReadAsStringAsync();
                        Dictionary<string, string> userPassDict = JsonConvert.DeserializeObject<Dictionary<string, string>>(apiResponse);

                        // Convert the list to an ObservableCollection and return it
                        return userPassDict;
                    }
                    else
                    {
                        Console.WriteLine($"HTTP Error: {response.StatusCode}");
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"An error occurred: {ex.Message}");
                    // Handle the exception here, you can return an empty ObservableCollection or throw the exception as needed.
                }
            }

            // Return an empty ObservableCollection if something went wrong
            return null;
        }

        public static async Task<Dictionary<string, int>> GetUserIdByEmail(string email)
        {
            using (HttpClient client = new HttpClient())
            {
                try
                {
                    // Define the API endpoint URL
                    string apiUrl = $"https://localhost:7147/Users/GetUserIdByEmail/{email}"; // Replace with your API URL

                    // Make the GET request to the API
                    HttpResponseMessage response = await client.GetAsync(apiUrl);

                    // Check if the request was successful (status code 200)
                    if (response.IsSuccessStatusCode)
                    {
                        // Read the response content as a string
                        string apiResponse = await response.Content.ReadAsStringAsync();
                        Dictionary<string, int> userIdDict = JsonConvert.DeserializeObject<Dictionary<string, int>>(apiResponse);

                        // Convert the list to an ObservableCollection and return it
                        return userIdDict;
                    }
                    else
                    {
                        Console.WriteLine($"HTTP Error: {response.StatusCode}");
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"An error occurred: {ex.Message}");
                    // Handle the exception here, you can return an empty ObservableCollection or throw the exception as needed.
                }
            }

            // Return an empty ObservableCollection if something went wrong
            return null;
        }

        public static async Task<int> UpdateUserPassByEmail(string email, Dictionary<string, string> updateDict)
        {
            using (HttpClient client = new HttpClient())
            {
                try
                {
                    // Define the API endpoint URL
                    string apiUrl = $"https://localhost:7147/Users/UpdateUserPasswordByEmail/{email}"; // Replace with your API URL
                    // Serialize the updateDict to JSON and create a StringContent object
                    string jsonRequest = JsonConvert.SerializeObject(updateDict);

                    // Create a StringContent with the JSON data
                    var content = new StringContent(jsonRequest, Encoding.UTF8, "application/json");

                    // Make the POST request to the API with the JSON content
                    HttpResponseMessage response = await client.PatchAsync(apiUrl, content);

                    // Check if the request was successful (status code 200) and return it
                    if (response.IsSuccessStatusCode)
                    {
                        return 200;
                    }
                    else
                    {
                        Console.WriteLine($"HTTP Error: {response.StatusCode}");
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"An error occurred: {ex.Message}");
                    // Handle the exception here, you can return null or throw the exception as needed.
                }
            }

            // Return 500 if something went wrong
            return 500;
        }

        public static async Task<int> UpdateUserPassById(string userId, Dictionary<string, string> updateDict)
        {
            using (HttpClient client = new HttpClient())
            {
                try
                {
                    // Define the API endpoint URL
                    string apiUrl = $"https://localhost:7147/Users/UpdateUserPasswordById/{userId}"; // Replace with your API URL
                    // Serialize the updateDict to JSON and create a StringContent object
                    string jsonRequest = JsonConvert.SerializeObject(updateDict);

                    // Create a StringContent with the JSON data
                    var content = new StringContent(jsonRequest, Encoding.UTF8, "application/json");

                    // Make the POST request to the API with the JSON content
                    HttpResponseMessage response = await client.PatchAsync(apiUrl, content);

                    // Check if the request was successful (status code 200) and return it
                    if (response.IsSuccessStatusCode)
                    {
                        return 200;
                    }
                    else
                    {
                        Console.WriteLine($"HTTP Error: {response.StatusCode}");
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"An error occurred: {ex.Message}");
                    // Handle the exception here, you can return null or throw the exception as needed.
                }
            }

            // Return 500 if something went wrong
            return 500;
        }

    }
}
