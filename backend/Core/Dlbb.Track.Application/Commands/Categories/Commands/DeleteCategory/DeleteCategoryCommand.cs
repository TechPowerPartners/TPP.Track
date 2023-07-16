using MediatR;

namespace Dlbb.Track.Application.Commands.Categories.Commands.DeleteCategory;
public class DeleteCategoryCommand : IRequest
{
	public Guid Id { get; set; }
}
