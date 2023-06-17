namespace Dlbb.Track.WebApi.Models.Sessions;

public class CreateSessionDto
{
	public DateTime StartTime { get; set; }
	public Guid ActivityId { get; set; }
}
