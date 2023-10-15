using System.Security.Claims;
using AutoMapper;
using Dlbb.Track.Application.Activities.Commands.CreateActivity;
using Dlbb.Track.Application.Activities.Commands.DeleteActivity;
using Dlbb.Track.Application.Activities.Commands.UpdateActivity;
using Dlbb.Track.Application.Activities.Queries.GetActivities;
using Dlbb.Track.Application.Activities.Queries.GetActivity;
using Dlbb.Track.Domain.Enums;
using Dlbb.Track.WebApi.Models.Activities;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Dlbb.Track.WebApi.Controllers;

[Authorize]
[Route("api/[controller]")]
[ApiController]
public class ActivityController : ControllerBase
{
	private readonly IMapper _mapper;
	private readonly IMediator _mediator;

	public ActivityController(IMapper mapper, IMediator mediator)
	{
		_mapper = mapper;
		_mediator = mediator;
	}

	[Authorize]
	[HttpGet("GetAll")]
	public async Task<List<ActivityVm>> GetAll()
	{
		var userid = Guid.Parse(User.Claims.SingleOrDefault(c => ClaimTypes.IsPersistent == c.Type)!.Value);
		var query = new GetActivitiesQuery(userid);

		return await _mediator.Send(query);
	}


	[HttpGet("{activityId}")]
	public async Task<ActivityVm> Get(Guid activityId)
	{
		var query = new GetActivityQuery()
		{
			Id = activityId
		};

		return await _mediator.Send(query);
	}


	[Authorize]
	[HttpPost("Create")]
	public async Task<Guid> CreateLocal([FromBody] CreateActivityDto aDto)
	{
		var command = _mapper.Map<CreateActivityCommand>(aDto);

		command.AppUserId = Guid.Parse
			(User.Claims.SingleOrDefault(c => ClaimTypes.IsPersistent == c.Type)!.Value);

		command.IsGlobal = false;

		return await _mediator.Send(command);
	}

	[Authorize(Policy = nameof(RoleEnum.Admin))]
	[HttpPost("CreateGlobal")]
	public async Task<Guid> CreateGlobal([FromBody] CreateActivityDto aDto)
	{
		var command = _mapper.Map<CreateActivityCommand>(aDto);

		command.AppUserId = Guid.Parse
			(User.Claims.SingleOrDefault(c => ClaimTypes.IsPersistent == c.Type)!.Value);

		command.IsGlobal = true;

		return await _mediator.Send(command);
	}

	[HttpPut("UpdateLocal")]
	[Authorize]
	public async Task UpdateLocal([FromBody] UpdateActivityDto aDto)
	{
		var command = _mapper.Map<UpdateActivityCommand>(aDto);
		command.IsGlobal = false;

		await _mediator.Send(command);
	}

	[HttpPut("UpdateGlobal")]
	[Authorize(Policy = nameof(RoleEnum.Admin))]
	public async Task UpdateGlobal([FromBody] UpdateActivityDto aDto)
	{
		var command = _mapper.Map<UpdateActivityCommand>(aDto);
		command.IsGlobal = true;

		await _mediator.Send(command);
	}

	[HttpDelete("DeleteGlobal: {activityId}")]
	[Authorize(Policy = nameof(RoleEnum.Admin))]
	public Task DeleteGlobal(Guid activityId)
	{
		var command = new DeleteActivityCommand()
		{
			Id = activityId,
		};

		command.IsGlobal = true;

		return _mediator.Send(command);
	}

	[HttpDelete("DeleteLocal: {activityId}")]
	[Authorize]
	public Task DeleteLocal(Guid activityId)
	{
		var command = new DeleteActivityCommand()
		{
			Id = activityId,
		};

		command.IsGlobal = false;

		return _mediator.Send(command);
	}
}
