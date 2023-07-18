using AutoMapper;
using Dlbb.Track.Common.Exceptions.Extensions;
using Dlbb.Track.Domain.Abstractions.Repositories;
using Dlbb.Track.Domain.Entities;
using MediatR;

namespace Dlbb.Track.Application.Commands.Categories.Commands.CreateCategory;
public class CreateCategoryCommandHandler : IRequestHandler<CreateCategoryCommand, Guid>
{
	private readonly IRepositoryWrapper _rep;
	private readonly IMapper _mapper;

	public CreateCategoryCommandHandler(IRepositoryWrapper rep, IMapper mapper)
	{
		_rep = rep;
		_mapper = mapper;
	}

	public async Task<Guid> Handle
		(CreateCategoryCommand request,
		CancellationToken cancellationToken)
	{
		var entity = _mapper.Map<Category>(request);

		var user = await _rep.UserRepository.FindAsync
			(request.AppUserId, cancellationToken);

		user!.ThrowUserFriendlyExceptionIfNull
			(Exceptions.Status.NotFound, $"Not found user");

		await _rep.CategoryRepository.AddAsync(entity, cancellationToken);
		await _rep.CategoryRepository.SaveAsync(cancellationToken);

		return entity.Id;
	}
}
