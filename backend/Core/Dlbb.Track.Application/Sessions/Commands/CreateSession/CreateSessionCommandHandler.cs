using Dlbb.Track.Domain.Entities;
using Dlbb.Track.Persistence.Contexts;
using MediatR;

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
		var session = new Session()
		{
			StartTime = request.StartTime,
			ActivityId = request.ActivityId,
		};

		await _dbContext.Sessions.AddAsync(session, cancellationToken);

		return session.Id;
	}
}
