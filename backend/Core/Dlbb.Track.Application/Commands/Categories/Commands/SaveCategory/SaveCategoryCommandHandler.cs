using Dlbb.Track.Common.Exceptions.Extensions;
using Dlbb.Track.Domain.Abstractions.Repositories;
using Dlbb.Track.Domain.Entities;
using Dlbb.Track.Domain.Specifications;
using Dlbb.Track.Persistence.Contexts;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Dlbb.Track.Application.Commands.Categories.Commands.SaveCategory;
public class SaveCategoryCommandHandler : IRequestHandler<SaveCategoryCommand>
{
	private readonly IRepositoryWrapper _rep;

	public SaveCategoryCommandHandler(IRepositoryWrapper rep)
	{
		_rep = rep;
	}

	public async Task<Unit> Handle
		(SaveCategoryCommand request,
		CancellationToken cancellationToken)
	{
		var entity = await _rep.CategoryRepository.FindCategoryAsync
			(request.Id, cancellationToken)!;

		List<Activity> activities = new List<Activity>();

		entity!.ThrowUserFriendlyExceptionIfNull
			(Exceptions.Status.NotFound, "Not found category");

		if (entity!.IsGlobal)
		{
			foreach (var id in request.ActivitiesId)
			{
				var activity = await _rep.ActivityRepository.GetSingleActivityAsync
					(new IsSpecActivity(activityId: id) &&
					new IsSpecActivity(isGlobal: true), cancellationToken);

				activity!.ThrowUserFriendlyExceptionIfNull
					(Exceptions.Status.NotFound, "Not found activity");

				activities.Add(activity!);
			}
		}
		else
		{
			foreach (var id in request.ActivitiesId)
			{
				var activity = await _rep.ActivityRepository.FindActivityAsync
					(id, cancellationToken);

				activity!.ThrowUserFriendlyExceptionIfNull
					(Exceptions.Status.NotFound, "Not found activity");

				activities.Add(activity!);
			}
		}

		entity.Activities = activities;

		_rep.CategoryRepository.UpdateCategory(entity);

		await _rep.Save(cancellationToken);

		return Unit.Value;
	}
}
