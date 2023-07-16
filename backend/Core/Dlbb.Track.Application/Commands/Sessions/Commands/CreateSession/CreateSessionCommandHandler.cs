using Dlbb.Track.Application.Exceptions;
using Dlbb.Track.Application.Sessions.Commands.CreateSession;
using Dlbb.Track.Common.Exceptions.Extensions;
using Dlbb.Track.Domain.Entities;
using Dlbb.Track.Persistence.Contexts;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Dlbb.Track.Application.Commands.Sessions.Commands.CreateSession;
public class CreateSessionCommandHandler : IRequestHandler<CreateSessionCommand, Guid>
{
	private readonly AppDbContext _dbContext;

	public CreateSessionCommandHandler(AppDbContext dbContext)
	{
		_dbContext = dbContext;
	}

	public async Task<Guid> Handle(CreateSessionCommand request, CancellationToken cancellationToken)
	{
		var activity = await _dbContext.Activities.SingleOrDefaultAsync
			(a => a.Id == request.ActivityId, cancellationToken);

		var user = await _dbContext.AppUsers.SingleOrDefaultAsync
			(u => u.Id == request.AppUserId, cancellationToken);

		activity!.ThrowUserFriendlyExceptionIfNull
			(status: Status.NotFound,
			message: $"Not found \"ActivityId\" : {request.ActivityId}");

		user!.ThrowUserFriendlyExceptionIfNull
			(status: Status.NotFound,
			message: $"Not Found \"AppUserId\" : {request.AppUserId}");

		var session = new Session()
		{
			StartTime = request.StartTime,
			Activity = activity!,
			ActivityId = request.ActivityId,
			AppUser = user!,
			AppUserId = request.AppUserId,
		};

		await _dbContext.Sessions.AddAsync(session, cancellationToken);

		await _dbContext.SaveChangesAsync(cancellationToken);

		return session.Id;
	}
}
