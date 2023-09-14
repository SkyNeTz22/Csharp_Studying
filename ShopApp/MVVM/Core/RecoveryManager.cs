using Newtonsoft.Json;
using ShopApp.MVVM.Model;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace ShopApp.MVVM.Core
{
    internal class RecoveryManager
    {
        public static async Task<List<Recovery>> GetRecoveryDetailsByEmail(string email)
        {
            using (HttpClient client = new HttpClient())
            {
                try
                {
                    // Define the API endpoint URL
                    string apiUrl = $"https://localhost:7147/Recovery/GetRecoveryDetails/{email}"; // Replace with your API URL

                    // Make the GET request to the API
                    HttpResponseMessage response = await client.GetAsync(apiUrl);

                    // Check if the request was successful (status code 200)
                    if (response.IsSuccessStatusCode)
                    {
                        // Read the response content as a string
                        string apiResponse = await response.Content.ReadAsStringAsync();
                        List<Recovery> recoveryItems = JsonConvert.DeserializeObject<List<Recovery>>(apiResponse);

                        // Convert the list to an ObservableCollection and return it
                        return recoveryItems;
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

        public static async Task<List<Recovery>> GetRecoveryDetailsByToken(string token)
        {
            using (HttpClient client = new HttpClient())
            {
                try
                {
                    // Define the API endpoint URL
                    string apiUrl = $"https://localhost:7147/Recovery/GetRecoveryDetailsByToken/{token}"; // Replace with your API URL

                    // Make the GET request to the API
                    HttpResponseMessage response = await client.GetAsync(apiUrl);

                    // Check if the request was successful (status code 200)
                    if (response.IsSuccessStatusCode)
                    {
                        // Read the response content as a string
                        string apiResponse = await response.Content.ReadAsStringAsync();
                        List<Recovery> recoveryItems = JsonConvert.DeserializeObject<List<Recovery>>(apiResponse);

                        // Convert the list to an ObservableCollection and return it
                        return recoveryItems;
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

        public static async Task<bool> AddRecoveryDetails(string email, Recovery recovery)
        {
            using (HttpClient client = new HttpClient())
            {
                try
                {
                    // Define the API endpoint URL
                    string apiUrl = $"https://localhost:7147/Recovery/AddRecoveryDetails/{email}"; // Replace with your API URL

                    // Serialize the 'recovery' object to JSON
                    string recoveryJson = JsonConvert.SerializeObject(recovery);

                    // Create a StringContent object with the JSON data
                    StringContent content = new StringContent(recoveryJson, Encoding.UTF8, "application/json");

                    // Make a POST request to the API with the JSON data
                    HttpResponseMessage response = await client.PostAsync(apiUrl, content);

                    // Check if the request was successful (status code 200)
                    if (response.IsSuccessStatusCode)
                    {
                        return true; // The recovery was successfully added
                    }
                    else
                    {
                        Console.WriteLine($"HTTP Error: {response.StatusCode}");
                        return false; // Handle the error case accordingly
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"An error occurred: {ex.Message}");
                    // Handle the exception here, return false or throw the exception as needed.
                    return false;
                }
            }
        }

        public static string GenerateToken(int length = 32)
        {
            using (var rng = new System.Security.Cryptography.RNGCryptoServiceProvider())
            {
                byte[] tokenBytes = new byte[length];
                rng.GetBytes(tokenBytes);
                return BitConverter.ToString(tokenBytes).Replace("-", "").ToLower();
            }
        }
    }
}
