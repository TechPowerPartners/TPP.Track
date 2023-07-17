using System.Linq.Expressions;
using Dlbb.Track.Domain.Abstractions.Repositories;
using Dlbb.Track.Domain.Entities;
using Dlbb.Track.Persistence.Contexts;
using Dlbb.Track.Repositories.Base;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace Dlbb.Track.Repositories;
public class SessionRepository : BaseRepository<Session>, ISessionRepository
{
	public SessionRepository(AppDbContext dbContext) : base(dbContext)
	{
	}

	public async Task CreateSessionAsync
		(Session session, CancellationToken cancellationToken) => 
		await AddAsync(session, cancellationToken);

	public void DeleteSession(Session session) => Delete(session);

	public ValueTask<Session> FindSessionAsync
		(Guid id, CancellationToken cancellationToken) =>
		FindAsync(id, cancellationToken);

	public IQueryable<Session> GetAllSessions() => Where((s) => true);

	public Task<Session> GetFirstSessionAsync
		(Expression<Func<Session, bool>> expression,
		CancellationToken cancellationToken) =>
		FirstOrDefaultAsync(cancellationToken, expression);

	public IQueryable<Session> GetSessions
		(Expression<Func<Session, bool>> expression) =>
		Where(expression);

	public Task<Session> GetSingleSessionAsync
		(Expression<Func<Session, bool>> expression,
		CancellationToken cancellationToken) =>
		SingleOrDefaultAsync(expression, cancellationToken);

	public void UpdateSession(Session session) => Update(session);

	public Task<bool> AnySessions(CancellationToken cancellationToken) =>
		AnyAsync(cancellationToken);

	public Task<bool> AnySessions
		(Expression<Func<Session, bool>> expression, CancellationToken cancellationToken) => 
		AnyAsync(expression, cancellationToken);
}
