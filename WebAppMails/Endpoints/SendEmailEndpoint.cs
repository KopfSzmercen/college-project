using Microsoft.AspNetCore.Mvc;
using WebAppMails.EmailStore;

namespace WebAppMails.Endpoints;

public static class SendEmailEndpoint
{
    public static void Register(WebApplication application)
    {
        application.MapPost("/emails", async (
            [FromBody] SendEmailRequest request,
            [FromServices] IEmailStore emailStore
        ) =>
        {
            await emailStore.EnqueueEmailAsync(request.Subject, request.Message);
            return Results.Ok();
        });
    }

    private sealed record SendEmailRequest(string Subject, string Message);
}