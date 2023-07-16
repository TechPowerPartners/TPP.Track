using Dlbb.Track.Application.Commands.Categories.Queries.GetCategory;
using MediatR;

namespace Dlbb.Track.Application.Commands.Categories.Queries.GetCategories;
public class GetCategoriesQuery : IRequest<List<CategoryVM>>
{
}
