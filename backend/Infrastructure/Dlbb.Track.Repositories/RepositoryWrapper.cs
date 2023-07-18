using Dlbb.Track.Domain.Abstractions.Repositories;

namespace Dlbb.Track.Repositories;
public class RepositoryWrapper : IRepositoryWrapper
{
	private readonly ISessionRepository _sessionRep;
	private readonly IActivityRepository _activityRep;
	private readonly IUserRepository _userRep;
	private readonly ICategoryRepository _categoryRep;

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

	}

	public async Task Save(CancellationToken cancellationToken)
	{
		await _categoryRep.SaveAsync(cancellationToken);
		await _sessionRep.SaveAsync(cancellationToken);
		await _activityRep.SaveAsync(cancellationToken);
		await _userRep.SaveAsync(cancellationToken);
	}
}
