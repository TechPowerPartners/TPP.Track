using System.ComponentModel.DataAnnotations;
using System.Security.Claims;

namespace Dlbb.Track.WebApi.Models.Activities;

public class CreateActivityDto
{
	public string Name { get; set; }
	public string? Description { get; set; }
}
