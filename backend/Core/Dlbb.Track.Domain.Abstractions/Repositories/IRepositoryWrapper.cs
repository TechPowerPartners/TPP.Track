namespace Dlbb.Track.Domain.Abstractions.Repositories;
public interface IRepositoryWrapper 
{
	ISessionRepository SessionRepository { get;  }
	IActivityRepository ActivityRepository { get; }
	IUserRepository UserRepository { get; }
	ICategoryRepository CategoryRepository { get; }
}
