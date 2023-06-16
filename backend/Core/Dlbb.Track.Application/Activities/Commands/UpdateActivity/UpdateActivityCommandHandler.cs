using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
		var activity  = await _context.Activities.FirstAsync
			(a => a.Id == request.Id,cancellationToken);

		activity.Name = request.Name;
		activity.Description = request.Description;

		await _context.SaveChangesAsync(cancellationToken);

		return Unit.Value;
	}
}
