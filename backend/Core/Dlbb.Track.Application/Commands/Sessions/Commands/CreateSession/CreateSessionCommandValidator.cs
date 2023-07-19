using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dlbb.Track.Application.Sessions.Commands.CreateSession;
using FluentValidation;

namespace Dlbb.Track.Application.Commands.Sessions.Commands.CreateSession;
public class CreateSessionCommandValidator: AbstractValidator<CreateSessionCommand>
{
	public CreateSessionCommandValidator()
	{
		RuleFor(c => c.AppUserId)
			.NotEqual(Guid.Empty).WithMessage("Пустой guid");

		RuleFor(c => c.StartTime)
			.NotEmpty().WithMessage("Время начала не указана");

		RuleFor(c => c.Description)
			.MaximumLength(250).WithMessage("Превышен лимит в 250 символов");

		RuleFor(c => c.ActivityId)
			.NotEqual(Guid.Empty).WithMessage("Пустой guid");
	}
}
