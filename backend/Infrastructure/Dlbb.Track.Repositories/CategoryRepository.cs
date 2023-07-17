using System.Linq.Expressions;
using Dlbb.Track.Domain.Abstractions.Repositories;
using Dlbb.Track.Domain.Entities;
using Dlbb.Track.Repositories.Base;
using Microsoft.EntityFrameworkCore;

namespace Dlbb.Track.Repositories;
public class CategoryRepository : BaseRepository<Category>, ICategoryRepository
{
	public CategoryRepository(DbContext dbContext) : base(dbContext)
	{
	}

	public Task<Category> GetSingleCategoryAsync
		(Expression<Func<Category, bool>> expression, CancellationToken cancellationToken) => SingleOrDefaultAsync(expression, cancellationToken);

	public Task<Category> GetFirstCategoryAsync
		(Expression<Func<Category, bool>> expression, CancellationToken cancellationToken) => FirstOrDefaultAsync(cancellationToken, expression);

	public IQueryable<Category> GetAllCategories() => Where((c) => true);

	public IQueryable<Category> GetCategories
		(Expression<Func<Category, bool>> expression) => Where(expression);

	public ValueTask<Category> FindCategoryAsync(Guid id, CancellationToken cancellationToken) => FindAsync(id, cancellationToken);

	public void UpdateCategory(Category category) => Update(category);

	public void DeleteCategory(Category category) => Delete(category);

	public async Task CreateCategoryAsync
			(Category category, CancellationToken cancellationToken) => await AddAsync(category, cancellationToken);

	public Task<bool> AnyCategories(CancellationToken cancellationToken) => AnyAsync(cancellationToken);

	public Task<bool> AnyCategories
		(Expression<Func<Category, bool>> expression, CancellationToken cancellationToken) => AnyAsync(expression, cancellationToken);
}
