using AutoMapper;
using Dlbb.Track.Application.Activities.Queries.GetActivity;
using Dlbb.Track.Domain.Abstractions.Repositories;
using Dlbb.Track.Persistence.Contexts;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Dlbb.Track.Application.Activities.Queries.GetActivities;
public class GetActivitiesQueryHandler :
	IRequestHandler<GetActivitiesQuery, List<ActivityVm>>
{
	private readonly IActivityRepository _rep;
	private readonly IMapper _mapper;

	public GetActivitiesQueryHandler(IActivityRepository rep, IMapper mapper)
	{
		_rep = rep;
		_mapper = mapper;
	}

	public Task<List<ActivityVm>> Handle
		(GetActivitiesQuery request, CancellationToken cancellationToken)
	{
		var activitesDb = _rep.GetAllActivities();

		List<ActivityVm> activityVms = new();
		foreach (var act in activitesDb)
		{
			activityVms.Add(_mapper.Map<ActivityVm>(act));
		}

		return Task.FromResult(activityVms);
	}
}
