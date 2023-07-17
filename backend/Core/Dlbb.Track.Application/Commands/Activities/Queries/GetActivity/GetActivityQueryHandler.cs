using AutoMapper;
using Dlbb.Track.Application.Exceptions;
using Dlbb.Track.Common.Exceptions.Extensions;
using Dlbb.Track.Domain.Specifications;
using Dlbb.Track.Persistence.Contexts;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Dlbb.Track.Application.Activities.Queries.GetActivity;
public class GetActivityQueryHandler : IRequestHandler<GetActivityQuery, ActivityVm>
{
	private readonly AppDbContext _context;
	private readonly IMapper _mapper;

	public GetActivityQueryHandler(AppDbContext context, IMapper mapper)
	{
		_context = context;
		_mapper = mapper;
	}

	public async Task<ActivityVm> Handle
		(GetActivityQuery request, CancellationToken cancellationToken)
	{
		var activity = await _context.Activities.SingleOrDefaultAsync
			(new IsSpecActivity(request.Id), cancellationToken);

		activity!.ThrowUserFriendlyExceptionIfNull
			(status: Status.NotFound,
			message: $"Not found \"Id\" : {request.Id}");

		return _mapper.Map<ActivityVm>(activity!);
	}
}
