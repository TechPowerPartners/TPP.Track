using AutoMapper;
using Dlbb.Track.Common.Exceptions.Extensions;
using Dlbb.Track.Domain.Specifications;
using Dlbb.Track.Persistence.Contexts;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Dlbb.Track.Application.Commands.Categories.Queries.GetCategory;
public class GetCategoryQueryHandler : IRequestHandler<GetCategoryQuery, CategoryVM>
{
	private readonly IMapper _mapper;
	private readonly AppDbContext _dbContext;

	public GetCategoryQueryHandler(AppDbContext dbContext, IMapper mapper)
	{
		_mapper = mapper;
		_dbContext = dbContext;
	}

	public async Task<CategoryVM> Handle
		(GetCategoryQuery request,
		CancellationToken cancellationToken)
	{
		var entity = await _dbContext.Categories.SingleOrDefaultAsync
			(new IsSpecCategory(request.Id), cancellationToken: cancellationToken);

		entity!.ThrowUserFriendlyExceptionIfNull
			(Exceptions.Status.NotFound, "Not found category");

		return _mapper.Map<CategoryVM>(entity);
	}
}
