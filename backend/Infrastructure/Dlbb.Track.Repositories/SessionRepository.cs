using Dlbb.Track.Domain.Abstractions.Repositories;
using Dlbb.Track.Domain.Entities;
using Dlbb.Track.Persistence.Contexts;
using Dlbb.Track.Repositories.Base;

namespace Dlbb.Track.Repositories;
public class SessionRepository : GenericRepository<Session>, ISessionRepository
{
	public SessionRepository(AppDbContext dbContext) : base(dbContext)
	{
	}
}
