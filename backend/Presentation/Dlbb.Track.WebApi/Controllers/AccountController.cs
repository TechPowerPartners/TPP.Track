using System.IdentityModel.Tokens.Jwt;
using AutoMapper;
using Dlbb.Track.Application.Accounts.Commands.Register;
using Dlbb.Track.Application.Sessions.Commands.CreateSession;
using Dlbb.Track.WebApi.Models.Account;
using Dlbb.Track.WebApi.Models.Sessions;
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

	[AllowAnonymous]
	[HttpPost("Register")]
	public async Task<string> CreateSession([FromBody] RegisterDto sDto)
	{
		var command = _mapper.Map<RegisterCommand>(sDto);

		var jwt = await _mediator.Send(command);

		return new JwtSecurityTokenHandler().WriteToken(jwt);
	}
}
