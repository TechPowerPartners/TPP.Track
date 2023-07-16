using Dlbb.Track.WebApi.Models.Activities;
using FluentValidation;

namespace Dlbb.Track.WebApi.Models.Validators;

public class UpdateAcitivityDtoValidator : AbstractValidator<UpdateActivityDto>
{
	public UpdateAcitivityDtoValidator()
	{
		RuleFor(x => x.Id).NotEmpty().WithMessage("Обязательно для заполнения");
		RuleFor(x => x.Name).Length(1, 250);
		RuleFor(x => x.Description).Length(0, 250);
	}
}
