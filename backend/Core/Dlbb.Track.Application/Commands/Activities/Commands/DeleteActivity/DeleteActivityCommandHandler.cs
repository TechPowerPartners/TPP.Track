using Dlbb.Track.Application.Exceptions;
using Dlbb.Track.Common.Exceptions.Extensions;
using Dlbb.Track.Domain.Abstractions.Repositories;
using Dlbb.Track.Domain.Specifications;
using Dlbb.Track.Persistence.Contexts;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Dlbb.Track.Application.Activities.Commands.DeleteActivity;
public class DeleteActivityCommandHandler : IRequestHandler<DeleteActivityCommand>
{
	private readonly IRepositoryWrapper _rep;

	public DeleteActivityCommandHandler(IRepositoryWrapper rep)
	{
		_rep = rep;
	}

	public async Task<Unit> Handle
		(DeleteActivityCommand request,
		CancellationToken cancellationToken)
	{
		var activity = await _rep.ActivityRepository.FindActivityAsync
			(request.Id, cancellationToken);

		(new IsSpecActivity(request.IsGlobal == false).IsSatisfiedBy(activity!))
			.ThrowUserFriendlyExceptionIfTrue
			(Status.Validation, "request isn't correct");

		activity!.ThrowUserFriendlyExceptionIfNull
			(status: Status.NotFound,
			message: $"Not found \"Id\" : {request.Id}");

		_rep.ActivityRepository.DeleteActivity(activity!);

		await _rep.Save(cancellationToken);

		return Unit.Value;
	}
}
