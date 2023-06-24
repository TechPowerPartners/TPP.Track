using System.Runtime.CompilerServices;

namespace Dlbb.Track.WebApi.Middlewares;

public static class CustomExceptionHandlerMiddlewareExtensions
{
	public static IApplicationBuilder UseCusomExceptionHandler(this IApplicationBuilder builder)
	{
		return builder.UseMiddleware<CustomExceptionHandlerMiddleware>();
	}
}
