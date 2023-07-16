using AutoMapper;
using Dlbb.Track.Application.Activities.Commands.CreateActivity;
using Dlbb.Track.Application.Activities.Commands.DeleteActivity;
using Dlbb.Track.Application.Activities.Commands.UpdateActivity;
using Dlbb.Track.Application.Activities.Queries.GetActivities;
using Dlbb.Track.Application.Activities.Queries.GetActivity;
using Dlbb.Track.WebApi.Models.Activities;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Dlbb.Track.WebApi.Controllers;

[Authorize]
[Route("api/[controller]")]
public class ActivityController : ControllerBase
{
	private readonly IMapper _mapper;
	private readonly IMediator _mediator;

	public ActivityController(IMapper mapper, IMediator mediator)
	{
		_mapper = mapper;
		_mediator = mediator;
	}

	[HttpGet("GetAll")]
	public async Task<List<ActivityVm>> GetAll()
	{
		var query = new GetActivitiesQuery();

		return await _mediator.Send(query);
	}


	[HttpGet("{ActivityId}")]
	public async Task<ActivityVm> Get(Guid ActivityId)
	{
		var query = new GetActivityQuery()
		{
			Id = ActivityId
		};

		return await _mediator.Send(query);
	}


    [Authorize]
    [HttpPost("Create")]
    public async Task<Guid> Create([FromBody] CreateActivityDto aDto)
    {
        aDto.Claims = User.Claims.ToList();
        var command = _mapper.Map<CreateActivityCommand>(aDto);

        return await _mediator.Send(command);
    }

    /// <summary>
    /// Обновить свою активность
    /// </summary>
    /// <param name="aDto">Claims не обязателен</param>
    /// <returns></returns>
    [HttpPut("Update")]
    [Authorize]
    public async Task Update([FromBody] UpdateActivityDto aDto)
    {
        aDto.Claims = User.Claims.ToList();
        var command = _mapper.Map<UpdateActivityCommand>(aDto);

        await _mediator.Send(command);
    }

    [HttpDelete("{ActivityId}")]
    [Authorize]
    public async Task Delete(Guid ActivityId)
    {
        var command = new DeleteActivityCommand()
        {
            Id = ActivityId,
        };

        await _mediator.Send(command);
    }
}
