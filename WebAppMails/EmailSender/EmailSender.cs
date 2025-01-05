namespace WebAppMails.EmailSender;

public sealed class EmailSender(IClock clock) : IEmailSender
{
    public Task<bool> SendEmailAsync(string subject, string htmlMessage, CancellationToken cancellationToken)
    {
        return SendEmailAsync(subject, htmlMessage);
    }

    public Task<bool> SendEmailAsync(string subject, string htmlMessage)
    {
        var formattedNow = clock.Now().ToString("yyyy-MM-dd HH:mm:ss");

        Console.WriteLine($"Sending email with subject: {subject} at {formattedNow}");

        return Task.FromResult(true);
    }
}