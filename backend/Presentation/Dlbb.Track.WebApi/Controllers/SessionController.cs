﻿using System.Security.Claims;
using AutoMapper;
using Dlbb.Track.Application.Activities.Queries.GetActivities;
using Dlbb.Track.Application.Sessions.Commands.CreateSession;
using Dlbb.Track.Application.Sessions.Commands.EndSession;
using Dlbb.Track.Application.Sessions.Queries.GetSession;
using Dlbb.Track.Application.Sessions.Queries.GetSessions;
using Dlbb.Track.WebApi.Models.Sessions;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Dlbb.Track.WebApi.Controllers;

[Route("api/[controller]")]
[Authorize]
[ApiController]
public class SessionController : ControllerBase
{
	private readonly IMediator _mediator;
	private readonly IMapper _mapper;

	public SessionController(IMediator mediator, IMapper mapper)
	{
		_mediator = mediator;
		_mapper = mapper;
	}

	[HttpGet("GetAll")]
	public async Task<List<SessionVm>> GetAll()
	{
		var userid = Guid.Parse(User.Claims.SingleOrDefault(c => ClaimTypes.IsPersistent == c.Type)!.Value);
		var query = new GetSessionsQuery(userid);
		return await _mediator.Send(query);
	}

	[HttpGet("{SessionId}")]
	public async Task<SessionVm> GetById(Guid SessionId)
	{
		return await _mediator.Send(new GetSessionQuery() { Id = SessionId });
	}

	[Authorize]
	[HttpPost("Create")]
	public async Task<Guid> CreateSession([FromBody] CreateSessionDto sDto)
	{
		var command = _mapper.Map<CreateSessionCommand>(sDto);
		command.AppUserId = Guid.Parse
			(User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.IsPersistent)?.Value!);

		return await _mediator.Send(command);
	}

	[HttpPut("End")]
	public async Task EndSession([FromBody] EndSessionDto sDto)
	{
		var command = _mapper.Map<EndSessionCommand>(sDto);

		await _mediator.Send(command);
	}
}
