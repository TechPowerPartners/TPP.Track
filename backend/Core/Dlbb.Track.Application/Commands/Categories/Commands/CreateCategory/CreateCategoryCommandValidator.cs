using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentValidation;

namespace Dlbb.Track.Application.Commands.Categories.Commands.CreateCategory;
public class CreateCategoryCommandValidator: AbstractValidator<CreateCategoryCommand>
{
	public CreateCategoryCommandValidator()
	{
		RuleFor(x => x.AppUserId)
			.NotEqual(Guid.Empty).WithMessage("Пустой GuId");

		RuleFor(x => x.Name)
			.NotEmpty().WithMessage("Обязательно для заполенения")
			.MaximumLength(50).WithMessage("Превышен лимит в 50 символов");

		RuleFor(x => x.Description)
			.MaximumLength(250).WithMessage("Превышен лимит в 250 символов");
	}
}
