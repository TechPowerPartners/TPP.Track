using Dlbb.Track.Common.Exceptions.Extensions;
using Dlbb.Track.Domain.Abstractions.Repositories;
using Dlbb.Track.Domain.Specifications;
using Dlbb.Track.Persistence.Contexts;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Dlbb.Track.Application.Commands.Categories.Commands.DeleteCategory;
public class DeleteCategoryCommandHandler : IRequestHandler<DeleteCategoryCommand>
{
	private readonly IRepositoryWrapper _rep;

	public DeleteCategoryCommandHandler(IRepositoryWrapper rep)
	{
		_rep = rep;
	}

	public async Task<Unit> Handle
		(DeleteCategoryCommand request,
		CancellationToken cancellationToken)
	{
		var entity = await _rep.CategoryRepository.FindAsync
			(request.Id, cancellationToken: cancellationToken);

		entity!.ThrowUserFriendlyExceptionIfNull
			(Exceptions.Status.NotFound, $"Not found user");

		_rep.CategoryRepository.Delete(entity!);

		await _rep.CategoryRepository.SaveAsync(cancellationToken);

		return Unit.Value;
	}
}
