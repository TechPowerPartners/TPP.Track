using MediatR;

namespace Dlbb.Track.Application.Commands.Categories.Commands.CreateCategory;
public class CreateCategoryCommand : IRequest<Guid>
{
	public string Name { get; set; }
	public string? Description { get; set; }
	public bool IsGlobal { get; set; }
	public Guid AppUserId { get; set; }

}
