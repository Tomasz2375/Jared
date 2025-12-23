using Jared.App;
using Jared.Client;
using Jared.Contracts.Middleware;
using Jared.Validators;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.Server;
using Radzen;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

// Logger
builder.Host.UseSerilog((context, loggerConfig) => loggerConfig
    .ReadFrom.Configuration(context.Configuration));

// Add services to the container.
builder.Services.AddScoped<AuthorizationHandler>();
builder.Services.AddValidators();
builder.Services.AddClient();
builder.Services.AddRazorPages();
builder.Services.AddRadzenComponents();
builder.Services.AddServerSideBlazor();
builder.Services.AddHttpContextAccessor();
builder.Services
    .AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(options =>
    {
        options.LoginPath = "/login";
        options.LogoutPath = "/logout";
        options.Cookie.Name = "jared-cookie";
        options.Cookie.HttpOnly = true;
        options.Cookie.SecurePolicy = CookieSecurePolicy.SameAsRequest;
        options.Cookie.SameSite = SameSiteMode.Lax;
        options.SlidingExpiration = true;
    });
builder.Services.AddAuthorization();
builder.Services.AddScoped<AuthenticationStateProvider, ServerAuthenticationStateProvider>();
builder.Services.AddControllers();
builder.Services.AddOptions();
builder.Services.AddScoped<HttpClient>(sp =>
{
    var navigation = sp.GetRequiredService<NavigationManager>();
    return new()
    {
        BaseAddress = new Uri(navigation.BaseUri),
    };
});
builder.Services.AddHttpClient("JaredApi", client =>
{
    client.BaseAddress = new Uri(Environment.GetEnvironmentVariable("JARED_API_URL")!);
}).AddHttpMessageHandler<AuthorizationHandler>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");

    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseMiddleware<RequestLogContextMiddleware>();
app.UseSerilogRequestLogging();
app.UseStaticFiles();
app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();
app.MapBlazorHub();
app.MapFallbackToPage("/_Host");

try
{
    Log.Information("Startingup application");
    await app.RunAsync();
}
catch (Exception e)
{
    Log.Fatal(e, "Application startup fail");
}
