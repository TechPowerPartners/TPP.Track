using Dlbb.Track.Application.Activities.Commands.UpdateActivity;
using FluentValidation;

namespace Dlbb.Track.WebApi.Models.Validators;

public class UpdateActivityCommandValidator : AbstractValidator<UpdateActivityCommand>
{
	public UpdateActivityCommandValidator()
	{
		RuleFor(x => x.Id).NotEmpty().WithMessage("Обязательно для заполнения").NotEqual(Guid.Empty);
		RuleFor(x => x.Name).NotEmpty().MaximumLength(250);
		RuleFor(x => x.Description).MaximumLength(250);
	}
}
