﻿using System.Reflection;
using Dlbb.Track.Application.Activities.Validators;
using Dlbb.Track.Application.Common.Behaviors;
using Dlbb.Track.Persistence.Services;
using Dlbb.Track.WebApi.Models.Validators;
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

		services.AddValidatorsFromAssemblyContaining<CreateActivityCommandValidator>();
		services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));

		return services;
	}
}
