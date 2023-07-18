using Dlbb.Track.Application.CompositionRoot;
using Dlbb.Track.Persistence.CompositionRoot;
using Dlbb.Track.Persistence.Services;
using Dlbb.Track.Repositories;
using Dlbb.Track.WebApi.Middlewares;
using Dlbb.Track.WebApi.SignalRHub;
using Dlbb.Track.WebApi.Startup;

namespace Dlbb.Track.WebApi;

public class Program
{
	public static async Task Main(string[] args)
	{
		var builder = WebApplication.CreateBuilder(args);

		builder.Services.AddApplication();
		builder.Services.AddEf(builder.Configuration);
		builder.Services.AddRepositories();
		builder.Services.UseAuthorizationAndAuthentification(builder);

		builder.Services.AddAuthorizationConfiguration();

		builder.Services.UseSwaggerAuthorize();

		builder.Services.ConfigureSeedingService(builder);

		builder.Services.AddAutoMapperConfiguration();

		builder
			.Services
			.AddControllers()
			.AddNewtonsoftJson();

		builder.Services.UseSignalR();

		builder.Services.AddEndpointsApiExplorer();

		builder.Services.AddSwaggerGen(options => options.AddSignalRSwaggerGen());

		builder.Services.AddCors(options =>
		{
			options.AddPolicy("AllowAll", policy =>
			{
				policy.AllowAnyHeader();
				policy.AllowAnyMethod();
				policy.AllowAnyOrigin();
			});
		});

		builder.Services.AddEndpointsApiExplorer();
		builder.Services.AddSwaggerGen();



		var app = builder.Build();

		if (app.Environment.IsDevelopment())
		{
			app.UseSwagger();
			app.UseSwaggerUI();
		}

		app.UseCusomExceptionHandler();

		await app.Services
				 .CreateScope()
				 .ServiceProvider
				 .GetService<ISeedingService>()
				 !.Initialize();

		app.UseHttpsRedirection();

		app.UseAuthentication();
		app.UseAuthorization();

		app.MapHub<TimerHub>("/timerhub");

		app.UseCors("AllowAll");

		app.MapControllers();

		app.Run();
	}
}
