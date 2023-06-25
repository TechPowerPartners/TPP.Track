﻿using MediatR;

namespace Dlbb.Track.Application.Activities.Commands.CreateActivity
{
	public class CreateActivityCommand : IRequest<Guid>
	{
		public string Name { get; set; }
		public string? Description { get; set; }
	}
}