using System.Linq.Expressions;
using Dlbb.Track.Domain.Abstractions.Repositories.Base;
using Dlbb.Track.Domain.Entities.Base;
using Microsoft.EntityFrameworkCore;

namespace Dlbb.Track.Repositories.Base;
public abstract class GenericRepository<TEntity> : IGenericRepository<TEntity> where TEntity: BaseEntity
{
	private readonly DbContext _context;
	private readonly DbSet<TEntity> _dbSet;

	public GenericRepository(DbContext dbContext)
	{
		_context = dbContext;
		_dbSet = _context.Set<TEntity>();
	}

	public Task<TEntity> SingleOrDefaultAsync
		(Expression<Func<TEntity, bool>> expression,
		CancellationToken cancellationToken) =>
		_dbSet.SingleOrDefaultAsync(expression, cancellationToken)!;

	public async Task AddAsync
		(TEntity entity, CancellationToken cancellationToken) =>
		await _dbSet.AddAsync(entity, cancellationToken);

	public Task<bool> AnyAsync
		(Expression<Func<TEntity, bool>> expression,
		CancellationToken cancellationToken) =>
		_dbSet.AnyAsync(expression, cancellationToken);

	public Task<bool> AnyAsync(CancellationToken cancellationToken) =>
		_dbSet.AnyAsync(cancellationToken);

	public void Delete(TEntity entity) => _context.Remove(entity);

	public Task<TEntity> FirstOrDefaultAsync
		(Expression<Func<TEntity, bool>> expression, CancellationToken cancellationToken) =>
		_dbSet.FirstOrDefaultAsync(expression, cancellationToken)!;

	public Task<TEntity> FirstOrDefaultAsync(CancellationToken cancellationToken) =>
		_dbSet.FirstOrDefaultAsync(cancellationToken)!;


	public Task<List<TEntity>> ToListAsync
		(Expression<Func<TEntity, bool>> expression, CancellationToken cancellationToken) =>
		_dbSet.Where(expression).ToListAsync(cancellationToken);

	public Task<List<TEntity>> ToListAsync
		(CancellationToken cancellationToken) =>
		_dbSet.ToListAsync(cancellationToken);

	public async Task<TEntity> FindAsync(Guid id, CancellationToken cancellationToken) =>
		await _dbSet.FindAsync(id, cancellationToken);

	public async Task<TEntity?> GetById(Guid id, CancellationToken cancellationToken) =>
		await _dbSet.FirstOrDefaultAsync(s => s.Id == id);

	public Task<List<TResult>> SelectAsync<TResult>
		(Expression<Func<TEntity, TResult>> expression, CancellationToken cancellationToken) =>
		_dbSet.Select(expression).ToListAsync(cancellationToken);

	public void Update(TEntity entity) => _dbSet.Update(entity);

	public Task SaveAsync(CancellationToken cancellationToken) =>
		_context.SaveChangesAsync(cancellationToken);
}
