using System.Reflection;
using Microsoft.Extensions.DependencyInjection;
using MediatR;
using Dlbb.Track.Application.Common.Mappings;

namespace Dlbb.Track.Application.CompositionRoot;
public static class ApplicationConfiguration
{
	public static IServiceCollection AddApplication
		(this IServiceCollection services)
	{
		services.AddMediatR(Assembly.GetExecutingAssembly());

		services.AddAutoMapper(config =>
		{
			config.AddProfile(new AssemblyMappingProfile());
		});

		return services;
	}
}
