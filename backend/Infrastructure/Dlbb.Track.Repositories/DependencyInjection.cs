using Dlbb.Track.Domain.Abstractions.Repositories;
using Microsoft.Extensions.DependencyInjection;

namespace Dlbb.Track.Repositories;
public static class DependencyInjection
{
	public static IServiceCollection AddRepositories(this IServiceCollection services)
	{
		services.AddTransient<ISessionRepository, SessionRepository>();
		services.AddTransient<IActivityRepository, ActivityRepository>();
		services.AddTransient<ICategoryRepository, CategoryRepository>();
		services.AddTransient<IUserRepository, UserRepository>();
		services.AddTransient<IRepositoryWrapper, RepositoryWrapper>();

		return services;
	}
}
