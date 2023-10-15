using System.IdentityModel.Tokens.Jwt;
using Dlbb.Track.Application.Accounts.Shared;
using Dlbb.Track.Application.Exceptions;
using Dlbb.Track.Common.Exceptions.Extensions;
using Dlbb.Track.Domain.Abstractions.Repositories;
using Dlbb.Track.Domain.Specifications;
using Dlbb.Track.Domain.Specifications.Users;
using Dlbb.Track.Persistence.Contexts;
using Dlbb.Track.Persistence.Services;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Dlbb.Track.Application.Accounts.Queries.Login;
public class LoginUserQueryHandler : IRequestHandler<LoginUserQuery, JwtSecurityToken>
{
	private readonly IUserRepository _userRep;
	private readonly PasswordHasher _hasher;

	public LoginUserQueryHandler
		(IUserRepository userRep,
		PasswordHasher hasher)
	{
		_userRep = userRep;
		_hasher = hasher;
	}

	public async Task<JwtSecurityToken> Handle
		(LoginUserQuery request,
		CancellationToken cancellationToken)
	{
		var userDb = await _userRep.SingleOrDefaultAsync
			(new UserByEmailSpec(request.ExpectedEmail),cancellationToken);

		userDb!.ThrowUserFriendlyExceptionIfNull
			(status: Status.NotFound,
			message: $"Not Found \"Email\" : {request.ExpectedEmail}");

		(_hasher.Verify(request.ExpectedPassword, userDb!.PasswordHash) == false)
			.ThrowUserFriendlyExceptionIfTrue
			(Status.NotFound, $"Not Found \"Password\" : {request.ExpectedPassword}");

		var claims = AutorizeUtils.GetClaimsFor(userDb);
		var jwt = AutorizeUtils.CreateJwt(claims);

		return jwt;
	}
}
