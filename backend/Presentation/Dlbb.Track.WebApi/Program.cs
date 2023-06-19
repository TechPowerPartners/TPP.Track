using System.Security.Claims;
using Dlbb.Track.Application.Common.Mappings;
using Dlbb.Track.Application.CompositionRoot;
using Dlbb.Track.Domain.Enums;
using Dlbb.Track.Persistence.CompositionRoot;
using Dlbb.Track.Persistence.Services;
using Dlbb.Track.WebApi.Mappings;
using Dlbb.Track.WebApi.Middlewares;
using Dlbb.Track.WebApi.SignalRHub;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using zgmapi.Data;

namespace Dlbb.Track.WebApi;

public class Program
{
	public static async Task Main(string[] args)
	{
		var builder = WebApplication.CreateBuilder(args);
		builder.Services.AddApplication();
		builder.Services.AddEf(builder.Configuration);

		builder.Services.AddAuthorization();
		builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
			.AddJwtBearer(options =>
			{
				JwtOptions.SetKey(builder.Configuration["JWT_KEY"]);
				options.TokenValidationParameters = new TokenValidationParameters
				{
					ValidateIssuer = true,
					ValidIssuer = JwtOptions.ISSUER,
					ValidateAudience = true,
					ValidAudience = JwtOptions.AUDIENCE,
					ValidateLifetime = false,
					IssuerSigningKey = JwtOptions.GetSymmetricSecurityKey(),
					ValidateIssuerSigningKey = true,
				};
			});

		builder.Services.AddAuthorization((opt) =>
		{
			opt.AddPolicy("Admin", p =>
				p.RequireAssertion(x => x.User.HasClaim(ClaimTypes.Role, RoleEnum.Admin.ToString())
										|| x.User.HasClaim(ClaimTypes.Role, RoleEnum.User.ToString())));
			opt.AddPolicy("User", p =>
				p.RequireAssertion(x => x.User.HasClaim(ClaimTypes.Role, RoleEnum.User.ToString())));
		});

		builder.Configuration.AddJsonFile("SeedingOptions.json");

		builder.Services.Configure<SeedingOptions>(builder.Configuration);

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

		app.UseCusomExceptionHandler();

		await app.Services
				.CreateScope()
				.ServiceProvider
				.GetService<ISeedingService>()
				.Initialize();

		app.UseHttpsRedirection();

		app.UseAuthentication();
		app.UseAuthorization();

		app.MapHub<TimerHub>("/timerhub");

		app.UseCors("AllowAll");

		app.MapControllers();

		app.Run();
	}
}
