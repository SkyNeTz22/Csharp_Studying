using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace EntityFramework_First.Core
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

        public static int CreateConfig(string configPath, string encryptedString)
        {
            XDocument xmlDoc = new XDocument(
                        new XDeclaration("1.0", "utf-8", null),
                        new XElement("configuration",
                            new XElement("connectionStrings",
                                new XElement("add",
                                    new XAttribute("name", "MyDbConnection"),
                                    new XAttribute("connectionString", encryptedString)
                                )
                            )
                        )
                    );

            // Save the XML document to a file
            xmlDoc.Save(configPath);
            return 1;
        }
    }
}
