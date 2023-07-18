using System.IdentityModel.Tokens.Jwt;
using AutoMapper;
using Dlbb.Track.Application.Accounts.Shared;
using Dlbb.Track.Common.Exceptions.Extensions;
using Dlbb.Track.Domain.Abstractions.Repositories;
using Dlbb.Track.Domain.Entities;
using Dlbb.Track.Domain.Enums;
using Dlbb.Track.Domain.Specifications;
using Dlbb.Track.Persistence.Contexts;
using Dlbb.Track.Persistence.Services;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Dlbb.Track.Application.Accounts.Commands.Register;

public class RegisterUserCommandHandler : IRequestHandler<RegisterUserCommand, JwtSecurityToken>
{
	private readonly IMapper _mapper;
	private readonly IUserRepository _userRepository;
	private readonly PasswordHasher _hasher;

	public RegisterUserCommandHandler
		(IUserRepository userRepository,
		PasswordHasher hasher,
		IMapper mapper)
	{
		_mapper = mapper;
		_userRepository= userRepository;
		_hasher = hasher;
	}

	public async Task<JwtSecurityToken> Handle
		(RegisterUserCommand request,
		CancellationToken cancellationToken)
	{
		var entity = _mapper.Map<AppUser>(request);

		entity.Role = RoleEnum.User;
		entity.PasswordHash = _hasher.Hash(request.Password);

		(await _userRepository.AnyAsync
		(new IsSpecUser(entity.Id, entity.Email), cancellationToken))
		.ThrowUserFriendlyExceptionIfTrue
		(Exceptions.Status.AlreadyExists, $"this email address or user name is taken");

		try
		{
			await _userRepository.AddAsync(entity,cancellationToken);
			await _userRepository.SaveAsync(cancellationToken);
		}
		catch (Npgsql.PostgresException error)
		{
			throw new Exception("Ошибка при сохранении данных в БД: " + error);
		}

		var res = await _userRepository.SingleOrDefaultAsync
			(new IsSpecUser(entity.Email),cancellationToken);

		var claims = AutorizeUtils.GetClaimsFor(res);
		var jwt = AutorizeUtils.CreateJwt(claims);

		return jwt;
	}
}
