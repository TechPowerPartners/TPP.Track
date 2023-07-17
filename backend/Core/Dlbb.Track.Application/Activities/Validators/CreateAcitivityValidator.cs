using Dlbb.Track.Application.Activities.Commands.CreateActivity;
using FluentValidation;

namespace Dlbb.Track.Application.Activities.Validators;

public class CreateActivityCommandValidator : AbstractValidator<CreateActivityCommand>
{
	public CreateActivityCommandValidator()
	{
		RuleFor(x => x.Name)
			.NotEmpty().WithMessage("Название не заполнено")
			.MaximumLength(50).WithMessage("Лимит 50 символов");

		RuleFor(x => x.Description)
			.MaximumLength(250).WithMessage("Лимит 50 символов");
	}
}
