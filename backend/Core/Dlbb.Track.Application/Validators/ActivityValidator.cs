
using Dlbb.Track.Domain.Entities;
using FluentValidation;

namespace Dlbb.Track.Application.Validators;

public class ActivityValidator : AbstractValidator<Activity>
{
	public ActivityValidator()
	{
		RuleFor(a => a.Name)
			.NotEmpty();
	}	
}
