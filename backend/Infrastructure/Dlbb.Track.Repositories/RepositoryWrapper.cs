using Dlbb.Track.Domain.Abstractions.Repositories;
using Dlbb.Track.Domain.Abstractions.Repositories.Base;

namespace Dlbb.Track.Repositories;
public class RepositoryWrapper : IRepositoryWrapper
{
	private readonly ISessionRepository _sessionRep;
	private readonly IActivityRepository _activityRep;
	private readonly IUserRepository _userRep;
	private readonly ICategoryRepository _categoryRep;
	private readonly List<IBaseRepository> _repositories;

	public ISessionRepository SessionRepository => _sessionRep;

	public IActivityRepository ActivityRepository => _activityRep;

	public IUserRepository UserRepository => _userRep;

	public ICategoryRepository CategoryRepository => _categoryRep;

	public RepositoryWrapper
		(ISessionRepository sessionRep,
		IActivityRepository activityRep,
		IUserRepository userRep,
		ICategoryRepository categoryRep)
	{
		_sessionRep = sessionRep;
		_activityRep = activityRep;
		_userRep = userRep;
		_categoryRep = categoryRep;

		_repositories = new();

		_repositories.Add(_sessionRep);
		_repositories.Add(_activityRep);
		_repositories.Add(_userRep);
		_repositories.Add(_categoryRep);
	}

	public void Dispose()
	{
		foreach (var rep in _repositories)
		{
			rep.Dispose();
		}
	}

	public async Task Save(CancellationToken cancellationToken)
	{
		foreach (var rep in _repositories)
		{
			await rep.Save(cancellationToken);
		}
	}
}
