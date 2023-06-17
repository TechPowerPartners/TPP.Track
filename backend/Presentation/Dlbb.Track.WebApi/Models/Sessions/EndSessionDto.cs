namespace Dlbb.Track.WebApi.Models.Sessions;

public class EndSessionDto
{
	public Guid Id { get; set; }
	public TimeOnly Duration { get; set; }
}
