using AutoMapper;
using Dlbb.Track.Common.Exceptions.Extensions;
using Dlbb.Track.Persistence.Contexts;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Dlbb.Track.Application.Commands.Categories.Commands.UpdateCategory;
public class UpdateCategoryCommandHandler : IRequestHandler<UpdateCategoryCommand>
{
	private readonly IMapper _mapper;
	private readonly AppDbContext _dbContext;

	public UpdateCategoryCommandHandler(AppDbContext dbContext, IMapper mapper)
	{
		_mapper = mapper;
		_dbContext = dbContext;
	}
	public async Task<Unit> Handle
		(UpdateCategoryCommand request,
		CancellationToken cancellationToken)
	{
		var entity = await _dbContext.Categories.SingleOrDefaultAsync
			(c => c.Id == request.id, cancellationToken);

		entity!.ThrowUserFriendlyExceptionIfNull
			(Exceptions.Status.NotFound, "Not found category");

		entity = _mapper.Map(request, entity);

		await _dbContext.SaveChangesAsync();

		return Unit.Value;
	}
}
