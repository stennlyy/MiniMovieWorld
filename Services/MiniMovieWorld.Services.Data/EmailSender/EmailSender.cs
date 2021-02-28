namespace MiniMovieWorld.Services.Data.EmailSender
{
    using System.Threading.Tasks;

    using Microsoft.Extensions.Configuration;
    using SendGrid;
    using SendGrid.Helpers.Mail;

    public class EmailSender : IEmailSenderService
    {
        private readonly IConfiguration configuration;

        public EmailSender(IConfiguration configuration)
        {
            this.configuration = configuration;
        }

        public async Task SendEmailAsync(string toEmail, string subject, string content)
        {
            var apiKey = this.configuration["SendGrid API KEY"];
            var client = new SendGridClient(apiKey);
            var from = new EmailAddress("stenli.bg@mail.bg", "Stennlyy");
            var to = new EmailAddress(toEmail);
            var msg = MailHelper.CreateSingleEmail(from, to, subject, content, content);
            var response = await client.SendEmailAsync(msg);
        }
    }
}
