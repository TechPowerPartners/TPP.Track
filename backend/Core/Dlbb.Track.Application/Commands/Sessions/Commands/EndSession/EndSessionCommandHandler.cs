using AutoMapper;
using Dlbb.Track.Application.Exceptions;
using Dlbb.Track.Common.Exceptions.Extensions;
using Dlbb.Track.Domain.Abstractions.Repositories;
using Dlbb.Track.Domain.Specifications;
using Dlbb.Track.Persistence.Contexts;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Dlbb.Track.Application.Sessions.Commands.EndSession;
public class EndSessionCommandHandler : IRequestHandler<EndSessionCommand>
{
	private readonly IMapper _mapper;
	private readonly IRepositoryWrapper _rep;

	public EndSessionCommandHandler(IRepositoryWrapper rep, IMapper mapper)
	{
		_mapper = mapper;
		_rep = rep;
	}

	public async Task<Unit> Handle
		(EndSessionCommand request,
		CancellationToken cancellationToken)
	{
		var session = await _rep.SessionRepository.FindSessionAsync
			(request.Id, cancellationToken);

		session!.ThrowUserFriendlyExceptionIfNull
			(status: Status.NotFound,
			message: $"Not found \"Id\" : {request.Id}");

		session = _mapper.Map(request, session);

		_rep.SessionRepository.UpdateSession(session!);

		await _rep.Save(cancellationToken);

		return Unit.Value;
	}
}
