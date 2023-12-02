using System.IdentityModel.Tokens.Jwt;
using AutoMapper;
using Microsoft.IdentityModel.Tokens;

namespace LittleByte.AutoMapper.AspNet;

public sealed class JwtSecurityTokenConverter : ITypeConverter<JwtSecurityToken?, string?>
{
    private readonly SecurityTokenHandler tokenHandler;

    public JwtSecurityTokenConverter(SecurityTokenHandler tokenHandler) => this.tokenHandler = tokenHandler;

    public string? Convert(JwtSecurityToken? source, string? destination, ResolutionContext context) =>
        source is null
            ? null
            : tokenHandler.WriteToken(source);
}