using System.Security.Claims;
using Dlbb.Track.Persistence.Contexts;
using MediatR;
using System.IdentityModel.Tokens.Jwt;
using zgmapi.Data;
using Microsoft.IdentityModel.Tokens;
using Dlbb.Track.Domain.Entities;

namespace Dlbb.Track.Application.Accounts.Commands.Register;

public class RegisterCommandHandler : IRequestHandler<RegisterCommand, Guid>
{
	private readonly AppDbContext _dbContext;

	public RegisterCommandHandler(AppDbContext dbContext)
	{
		_dbContext = dbContext;
	}

	public async Task<Guid> Handle(RegisterCommand request, CancellationToken cancellationToken)
	{
		
	}

	private JwtSecurityToken CreateJwt(List<Claim> claims)
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

	private List<Claim> GetClaimsFor(AppUser user)
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
