using System.Security.Claims;
using AutoMapper;
using Dlbb.Track.Common.Exceptions.Extensions;
using Dlbb.Track.Domain.Abstractions.Repositories;
using Dlbb.Track.Domain.Entities;
using Dlbb.Track.Domain.Specifications;
using Dlbb.Track.Persistence.Contexts;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Dlbb.Track.Application.Activities.Commands.CreateActivity;
public class CreateActivityCommandHandler : IRequestHandler<CreateActivityCommand, Guid>
{
	private readonly IMapper _mapper;
	private readonly IRepositoryWrapper _rep;

	public CreateActivityCommandHandler(IRepositoryWrapper rep, IMapper mapper)
	{
		_mapper = mapper;
		_rep = rep;
	}

	public async Task<Guid> Handle
		(CreateActivityCommand request,
		CancellationToken cancellationToken)
	{
		var id = request.AppUserId;

		var user = await _rep.UserRepository.FindUserAsync
			(id,cancellationToken)!;

		user!.ThrowUserFriendlyExceptionIfNull
			(Exceptions.Status.NotFound, $"Not found user id: {id}");

		var entity = _mapper.Map<Activity>(request);

		entity.AppUser = user!;

		await _rep.ActivityRepository.CreateActivityAsync(entity, cancellationToken);
		await _rep.Save(cancellationToken);

		return entity.Id;
	}
}
