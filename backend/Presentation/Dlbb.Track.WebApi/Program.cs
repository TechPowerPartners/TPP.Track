using Dlbb.Track.Application.CompositionRoot;
using Dlbb.Track.Persistence.CompositionRoot;
using Dlbb.Track.WebApi.SignalRHub;
using Microsoft.Extensions.Options;
﻿using Dlbb.Track.Application.CompositionRoot;
using Dlbb.Track.Persistence.CompositionRoot;


namespace Dlbb.Track.WebApi
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

			builder.Services.AddEf();
			builder.Services.AddApplication();
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
}
