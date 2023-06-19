using System.Security.Claims;
using MediatR;

namespace Dlbb.Track.Application.Accounts.Queries.GetUser;
public class GetUserQuery : IRequest<AppUserVM>
{
	public List<Claim> Claims { get; set; }
}
