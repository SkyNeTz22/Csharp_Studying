using Microsoft.AspNetCore.Mvc;
using MimeKit;
using MailKit.Net.Smtp;

namespace ShopAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class MailController : Controller
    {
        public class EmailRequestDto
        {
            public string BodyMessage { get; set; }
        }
        private readonly ILogger<MailController> _logger;

        public MailController(ILogger<MailController> logger)
        {
            _logger = logger;
        }
        [HttpPost("SendRecoveryMail/{email}")]
        public IActionResult SendRecoveryMail(string email, [FromBody] EmailRequestDto requestDto)
        {
            Console.WriteLine("SendRecoveryMail Endpoint called");
            try
            {
                string subject = "Password Recovery";

                // Construct the email body using the bodyMessage parameter
                TextPart bodyText = new TextPart();
                bodyText.SetText("utf-8", requestDto.BodyMessage);

                // Create and send the email
                SendMail(email, subject, bodyText);

                return Ok("Recovery email sent successfully.");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred: {ex.Message}");
            }
        }

        private void SendMail(string recipient, string subject, TextPart body)
        {
            if (string.IsNullOrEmpty(recipient))
            {
                return;
            }
            try
            {
                // Sender and recipient email addresses
                string senderEmail = "monitoringSender01@gmail.com";

                // Create the email message
                MimeMessage message = new MimeMessage();
                message.From.Add(new MailboxAddress("Sender", senderEmail));
                message.To.Add(new MailboxAddress("Recipient", recipient));
                message.Subject = subject;
                message.Body = body;

                // Create the SMTP client
                using (var client = new SmtpClient())
                {
                    // Connect to the local SMTP server
                    client.Connect("smtp.gmail.com", 465, true);
                    client.Authenticate("enemymailer@gmail.com", "knpceoymnxgmvwfs");
                    // Send the email
                    client.Send(message);
                    // Disconnect from the server
                    client.Disconnect(true);
                }
            }
            catch (Exception)
            {
                return;
            }
        }
    }
}
