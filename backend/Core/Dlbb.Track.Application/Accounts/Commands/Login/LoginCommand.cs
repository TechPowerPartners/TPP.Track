using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;

namespace Dlbb.Track.Application.Accounts.Commands.Login;
public class LoginCommand : IRequest<Guid>
{
	public string Email { get; set; } = null!;
	public string Password { get; set; } = null!;
}
