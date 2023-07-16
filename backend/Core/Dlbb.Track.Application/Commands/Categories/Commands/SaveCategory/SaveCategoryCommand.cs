using MediatR;

namespace Dlbb.Track.Application.Commands.Categories.Commands.SaveCategory;
public class SaveCategoryCommand : IRequest
{
	public Guid Id { get; set; }
	public bool IsGlobal { get; set; }
	public List<Guid> ActivitiesId { get; set; }
}
