﻿using AutoMapper;
using Dlbb.Track.Application.Activities.Queries.GetActivity;
using Dlbb.Track.Persistence.Contexts;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Dlbb.Track.Application.Activities.Queries.GetActivities;
public class GetActivitiesQueryHandler :
	IRequestHandler<GetActivitiesQuery, List<ActivityVm>>
{
	private readonly AppDbContext _context;
	private readonly IMapper _mapper;

	public GetActivitiesQueryHandler(AppDbContext context, IMapper mapper)
	{
		_context = context;
		_mapper = mapper;
	}

	public async Task<List<ActivityVm>> Handle
		(GetActivitiesQuery request, CancellationToken cancellationToken)
	{
		var activitesDb = await _context.Activities
			.ToListAsync();

		List<ActivityVm> activityVms = new();
		foreach (var act in activitesDb)
		{
			activityVms.Add(_mapper.Map<ActivityVm>(act));
		}

		return activityVms;
	}
}