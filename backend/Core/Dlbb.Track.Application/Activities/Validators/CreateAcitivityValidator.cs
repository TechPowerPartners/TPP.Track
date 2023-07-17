using Dlbb.Track.Application.Activities.Commands.CreateActivity;
using FluentValidation;

namespace Dlbb.Track.Application.Activities.Validators;

public class CreateActivityCommandValidator : AbstractValidator<CreateActivityCommand>
{
	public CreateActivityCommandValidator()
	{
		RuleFor(x => x.Name).NotEmpty().WithMessage("Обязательно для заполнения");
		RuleFor(x => x.Name).MaximumLength(250);
		RuleFor(x => x.Description).MaximumLength(250);
	}
}
