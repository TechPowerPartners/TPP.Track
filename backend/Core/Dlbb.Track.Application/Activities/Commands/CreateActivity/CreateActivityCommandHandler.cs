using System.Security.Claims;
using Dlbb.Track.Domain.Entities;
using Dlbb.Track.Persistence.Contexts;
using MediatR;

namespace Dlbb.Track.Application.Activities.Commands.CreateActivity;
public class CreateActivityCommandHandler : IRequestHandler<CreateActivityCommand, Guid>
{
	private readonly AppDbContext _context;

	public CreateActivityCommandHandler(AppDbContext context)
	{
		_context = context;
	}

	public async Task<Guid> Handle(CreateActivityCommand request, CancellationToken cancellationToken)
	{
		var id = request.Claims.First(c => c.Type == ClaimTypes.IsPersistent).Value;
		var owner = _context.AppUsers.FirstOrDefault(o => o.Id == Guid.Parse(id))!;
		var activity = new Activity()
		{
			Name = request.Name,
			Description = request.Description,
			AppUser = owner
		};

		await _context.Activities.AddAsync(activity, cancellationToken);
		await _context.SaveChangesAsync(cancellationToken);

		return activity.Id;
	}
}
