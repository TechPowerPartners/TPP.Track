using Dlbb.Track.Application.Common.Mappings;
using Dlbb.Track.WebApi.Mappings;

namespace Dlbb.Track.WebApi.Startup;

public static class AddAutoMapperExtension
{
	public static IServiceCollection AddAutoMapperConfiguration(this IServiceCollection services)
	{
		services.AddAutoMapper(config =>
		{
			config.AddProfile(new ApplicationMappingProfile());
			config.AddProfile(new WebApiMappingProfile());
		});

		return services;
	}
}
