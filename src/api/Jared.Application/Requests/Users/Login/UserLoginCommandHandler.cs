using Jared.Domain.Models;
using Jared.Domain.Options;
using Jared.Shared.Abstractions;
using Jared.Shared.Interfaces;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Jared.Application.Requests.Users.Login;

public class UserLoginCommandHandler(
    IDataContext dataContext,
    IPasswordHasher<User> passwordHasher,
    AuthenticationOptions authenticationOptions)
    : IRequestHandler<UserLoginCommand, Result<string>>
{
    private readonly IDataContext dataContext = dataContext;
    private readonly IPasswordHasher<User> passwordHasher = passwordHasher;
    private readonly AuthenticationOptions authenticationOptions = authenticationOptions;

    public async Task<Result<string>> Handle(
        UserLoginCommand request,
        CancellationToken cancellationToken)
    {
        try
        {
            var user = await dataContext
                .Set<User>()
                .Include(x => x.Role)
                .FirstOrDefaultAsync(
                    x => x.Email.ToLower() == request.dto.Email.ToLower(),
                    cancellationToken);

            if (user is null)
            {
                return Result.Fail<string>("Invalid user name or password");
            }

            var passwordVerificationResult = passwordHasher.VerifyHashedPassword(
                user,
                user.PasswordHash,
                request.dto.Password);

            if (passwordVerificationResult == PasswordVerificationResult.Failed)
            {
                return Result.Fail<string>("Invalid user name or password");
            }

            var token = generateToken(user);

            return Result.Ok(token);
        }
        catch (Exception ex)
        {
            return Result.Fail<string>(ex.Message);
        }
    }

    private string generateToken(User user)
    {
        List<Claim> claims = new()
        {
            new(ClaimTypes.NameIdentifier, user.Id.ToString()),
            new(ClaimTypes.Name, $"{user.FirstName} {user.LastName}"),
            new(ClaimTypes.Role, $"{user.Role!.Name}"),
            new("DateOfBirth", user.DateOfBirth!.Value.ToString("dd-MM-yyyy")),
        };

        SymmetricSecurityKey key =
            new(Encoding.UTF8.GetBytes(authenticationOptions.JwtKey));
        SigningCredentials credentials =
            new(key, SecurityAlgorithms.HmacSha512Signature);
        var expires = DateTime.Now.AddDays(authenticationOptions.JwtExpireDays);

        JwtSecurityToken token = new(
            issuer: authenticationOptions.JwtIssurer,
            audience: authenticationOptions.JwtIssurer,
            claims: claims,
            expires: expires,
            signingCredentials: credentials);
        JwtSecurityTokenHandler tokenHandler = new();

        return tokenHandler.WriteToken(token);
    }
}
