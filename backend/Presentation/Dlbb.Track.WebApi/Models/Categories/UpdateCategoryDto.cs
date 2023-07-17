namespace Dlbb.Track.WebApi.Models.Categories;

public class UpdateCategoryDto
{
	public Guid Id { get; set; }
	public string Name { get; set; }
	public string? Description { get; set; }
}
