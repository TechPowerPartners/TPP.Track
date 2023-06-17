using Dlbb.Track.Persistence.CompositionRoot;
using Dlbb.Track.WebApi.SignalRHub;
using Microsoft.Extensions.Options;

namespace Dlbb.Track.WebApi
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

			// Add services to the container.
			builder.Services.AddEf();
            builder.Services.AddControllers();
			builder.Services.AddSignalR();
			// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
			builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen(options => options.AddSignalRSwaggerGen());

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();

			app.MapHub<TimerHub>("/timerhub");

			app.MapControllers();

			app.Run();
        }
    }
}
