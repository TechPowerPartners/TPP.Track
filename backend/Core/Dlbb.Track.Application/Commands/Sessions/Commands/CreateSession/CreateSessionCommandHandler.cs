using AutoMapper;
using Dlbb.Track.Application.Exceptions;
using Dlbb.Track.Application.Sessions.Commands.CreateSession;
using Dlbb.Track.Common.Exceptions.Extensions;
using Dlbb.Track.Domain.Abstractions.Repositories;
using Dlbb.Track.Domain.Entities;
using Dlbb.Track.Domain.Specifications;
using Dlbb.Track.Persistence.Contexts;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Dlbb.Track.Application.Commands.Sessions.Commands.CreateSession;
public class CreateSessionCommandHandler : IRequestHandler<CreateSessionCommand, Guid>
{
	private readonly IMapper _mapper;
	private readonly IRepositoryWrapper _rep;

	public CreateSessionCommandHandler(IRepositoryWrapper rep, IMapper mapper)
	{
		_mapper = mapper;
		_rep = rep;
	}

	public async Task<Guid> Handle
		(CreateSessionCommand request,
		CancellationToken cancellationToken)
	{
		var activity = await _rep.ActivityRepository.FindActivityAsync
			(request.ActivityId, cancellationToken);

		activity!.ThrowUserFriendlyExceptionIfNull
			(status: Status.NotFound,
			message: $"Not found \"ActivityId\" : {request.ActivityId}");

		var user = await _rep.UserRepository.FindUserAsync
			(request.AppUserId, cancellationToken);

		user!.ThrowUserFriendlyExceptionIfNull
			(status: Status.NotFound,
			message: $"Not Found \"AppUserId\" : {request.AppUserId}");

		var session = _mapper.Map<Session>(request);

		session.Activity = activity!;
		session.AppUser = user!;

		await _rep.SessionRepository.CreateSessionAsync(session, cancellationToken);

		await _rep.Save(cancellationToken);

		return session.Id;
	}
}
