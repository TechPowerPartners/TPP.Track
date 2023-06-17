using Dlbb.Track.Domain.Entities;
using Dlbb.Track.Persistence.Contexts;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

namespace Dlbb.Track.Persistence.Services;
public class SeedingService : ISeedingService
{
	private readonly SeedingOptions _options;
	private readonly AppDbContext _dbContext;
	private readonly List<Activity> _activityTemplates;

	public SeedingService(AppDbContext dbContext,IOptions<SeedingOptions> seedingOptions)
	{
		_options = seedingOptions.Value;

		_dbContext = dbContext;
		_activityTemplates = new()
		{
			new()
			{
				Name = "Dota 2",
				Description="degradiruu",
				Id = Guid.NewGuid()
			},
			new()
			{
				Name = "Pause",
				Description = "nichego ne delau",
				Id= Guid.NewGuid(),
			},
			new()
			{
				Name = "Food",
				Description = "em",
				Id = Guid.NewGuid()
			},
			new()
			{
				Name = "Cleaning",
				Description = "ubiraus",
				Id = Guid.NewGuid(),
			}
		};
		foreach (var template in _activityTemplates)
		{
			template.Sessions.Add(GenerateSession(template.Id));
			template.Sessions.Add(GenerateSession
				(template.Id, template.Sessions[template.Sessions.Count()-1].StartTime));
			template.Sessions.Add(GenerateSession
				(template.Id, template.Sessions[template.Sessions.Count() - 1].StartTime));
		}
	}

	private Session GenerateSession(Guid activityId, DateTime? lastSessionEndTime = null)
	{
		var rnd = new Random();

		if (lastSessionEndTime is null)
		{
			lastSessionEndTime = new DateTime
				(23, 6, 17, rnd.Next(24), rnd.Next(60), rnd.Next(60));
		}

		var result = new Session();

		result.StartTime = lastSessionEndTime!.Value;
		result.Duration = new TimeOnly(rnd.Next(24),rnd.Next(1,60));
		result.EndTime = result.StartTime + result.Duration.Value.ToTimeSpan();
		result.ActivityId = activityId;

		return result;
	}

	public async Task Initialize()
	{
		if (_options.SeedEnabled == false)
		{
			return;
		}
		var activities = _dbContext.Activities;
		if (await activities.AnyAsync())
		{
			return;
		}

		var sessions = _dbContext.Sessions;
		if (await sessions.AnyAsync())
		{
			return;
		}

		foreach (var template in _activityTemplates)
		{
			await activities.AddAsync(template);

			foreach (var session in template.Sessions)
			{
				await sessions.AddAsync(session);
			}
		}

		await _dbContext.SaveChangesAsync();
	}
}
