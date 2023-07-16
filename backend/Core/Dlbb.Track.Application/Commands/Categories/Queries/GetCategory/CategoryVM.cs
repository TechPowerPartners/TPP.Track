namespace Dlbb.Track.Application.Commands.Categories.Queries.GetCategory;
public class CategoryVM
{
	public Guid Id { get; set; }
	public string Name { get; set; }
	public string? Description { get; set; }
	public bool IsGlobal { get; set; }
	public Guid AppUserId { get; set; }
}
