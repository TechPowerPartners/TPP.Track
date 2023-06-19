using Dlbb.Track.Domain.Entities.Base;

namespace Dlbb.Track.Domain.Entities
{
	public class Activity : BaseActivity
	{
		public Guid AppUserId { get; set; }
		public AppUser AppUser { get; set; } = new();
		public ICollection<Session> Sessions { get; set; }
	}
}
