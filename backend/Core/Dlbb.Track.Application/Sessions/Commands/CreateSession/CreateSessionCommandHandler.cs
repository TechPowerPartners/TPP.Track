using Dlbb.Track.Application.Exceptions;
using Dlbb.Track.Domain.Entities;
using Dlbb.Track.Persistence.Contexts;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Dlbb.Track.Application.Sessions.Commands.CreateSession;
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
			(a => a.Id == request.ActivityId);

		if(activity is null)
		{
			throw new UserFriendlyException
				(Status.NotFound, $"Not found \"ActivityId\" : {request.ActivityId}");
		}

		var session = new Session()
		{
			StartTime = request.StartTime,
			Activity = activity,
			ActivityId = request.ActivityId,
		};


		await _dbContext.Sessions.AddAsync(session, cancellationToken);

		await _dbContext.SaveChangesAsync(cancellationToken);

		return session.Id;
	}
}
