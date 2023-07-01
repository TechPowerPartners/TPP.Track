using AutoMapper;
using Dlbb.Track.Application.Activities.Commands.CreateActivity;
using Dlbb.Track.Application.Activities.Commands.DeleteActivity;
using Dlbb.Track.Application.Activities.Commands.UpdateActivity;
using Dlbb.Track.Application.Activities.Queries.GetActivities;
using Dlbb.Track.Application.Activities.Queries.GetActivity;
using Dlbb.Track.WebApi.Models.Activities;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Dlbb.Track.WebApi.Controllers
{
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


		[HttpPost("Create")]
		public async Task<IActionResult> Create([FromBody] CreateActivityDto aDto)
		{
			var command = _mapper.Map<CreateActivityCommand>(aDto);

			try
			{
				var id = await _mediator.Send(command);
				return Ok(id);
			}
			catch (Exception e)
			{
				return BadRequest("Активность с таким именем уже существует");
			}
		}

		[HttpPut("Update")]
		public async Task<IActionResult> Update([FromBody] UpdateActivityDto aDto)
		{
			var command = _mapper.Map<UpdateActivityCommand>(aDto);

			try
			{
				var id = await _mediator.Send(command);
				return Ok(id);
			}
			catch (Exception e)
			{
				return BadRequest("Активность с таким именем уже существует");
			}		
		}

		[HttpDelete("{ActivityId}")]
		public async Task Delete(Guid ActivityId)
		{
			var command = new DeleteActivityCommand()
			{
				Id = ActivityId,
			};

			await _mediator.Send(command);
		}
	}
}
