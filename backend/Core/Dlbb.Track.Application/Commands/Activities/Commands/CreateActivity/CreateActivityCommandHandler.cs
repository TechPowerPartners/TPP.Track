using System.Security.Claims;
using AutoMapper;
using Dlbb.Track.Common.Exceptions.Extensions;
using Dlbb.Track.Domain.Entities;
using Dlbb.Track.Persistence.Contexts;
using MediatR;

namespace Dlbb.Track.Application.Activities.Commands.CreateActivity;
public class CreateActivityCommandHandler : IRequestHandler<CreateActivityCommand, Guid>
{
	private readonly IMapper _mapper;
	private readonly AppDbContext _context;

	public CreateActivityCommandHandler(AppDbContext context, IMapper mapper)
	{
		_mapper = mapper;
		_context = context;
	}

	public async Task<Guid> Handle
		(CreateActivityCommand request,
		CancellationToken cancellationToken)
	{
		var id = request.Claims.First(c => c.Type == ClaimTypes.IsPersistent).Value;

		(id is null || id == String.Empty).ThrowUserFriendlyExceptionIfTrue
			(Exceptions.Status.Validation, $"request isn't correct");

		var user = _context.AppUsers.FirstOrDefault(o => o.Id == Guid.Parse(id!))!;

		user.ThrowUserFriendlyExceptionIfNull
			(Exceptions.Status.NotFound, $"Not found user id: {id}");

		var entity = _mapper.Map<Activity>(request);

		entity.AppUser = user;

		await _context.Activities.AddAsync(entity, cancellationToken);
		await _context.SaveChangesAsync(cancellationToken);

		return entity.Id;
	}
}
