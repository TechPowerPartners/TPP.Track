using Dlbb.Track.Common.Exceptions.Extensions;
using Dlbb.Track.Persistence.Contexts;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Dlbb.Track.Application.Commands.Categories.Commands.DeleteCategory;
public class DeleteCategoryCommandHandler : IRequestHandler<DeleteCategoryCommand>
{
	private readonly AppDbContext _dbContext;

	public DeleteCategoryCommandHandler(AppDbContext dbContext)
	{
		_dbContext = dbContext;
	}

	public async Task<Unit> Handle(DeleteCategoryCommand request, CancellationToken cancellationToken)
	{
		var entity = await _dbContext.Categories.SingleOrDefaultAsync
			(c => c.Id == request.Id, cancellationToken: cancellationToken);

		entity!.ThrowUserFriendlyExceptionIfNull
			(Exceptions.Status.NotFound, $"Not found user");

		_dbContext.Categories.Remove(entity!);

		await _dbContext.SaveChangesAsync();

		return Unit.Value;
	}
}
