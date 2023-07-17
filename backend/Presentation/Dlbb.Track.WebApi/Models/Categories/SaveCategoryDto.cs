namespace Dlbb.Track.WebApi.Models.Categories;

public class SaveCategoryDto
{
	public Guid Id { get; set; }
	public List<Guid> ActivitiesId { get; set; }
}
