using WebAppMails.EmailSender;
using WebAppMails.EmailStore;

namespace WebAppMails.EmailProcessing;

internal sealed class EmailProcessingJob(
    IEmailStore emailStore,
    IEmailSender emailSender
) : IHostedService
{
    private const int LoopDelayMs = 5000;
    private const int BatchSize = 10;
    private const int MaxSendAttempts = 3;
    private volatile bool _isRunning;

    public Task StartAsync(CancellationToken cancellationToken)
    {
        Task.Run(() => ProcessingLoop(cancellationToken), cancellationToken);
        return Task.CompletedTask;
    }

    public Task StopAsync(CancellationToken cancellationToken)
    {
        _isRunning = false;
        return Task.CompletedTask;
    }

    private async Task ProcessingLoop(CancellationToken cancellationToken)
    {
        while (true)
        {
            if (cancellationToken.IsCancellationRequested) break;

            await ProcessEmails(cancellationToken);

            await Task.Delay(LoopDelayMs, cancellationToken);
        }
    }

    private async Task ProcessEmails(CancellationToken cancellationToken)
    {
        if (_isRunning) return;
        _isRunning = true;

        var emails = await emailStore
            .FindManyAsync(
                e => e.Status == EmailSendingStatus.Waiting || e is
                    { Status: EmailSendingStatus.Failed, SendAttempts: < MaxSendAttempts },
                null,
                BatchSize
            );

        foreach (var email in emails)
        {
            try
            {
                await emailSender.SendEmailAsync(email.Subject, email.Message, cancellationToken);
                email.Status = EmailSendingStatus.Sent;
                email.SendAttempts = 1;
            }
            catch (Exception)
            {
                email.Status = EmailSendingStatus.Failed;
                email.SendAttempts++;
            }

            await emailStore.UpdateAsync(email);
        }

        _isRunning = false;
    }
}