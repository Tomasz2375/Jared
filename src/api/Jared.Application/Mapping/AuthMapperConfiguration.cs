using Jared.Domain.Models;
using Jared.Dtos.Auth;
using Mapster;

namespace Jared.Application.Mapping;

public class AuthMapperConfiguration : IRegister
{
    public void Register(TypeAdapterConfig config)
    {
        config
            .NewConfig<User, LoginResponseDto>()
            .Map(d => d.Role, s => s.Role!.Name);
    }
}
