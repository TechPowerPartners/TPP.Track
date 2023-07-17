using AutoMapper;
using Dlbb.Track.Application.Exceptions;
using Dlbb.Track.Common.Exceptions.Extensions;
using Dlbb.Track.Domain.Abstractions.Repositories;
using MediatR;

namespace Dlbb.Track.Application.Sessions.Queries.GetSession;
public class GetSessionQueryHandler : IRequestHandler<GetSessionQuery, SessionVm>
{
	private readonly ISessionRepository _rep;
	private readonly IMapper _mapper;

	public GetSessionQueryHandler(ISessionRepository rep, IMapper mapper)
	{
		_rep = rep;
		_mapper = mapper;
	}

	public async Task<SessionVm> Handle(GetSessionQuery request, CancellationToken cancellationToken)
	{
		var session = await _rep.FindSessionAsync
			(request.Id, cancellationToken);

		session!.ThrowUserFriendlyExceptionIfNull
			(status: Status.NotFound,
			message: $"Not found \"Id\" : {request.Id}");

		var result = _mapper.Map<SessionVm>(session);

		return result;
	}
}
