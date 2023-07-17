using System.Linq.Expressions;
using Dlbb.Track.Domain.Abstractions.Repositories.Base;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace Dlbb.Track.Repositories.Base;
public abstract class BaseRepository<TEntity> : IBaseRepository where TEntity : class
{
	private bool disposed = false;
	private readonly DbContext _context;
	private readonly DbSet<TEntity> _dbSet;

	protected BaseRepository(DbContext dbContext)
	{
		_context = dbContext;
		_dbSet = _context.Set<TEntity>();
	}

	protected Task<TEntity> SingleOrDefaultAsync
		(Expression<Func<TEntity, bool>> expression,
		CancellationToken cancellationToken) =>
		_dbSet.SingleOrDefaultAsync(expression, cancellationToken)!;

	protected ValueTask<EntityEntry<TEntity>> AddAsync
		(TEntity entity, CancellationToken cancellationToken) =>
		_dbSet.AddAsync(entity, cancellationToken);

	protected Task<bool> AnyAsync
		(Expression<Func<TEntity, bool>> expression,
		CancellationToken cancellationToken) =>
		_dbSet.AnyAsync(expression, cancellationToken);

	protected Task<bool> AnyAsync(CancellationToken cancellationToken) =>
		_dbSet.AnyAsync(cancellationToken);

	protected EntityEntry<TEntity> Delete (TEntity entity) => _context.Remove(entity);

	protected Task<TEntity> FirstOrDefaultAsync
		(CancellationToken cancellationToken,
		Expression<Func<TEntity, bool>> expression = null)
	{
		if (expression is null)
		{
			return _dbSet.FirstOrDefaultAsync(cancellationToken)!;
		}
		else
		{
			return _dbSet.FirstOrDefaultAsync(expression, cancellationToken)!;
		}
	}

	protected IQueryable<TEntity> Where
		(Expression<Func<TEntity, bool>> expression) => _dbSet.Where(expression);

	protected ValueTask<TEntity> FindAsync(Guid id, CancellationToken cancellationToken) =>
		_dbSet.FindAsync(id, cancellationToken)!;

	protected EntityEntry<TEntity> Update
		(TEntity entity) => _dbSet.Update(entity);

	protected virtual void Dispose(bool disposing)
	{
		if (disposed == false)
		{
			if (disposing)
			{
				_context.Dispose();
			}
		}
		disposed = true;
	}

	public void Dispose()
	{
		Dispose(true);
		GC.SuppressFinalize(this);
	}

	public Task Save(CancellationToken cancellationToken) => 
		_context.SaveChangesAsync(cancellationToken);
}
