using Microsoft.EntityFrameworkCore;
using Dlbb.Track.Persistence.Contexts;
using Dlbb.Track.Domain.Entities;

namespace Dlbb.Application.Tests.Common;
public class AppDbContextFactory
{
	public static Guid ActivityIdForDelete = Guid.NewGuid();
	public static Guid ActivityIdForUpdate = Guid.NewGuid();
	public static Guid ActivityIdForGet = Guid.NewGuid();

	public static AppDbContext Create()
	{
		var options = new DbContextOptionsBuilder<AppDbContext>()
			.UseInMemoryDatabase(Guid.NewGuid().ToString())
			.Options;

		var context = new AppDbContext(options);

		context.Database.EnsureCreated();

		context.Activities.AddRange(CreateActivities());

		context.SaveChanges();

		return context;
	}

	public static void Destroy(AppDbContext context)
	{
		context.Database.EnsureDeleted();
		context.Dispose();
	}

	private static List<Activity> CreateActivities()
	{
		List<Activity> _activityTemplates = new()
		{
			new()
			{
				Name = "Dota 2",
				Description="degradiruu",
				Id = ActivityIdForDelete,
			},
			new()
			{
				Name = "Pause",
				Description = "nichego ne delau",
				Id= ActivityIdForUpdate,
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
				Id = ActivityIdForGet,
			}
		};

		foreach (var template in _activityTemplates)
		{
			template.Sessions.Add(GenerateSession(template.Id));
			template.Sessions.Add(GenerateSession
				(template.Id, template.Sessions[template.Sessions.Count() - 1].StartTime));
			template.Sessions.Add(GenerateSession
				(template.Id, template.Sessions[template.Sessions.Count() - 1].StartTime));
		}

		return _activityTemplates;
	}

	private static Session GenerateSession(Guid activityId, DateTime? lastSessionEndTime = null)
	{
		var rnd = new Random();

		if (lastSessionEndTime is null)
		{
			lastSessionEndTime = new DateTime
				(23, 6, 17, rnd.Next(24), rnd.Next(60), rnd.Next(60));
		}

		var result = new Session();

		result.StartTime = lastSessionEndTime!.Value;
		result.Duration = new TimeOnly(rnd.Next(24), rnd.Next(1, 60));
		result.EndTime = result.StartTime + result.Duration.Value.ToTimeSpan();
		result.ActivityId = activityId;

		return result;
	}
}
