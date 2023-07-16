using MediatR;

namespace Dlbb.Track.Application.Commands.Categories.Queries.GetCategory;
public class GetCategoryQuery : IRequest<CategoryVM>
{
	public Guid Id { get; set; }
}
