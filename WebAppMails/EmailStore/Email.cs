namespace WebAppMails.EmailStore;

public enum EmailSendingStatus
{
    Sent,
    Failed,
    Waiting
}

public sealed record Email
{
    public required Guid Id { get; init; }

    public required string Subject { get; init; }

    public required string Message { get; init; }

    public required DateTime QueuedAt { get; init; }

    public required EmailSendingStatus Status { get; set; }

    public required int SendAttempts { get; set; }

    public static Email CreateNewToSend(string subject, string message, DateTime now)
    {
        return new Email
        {
            Id = Guid.NewGuid(),
            Subject = subject,
            Message = message,
            QueuedAt = now,
            Status = EmailSendingStatus.Waiting,
            SendAttempts = 0
        };
    }
}