using System.IdentityModel.Tokens.Jwt;
using MediatR;

namespace Dlbb.Track.Application.Accounts.Commands.Register;
public class RegisterUserCommand: IRequest<JwtSecurityToken>
{
	public string Email { get; set; } = null!;
	public string Password { get; set; } = null!;
	public string UserName { get; set; } = null!;
}
