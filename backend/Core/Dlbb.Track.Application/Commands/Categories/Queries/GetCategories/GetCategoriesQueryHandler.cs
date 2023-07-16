using AutoMapper;
using Dlbb.Track.Application.Commands.Categories.Queries.GetCategory;
using Dlbb.Track.Persistence.Contexts;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Dlbb.Track.Application.Commands.Categories.Queries.GetCategories;
public class GetCategoriesQueryHandler : IRequestHandler<GetCategoriesQuery, List<CategoryVM>>
{
	private readonly IMapper _mapper;
	private readonly AppDbContext _dbContext;

	public GetCategoriesQueryHandler(AppDbContext dbContext, IMapper mapper)
	{
		_mapper = mapper;
		_dbContext = dbContext;
	}

	public async Task<List<CategoryVM>> Handle
		(GetCategoriesQuery request,
		CancellationToken cancellationToken)
	{
		var entities = await _dbContext.Categories.ToListAsync(cancellationToken);

		return _mapper.Map<List<CategoryVM>>(entities);
	}
}
