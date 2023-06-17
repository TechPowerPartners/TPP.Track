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

		[HttpGet]
		public async Task<List<ActivityVm>> GetAll()
		{
			var query = new GetActivitiesQuery();

			return await _mediator.Send(query);
		}


		[HttpGet("{id}")]
		public async Task<ActivityVm> Get(Guid id)
		{
			var query = new GetActivityQuery()
			{
				Id = id
			};

			return await _mediator.Send(query);
		}


		[HttpPost]
		public async Task<Guid> Create([FromBody] CreateActivityDto aDto)
		{
			var command = _mapper.Map<CreateActivityCommand>(aDto);

			return await _mediator.Send(command);
		}

		[HttpPut]
		public async Task Update([FromBody] UpdateActivityDto aDto)
		{
			var command = _mapper.Map<UpdateActivityCommand>(aDto);

			await _mediator.Send(command);
		}

		[HttpDelete("{id}")]
		public async Task Delete(Guid id)
		{
			var command = new DeleteActivityCommand()
			{
				Id = id,
			};

			await _mediator.Send(command);
		}
	}
}
