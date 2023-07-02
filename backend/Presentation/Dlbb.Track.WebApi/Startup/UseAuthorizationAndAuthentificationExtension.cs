using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using zgmapi.Data;

namespace Dlbb.Track.WebApi.Startup;

public static class UseAuthorizationAndAuthentificationExtension
{
	public static IServiceCollection UseAuthorizationAndAuthentification
		(this IServiceCollection services, WebApplicationBuilder builder)
	{
		services.AddAuthorization();
		services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
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

		return services;
	}
}
