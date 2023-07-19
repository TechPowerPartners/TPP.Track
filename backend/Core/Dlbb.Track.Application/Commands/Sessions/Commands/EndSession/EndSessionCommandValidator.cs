using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dlbb.Track.Application.Sessions.Commands.CreateSession;
using Dlbb.Track.Application.Sessions.Commands.EndSession;
using FluentValidation;

namespace Dlbb.Track.Application.Commands.Sessions.Commands.EndSession;
public class EndSessionCommandValidator : AbstractValidator<EndSessionCommand>
{
	public EndSessionCommandValidator()
	{
		RuleFor(c => c.Id)
			.NotEqual(Guid.Empty).WithMessage("Пустой guid");

		RuleFor(c => c.Description)
			.MaximumLength(250).WithMessage("Превышен лимит в 250 символов");

		RuleFor(c => c.Duration)
			.NotEmpty().WithMessage("Длительность не указан");
	}
}
