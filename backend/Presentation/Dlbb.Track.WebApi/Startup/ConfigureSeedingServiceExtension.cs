using Dlbb.Track.Persistence.Services;

namespace Dlbb.Track.WebApi.Startup;

public static class ConfigureSeedingServiceExtension
{
	public static IServiceCollection ConfigureSeedingService
		(this IServiceCollection services,WebApplicationBuilder builder )
	{
		builder.Configuration.AddJsonFile("SeedingOptions.json");
		services.Configure<SeedingOptions>(builder.Configuration);

		return services;
	}
}
