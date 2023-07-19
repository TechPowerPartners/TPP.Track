using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dlbb.Track.Application.Accounts.Commands.Register;
using FluentValidation;

namespace Dlbb.Track.Application.Commands.Accounts.Commands.Register;
public class RegisterUserCommandValidator : AbstractValidator<RegisterUserCommand>
{
	public RegisterUserCommandValidator()
	{
		RuleFor(m => m.Email)
			.NotEmpty().WithMessage("Обязательное поле")
			.EmailAddress().WithMessage("Напишите правильно вашу почту");

		RuleFor(m => m.Password)
			.NotEmpty().WithMessage("Обязательное поле")
			.Length(4, 50).WithMessage("Длина от 4 до 50");

		RuleFor(m => m.UserName)
			.NotEmpty().WithMessage("Обязательное поле")
			.Length(4, 50).WithMessage("Длина от 4 до 50");
	}
}
