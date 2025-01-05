using Microsoft.AspNetCore.Mvc;
using WebAppMails.EmailStore;

namespace WebAppMails.Endpoints;

public class BrowseEmailsAsync
{
    public static void Register(WebApplication application)
    {
        application.MapGet("/emails", async (
            [AsParameters] BrowseEmailsAsyncRequest request,
            [FromServices] IEmailStore emailStore
        ) =>
        {
            var emails = await emailStore.FindManyAsync(
                e =>
                    request.Status is null || e.Status == request.Status,
                null,
                null);

            return Results.Ok(emails);
        });
    }

    private sealed record BrowseEmailsAsyncRequest
    {
        [FromQuery] public EmailSendingStatus? Status { get; init; }
    }
}