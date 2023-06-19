using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Dlbb.Track.Domain.Entities;
using MediatR;

namespace Dlbb.Track.Application.Accounts.Queries.GetUser;
public class GetUserQuery: IRequest<AppUserVM>
{
	public List<Claim> Claims { get; set; }
}
