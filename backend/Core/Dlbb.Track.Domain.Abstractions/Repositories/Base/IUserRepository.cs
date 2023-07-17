using System.Linq.Expressions;
using Dlbb.Track.Domain.Entities;

namespace Dlbb.Track.Domain.Abstractions.Repositories.Base;
public interface IUserRepository : IBaseRepository
{
	Task<bool> AnyUsers(CancellationToken cancellationToken);
	Task<bool> AnyUsers(Expression<Func<AppUser, bool>> expression, CancellationToken cancellationToken);
	Task CreateUserAsync(AppUser user, CancellationToken cancellationToken);
	void DeleteUser(AppUser user);
	ValueTask<AppUser> FindUserAsync(Guid id, CancellationToken cancellationToken);
	IQueryable<AppUser> GetAllUsers();
	Task<AppUser> GetFirstUserAsync(Expression<Func<AppUser, bool>> expression, CancellationToken cancellationToken);
	Task<AppUser> GetSingleUserAsync(Expression<Func<AppUser, bool>> expression, CancellationToken cancellationToken);
	IQueryable<AppUser> GetUsers(Expression<Func<AppUser, bool>> expression, CancellationToken cancellationToken);
	void UpdateUser(AppUser user);
}
