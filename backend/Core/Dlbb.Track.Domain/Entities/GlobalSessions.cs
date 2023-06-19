using Dlbb.Track.Domain.Entities.Base;

namespace Dlbb.Track.Domain.Entities;
public class GlobalSessions: BaseEntity
{
	public Guid? AppUserId { get; set; }
	public AppUser? AppUser { get; set; } = new();
	public Guid GlobalActivityId { get; set; }
	public GlobalActivity GlobalActivity { get; set; } = new();
	public TimeOnly? Duration { get; set; }
	public DateTime StartTime { get; set; }

}
