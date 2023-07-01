using System.Reflection;
using Dlbb.Track.Application.Validators;
using Dlbb.Track.Domain.Entities;
using Dlbb.Track.Persistence.Services;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.DependencyInjection;

namespace Dlbb.Track.Application.CompositionRoot;
public static class ApplicationConfiguration
{
	public static IServiceCollection AddApplication
		(this IServiceCollection services)
	{
		services.AddMediatR(Assembly.GetExecutingAssembly());
		services.AddSingleton<PasswordHasher>();
		services.AddScoped<IValidator<Activity>, ActivityValidator>();
		
		return services;
	}
}
