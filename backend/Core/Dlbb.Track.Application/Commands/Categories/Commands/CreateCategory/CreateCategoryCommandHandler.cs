using AutoMapper;
using Dlbb.Track.Common.Exceptions.Extensions;
using Dlbb.Track.Domain.Entities;
using Dlbb.Track.Persistence.Contexts;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Dlbb.Track.Application.Commands.Categories.Commands.CreateCategory;
public class CreateCategoryCommandHandler : IRequestHandler<CreateCategoryCommand, Guid>
{
	private readonly IMapper _mapper;
	private readonly AppDbContext _dbContext;

	public CreateCategoryCommandHandler(AppDbContext dbContext, IMapper mapper)
	{
		_mapper = mapper;
		_dbContext = dbContext;
	}

	public async Task<Guid> Handle
		(CreateCategoryCommand request,
		CancellationToken cancellationToken)
	{
		var entity = _mapper.Map<Category>(request);

		var user = await _dbContext.AppUsers.SingleOrDefaultAsync
			(u => u.Id == request.AppUserId, cancellationToken);

		user!.ThrowUserFriendlyExceptionIfNull
			(Exceptions.Status.NotFound, $"Not found user");

		await _dbContext.Categories.AddAsync(entity, cancellationToken);
		await _dbContext.SaveChangesAsync(cancellationToken);

		return entity.Id;
	}
}
