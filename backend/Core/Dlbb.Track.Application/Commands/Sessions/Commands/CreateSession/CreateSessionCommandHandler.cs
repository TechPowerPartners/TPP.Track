using Dlbb.Track.Application.Exceptions;
using Dlbb.Track.Application.Sessions.Commands.CreateSession;
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
		request.AppUserId = Guid.Parse("b1ce9b6e-17c2-4041-86a0-16f3081cc299");

		var activity = await _dbContext.Activities.SingleOrDefaultAsync
			(a => a.Id == request.ActivityId, cancellationToken);

		var user = await _dbContext.AppUsers.SingleOrDefaultAsync
			(u => u.Id == request.AppUserId, cancellationToken);

		if (activity is null)
		{
			throw new UserFriendlyException
				(Status.NotFound, $"Not found \"ActivityId\" : {request.ActivityId}");
		}

		if (user is null)
		{
			throw new UserFriendlyException
				(Status.NotFound, $"Not Found \"AppUserId\" : {request.AppUserId}");
		}

		var session = new Session()
		{
			StartTime = request.StartTime,
			Activity = activity,
			ActivityId = request.ActivityId,
			AppUser = user,
			AppUserId = request.AppUserId,
		};

		await _dbContext.Sessions.AddAsync(session, cancellationToken);

		await _dbContext.SaveChangesAsync(cancellationToken);

		return session.Id;
	}
}
