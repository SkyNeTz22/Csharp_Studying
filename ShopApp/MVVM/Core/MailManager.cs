using Newtonsoft.Json;
using ShopApp.MVVM.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace ShopApp.MVVM.Core
{
    internal class MailManager
    {
        public static async Task<bool> SendMail(string email, Dictionary<string, string> body)
        {
            using (HttpClient client = new HttpClient())
            {
                try
                {
                    // Define the API endpoint URL
                    string apiUrl = $"https://localhost:7147/Mail/SendRecoveryMail/{email}"; // Replace with your API URL

                    // Serialize the 'recovery' object to JSON
                    string recoveryJson = JsonConvert.SerializeObject(body);

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
    }
}
