using Dlbb.Track.Domain.Abstractions.Repositories.Base;

namespace Dlbb.Track.Domain.Abstractions.Repositories;
public interface IRepositoryWrapper : IDisposable
{
	ISessionRepository SessionRepository { get;  }
	IActivityRepository ActivityRepository { get; }
	IUserRepository UserRepository { get; }
	ICategoryRepository CategoryRepository { get; }

	Task Save(CancellationToken cancellationToken);
}
