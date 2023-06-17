﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dlbb.Track.Domain.Entities;
using Dlbb.Track.Persistence.Contexts;
using MediatR;

namespace Dlbb.Track.Application.Activities.Commands.CreateActivity;
public class CreateActivityCommandHandler : IRequestHandler<CreateActivityCommand, Guid>
{
	private readonly AppDbContext _context;

	public CreateActivityCommandHandler(AppDbContext context)
	{
		_context = context;
	}

	public async Task<Guid> Handle(CreateActivityCommand request, CancellationToken cancellationToken)
	{
		var activity = new Activity()
		{
			Name = request.Name,
			Description = request.Description,
		};

		await _context.Activities.AddAsync(activity, cancellationToken);
		await _context.SaveChangesAsync(cancellationToken);

		return activity.Id;
	}
}