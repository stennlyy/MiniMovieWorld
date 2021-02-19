namespace MiniMovieWorld.Services.Data.EmailSender
{
    using System.Threading.Tasks;

    public interface IEmailSenderService
    {
        Task SendEmailAsync(string toEmail, string subject, string content);
    }
}
