using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dlbb.Track.Application.Activities.Commands.DeleteActivity;
using FluentValidation;

namespace Dlbb.Track.Application.Commands.Activities.Commands.DeleteActivity;
public class DeleteActivityCommandValidator: AbstractValidator<DeleteActivityCommand>
{
	public DeleteActivityCommandValidator()
	{
		RuleFor(c => c.Id)
			.NotEqual(Guid.Empty).WithMessage("Пустой guid");
	}
}
