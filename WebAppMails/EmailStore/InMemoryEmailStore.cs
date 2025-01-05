using System.Collections.Concurrent;
using System.Text.Json;

namespace WebAppMails.EmailStore;

internal sealed class InMemoryEmailStore(
    IClock clock
) : IEmailStore
{
    private readonly ConcurrentDictionary<Guid, Email> _emails = [];

    public async Task EnqueueEmailAsync(string subject, string message)
    {
        await Task.Delay(500);

        var email = Email.CreateNewToSend(subject, message, clock.Now());

        _emails.TryAdd(email.Id, email);
    }

    public Task<IEnumerable<Email>> FindManyAsync(Func<Email, bool>? where, int? skip, int? take)
    {
        var query = _emails.Values.AsEnumerable();

        if (where is not null) query = query.Where(where);

        if (skip.HasValue) query = query.Skip(skip.Value);

        if (take.HasValue) query = query.Take(take.Value);

        var enumerable = query as Email[] ?? query.ToArray();

        var deepCopies = enumerable.Select(e =>
        {
            var serialized = JsonSerializer.Serialize(e);

            return JsonSerializer.Deserialize<Email>(serialized)!;
        });

        return Task.FromResult(deepCopies);
    }

    public Task UpdateAsync(Email email)
    {
        _emails.TryGetValue(email.Id, out var existingEmail);

        if (existingEmail is null) return Task.CompletedTask;

        _emails.TryRemove(email.Id, out _);
        _emails.TryAdd(email.Id, email);

        return Task.CompletedTask;
    }
}