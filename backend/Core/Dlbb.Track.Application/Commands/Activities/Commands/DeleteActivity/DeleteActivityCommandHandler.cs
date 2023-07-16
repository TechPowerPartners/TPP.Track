using Dlbb.Track.Application.Exceptions;
using Dlbb.Track.Common.Exceptions.Extensions;
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

	public async Task<Unit> Handle
		(DeleteActivityCommand request,
		CancellationToken cancellationToken)
	{
		var activity = await _context.Activities.SingleOrDefaultAsync
			(a => a.Id == request.Id, cancellationToken);

		activity!.ThrowUserFriendlyExceptionIfNull
			(status: Status.NotFound,
			message: $"Not found \"Id\" : {request.Id}");

		_context.Activities.Remove(activity!);

		await _context.SaveChangesAsync(cancellationToken);

		return Unit.Value;
	}
}
