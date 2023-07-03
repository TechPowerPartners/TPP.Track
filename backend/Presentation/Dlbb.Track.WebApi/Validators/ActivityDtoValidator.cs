using Dlbb.Track.WebApi.Models.Activities;
using FluentValidation;

namespace Dlbb.Track.WebApi.Validators;

public class ActivityDtoValidator : AbstractValidator<ActivityDtoBase>
{
	public ActivityDtoValidator()
	{
		RuleFor(a => a.Name)
			.NotEmpty()
			.WithMessage("Название активности должно быть указано");
	}
}

