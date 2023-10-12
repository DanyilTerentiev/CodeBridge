using System.Reflection;
using System.Threading.RateLimiting;
using CodeBridge.DI;
using CodeBridge.Domain.Models;
using CodeBridge.WebAPI.Middleware;
using ExceptionHandler;
using FluentValidation;
using Microsoft.AspNetCore.RateLimiting;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services
    .AddDb(builder.Configuration)
    .AddServices()
    .AddValidatorsFromAssembly(Assembly.GetAssembly(typeof(Program)));

var myOptions = builder.Configuration.GetSection("MyRateOptions").Get<RateLimitOptions>();

builder.Services.AddRateLimiter(l => l
    .AddFixedWindowLimiter(policyName: "fixed", options =>
    {
        options.PermitLimit = myOptions!.PermitLimit;
        options.Window = TimeSpan.FromSeconds(myOptions.Window);
        options.QueueProcessingOrder = QueueProcessingOrder.OldestFirst;
    }).
    RejectionStatusCode = myOptions!.StatusCode);

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.UseExceptions();

app.UseRateLimiter();

app.Run();