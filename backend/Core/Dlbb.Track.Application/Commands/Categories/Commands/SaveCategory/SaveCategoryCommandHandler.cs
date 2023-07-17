using Dlbb.Track.Common.Exceptions.Extensions;
using Dlbb.Track.Domain.Entities;
using Dlbb.Track.Domain.Specifications;
using Dlbb.Track.Persistence.Contexts;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Dlbb.Track.Application.Commands.Categories.Commands.SaveCategory;
public class SaveCategoryCommandHandler : IRequestHandler<SaveCategoryCommand>
{
	private readonly AppDbContext _dbContext;

	public SaveCategoryCommandHandler(AppDbContext dbContext)
	{
		_dbContext = dbContext;
	}

	public async Task<Unit> Handle
		(SaveCategoryCommand request,
		CancellationToken cancellationToken)
	{
		var entity = await _dbContext.Categories.SingleOrDefaultAsync
			(new IsSpecCategory(request.Id), cancellationToken)!;

		List<Activity> activities = new List<Activity>();

		entity!.ThrowUserFriendlyExceptionIfNull
			(Exceptions.Status.NotFound, "Not found category");

		if (entity!.IsGlobal)
		{
			foreach (var id in request.ActivitiesId)
			{
				var activity = await _dbContext.Activities.SingleOrDefaultAsync
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
				var activity = await _dbContext.Activities.SingleOrDefaultAsync
					(new IsSpecActivity(id), cancellationToken);

				activity!.ThrowUserFriendlyExceptionIfNull
					(Exceptions.Status.NotFound, "Not found activity");

				activities.Add(activity!);
			}
		}

		entity.Activities = activities;

		_dbContext.Update(entity);

		await _dbContext.SaveChangesAsync(cancellationToken);

		return Unit.Value;
	}
}
