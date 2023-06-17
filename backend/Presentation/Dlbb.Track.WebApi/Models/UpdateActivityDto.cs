namespace Dlbb.Track.WebApi.Models;

public class UpdateActivityDto
{
	public Guid Id { get; set; }
	public string Name { get; set; }
	public string? Description { get; set; }
}
