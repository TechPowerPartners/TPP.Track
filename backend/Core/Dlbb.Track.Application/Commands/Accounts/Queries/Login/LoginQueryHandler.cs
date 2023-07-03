using System;
using System.Collections.Generic;
using System.Data;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Dlbb.Track.Application.Accounts.Shared;
using Dlbb.Track.Application.Exceptions;
using Dlbb.Track.Common.Exceptions.Extensions;
using Dlbb.Track.Persistence.Contexts;
using Dlbb.Track.Persistence.Services;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Dlbb.Track.Application.Accounts.Queries.Login;
public class LoginQueryHandler : IRequestHandler<LoginQuery, JwtSecurityToken>
{
	private readonly AppDbContext _dbContext;
	private readonly IMapper _mapper;
	private readonly PasswordHasher _hasher;

	public LoginQueryHandler(AppDbContext dbContext, IMapper mapper, PasswordHasher hasher)
	{
		_dbContext = dbContext;
		_mapper = mapper;
		_hasher = hasher;
	}

	public async Task<JwtSecurityToken> Handle(LoginQuery request, CancellationToken cancellationToken)
	{
		var userDb = await _dbContext.AppUsers.SingleOrDefaultAsync(u => u.Email == request.ExpectedEmail);

		userDb!.ThrowUserFriendlyExceptionIfNull
			(status: Status.NotFound,
			message: $"Not Found \"Email\" : {request.ExpectedEmail}");

		var isTruePassword = _hasher.Verify(request.ExpectedPassword, userDb!.PassworHash);

		if (isTruePassword)
		{
			var claims = AutorizeUtils.GetClaimsFor(userDb);
			var jwt = AutorizeUtils.CreateJwt(claims);
			return jwt;
		}
		else
		{
			throw new UserFriendlyException
				(Status.NotFound, $"Not Found \"Password\" : {request.ExpectedPassword}");
		}
	}
}
