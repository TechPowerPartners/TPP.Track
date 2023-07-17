using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Dlbb.Track.Domain.Entities;
using Microsoft.IdentityModel.Tokens;
using zgmapi.Data;

namespace Dlbb.Track.Application.Accounts.Shared;
public static class AutorizeUtils
{
	public static JwtSecurityToken CreateJwt(List<Claim> claims)
	{
		var jwt = new JwtSecurityToken(
			issuer: JwtOptions.ISSUER,
			audience: JwtOptions.AUDIENCE,
			claims: claims,
			expires: DateTime.UtcNow.Add(TimeSpan.FromDays(1)),
			signingCredentials: new SigningCredentials(JwtOptions.GetSymmetricSecurityKey(),
													SecurityAlgorithms.HmacSha256)
		);
		return jwt;
	}

	public static List<Claim> GetClaimsFor(AppUser user)
	{
		var claims = new List<Claim>
		{
			new Claim(ClaimTypes.Email, user.Email),
			new Claim(ClaimTypes.IsPersistent, user.Id.ToString()),
			new Claim(ClaimTypes.Role, user.Role.ToString())
		};
		return claims;
	}
}
