using MediatR;

namespace Dlbb.Track.Application.Commands.Categories.Commands.DeleteCategory;
public class DeleteCategoryCommand : IRequest
{
	public bool IsGlobal { get; set; }
	public Guid Id { get; set; }
}
