using System.IdentityModel.Tokens.Jwt;
using AutoMapper;
using Dlbb.Track.Application.Accounts.Commands.Register;
using Dlbb.Track.Application.Accounts.Queries.GetUser;
using Dlbb.Track.Application.Accounts.Queries.Login;
using Dlbb.Track.WebApi.Models.Account;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Dlbb.Track.WebApi.Controllers;
[Route("api/[controller]")]
[ApiController]
public class AccountController : ControllerBase
{
	private readonly IMediator _mediator;
	private readonly IMapper _mapper;

	public AccountController(IMediator mediator, IMapper mapper)
	{
		_mediator = mediator;
		_mapper = mapper;
	}


	/// <summary>
	/// Зарегистрировать аккаунт
	/// </summary>
	/// <param name="loginVm">Email и пароль</param>
	/// <returns>Jwt token</returns>
	[AllowAnonymous]
	[HttpPost("Register")]
	public async Task<string> Register([FromBody] RegisterDto sDto)
	{
		var command = _mapper.Map<RegisterUserCommand>(sDto);

		var jwt = await _mediator.Send(command);

		return new JwtSecurityTokenHandler().WriteToken(jwt);
	}

	[Authorize]
	[HttpGet("Info")]
	public async Task<AppUserVM> InfoAsync()
	{
		var claims = User.Claims.ToList();

		return await _mediator.Send(new GetUserQuery() { Claims = claims });
	}

	/// <summary>
	/// Зайти в аккаунт
	/// </summary>
	/// <param name="loginVm">Email и пароль</param>
	/// <returns>Jwt token</returns>
	[AllowAnonymous]
	[HttpPost("Login")]
	public async Task<string> Login([FromBody] LoginVm loginVm)
	{
		var command = _mapper.Map<LoginUserQuery>(loginVm);

		var jwt = await _mediator.Send(command);

		return new JwtSecurityTokenHandler().WriteToken(jwt);
	}
}
