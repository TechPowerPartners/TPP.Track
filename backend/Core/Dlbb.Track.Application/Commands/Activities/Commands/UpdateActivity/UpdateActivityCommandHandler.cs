using AutoMapper;
using Dlbb.Track.Application.Exceptions;
using Dlbb.Track.Common.Exceptions.Extensions;
using Dlbb.Track.Persistence.Contexts;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Dlbb.Track.Application.Activities.Commands.UpdateActivity;
public class UpdateActivityCommandHandler : IRequestHandler<UpdateActivityCommand>
{
	private readonly IMapper _mapper;
	private readonly AppDbContext _context;

	public UpdateActivityCommandHandler(AppDbContext context, IMapper mapper)
	{
		_mapper = mapper;
		_context = context;
	}

	public async Task<Unit> Handle
		(UpdateActivityCommand request,
		CancellationToken cancellationToken)
	{
		var activity = await _context.Activities.SingleOrDefaultAsync
			(a => a.Id == request.Id, cancellationToken);

		(activity!.IsGlobal != request.IsGlobal).ThrowUserFriendlyExceptionIfTrue
			(Status.Validation, "request isn't correct");

		activity!.ThrowUserFriendlyExceptionIfNull
			(status: Status.NotFound,
			message: $"Not found \"ActivityId\": {request.Id}");

		activity = _mapper.Map(request, activity);

		await _context.SaveChangesAsync(cancellationToken);

		return Unit.Value;
	}
}
