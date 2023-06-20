using System.Security.Claims;

namespace Dlbb.Track.WebApi.Models.Activities;

public class UpdateActivityDto
{
	public Guid Id { get; set; }
	public string Name { get; set; }
	public string? Description { get; set; }
	public List<Claim> Claims { get; set; }
}
