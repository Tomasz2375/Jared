using ConfigurationSubstitution;
using FluentValidation.AspNetCore;
using Jared.Application;
using Jared.Application.Mapping;
using Jared.Domain.Models;
using Jared.Domain.Options;
using Jared.Infrastructure;
using Jared.Shared;
using Jared.Shared.Interfaces;
using Jared.Shared.Middleware;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Serilog;
using System.Globalization;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Logger
builder.Host.UseSerilog((context, loggerConfig) => loggerConfig
    .ReadFrom.Configuration(context.Configuration));
// Culture
var cultureInfo = new CultureInfo("en-US");
cultureInfo.DateTimeFormat.ShortTimePattern = "HH:mm";
cultureInfo.DateTimeFormat.LongTimePattern = "HH:mm:ss";
cultureInfo.DateTimeFormat.ShortDatePattern = "dd.MM.yyyy";

CultureInfo.DefaultThreadCurrentCulture = cultureInfo;
CultureInfo.DefaultThreadCurrentUICulture = cultureInfo;

// Authntication.
AuthenticationOptions authenticationOptins = new();
builder
    .Configuration
    .GetSection(AuthenticationOptions.Section)
    .Bind(authenticationOptins);
builder.Services.AddSingleton(authenticationOptins);
builder.Services.AddAuthentication(option =>
{
    option.DefaultAuthenticateScheme = "Bearer";
    option.DefaultScheme = "Bearer";
    option.DefaultChallengeScheme = "Bearer";
}).AddJwtBearer(config =>
{
    config.RequireHttpsMetadata = false;
    config.SaveToken = true;
    config.TokenValidationParameters = new()
    {
        ValidIssuer = authenticationOptins.JwtIssurer,
        ValidAudience = authenticationOptins.JwtIssurer,
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(authenticationOptins.JwtKey)),
    };
});

builder.Services.AddInfrastructure(builder.Configuration);
builder.Services.AddApplication();
builder.Services.AddShared();
builder.Services.AddControllers();
builder.Services.AddFluentValidationAutoValidation();
builder.Services.AddSwaggerGen();
builder.Services.AddEndpointsApiExplorer();
builder.Services.RegisterMappingConfigurations();
builder.Services.AddHttpContextAccessor();
builder.Services.AddScoped<IPasswordHasher<User>, PasswordHasher<User>>();
// CORS
var corsSettings = builder.Configuration.GetSection("Cors:AllowedOrigins").Get<string[]>(); ;
if (corsSettings is not null)
{
    builder.Services.AddCors(options =>
    {
        options.AddPolicy("Jared.App", builder =>
        {
            builder.WithOrigins(corsSettings).AllowAnyHeader().AllowAnyMethod();
        });
    });
}
builder.Configuration.EnableSubstitutions("{%", "%}");

var app = builder.Build();

using var scope = app.Services.CreateScope();
var dataContext = scope.ServiceProvider.GetService<IDataContext>();
var pendingMigrations = dataContext!.Database.GetPendingMigrations();
if (pendingMigrations.Any())
{
    dataContext.Database.Migrate();
}

// Configure the HTTP request pipeline.
app.UseHttpsRedirection();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
    app.UseCors("Jared.App");
}

app.UseMiddleware<RequestLogContextMiddleware>();
app.UseSerilogRequestLogging();
app.UseStaticFiles();
app.UseSwaggerUI();
app.UseSwagger();
app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();

try
{
    Log.Information("Startingup application");
    app.Run();
}
catch (Exception e)
{
    Log.Fatal(e, "Application startup fail");
}

public partial class Program
{
}
