using System.Linq.Expressions;
using Dlbb.Track.Domain.Abstractions.Repositories.Base;
using Dlbb.Track.Domain.Entities;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace Dlbb.Track.Domain.Abstractions.Repositories;
public interface ISessionRepository : IBaseRepository
{
	Task<Session> GetSingleSessionAsync
		(Expression<Func<Session, bool>> expression, CancellationToken cancellationToken);

	Task<Session> GetFirstSessionAsync
		(Expression<Func<Session, bool>> expression, CancellationToken cancellationToken);

	IQueryable<Session> GetAllSessions();

	IQueryable<Session> GetSessions
		(Expression<Func<Session, bool>> expression);

	ValueTask<Session> FindSessionAsync(Guid id, CancellationToken cancellationToken);

	void UpdateSession(Session session);

	void DeleteSession(Session session);

	Task CreateSessionAsync
		(Session session, CancellationToken cancellationToken);

	Task<bool> AnySessions(CancellationToken cancellationToken);

	Task<bool> AnySessions(Expression<Func<Session, bool>> expression, CancellationToken cancellationToken);
}
