using System.ComponentModel.DataAnnotations;

namespace Dlbb.Track.WebApi.Models.Activities;

public class CreateActivityDto : ActivityDtoBase
{
	[Required]
	public override string? Name { get; set; }
}
