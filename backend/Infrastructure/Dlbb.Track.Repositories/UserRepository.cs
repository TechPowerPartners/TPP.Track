using System.Linq.Expressions;
using Dlbb.Track.Domain.Abstractions.Repositories.Base;
using Dlbb.Track.Domain.Entities;
using Dlbb.Track.Repositories.Base;
using Microsoft.EntityFrameworkCore;

namespace Dlbb.Track.Repositories;
public class UserRepository : BaseRepository<AppUser>, IUserRepository
{
	public UserRepository(DbContext dbContext) : base(dbContext)
	{
	}

	public Task<AppUser> GetSingleUserAsync
		(Expression<Func<AppUser, bool>> expression, CancellationToken cancellationToken)
	{
		return SingleOrDefaultAsync(expression, cancellationToken);
	}

	public Task<AppUser> GetFirstUserAsync
		(Expression<Func<AppUser, bool>> expression, CancellationToken cancellationToken)
	{
		return FirstOrDefaultAsync(cancellationToken, expression);
	}

	public IQueryable<AppUser> GetAllUsers()
	{
		return Where((u) => true);
	}

	public IQueryable<AppUser> GetUsers
		(Expression<Func<AppUser, bool>> expression, CancellationToken cancellationToken)
	{
		return Where(expression);
	}

	public ValueTask<AppUser> FindUserAsync(Guid id, CancellationToken cancellationToken)
	{
		return FindAsync(id, cancellationToken);
	}

	public void UpdateUser(AppUser user)
	{
		Update(user);
	}

	public void DeleteUser(AppUser user)
	{
		Delete(user);
	}

	public async Task CreateUserAsync(AppUser user, CancellationToken cancellationToken)
	{
		await AddAsync(user, cancellationToken);
	}

	public Task<bool> AnyUsers(CancellationToken cancellationToken)
	{
		return AnyAsync(cancellationToken);
	}

	public Task<bool> AnyUsers
		(Expression<Func<AppUser, bool>> expression, CancellationToken cancellationToken)
	{
		return AnyAsync(expression, cancellationToken);
	}
}
