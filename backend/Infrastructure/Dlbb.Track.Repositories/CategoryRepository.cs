using Dlbb.Track.Domain.Abstractions.Repositories;
using Dlbb.Track.Domain.Entities;
using Dlbb.Track.Persistence.Contexts;
using Dlbb.Track.Repositories.Base;
using Microsoft.EntityFrameworkCore;

namespace Dlbb.Track.Repositories;
public class CategoryRepository : GenericRepository<Category>, ICategoryRepository
{
	public CategoryRepository(AppDbContext dbContext) : base(dbContext)
	{
	}
}
