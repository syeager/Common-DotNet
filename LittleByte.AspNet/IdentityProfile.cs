using System.IdentityModel.Tokens.Jwt;
using JetBrains.Annotations;

namespace LittleByte.AspNet;

[UsedImplicitly]
internal sealed class IdentityProfile : Profile
{
    public IdentityProfile()
    {
        CreateMap<JwtSecurityToken?, string?>().ConvertUsing<JwtSecurityTokenConverter>();
    }
}