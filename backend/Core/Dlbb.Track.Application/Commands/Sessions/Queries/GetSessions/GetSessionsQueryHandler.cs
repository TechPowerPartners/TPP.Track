using AutoMapper;
using Dlbb.Track.Application.Sessions.Queries.GetSession;
using Dlbb.Track.Domain.Abstractions.Repositories;
using MediatR;

namespace Dlbb.Track.Application.Sessions.Queries.GetSessions;
public class GetSessionsQueryHandler : IRequestHandler<GetSessionsQuery, List<SessionVm>>
{
	private readonly ISessionRepository _rep;
	private readonly IMapper _mapper;

	public GetSessionsQueryHandler(ISessionRepository rep, IMapper mapper)
	{
		_rep = rep;
		_mapper = mapper;
	}

	public async Task<List<SessionVm>> Handle
		(GetSessionsQuery request, CancellationToken cancellationToken)
	{
		return await _rep.SelectAsync(s => _mapper.Map<SessionVm>(s), cancellationToken);
	}
}
