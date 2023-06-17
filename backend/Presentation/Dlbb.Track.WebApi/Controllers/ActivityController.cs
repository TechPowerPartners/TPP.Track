﻿using AutoMapper;
using Dlbb.Track.Application.Activities.Commands.CreateActivity;
using Dlbb.Track.Application.Activities.Commands.DeleteActivity;
using Dlbb.Track.Application.Activities.Commands.UpdateActivity;
using Dlbb.Track.Application.Activities.Queries.GetActivities;
using Dlbb.Track.Application.Activities.Queries.GetActivity;
using Dlbb.Track.Domain.Entities;
using Dlbb.Track.Persistence.Contexts;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Dlbb.Track.WebApi.Controllers
{
	[Produces("application/json")]
	[Route("api/[controller]")]
	public class ActivityController : ControllerBase
	{
		private readonly AppDbContext _context;
		private readonly IMapper _mapper;
		private readonly IMediator _mediator;

		public ActivityController(AppDbContext context,IMapper mapper,IMediator mediator)
		{
			_context = context;
			_mapper = mapper;
			_mediator= mediator;
		}

		[HttpGet]
		[ProducesResponseType(StatusCodes.Status200OK)]
		public async Task<ActionResult<List<ActivityVm>>> GetAll()
		{
			var query = new GetActivitiesCommand();

			return Ok(await _mediator.Send(query));
		}


		[HttpGet("{id}")]
		[ProducesResponseType(StatusCodes.Status200OK)]
		public async Task<ActionResult<ActivityVm>> Get(Guid id)
		{
			var query = new GetActivityCommand()
			{
				Id = id
			};

			return Ok(await _mediator.Send(query));
		}


		[HttpPost]
		[ProducesResponseType(StatusCodes.Status201Created)]
		public async Task<ActionResult<Guid>> Create([FromBody] ActivityVm aVm)
		{
			var command = new CreateActivityCommand()
			{
				Name = aVm.Name,
				Description = aVm.Description,
			};

			return Ok(await _mediator.Send(command));
		}

		[HttpPut]
		[ProducesResponseType(StatusCodes.Status204NoContent)]
		public async Task<IActionResult> Update([FromBody] ActivityVm activity)
		{
			var command = new UpdateActivityCommand()
			{
				Id = activity.Id,
				Name = activity.Name,
				Description = activity.Description,
			};

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