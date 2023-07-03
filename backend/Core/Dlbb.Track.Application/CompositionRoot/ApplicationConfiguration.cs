using System.Reflection;
using Dlbb.Track.Persistence.Services;
using MediatR;
using Microsoft.Extensions.DependencyInjection;

namespace Dlbb.Track.Application.CompositionRoot;
public static class ApplicationConfiguration
{
	public static IServiceCollection AddApplication
		(this IServiceCollection services)
	{
		services.AddMediatR(Assembly.GetExecutingAssembly());
		services.AddSingleton<PasswordHasher>();

		return services;
	}
}
