using Microsoft.AspNetCore.Mvc;
using MimeKit;
using MailKit.Net.Smtp;
using MailKit.Security;

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

                // Create and send the email
                SendHtmlEmail(email, subject, requestDto.BodyMessage);

                return Ok("Recovery email sent successfully.");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred: {ex.Message}");
            }
        }

        private void SendHtmlEmail(string recipient, string subject, string bodyHtml)
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

                // Create the HTML body part
                var body = new TextPart("html")
                {
                    Text = bodyHtml
                };

                // Set the message body
                message.Body = body;

                // Create the SMTP client
                using (var client = new SmtpClient())
                {
                    // Connect to the Gmail SMTP server
                    client.Connect("smtp.gmail.com", 465, SecureSocketOptions.SslOnConnect);

                    // Authenticate with your Gmail account
                    client.Authenticate("enemymailer@gmail.com", "knpceoymnxgmvwfs");

                    // Send the email
                    client.Send(message);

                    // Disconnect from the server
                    client.Disconnect(true);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error sending email: {ex.Message}");
                // Handle the exception as needed
            }
        }
    }
}
