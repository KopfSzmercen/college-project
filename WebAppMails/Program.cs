using WebAppMails;
using WebAppMails.EmailProcessing;
using WebAppMails.EmailSender;
using WebAppMails.EmailStore;
using WebAppMails.Endpoints;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddSingleton<IClock, Clock>();
builder.Services.AddSingleton<IEmailStore, InMemoryEmailStore>();
builder.Services.AddSingleton<IEmailSender, EmailSender>();
builder.Services.AddHostedService<EmailProcessingJob>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

SendEmailEndpoint.Register(app);
BrowseEmailsAsync.Register(app);

app.Run();