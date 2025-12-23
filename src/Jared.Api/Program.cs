using System.Globalization;
using System.Security.Claims;
using System.Text;
using ConfigurationSubstitution;
using FluentValidation.AspNetCore;
using Jared.Application;
using Jared.Application.Mapping;
using Jared.Contracts.Middleware;
using Jared.Domain.Abstractions;
using Jared.Domain.Models;
using Jared.Domain.Options;
using Jared.Infrastructure;
using Jared.Validators;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Serilog;

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

// Authentication
AuthenticationOptions authenticationOptions = new();
builder.Configuration
    .GetSection(AuthenticationOptions.Section)
    .Bind(authenticationOptions);

builder.Services.AddSingleton(authenticationOptions);

builder.Services
    .AddAuthentication(options =>
    {
        options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
        options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    })
    .AddJwtBearer(options =>
    {
        options.RequireHttpsMetadata = true;
        options.SaveToken = false;
        options.TokenValidationParameters = new()
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = authenticationOptions.JwtIssurer,
            ValidAudience = authenticationOptions.JwtIssurer,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(authenticationOptions.JwtKey)),
            ClockSkew = TimeSpan.Zero,
            NameClaimType = ClaimTypes.Name,
            RoleClaimType = ClaimTypes.Role,
        };
    });
builder.Services.AddAuthorization();
builder.Services.AddValidators();
builder.Services.AddInfrastructure(builder.Configuration);
builder.Services.AddApplication();
builder.Services.AddControllers();
builder.Services.AddFluentValidationAutoValidation();
builder.Services.AddSwaggerGen();
builder.Services.AddEndpointsApiExplorer();
builder.Services.RegisterMappingConfigurations();
builder.Services.AddHttpContextAccessor();
builder.Services.AddScoped<IPasswordHasher<User>, PasswordHasher<User>>();

// CORS
var corsSettings = builder.Configuration.GetSection("Cors:AllowedOrigins").Get<string[]>();
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
#pragma warning disable S6966 // Awaitable method should be used
var pendingMigrations = dataContext!.Database.GetPendingMigrations();
if (pendingMigrations.Any())
{
    dataContext.Database.Migrate();
}
#pragma warning restore S6966 // Awaitable method should be used

// Configure the HTTP request pipeline.
app.UseHttpsRedirection();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");

    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseCors("Jared.App");
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
    await app.RunAsync();
}
catch (Exception e)
{
    Log.Fatal(e, "Application startup fail");
}

public partial class Program
{
}