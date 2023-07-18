using System.Linq.Expressions;
using Dlbb.Track.Domain.Entities;

namespace Dlbb.Track.Domain.Abstractions.Repositories.Base
{
	public interface IGenericRepository<TEntity> where TEntity : class
	{
		Task AddAsync(TEntity entity, CancellationToken cancellationToken);
		Task<bool> AnyAsync
			(Expression<Func<TEntity, bool>> expression, CancellationToken cancellationToken);
		Task<bool> AnyAsync(CancellationToken cancellationToken);
		void Delete(TEntity entity);
		Task<TEntity> FindAsync(Guid id, CancellationToken cancellationToken);
		Task<TEntity> FirstOrDefaultAsync
			(Expression<Func<TEntity, bool>> expression, CancellationToken cancellationToken);
		Task<TEntity> FirstOrDefaultAsync
			(CancellationToken cancellationToken);
		Task SaveAsync
			(CancellationToken cancellationToken);
		Task<List<TResult>> SelectAsync<TResult>(Expression<Func<TEntity, TResult>> expression, CancellationToken cancellationToken);
		Task<TEntity> SingleOrDefaultAsync
			(Expression<Func<TEntity, bool>> expression, CancellationToken cancellationToken);
		Task<List<TEntity>> ToListAsync
			(Expression<Func<TEntity, bool>> expression, CancellationToken cancellationToken);
		Task<List<TEntity>> ToListAsync(CancellationToken cancellationToken);
		void Update(TEntity entity);
	}
}
