namespace Dlbb.Track.Application.Sessions.Queries.GetSession;
public class SessionVm
{
	public Guid Id { get; set; }
	public DateTime StartTime { get; set; }
	public Guid ActivityId { get; set; }
	public TimeOnly? Duration { get; set; }
	public DateTime? EndTime { get; set; }
}
