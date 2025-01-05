namespace WebAppMails;

public sealed class Clock : IClock
{
    public DateTime Now()
    {
        return DateTime.UtcNow;
    }
}