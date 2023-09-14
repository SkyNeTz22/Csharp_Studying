using Microsoft.AspNetCore.Mvc;
using ShopAPI.Core;
using ShopAPI.Models;

namespace ShopAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UsersController : ControllerBase
    {
        private readonly ILogger<UsersController> _logger;

        public UsersController(ILogger<UsersController> logger)
        {
            _logger = logger;
        }
        [HttpGet(Name = "GetUsers")]
        public IEnumerable<User> GetUsers()
        {
            Console.WriteLine("GetUsers Endpoint called");
            var cfg = ConfigManager.LoadConfig("Config\\config.xml");
            string connectionStringDecrypted = EncryptionHelper.DecryptString(cfg.Root.Element("connectionStrings").Element("add").Attribute("connectionString").Value);
            using (var context = new ShopContext(connectionStringDecrypted))
            {
                return context.Users.ToArray();
            }
        }

        [HttpGet("GetUserPass/{email}", Name = "GetUserPass")]
        public ActionResult<Dictionary<string, string>> GetUserPass(string email)
        {
            Console.WriteLine("GetUserPass Endpoint called");
            var cfg = ConfigManager.LoadConfig("Config\\config.xml");
            string connectionStringDecrypted = EncryptionHelper.DecryptString(cfg.Root.Element("connectionStrings").Element("add").Attribute("connectionString").Value);
            using (var context = new ShopContext(connectionStringDecrypted))
            {
                Dictionary<string, string> userDict = context.Users
                    .Where(x => x.Email == email)
                    .Select(x => new Dictionary<string, string>
                    {
                        { "Pass", x.Pass }
                    })
                    .SingleOrDefault(); 
                if (userDict == null || userDict.Count == 0)
                {
                    return NotFound(); // Return 404 Not Found if the user doesn't exist
                }

                return userDict;
            }
        }

        [HttpGet("GetUserIdByEmail/{email}", Name = "GetUserIdByEmail")]
        public ActionResult<Dictionary<string, int>> GetUserId(string email)
        {
            Console.WriteLine("GetUserIdByEmail Endpoint called");
            var cfg = ConfigManager.LoadConfig("Config\\config.xml");
            string connectionStringDecrypted = EncryptionHelper.DecryptString(cfg.Root.Element("connectionStrings").Element("add").Attribute("connectionString").Value);
            using (var context = new ShopContext(connectionStringDecrypted))
            {
                Dictionary<string, int> userDict = context.Users
                    .Where(x => x.Email == email)
                    .Select(x => new Dictionary<string, int>
                    {
                        { "UserID", x.Id }
                    })
                    .SingleOrDefault();
                if (userDict == null || userDict.Count == 0)
                {
                    return NotFound(); // Return 404 Not Found if the user doesn't exist
                }

                return userDict;
            }
        }

        [HttpPost("AddUser", Name = "AddUser")]
        public IActionResult AddUser([FromBody] User user)
        {
            Console.WriteLine("AddUser Endpoint called");
            if (user == null)
            {
                return BadRequest("User data is invalid.");
            }

            try
            {
                var cfg = ConfigManager.LoadConfig("Config\\config.xml");
                string connectionStringDecrypted = EncryptionHelper.DecryptString(cfg.Root.Element("connectionStrings").Element("add").Attribute("connectionString").Value);
                user.Pass = BCrypt.Net.BCrypt.HashPassword(user.Pass);
                user.Carts = null;
                user.Notifications = null;
                user.Orders = null;
                using (var context = new ShopContext(connectionStringDecrypted))
                {
                    context.Users.Add(user);
                    context.SaveChanges();
                }
                return Ok("User added successfully.");
            }
            catch (Exception ex)
            {
                var innerException = ex.InnerException;
                while (innerException != null)
                {
                    Console.WriteLine($"Inner Exception: {innerException.Message}");
                    innerException = innerException.InnerException;
                }

                return StatusCode(500, $"An error occurred while saving changes.");
            }
        }

        [HttpPatch("UpdateUserPasswordByEmail/{email}", Name = "UpdateUserPasswordByEmail")]
        public IActionResult UpdateUserPasswordByEmail([FromBody] Dictionary<string, string> updateDict)
        {
            Console.WriteLine("UpdateUserPasswordByEmail Endpoint called");
            if (updateDict == null)
            {
                return BadRequest("User data is invalid.");
            }

            try
            {
                var cfg = ConfigManager.LoadConfig("Config\\config.xml");
                string connectionStringDecrypted = EncryptionHelper.DecryptString(cfg.Root.Element("connectionStrings").Element("add").Attribute("connectionString").Value);
                using (var context = new ShopContext(connectionStringDecrypted))
                {
                    var userToUpdate = context.Users.FirstOrDefault(u => u.Email == updateDict["Email"]);
                    if (userToUpdate != null)
                    {
                        userToUpdate.Pass = BCrypt.Net.BCrypt.HashPassword(updateDict["NewPass"]);
                        context.SaveChanges();
                        return Ok("User password updated successfully.");
                    }
                    else
                    {
                        return NotFound("User not found.");
                    }
                }
            }
            catch (Exception ex)
            {
                var innerException = ex.InnerException;
                while (innerException != null)
                {
                    Console.WriteLine($"Inner Exception: {innerException.Message}");
                    innerException = innerException.InnerException;
                }

                return StatusCode(500, $"An error occurred while saving changes.");
            }
        }

        [HttpPatch("UpdateUserPasswordById/{UserID}", Name = "UpdateUserPasswordById")]
        public IActionResult UpdateUserPasswordById([FromBody] Dictionary<string, string> updateDict)
        {
            Console.WriteLine("UpdateUserPasswordById Endpoint called");
            if (updateDict == null)
            {
                return BadRequest("User data is invalid.");
            }

            try
            {
                var cfg = ConfigManager.LoadConfig("Config\\config.xml");
                string connectionStringDecrypted = EncryptionHelper.DecryptString(cfg.Root.Element("connectionStrings").Element("add").Attribute("connectionString").Value);
                using (var context = new ShopContext(connectionStringDecrypted))
                {
                    var userToUpdate = context.Users.FirstOrDefault(u => u.Id.ToString() == updateDict["UserID"]);
                    if (userToUpdate != null)
                    {
                        userToUpdate.Pass = BCrypt.Net.BCrypt.HashPassword(updateDict["NewPass"]);
                        context.SaveChanges();
                        return Ok("User password updated successfully.");
                    }
                    else
                    {
                        return NotFound("User not found.");
                    }
                }
            }
            catch (Exception ex)
            {
                var innerException = ex.InnerException;
                while (innerException != null)
                {
                    Console.WriteLine($"Inner Exception: {innerException.Message}");
                    innerException = innerException.InnerException;
                }

                return StatusCode(500, $"An error occurred while saving changes.");
            }
        }
    }
}

        