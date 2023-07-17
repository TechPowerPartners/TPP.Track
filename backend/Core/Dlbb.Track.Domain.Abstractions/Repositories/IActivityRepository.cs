using System.Linq.Expressions;
using Dlbb.Track.Domain.Abstractions.Repositories.Base;
using Dlbb.Track.Domain.Entities;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace Dlbb.Track.Domain.Abstractions.Repositories;
public interface IActivityRepository : IBaseRepository
{
	Task<Activity> GetSingleActivityAsync
		(Expression<Func<Activity, bool>> expression, CancellationToken cancellationToken);

	Task<Activity> GetFirstActivityAsync
		(Expression<Func<Activity, bool>> expression, CancellationToken cancellationToken);

	IQueryable<Activity> GetAllActivities();

	IQueryable<Activity> GetActivities(Expression<Func<Activity, bool>> expression);

	ValueTask<Activity> FindActivityAsync(Guid id, CancellationToken cancellationToken);

	EntityEntry<Activity> UpdateActivity(Activity activity);

	EntityEntry<Activity> DeleteActivity(Activity activity);

	ValueTask<EntityEntry<Activity>> CreateActivityAsync
		(Activity activity, CancellationToken cancellationToken);
	Task<bool> AnyActivities(CancellationToken cancellationToken);
	Task<bool> AnyActivities(Expression<Func<Activity, bool>> expression, CancellationToken cancellationToken);
}
