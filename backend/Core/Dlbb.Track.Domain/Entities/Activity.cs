using Dlbb.Track.Domain.Entities.Base;

namespace Dlbb.Track.Domain.Entities
{
	public class Activity : BaseEntity
	{
		public string Name { get; set; }
		public string Description { get; set; }
		public bool Global { get; set; }
		public Guid AppUserId { get; set; }
		public AppUser AppUser { get; set; } = new();
		public ICollection<Session> Sessions { get; set; }
		public ICollection<Category> Categories { get; set; }
	}
}
