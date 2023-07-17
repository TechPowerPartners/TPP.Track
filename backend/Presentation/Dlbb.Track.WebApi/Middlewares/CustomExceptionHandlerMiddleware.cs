using System;
using System.Net;
using System.Text.Json;
using Dlbb.Track.Application.Exceptions;
using FluentValidation;

namespace Dlbb.Track.WebApi.Middlewares;

public class CustomExceptionHandlerMiddleware
{
	private readonly RequestDelegate _next;

	public CustomExceptionHandlerMiddleware(RequestDelegate next)
	{
		_next = next;
	}

	public async Task Invoke(HttpContext ctx)
	{
		try
		{
			await _next(ctx);
		}
		catch (ValidationException exception)
		{
			await HandleValidationsException(ctx, exception);
		}
		catch (Exception exception)
		{
			await HandleExceptionAsync(ctx, exception);
		}
	}

	private Task HandleValidationsException(HttpContext ctx, ValidationException exception)
	{
		ctx.Response.StatusCode = (int)HttpStatusCode.BadRequest;

		//send List<ValidationFailure>
		var result = JsonSerializer.Serialize(new { errors = exception.Message });

		return ctx.Response.WriteAsync(result);
	}

	private Task HandleExceptionAsync(HttpContext ctx, Exception exception)
	{
		
		var code = HttpStatusCode.InternalServerError;
		var result = string.Empty;

		switch (exception)
		{
			case UserFriendlyException e:
				code = (HttpStatusCode)e.Status;
				result = e.Message;
				break;
		}

		ctx.Response.StatusCode = (int)code;

		if (result == String.Empty)
		{
			result = JsonSerializer.Serialize(new { error = exception.Message });
		}

		return ctx.Response.WriteAsync(result);
	}
}
