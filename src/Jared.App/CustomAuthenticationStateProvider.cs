using Blazored.LocalStorage;
using Microsoft.AspNetCore.Components.Authorization;
using System.Security.Claims;
using System.Text.Json;

namespace Jared.App;

public class CustomAuthenticationStateProvider : AuthenticationStateProvider
{
    private readonly ILocalStorageService localStorageService;
    private readonly HttpClient httpClient;

    public CustomAuthenticationStateProvider(
        ILocalStorageService localStorageService,
        HttpClient httpClient)
    {
        this.localStorageService = localStorageService;
        this.httpClient = httpClient;
    }

    public override async Task<AuthenticationState> GetAuthenticationStateAsync()
    {
        var authenticationToken = await localStorageService.GetItemAsStringAsync("authToken");

        var identity = new ClaimsIdentity();
        httpClient.DefaultRequestHeaders.Authorization = null;

        if (!string.IsNullOrEmpty(authenticationToken))
        {
            try
            {
                identity = new ClaimsIdentity(parseClaimsFromJwt(authenticationToken), "jwt");
                httpClient.DefaultRequestHeaders.Authorization =
                    new("Bearer", authenticationToken.Replace("\"",""));
            }
            catch (Exception e)
            {
                await localStorageService.RemoveItemAsync("authToken");
                identity = new ClaimsIdentity();
                Console.WriteLine(e.Message);
            }
        }

        var user = new ClaimsPrincipal(identity);
        var state = new AuthenticationState(user);

        NotifyAuthenticationStateChanged(Task.FromResult(state));

        return state;
    }

    private byte[] parseBase64WithoutPadding(string base64)
    {
        switch (base64.Length % 4)
        {
            case 2: base64 += "=="; break;
            case 3: base64 += "="; break;
        }
        return Convert.FromBase64String(base64);
    }

    private IEnumerable<Claim>? parseClaimsFromJwt(string jwt)
    {
        var payload = jwt.Split(".");
        var jsonBytes = parseBase64WithoutPadding(payload[1]);
        var keyValuePairs = JsonSerializer
            .Deserialize<Dictionary<string, object>>(jsonBytes);

        var claims = keyValuePairs?.Select(x => new Claim(x.Key, x.Value.ToString()!));

        return claims;
    }
}
