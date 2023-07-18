using AutoMapper;
using Dlbb.Track.Application.Exceptions;
using Dlbb.Track.Common.Exceptions.Extensions;
using Dlbb.Track.Domain.Abstractions.Repositories;
using Dlbb.Track.Domain.Entities;
using Dlbb.Track.Domain.Specifications;
using Dlbb.Track.Persistence.Contexts;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Dlbb.Track.Application.Activities.Commands.UpdateActivity;
public class UpdateActivityCommandHandler : IRequestHandler<UpdateActivityCommand>
{
	private readonly IMapper _mapper;
	private readonly IRepositoryWrapper _rep;

	public UpdateActivityCommandHandler(IRepositoryWrapper rep, IMapper mapper)
	{
		_mapper = mapper;
		_rep = rep;
	}

	public async Task<Unit> Handle
		(UpdateActivityCommand request,
		CancellationToken cancellationToken)
	{
		var activity = await _rep.ActivityRepository.FindAsync
			(request.Id, cancellationToken);

		(new IsSpecActivity(request.IsGlobal == false).IsSatisfiedBy(activity!))
			.ThrowUserFriendlyExceptionIfTrue
			(Status.Validation, "request isn't correct");

		activity!.ThrowUserFriendlyExceptionIfNull
			(status: Status.NotFound,
			message: $"Not found \"ActivityId\": {request.Id}");

		activity = _mapper.Map(request, activity);

		_rep.ActivityRepository.Update(activity!);

		await _rep.ActivityRepository.SaveAsync(cancellationToken);

		return Unit.Value;
	}
}
