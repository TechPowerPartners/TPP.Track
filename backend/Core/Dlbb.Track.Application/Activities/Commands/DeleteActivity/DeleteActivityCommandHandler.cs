using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dlbb.Track.Persistence.Contexts;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Dlbb.Track.Application.Activities.Commands.DeleteActivity;
public class DeleteActivityCommandHandler : IRequestHandler<DeleteActivityCommand>
{
	private readonly AppDbContext _context;

	public DeleteActivityCommandHandler(AppDbContext context)
	{
		_context = context;
	}
	public async Task<Unit> Handle(DeleteActivityCommand request, CancellationToken cancellationToken)
	{
		_context.Activities.Remove
			(await _context.Activities.FirstAsync(a=> a.Id == request.Id, cancellationToken));

		await _context.SaveChangesAsync(cancellationToken);

		return Unit.Value;
	}
}
