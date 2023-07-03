using Dlbb.Track.Application.Activities.Commands.CreateActivity;
using Dlbb.Track.Application.Exceptions;
using Dlbb.Track.Domain.Entities;
using Dlbb.Track.Persistence.Contexts;
using MediatR;

namespace Dlbb.Track.Application.Commands.Activities.Commands.CreateActivity;
public class CreateActivityCommandHandler : IRequestHandler<CreateActivityCommand, Guid>
{
	private readonly AppDbContext _context;

	public CreateActivityCommandHandler(AppDbContext context)
	{
		_context = context;
	}

	public async Task<Guid> Handle(CreateActivityCommand request, CancellationToken cancellationToken)
	{
		if (_context.Activities.Any(a => a.Name == request.Name))
		{
			throw new UserFriendlyException(Status.Validation, "Активность с таким именем уже существует");
		}
			
		var activity = new Activity()
		{
			Name = request.Name,
			Description = request.Description,
		};
		
		
		await _context.Activities.AddAsync(activity, cancellationToken);
		await _context.SaveChangesAsync(cancellationToken);

		return activity.Id;
	}
}