using System.IdentityModel.Tokens.Jwt;
using MediatR;

namespace Dlbb.Track.Application.Accounts.Queries.Login;
public class LoginUserQuery : IRequest<JwtSecurityToken>
{
	public string ExpectedEmail { get; set; } = null!;
	public string ExpectedPassword { get; set; } = null!;
}
