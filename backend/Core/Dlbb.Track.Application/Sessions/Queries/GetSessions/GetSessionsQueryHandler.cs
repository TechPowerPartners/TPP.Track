using AutoMapper;
using Dlbb.Track.Application.Sessions.Queries.GetSession;
using Dlbb.Track.Persistence.Contexts;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Dlbb.Track.Application.Sessions.Queries.GetSessions;
public class GetSessionsQueryHandler : IRequestHandler<GetSessionsQuery, List<SessionVm>>
{
	private readonly IMapper _mapper;
	private readonly AppDbContext _dbContext;

	public GetSessionsQueryHandler(AppDbContext dbContext,IMapper mapper)
	{
		_mapper = mapper;
		_dbContext = dbContext;
	}

	public async Task<List<SessionVm>> Handle
		(GetSessionsQuery request, CancellationToken cancellationToken)
	{
		return await _dbContext.Sessions
			.Select(s => _mapper.Map<SessionVm>(s))
			.ToListAsync(cancellationToken);
	}
}
