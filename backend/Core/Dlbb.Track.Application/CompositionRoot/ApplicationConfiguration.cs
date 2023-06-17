using System.Reflection;
using Dlbb.Track.Application.Common.Mappings;
using MediatR;
using Microsoft.Extensions.DependencyInjection;

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
