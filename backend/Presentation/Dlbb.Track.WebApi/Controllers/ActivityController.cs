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
	[Produces("application/json")]
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

		[HttpGet]
		[ProducesResponseType(StatusCodes.Status200OK)]
		public async Task<ActionResult<List<ActivityVm>>> GetAll()
		{
			var query = new GetActivitiesQuery();

			return Ok(await _mediator.Send(query));
		}


		[HttpGet("{id}")]
		[ProducesResponseType(StatusCodes.Status200OK)]
		public async Task<ActionResult<ActivityVm>> Get(Guid id)
		{
			var query = new GetActivityQuery()
			{
				Id = id
			};

			return Ok(await _mediator.Send(query));
		}


		[HttpPost]
		[ProducesResponseType(StatusCodes.Status201Created)]
		public async Task<ActionResult<Guid>> Create([FromBody] CreateActivityDto aDto)
		{
			var command = _mapper.Map<CreateActivityCommand>(aDto);

			return Ok(await _mediator.Send(command));
		}

		[HttpPut]
		[ProducesResponseType(StatusCodes.Status204NoContent)]
		public async Task<IActionResult> Update([FromBody] UpdateActivityDto aDto)
		{
			var command = _mapper.Map<UpdateActivityCommand>(aDto);

			await _mediator.Send(command);

			return NoContent();
		}

		[HttpDelete("{id}")]
		[ProducesResponseType(StatusCodes.Status204NoContent)]
		public async Task<IActionResult> Delete(Guid id)
		{
			var command = new DeleteActivityCommand()
			{
				Id = id,
			};

			await _mediator.Send(command);

			return NoContent();
		}
	}
}
