using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Org.BouncyCastle.Bcpg;
using ShopAPI.Core;
using ShopAPI.Models;

namespace ShopAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class RecoveryController : Controller
    {
        private readonly ILogger<RecoveryController> _logger;

        public RecoveryController(ILogger<RecoveryController> logger)
        {
            _logger = logger;
        }

        [HttpGet("GetRecoveryDetails/{email}", Name = "GetRecoveryDetails")]
        public IEnumerable<Recovery> GetRecoveryDetails(string email)
        {
            Console.WriteLine("GetRecoveryDetails Endpoint called");
            var cfg = ConfigManager.LoadConfig("Config\\config.xml");
            string connectionStringDecrypted = EncryptionHelper.DecryptString(cfg.Root.Element("connectionStrings").Element("add").Attribute("connectionString").Value);
            using (var context = new ShopContext(connectionStringDecrypted))
            {
                return new Recovery[] {
                    context.Recoveries
                            .Where(r => r.UserId == context.Users.FirstOrDefault(u => u.Email == email).Id)
                            .OrderByDescending(r => r.Expiry)
                            .FirstOrDefault()
                };
            }
        }

        [HttpPost("AddRecoveryDetails/{email}", Name = "AddRecoveryDetails")]
        public IActionResult AddRecoveryDetails([FromBody] Recovery recovery)
        {
            Console.WriteLine("AddRecoveryDetails Endpoint called");
            if (recovery == null)
            {
                return BadRequest("User data is invalid.");
            }
            try
            {
                var cfg = ConfigManager.LoadConfig("Config\\config.xml");
                string connectionStringDecrypted = EncryptionHelper.DecryptString(cfg.Root.Element("connectionStrings").Element("add").Attribute("connectionString").Value);
                using (var context = new ShopContext(connectionStringDecrypted))
                {
                    context.Recoveries.Add(recovery);
                    context.SaveChanges();
                }
                return Ok("Recovery row added successfully.");
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

        [HttpGet("GetRecoveryDetailsByToken/{token}", Name = "GetRecoveryDetailsByToken")]
        public IEnumerable<Recovery> GetRecoveryDetailsByToken(string token)
        {
            Console.WriteLine("GetRecoveryDetailsByToken Endpoint called");
            var cfg = ConfigManager.LoadConfig("Config\\config.xml");
            string connectionStringDecrypted = EncryptionHelper.DecryptString(cfg.Root.Element("connectionStrings").Element("add").Attribute("connectionString").Value);
            using (var context = new ShopContext(connectionStringDecrypted))
            {
                return new Recovery[] {
                    context.Recoveries
                            .Where(r => r.Token == token)
                            .OrderByDescending(r => r.Expiry)
                            .FirstOrDefault()
                };
            }
        }
    }
}
