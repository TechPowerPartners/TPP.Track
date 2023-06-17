using AutoMapper;
using Dlbb.Track.Application.Sessions.Commands.CreateSession;
using Dlbb.Track.Application.Sessions.Commands.EndSession;
using Dlbb.Track.Application.Sessions.Queries.GetSession;
using Dlbb.Track.Application.Sessions.Queries.GetSessions;
using Dlbb.Track.WebApi.Models.Sessions;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Dlbb.Track.WebApi.Controllers;

[Produces("aplication/json")]
[Route("api/[controller]")]
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

	[HttpGet("getAll")]
	[ProducesResponseType(StatusCodes.Status200OK)]
	public async Task<ActionResult<List<SessionVm>>> GetAll()
	{
		return Ok(await _mediator.Send(new GetSessionsQuery()));
	}

	[HttpGet("{id}")]
	[ProducesResponseType(StatusCodes.Status200OK)]
	public async Task<ActionResult<SessionVm>> GetById(Guid id)
	{
		return Ok(await _mediator.Send(new GetSessionQuery() { Id = id }));
	}

	[HttpPost("Create")]
	[ProducesResponseType(StatusCodes.Status201Created)]
	public async Task<ActionResult<Guid>> CreateSession([FromBody] CreateSessionDto sDto)
	{
		var command = _mapper.Map<CreateSessionCommand>(sDto);

		return Ok(await _mediator.Send(command));
	}

	[HttpPut]
	[ProducesResponseType(StatusCodes.Status204NoContent)]
	public async Task<ActionResult> EndSession([FromBody] EndSessionDto sDto)
	{
		var command = _mapper.Map<EndSessionCommand>(sDto);

		await _mediator.Send(command);

		return NoContent();
	}
}
