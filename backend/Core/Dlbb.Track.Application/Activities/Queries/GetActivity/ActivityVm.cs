using Dlbb.Track.Domain.Entities;

namespace Dlbb.Track.Application.Activities.Queries.GetActivity;
public class ActivityVm
{
	public Guid Id { get; set; }
	public string Name { get; set; } = string.Empty;
	public string? Description { get; set; }
	public Guid AppUserId { get; set; }
}
