using System.Security.Claims;
using AutoMapper;
using Dlbb.Track.Common.Exceptions.Extensions;
using Dlbb.Track.Domain.Entities;
using Dlbb.Track.Domain.Specifications;
using Dlbb.Track.Persistence.Contexts;
using MediatR;
using Microsoft.EntityFrameworkCore;

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
		var id = request.AppUserId;

		var user = await _context.AppUsers.FirstOrDefaultAsync
			(new IsSpecUser(id),cancellationToken)!;

		user!.ThrowUserFriendlyExceptionIfNull
			(Exceptions.Status.NotFound, $"Not found user id: {id}");

		var entity = _mapper.Map<Activity>(request);

		entity.AppUser = user!;

		await _context.Activities.AddAsync(entity, cancellationToken);
		await _context.SaveChangesAsync(cancellationToken);

		return entity.Id;
	}
}
