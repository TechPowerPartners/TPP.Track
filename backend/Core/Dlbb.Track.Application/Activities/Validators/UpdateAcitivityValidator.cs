using Dlbb.Track.Application.Activities.Commands.UpdateActivity;
using FluentValidation;

namespace Dlbb.Track.WebApi.Models.Validators;

public class UpdateActivityCommandValidator : AbstractValidator<UpdateActivityCommand>
{
	public UpdateActivityCommandValidator()
	{
		RuleFor(x => x.Id)
			.NotEmpty().WithName("Обязательно для заполнения")
			.NotEqual(Guid.Empty).WithMessage("Пустой GuId");

		RuleFor(x => x.Name)
			.NotEmpty().WithMessage("Обязательно для заполенения")
			.MaximumLength(50).WithMessage("Превышен лимит в 50 символов");

		RuleFor(x => x.Description)
			.MaximumLength(250).WithMessage("Превышен лимит в 250 символов");
	}
}
