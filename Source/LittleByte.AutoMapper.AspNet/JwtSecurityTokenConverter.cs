using System.IdentityModel.Tokens.Jwt;
using AutoMapper;
using Microsoft.IdentityModel.Tokens;

namespace LittleByte.AutoMapper.AspNet;

public sealed class JwtSecurityTokenConverter(SecurityTokenHandler tokenHandler)
    : ITypeConverter<JwtSecurityToken?, string?>
{
    public string? Convert(JwtSecurityToken? source, string? destination, ResolutionContext context) =>
        source is null
            ? null
            : tokenHandler.WriteToken(source);
}