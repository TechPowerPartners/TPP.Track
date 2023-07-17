using Dlbb.Track.Domain.Entities.Base;
using Dlbb.Track.Domain.Enums;

namespace Dlbb.Track.Domain.Entities
{
	public class AppUser : BaseEntity
	{
		public string Email { get; set; } = string.Empty;
		public string PasswordHash { get; set; } = string.Empty;
		public string UserName { get; set;} = string.Empty;
		public RoleEnum Role { get; set; }
		public ICollection<Activity> Activities { get; set; }
		public ICollection<Session> Sessions { get; set; }
		public ICollection<Category> Categories { get; set; }
	}
}
