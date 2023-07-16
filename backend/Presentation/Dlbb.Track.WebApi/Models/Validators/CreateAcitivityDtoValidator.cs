using Dlbb.Track.WebApi.Models.Activities;
using FluentValidation;

namespace Dlbb.Track.WebApi.Models.Validators;

public class CreateActivityDtoValidator : AbstractValidator<CreateActivityDto>
{
	public CreateActivityDtoValidator()
	{
		RuleFor(x => x.Name).NotEmpty().WithMessage("Обязательно для заполнения");
		RuleFor(x => x.Name).Length(1, 250);
		RuleFor(x => x.Description).Length(0, 250);
	}
}
