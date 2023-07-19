using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentValidation;

namespace Dlbb.Track.Application.Commands.Categories.Commands.UpdateCategory;
public class UpdateCategoryCommandValidator: AbstractValidator<UpdateCategoryCommand>
{
	public UpdateCategoryCommandValidator()
	{
		RuleFor(c => c.Id)
			.NotEqual(Guid.Empty).WithMessage("Пустой guid");

		RuleFor(c => c.Name)
			.NotEmpty().WithMessage("Обязательное поле")
			.MaximumLength(50).WithMessage("Превышен лимит в 50 символов");

		RuleFor(x => x.Name)
			.NotEmpty().WithMessage("Обязательно для заполенения")
			.MaximumLength(50).WithMessage("Превышен лимит в 50 символов");

		RuleFor(x => x.Description)
			.MaximumLength(250).WithMessage("Превышен лимит в 250 символов");

	}
}
