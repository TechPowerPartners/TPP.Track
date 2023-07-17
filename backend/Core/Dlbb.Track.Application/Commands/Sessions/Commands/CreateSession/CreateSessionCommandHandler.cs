using AutoMapper;
using Dlbb.Track.Application.Exceptions;
using Dlbb.Track.Application.Sessions.Commands.CreateSession;
using Dlbb.Track.Common.Exceptions.Extensions;
using Dlbb.Track.Domain.Entities;
using Dlbb.Track.Domain.Specifications;
using Dlbb.Track.Persistence.Contexts;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Dlbb.Track.Application.Commands.Sessions.Commands.CreateSession;
public class CreateSessionCommandHandler : IRequestHandler<CreateSessionCommand, Guid>
{
	private readonly IMapper _mapper;
	private readonly AppDbContext _dbContext;

	public CreateSessionCommandHandler(AppDbContext dbContext, IMapper mapper)
	{
		_mapper = mapper;
		_dbContext = dbContext;
	}

	public async Task<Guid> Handle
		(CreateSessionCommand request,
		CancellationToken cancellationToken)
	{
		var activity = await _dbContext.Activities.SingleOrDefaultAsync
			(new IsSpecActivity(request.ActivityId), cancellationToken);

		activity!.ThrowUserFriendlyExceptionIfNull
			(status: Status.NotFound,
			message: $"Not found \"ActivityId\" : {request.ActivityId}");

		var user = await _dbContext.AppUsers.SingleOrDefaultAsync
			(new IsSpecUser(request.AppUserId), cancellationToken);

		user!.ThrowUserFriendlyExceptionIfNull
			(status: Status.NotFound,
			message: $"Not Found \"AppUserId\" : {request.AppUserId}");

		var session = _mapper.Map<Session>(request);

		session.Activity = activity!;
		session.AppUser = user!;

		await _dbContext.Sessions.AddAsync(session, cancellationToken);

		await _dbContext.SaveChangesAsync(cancellationToken);

		return session.Id;
	}
}
