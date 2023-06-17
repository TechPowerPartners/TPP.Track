using AutoMapper;
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
		var activity = await _context.Activities.FirstAsync
			(a => a.Id == request.Id, cancellationToken);

		return _mapper.Map<ActivityVm>(activity);
	}
}
