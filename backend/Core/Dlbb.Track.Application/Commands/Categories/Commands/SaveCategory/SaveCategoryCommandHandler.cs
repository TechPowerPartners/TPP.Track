using AutoMapper;
using Dlbb.Track.Common.Exceptions.Extensions;
using Dlbb.Track.Domain.Entities;
using Dlbb.Track.Persistence.Contexts;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Dlbb.Track.Application.Commands.Categories.Commands.SaveCategory;
public class SaveCategoryCommandHandler : IRequestHandler<SaveCategoryCommand>
{
	private readonly IMapper _mapper;
	private readonly AppDbContext _dbContext;

	public SaveCategoryCommandHandler(AppDbContext dbContext, IMapper mapper)
	{
		_mapper = mapper;
		_dbContext = dbContext;
	}

	public async Task<Unit> Handle
		(SaveCategoryCommand request,
		CancellationToken cancellationToken)
	{
		var entity = await _dbContext.Categories.SingleOrDefaultAsync
			(c => c.Id == request.Id, cancellationToken)!;

		List<Activity> activities = new List<Activity>();

		entity!.ThrowUserFriendlyExceptionIfNull
			(Exceptions.Status.NotFound, "Not found category");

		if (entity!.IsGlobal)
		{
			foreach (var id in request.ActivitiesId)
			{
				var activity = await _dbContext.Activities.SingleOrDefaultAsync
					(a => a.Id == id && a.IsGlobal, cancellationToken);

				activity!.ThrowUserFriendlyExceptionIfNull
					(Exceptions.Status.NotFound, "Not found activity");

				activities.Add(activity!);
			}
		}
		else
		{
			foreach (var id in request.ActivitiesId)
			{
				var activity = await _dbContext.Activities.SingleOrDefaultAsync
					(a => a.Id == id, cancellationToken);

				activity!.ThrowUserFriendlyExceptionIfNull
					(Exceptions.Status.NotFound, "Not found activity");

				activities.Add(activity!);
			}
		}

		entity.Activities = activities;

		_dbContext.Update(entity);

		await _dbContext.SaveChangesAsync(cancellationToken);

		return Unit.Value;
	}
}
