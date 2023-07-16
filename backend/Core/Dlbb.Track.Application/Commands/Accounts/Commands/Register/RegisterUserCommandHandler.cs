﻿using System.IdentityModel.Tokens.Jwt;
using AutoMapper;
using Dlbb.Track.Application.Accounts.Shared;
using Dlbb.Track.Common.Exceptions.Extensions;
using Dlbb.Track.Domain.Entities;
using Dlbb.Track.Domain.Enums;
using Dlbb.Track.Persistence.Contexts;
using Dlbb.Track.Persistence.Services;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Dlbb.Track.Application.Accounts.Commands.Register;

public class RegisterUserCommandHandler : IRequestHandler<RegisterUserCommand, JwtSecurityToken>
{
	private readonly IMapper _mapper;
	private readonly AppDbContext _dbContext;
	private readonly PasswordHasher _hasher;

	public RegisterUserCommandHandler
		(AppDbContext dbContext,
		PasswordHasher hasher,
		IMapper mapper)
	{
		_mapper = mapper;
		_dbContext = dbContext;
		_hasher = hasher;
	}

	public async Task<JwtSecurityToken> Handle
		(RegisterUserCommand request,
		CancellationToken cancellationToken)
	{
		var entity = _mapper.Map<AppUser>(request);

		entity.Role = RoleEnum.User;
		entity.PasswordHash = _hasher.Hash(request.Password);

		(await _dbContext.AppUsers.AnyAsync
		(u => u.Email == entity.Email ||
		u.UserName == entity.UserName))
		.ThrowUserFriendlyExceptionIfTrue
		(Exceptions.Status.AlreadyExists, $"this email address or user name is taken");

		try
		{
			await _dbContext.AppUsers.AddAsync(entity);
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