using Jared.Shared.Dtos.UserDtos;
using System.IdentityModel.Tokens.Jwt;

namespace Jared.Presentation.Services;

public class UserService : IUserService
{
    private const string ID_TYPE = "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/nameidentifier";
    private const string NAME_TYPE = "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/name";
    private const string USER_ROLE = "http://schemas.microsoft.com/ws/2008/06/identity/claims/role";
    private const string BIRTHDAY_TYPE = "DateOfBirth";

    private readonly HttpClient httpClient;

    public UserService(HttpClient httpClient)
    {
        this.httpClient = httpClient;
    }

    public int GetUserId()
    {
        var token = httpClient.DefaultRequestHeaders.Authorization;

        if (token is null)
        {
            return 0;
        }

        JwtSecurityTokenHandler handler = new();
        var jwtToken = handler.ReadJwtToken(token.Parameter);

        if (jwtToken is null)
        {
            return 0;
        }

        if (int.TryParse(jwtToken.Claims.FirstOrDefault(claim => claim.Type == ID_TYPE)?.Value, out int id))
        {
            return id;
        }

        return 0;
    }


    public string GetUserRole()
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
        var role = jwtToken.Claims.FirstOrDefault(claim => claim.Type == USER_ROLE);
        return role is null ? string.Empty : role.Value;
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
}
