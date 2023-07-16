using MediatR;

namespace Dlbb.Track.Application.Commands.Categories.Commands.UpdateCategory;
public class UpdateCategoryCommand : IRequest
{
	public Guid id { get; set; }
	public string Name { get; set; }
	public string? Description { get; set; }
}
