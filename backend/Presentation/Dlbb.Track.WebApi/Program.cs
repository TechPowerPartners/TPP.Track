<<<<<<< HEAD
﻿using Dlbb.Track.Persistence.CompositionRoot;
using Dlbb.Track.WebApi.SignalRHub;
using Microsoft.Extensions.Options;
=======
﻿using Dlbb.Track.Application.CompositionRoot;
using Dlbb.Track.Persistence.CompositionRoot;
>>>>>>> 4dac396ead7171224695c62c31be23d27bb99c4d

namespace Dlbb.Track.WebApi
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

			// Add services to the container.
			builder.Services.AddEf();
			builder.Services.AddApplication();
            builder.Services.AddControllers();
<<<<<<< HEAD
			builder.Services.AddSignalR();
			// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
			builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen(options => options.AddSignalRSwaggerGen());
=======

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
>>>>>>> 4dac396ead7171224695c62c31be23d27bb99c4d

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();

<<<<<<< HEAD
			app.MapHub<TimerHub>("/timerhub");

=======
			app.UseCors("AllowAll");
>>>>>>> 4dac396ead7171224695c62c31be23d27bb99c4d
			app.MapControllers();

			app.Run();
        }
    }
}
