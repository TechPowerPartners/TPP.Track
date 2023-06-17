using Dlbb.Track.Application.Common.Mappings;
using Dlbb.Track.Application.CompositionRoot;
using Dlbb.Track.Persistence.CompositionRoot;
using Dlbb.Track.Persistence.Services;
using Dlbb.Track.WebApi.Mappings;

namespace Dlbb.Track.WebApi
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
			// Add services to the container.
			builder.Services.AddEf(builder.Configuration);
			builder.Services.AddApplication();
			builder.Configuration.AddJsonFile("SeedingOptions.json");

			builder.Services.Configure<SeedingOptions>(builder.Configuration);

			builder.Services.AddAutoMapper(config =>
			{
				config.AddProfile(new ApplicationMappingProfile());
				config.AddProfile(new WebApiMappingProfile());
			});

            builder.Services.AddControllers();

			builder.Services.AddCors(options =>
			{
				options.AddPolicy("AllowAll", policy =>
				{
					policy.AllowAnyHeader();
					policy.AllowAnyMethod();
					policy.AllowAnyOrigin();
				});
			});

			// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
			builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            var app = builder.Build();

			await app.Services
				.CreateScope()
				.ServiceProvider
				.GetService<ISeedingService>()
				!.Initialize();
			
            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();

			app.UseCors("AllowAll");
			app.MapControllers();

            app.Run();
        }
    }
}
