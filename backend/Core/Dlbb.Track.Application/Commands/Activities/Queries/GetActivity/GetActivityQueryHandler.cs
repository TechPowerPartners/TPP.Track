using AutoMapper;
using Dlbb.Track.Application.Exceptions;
using Dlbb.Track.Common.Exceptions.Extensions;
using Dlbb.Track.Domain.Abstractions.Repositories;
using Dlbb.Track.Persistence.Contexts;
using MediatR;

namespace Dlbb.Track.Application.Activities.Queries.GetActivity;
public class GetActivityQueryHandler : IRequestHandler<GetActivityQuery, ActivityVm>
{
	private readonly AppDbContext _context;
	private readonly IActivityRepository _rep;
	private readonly IMapper _mapper;

	public GetActivityQueryHandler(IActivityRepository rep, IMapper mapper)
	{
		_rep = rep;
		_mapper = mapper;
	}

	public async Task<ActivityVm> Handle
		(GetActivityQuery request, CancellationToken cancellationToken)
	{
		var activity = await _rep.FindActivityAsync
			(request.Id, cancellationToken);

		activity!.ThrowUserFriendlyExceptionIfNull
			(status: Status.NotFound,
			message: $"Not found \"Id\" : {request.Id}");

		return _mapper.Map<ActivityVm>(activity!);
	}
}
