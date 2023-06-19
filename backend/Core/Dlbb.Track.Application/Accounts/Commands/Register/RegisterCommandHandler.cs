using System.Security.Claims;
using Dlbb.Track.Persistence.Contexts;
using MediatR;
using System.IdentityModel.Tokens.Jwt;
using Dlbb.Track.Application.Accounts.Shared;
using Dlbb.Track.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Dlbb.Track.Persistence.Services;

namespace Dlbb.Track.Application.Accounts.Commands.Register;

public class RegisterCommandHandler : IRequestHandler<RegisterCommand, JwtSecurityToken>
{
	private readonly AppDbContext _dbContext;
	private readonly PasswordHasher _hasher;

	public RegisterCommandHandler(AppDbContext dbContext, PasswordHasher hasher)
	{
		_dbContext = dbContext;
		_hasher = hasher;
	}

	public async Task<JwtSecurityToken> Handle(RegisterCommand request, CancellationToken cancellationToken)
	{
		var appUserDb = new AppUser()
		{
			Email = request.Email,
			PasswordHash = _hasher.Hash(request.Password),
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

		var claims = AutorizeUtils.GetClaimsFor(res);
		var jwt = AutorizeUtils.CreateJwt(claims);

		return jwt;
 	}
}
