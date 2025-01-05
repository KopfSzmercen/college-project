namespace WebAppMails.EmailSender;

public interface IEmailSender
{
    Task<bool> SendEmailAsync(string subject, string htmlMessage, CancellationToken cancellationToken = default);
}