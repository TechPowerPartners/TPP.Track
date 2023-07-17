using System.Linq.Expressions;
using Dlbb.Track.Domain.Abstractions.Repositories.Base;
using Dlbb.Track.Domain.Entities;

namespace Dlbb.Track.Domain.Abstractions.Repositories;
public interface ICategoryRepository : IBaseRepository
{
	Task<bool> AnyCategories(CancellationToken cancellationToken);
	Task<bool> AnyCategories
		(Expression<Func<Category, bool>> expression, CancellationToken cancellationToken);
	Task CreateCategoryAsync(Category category, CancellationToken cancellationToken);
	void DeleteCategory(Category category);
	ValueTask<Category> FindCategoryAsync(Guid id, CancellationToken cancellationToken);
	IQueryable<Category> GetAllCategories();
	IQueryable<Category> GetCategories(Expression<Func<Category, bool>> expression);
	Task<Category> GetFirstCategoryAsync(Expression<Func<Category, bool>> expression, CancellationToken cancellationToken);
	Task<Category> GetSingleCategoryAsync(Expression<Func<Category, bool>> expression, CancellationToken cancellationToken);
	void UpdateCategory(Category category);
}
