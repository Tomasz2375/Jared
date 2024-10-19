using Jared.Shared.Dtos.UserDtos;
using System.IdentityModel.Tokens.Jwt;

namespace Jared.Presentation.Services;

public class UserService : IUserService
{
    private const string NAME_TYPE = "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/name";
    private const string BIRTHDAY_TYPE = "DateOfBirth";
    private readonly HttpClient httpClient;

    public UserService(HttpClient httpClient)
    {
        this.httpClient = httpClient;
    }

    public UserUpdateDto GetUserData()
    {
        UserUpdateDto dto = new();
        var token = httpClient.DefaultRequestHeaders.Authorization;

        if (token is null)
        {
            return dto;
        }

        JwtSecurityTokenHandler handler = new();
        var jwtToken = handler.ReadJwtToken(token.Parameter);

        if (jwtToken is null)
        {
            return dto;
        }

        var fullName = jwtToken.Claims.FirstOrDefault(claim => claim.Type == NAME_TYPE)?.Value;
        if (fullName is not null)
        {
            dto.FirstName = fullName!.Split(" ")[0];
            dto.LastName = fullName.Split(" ")[1];
        }
        var claimDateOfBirth = jwtToken.Claims.FirstOrDefault(claim => claim.Type.Contains(BIRTHDAY_TYPE))?.Value;
        if (DateTime.TryParse(claimDateOfBirth, out DateTime dateOfBirth))
        {
            dto.DateOfBirth = dateOfBirth;
        }

        return dto!;
    }

    public string GetUserName()
    {
        var token = httpClient.DefaultRequestHeaders.Authorization;

        if (token is null)
        {
            return string.Empty;
        }

        JwtSecurityTokenHandler handler = new();
        var jwtToken = handler.ReadJwtToken(token.Parameter);

        if (jwtToken is null)
        {
            return string.Empty;
        }

        var fullName = jwtToken.Claims.FirstOrDefault(claim => claim.Type == NAME_TYPE)?.Value;
        if (fullName is null)
        {
            return string.Empty;
        }

        return fullName;
    }
}
