using System.Diagnostics.CodeAnalysis;
using System.IdentityModel.Tokens.Jwt;
using AutoMapper;
using Microsoft.IdentityModel.Tokens;

namespace LittleByte.Common.Identity.Mappings;

[SuppressMessage("ReSharper", "ClassNeverInstantiated.Global")]
public sealed class JwtSecurityTokenConverter : ITypeConverter<JwtSecurityToken?, string?>
{
    private readonly SecurityTokenHandler tokenHandler;

    public JwtSecurityTokenConverter(SecurityTokenHandler tokenHandler)
    {
        this.tokenHandler = tokenHandler;
    }

    public string? Convert(JwtSecurityToken? source, string? destination, ResolutionContext context)
    {
        return source is null
            ? null
            : tokenHandler.WriteToken(source);
    }
}