using AutoMapper;
using Dlbb.Track.Application.Sessions.Queries.GetSession;
using Dlbb.Track.Domain.Abstractions.Repositories;
using Dlbb.Track.Domain.Specifications;
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
	    var	result  =await _rep.ToListAsync(new IsSpecUserSessions(request.userid).Expression, cancellationToken);
		return _mapper.Map<List<SessionVm>>(result);
	}
}
