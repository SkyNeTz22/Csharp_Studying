using EntityFramework_First.Core;
using EntityFramework_First.Models;

namespace EntityFramework_First { 
    class Program
    {
        //Scaffold-DbContext "Server=DESKTOP-F8ON76N;User ID=dbuser;Password=dbpass;Database=shop;Trusted_Connection=False;TrustServerCertificate=true" Microsoft.EntityFrameworkCore.SqlServer -OutputDir Models
        static void Main(string[] args)
        {
            string cfgPath = "Config\\config.xml";
            var cfg = ConfigManager.LoadConfig(cfgPath);
            string connectionStringDecrypted = EncryptionHelper.DecryptString(cfg.Root.Element("connectionStrings").Element("add").Attribute("connectionString").Value);

            using (var context = new ShopContext(connectionStringDecrypted))
            {
                //var users = context.Users.ToList(); // Retrieve all users
                //foreach (var user in users)
                //{
                //    Console.WriteLine($"User: {user.FirstName} {user.LastName}");
                //}
                // Create a new user object
                var newUser = new UserModel
                {
                    FirstName = "John",
                    LastName = "Doe",
                    Email = "john2@example.com",
                    Pass = "pass", // Replace with the actual hashed password
                    RegisteredDate = DateTime.Now,
                    UserFlag = 1,
                    Deleted = false
                };
                // Add the user to the Users DbSet
                //context.Users.Add(newUser);

                // Save the changes to the database
                //context.SaveChanges();

                var userEmail = "john2@example.com";

                var userToUpdate = context.Users.FirstOrDefault(u => u.Email == userEmail);

                if (userToUpdate != null)
                {
                    userToUpdate.Deleted = true;

                    context.SaveChanges();
                }
                else
                {
                    Console.WriteLine("User not found.");
                }
            }
        }
    }
}