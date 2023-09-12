using EntityFramework_First.Core;
using System.Xml.Linq;

namespace Cryptonite_Helper {
    public class CryptoniteHelper { 
        public static void Main(string[] args) {
            string cfgPath = "Config/config.xml";
            XDocument cfg = ConfigManager.LoadConfig(cfgPath);
            if (cfg != null)
            {
                // Now, you can access and manipulate the loaded XML document as needed
                string connectionStringEncrypted = EncryptionHelper.EncryptString(cfg.Root.Element("connectionStrings").Element("add").Attribute("connectionString").Value);
                Console.WriteLine(connectionStringEncrypted);
                ConfigManager.CreateConfig(cfgPath, connectionStringEncrypted);
            }
        }
    }
}
