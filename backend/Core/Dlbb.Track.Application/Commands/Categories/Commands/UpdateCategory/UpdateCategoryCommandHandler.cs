using AutoMapper;
using Dlbb.Track.Common.Exceptions.Extensions;
using Dlbb.Track.Domain.Abstractions.Repositories;
using Dlbb.Track.Domain.Specifications;
using MediatR;

namespace Dlbb.Track.Application.Commands.Categories.Commands.UpdateCategory;
public class UpdateCategoryCommandHandler : IRequestHandler<UpdateCategoryCommand>
{
	private readonly IMapper _mapper;
	private readonly IRepositoryWrapper _rep;

	public UpdateCategoryCommandHandler(IRepositoryWrapper rep, IMapper mapper)
	{
		_mapper = mapper;
		_rep = rep;
	}

	public async Task<Unit> Handle
		(UpdateCategoryCommand request,
		CancellationToken cancellationToken)
	{
		var entity = await _rep.CategoryRepository.FindCategoryAsync
			(request.Id, cancellationToken);

		(new IsSpecCategory(request.IsGlobal == false).IsSatisfiedBy(entity!))
			.ThrowUserFriendlyExceptionIfTrue
			(Exceptions.Status.Validation, "request isn't correct");

		entity!.ThrowUserFriendlyExceptionIfNull
			(Exceptions.Status.NotFound, "Not found category");

		entity = _mapper.Map(request, entity);

		_rep.CategoryRepository.UpdateCategory(entity!);

		await _rep.Save(cancellationToken);

		return Unit.Value;
	}
}
