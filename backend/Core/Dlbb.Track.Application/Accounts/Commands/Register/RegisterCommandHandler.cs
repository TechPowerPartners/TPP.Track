using System.Security.Claims;
using Dlbb.Track.Persistence.Contexts;
using MediatR;
using System.IdentityModel.Tokens.Jwt;
using zgmapi.Data;
using Microsoft.IdentityModel.Tokens;
using Dlbb.Track.Domain.Entities;
using Dlbb.Track.Application.Common.Utils;
using Microsoft.EntityFrameworkCore;

namespace Dlbb.Track.Application.Accounts.Commands.Register;

public class RegisterCommandHandler : IRequestHandler<RegisterCommand, JwtSecurityToken>
{
	private readonly AppDbContext _dbContext;

	public RegisterCommandHandler(AppDbContext dbContext)
	{
		_dbContext = dbContext;
	}

	public async Task<JwtSecurityToken> Handle(RegisterCommand request, CancellationToken cancellationToken)
	{
		var appUserDb = new AppUser()
		{
			Email = request.Email,
			PassworHash = PasswordHasher.Hash(request.Password),
			Role = Domain.Enums.RoleEnum.User,
			UserName = request.Email
		};


		try
		{
			await _dbContext.AppUsers.AddAsync(appUserDb);
			await _dbContext.SaveChangesAsync();
		}
		catch (Npgsql.PostgresException error)
		{
			throw new Exception("Ошибка при сохранении данных в БД: " + error);
		}


		var res = await _dbContext.AppUsers.SingleAsync(u => u.Email == request.Email);

		var claims = GetClaimsFor(res);
		var jwt = CreateJwt(claims);

		return jwt;
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
