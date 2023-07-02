namespace Dlbb.Track.WebApi.Startup;

public static class UseSingnalRExtension
{
	public static IServiceCollection UseSignalR(this IServiceCollection services)
	{
		services.AddSignalR(opt =>
		{
			opt.EnableDetailedErrors = true;
		});

		return services;
	}
}
