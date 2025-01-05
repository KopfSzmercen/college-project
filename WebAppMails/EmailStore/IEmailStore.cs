namespace WebAppMails.EmailStore;

public interface IEmailStore
{
    public Task EnqueueEmailAsync(string subject, string message);

    public Task<IEnumerable<Email>> FindManyAsync(Func<Email, bool>? where, int? skip, int? take);

    public Task UpdateAsync(Email email);
}