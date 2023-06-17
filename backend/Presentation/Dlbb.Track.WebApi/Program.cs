using Dlbb.Track.Application.Common.Mappings;
using Dlbb.Track.Application.CompositionRoot;
using Dlbb.Track.Persistence.CompositionRoot;
using Dlbb.Track.WebApi.Mappings;
using Dlbb.Track.WebApi.SignalRHub;


namespace Dlbb.Track.WebApi;

public class Program
{
	public static void Main(string[] args)
	{
		var builder = WebApplication.CreateBuilder(args);
		builder.Services.AddEf(builder.Configuration);
		builder.Services.AddApplication();

		builder.Services.AddAutoMapper(config =>
		{
			config.AddProfile(new ApplicationMappingProfile());
			config.AddProfile(new WebApiMappingProfile());
		});

		builder.Services.AddControllers();

		builder.Services.AddSignalR();

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

		app.UseHttpsRedirection();

		app.UseAuthorization();

		app.MapHub<TimerHub>("/timerhub");

		app.UseCors("AllowAll");

		app.MapControllers();

		app.Run();
	}
}
