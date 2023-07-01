using Dlbb.Track.Application.Exceptions;
using Dlbb.Track.Persistence.Contexts;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Dlbb.Track.Application.Activities.Commands.UpdateActivity;
public class UpdateActivityCommandHandler : IRequestHandler<UpdateActivityCommand>
{
	private readonly AppDbContext _context;

	public UpdateActivityCommandHandler(AppDbContext context)
	{
		_context = context;
	}

	public async Task<Unit> Handle(UpdateActivityCommand request, CancellationToken cancellationToken)
	{
		if (_context.Activities.Any(a => a.Name == request.Name))
		{
			throw new Exception("Активность с таким именем уже существует");
		}
		
		var activity = await _context.Activities.SingleOrDefaultAsync
			(a => a.Id == request.Id, cancellationToken);

		if (activity is null)
		{
			throw new UserFriendlyException
				(Status.NotFound, $"Not found \"Id\" : {request.Id}");
		}

		activity.Name = request.Name;
		activity.Description = request.Description;

		await _context.SaveChangesAsync(cancellationToken);

		return Unit.Value;
	}
}
