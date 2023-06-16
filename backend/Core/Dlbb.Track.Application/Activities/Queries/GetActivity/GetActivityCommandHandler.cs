using AutoMapper;
using Dlbb.Track.Persistence.Contexts;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Dlbb.Track.Application.Activities.Queries.GetActivity;
public class GetActivityCommandHandler : IRequestHandler<GetActivityCommand, ActivityVm>
{
	private readonly AppDbContext _context;
	private readonly IMapper _mapper;

	public GetActivityCommandHandler(AppDbContext context, IMapper mapper)
	{
		_context = context;
		_mapper = mapper;
	}

	public async Task<ActivityVm> Handle
		(GetActivityCommand request, CancellationToken cancellationToken)
	{
		var activity = await _context.Activities.FirstAsync
			(a => a.Id == request.Id, cancellationToken);

		return _mapper.Map<ActivityVm>(activity);
	}
}
