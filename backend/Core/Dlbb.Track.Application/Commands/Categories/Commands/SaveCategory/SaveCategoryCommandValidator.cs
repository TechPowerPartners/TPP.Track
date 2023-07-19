using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentValidation;

namespace Dlbb.Track.Application.Commands.Categories.Commands.SaveCategory;
public class SaveCategoryCommandValidator: AbstractValidator<SaveCategoryCommand>
{
	public SaveCategoryCommandValidator()
	{
		RuleFor(c => c.Id).
			NotEqual(Guid.Empty).WithMessage("Пустой guid");

		RuleFor(c => c.ActivitiesId)
			.NotNull().WithMessage("Список null");
	}
}
