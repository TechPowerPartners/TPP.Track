using System.Security.Claims;
using Dlbb.Track.Domain.Enums;

namespace Dlbb.Track.WebApi.Startup;

public static class AddAuthorizationExtension
{
	public static IServiceCollection AddAuthorizationConfiguration(this IServiceCollection services)
	{
		services.AddAuthorization((opt) =>
		{
			opt.AddPolicy(nameof(RoleEnum.Admin), p =>
				p.RequireAssertion
				(x => x.User.HasClaim(ClaimTypes.Role, RoleEnum.Admin.ToString())
				|| x.User.HasClaim(ClaimTypes.Role, RoleEnum.User.ToString())));
			opt.AddPolicy(nameof(RoleEnum.User), p =>
				p.RequireAssertion
				(x => x.User.HasClaim(ClaimTypes.Role, RoleEnum.User.ToString())));
		});

		return services;
	}
}
