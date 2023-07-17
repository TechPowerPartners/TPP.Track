using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dlbb.Track.Domain.Entities;
using Dlbb.Track.Domain.Enums;

namespace Dlbb.Track.Application.Accounts.Queries.GetUser;
public class AppUserVM
{
	public Guid Id { get; set; }
	public string Email { get; set; } = string.Empty;
	public string UserName { get; set; } = string.Empty;
	public RoleEnum Role { get; set; } = RoleEnum.User;
}
