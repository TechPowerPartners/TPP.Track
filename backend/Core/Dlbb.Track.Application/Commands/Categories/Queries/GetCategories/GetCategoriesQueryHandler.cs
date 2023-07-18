using AutoMapper;
using Dlbb.Track.Application.Commands.Categories.Queries.GetCategory;
using Dlbb.Track.Domain.Abstractions.Repositories;
using Dlbb.Track.Persistence.Contexts;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Dlbb.Track.Application.Commands.Categories.Queries.GetCategories;
public class GetCategoriesQueryHandler : IRequestHandler<GetCategoriesQuery, List<CategoryVM>>
{
	private readonly IMapper _mapper;
	private readonly ICategoryRepository _rep;

	public GetCategoriesQueryHandler(ICategoryRepository rep, IMapper mapper)
	{
		_mapper = mapper;
		_rep = rep;
	}

	public async Task<List<CategoryVM>> Handle
		(GetCategoriesQuery request,
		CancellationToken cancellationToken)
	{
		var entities = await _rep.ToListAsync(cancellationToken);

		return _mapper.Map<List<CategoryVM>>(entities);
	}
}
