using System.IdentityModel.Tokens.Jwt;
using AutoMapper;
using JetBrains.Annotations;

namespace LittleByte.AutoMapper.AspNet;

[UsedImplicitly]
internal sealed class IdentityProfile : Profile
{
    public IdentityProfile()
    {
        CreateMap<JwtSecurityToken?, string?>().ConvertUsing<JwtSecurityTokenConverter>();
    }
}