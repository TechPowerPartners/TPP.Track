using Dlbb.Track.Persistence.Contexts;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;


namespace Dlbb.Track.Persistence.CompositionRoot;
public static class EFConfiguration
{
	public static IServiceCollection AddEf(this IServiceCollection services)
	{
		services.AddDbContext<AppDbContext>(options =>
			options.UseNpgsql
			("Host=localhost;Port=5432;Database=Dlbb.Track.Db;Username=postgres;Password=zalupa"));

		return services;
	}
}
