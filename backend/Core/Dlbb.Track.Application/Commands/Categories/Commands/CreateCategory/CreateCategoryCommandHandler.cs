using AutoMapper;
using Dlbb.Track.Common.Exceptions.Extensions;
using Dlbb.Track.Domain.Abstractions.Repositories;
using Dlbb.Track.Domain.Entities;
using Dlbb.Track.Domain.Specifications;
using Dlbb.Track.Persistence.Contexts;
using MediatR;
using Microsoft.EntityFrameworkCore;

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

		var user = await _rep.UserRepository.FindUserAsync
			(request.AppUserId, cancellationToken);

		user!.ThrowUserFriendlyExceptionIfNull
			(Exceptions.Status.NotFound, $"Not found user");

		await _rep.CategoryRepository.CreateCategoryAsync(entity, cancellationToken);
		await _rep.Save(cancellationToken);

		return entity.Id;
	}
}
