using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace ShopAPI.Core
{
    internal class ConfigManager
    {
        public static XDocument LoadConfig(string configPath)
        {
            try
            {
                // Load the XML file
                XDocument xmlDoc = XDocument.Load(configPath);
                return xmlDoc;
            }
            catch (Exception ex)
            {
                // Handle any exceptions that may occur during file loading
                Console.WriteLine($"Error loading XML file: {ex.Message}");
                return null;
            }
        }
    }
}
