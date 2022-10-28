using AutoMapper;
using JetBrains.Annotations;
using System.IdentityModel.Tokens.Jwt;

namespace LittleByte.Common.Identity.Mappings;

[UsedImplicitly]
internal sealed class IdentityProfile : Profile
{
    public IdentityProfile()
    {
        CreateMap<JwtSecurityToken?, string?>().ConvertUsing<JwtSecurityTokenConverter>();
    }
}