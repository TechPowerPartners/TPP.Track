using MediatR;

namespace Dlbb.Track.Application.Accounts.Commands.Register;
public class RegisterCommand: IRequest<Guid>
{
	public string Email { get; set; } = null!;
	public string Password { get; set; } = null!;
}
