using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentValidation;

namespace Dlbb.Track.Application.Commands.Categories.Commands.DeleteCategory;
public class DeleteCategoryCommandValidator: AbstractValidator<DeleteCategoryCommand>
{
	public DeleteCategoryCommandValidator()
	{
		RuleFor(c => c.Id)
			.NotEqual(Guid.Empty).WithMessage("Пустой guid");
	}
}
