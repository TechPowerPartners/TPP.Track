using Dlbb.Track.Domain.Entities.Base;
using Dlbb.Track.Domain.Enums;

namespace Dlbb.Track.Domain.Entities
{
	public class AppUser : BaseEntity
	{
		public string Email { get; set; } = string.Empty;
		public string PassworHash { get; set; } = string.Empty;
		public string UserName { get; set;} = string.Empty;
		public RoleEnum Role { get; } = RoleEnum.User;
	}
}
