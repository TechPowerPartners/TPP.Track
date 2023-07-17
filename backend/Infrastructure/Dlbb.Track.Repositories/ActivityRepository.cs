using System.Linq.Expressions;
using Dlbb.Track.Domain.Abstractions.Repositories;
using Dlbb.Track.Domain.Entities;
using Dlbb.Track.Repositories.Base;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace Dlbb.Track.Repositories;
public class ActivityRepository : BaseRepository<Activity>, IActivityRepository
{
	public ActivityRepository(DbContext dbContext) : base(dbContext)
	{
	}

	public ValueTask<EntityEntry<Activity>> CreateActivityAsync
		(Activity activity, CancellationToken cancellationToken) => 
		AddAsync(activity, cancellationToken);

	public EntityEntry<Activity> DeleteActivity(Activity activity) =>
		Delete(activity);

	public ValueTask<Activity> FindActivityAsync
		(Guid id, CancellationToken cancellationToken) =>
		FindAsync(id, cancellationToken);

	public IQueryable<Activity> GetActivities(Expression<Func<Activity, bool>> expression) =>
		Where(expression);

	public IQueryable<Activity> GetAllActivities() =>
		Where((a) => true);

	public Task<Activity> GetFirstActivityAsync
		(Expression<Func<Activity, bool>> expression, CancellationToken cancellationToken) =>
		FirstOrDefaultAsync(cancellationToken, expression);

	public Task<Activity> GetSingleActivityAsync
		(Expression<Func<Activity, bool>> expression, CancellationToken cancellationToken) =>
		SingleOrDefaultAsync(expression, cancellationToken);

	public EntityEntry<Activity> UpdateActivity(Activity activity) =>
		Update(activity);

	public Task<bool> AnyActivities(CancellationToken cancellationToken) =>
		AnyAsync(cancellationToken);

	public Task<bool> AnyActivities
		(Expression<Func<Activity, bool>> expression, CancellationToken cancellationToken) =>
		AnyAsync(expression, cancellationToken);
}
