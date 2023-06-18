using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dlbb.Track.Application.Exceptions;
using Dlbb.Track.Persistence.Contexts;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Dlbb.Track.Application.Sessions.Commands.EndSession;
public class EndSessionCommandHandler : IRequestHandler<EndSessionCommand>
{
	private readonly AppDbContext _dbContext;

	public EndSessionCommandHandler(AppDbContext dbContext)
	{
		_dbContext = dbContext;
	}

	public async Task<Unit> Handle(EndSessionCommand request, CancellationToken cancellationToken)
	{
		var session = await _dbContext.Sessions.SingleOrDefaultAsync
			(s => s.Id == request.Id, cancellationToken);

		if (session is null)
		{
			throw new UserFriendlyException
				(Status.NotFound, $"Not found \"Id\" : {request.Id}");
		}

		session.EndTime = session.StartTime + request.Duration.ToTimeSpan();
		session.Duration = request.Duration;

		await _dbContext.SaveChangesAsync(cancellationToken);

		return Unit.Value;
	}
}
