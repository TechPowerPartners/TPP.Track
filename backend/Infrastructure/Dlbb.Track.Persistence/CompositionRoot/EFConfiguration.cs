using Dlbb.Track.Persistence.Contexts;
using Dlbb.Track.Persistence.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;


namespace Dlbb.Track.Persistence.CompositionRoot;
public static class EFConfiguration
{
  public static IServiceCollection AddEf
	  (this IServiceCollection services, IConfiguration configuration)
  {
	services.AddDbContext<AppDbContext>(options =>
		options.UseNpgsql
		(configuration.GetConnectionString("PostgreConnection")));

	services.AddScoped<ISeedingService, SeedingService>();

	services.AddSingleton<ITimerService, TimerService>();

	return services;
  }
}
