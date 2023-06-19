using Dlbb.Track.Domain.Entities;
using Dlbb.Track.Persistence.Contexts;
using Microsoft.EntityFrameworkCore;

namespace Dlbb.Application.Tests.Common;
public class AppDbContextFactory
{
	public static Guid ActivityIdForDelete = Guid.NewGuid();

	public static Guid ActivityIdForUpdate = Guid.NewGuid();

	public static Guid ActivityIdForGet = Guid.NewGuid();

	public static Guid SessionIdForEnd = Guid.NewGuid();

	public static Guid SessionIdForGet = Guid.NewGuid();
	public static DateTime SessionStartTimeForGet = new DateTime(23, 2, 2, 2, 23, 23);

	public static Guid UserAId = Guid.NewGuid();
	public static Guid UserBId = Guid.NewGuid();

	public static AppDbContext Create()
	{
		var options = new DbContextOptionsBuilder<AppDbContext>()
			.UseInMemoryDatabase(Guid.NewGuid().ToString())
			.Options;

		var context = new AppDbContext(options);

		context.Database.EnsureCreated();

		context.AppUsers.AddRange(CreateUsers());

		context.Activities.AddRange(CreateActivities());

		context.SaveChanges();

		return context;
	}

	private static List<AppUser> CreateUsers()
	{
		var users = new List<AppUser>()
		{
			new AppUser()
			{
				Id = UserAId,
				Email = "zalupa@gmail.com",
				UserName = "Stas",
				PassworHash = "UserAPassword",
				Role = Track.Domain.Enums.RoleEnum.User,
			},
			new AppUser()
			{
				Id = UserBId,
				Email = "hui@gmail.com",
				UserName = "Makson",
				PassworHash = "UserBPassword",
				Role = Track.Domain.Enums.RoleEnum.User,
			}
		};

		return users;
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
				Name = "Pause",
				Description = "nichego ne delau",
				Id= ActivityIdForUpdate,
			},
			new()
			{
				Name = "Dota 2",
				Description="degradiruu",
				Id = ActivityIdForDelete,
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
			template.Sessions.Add
				(GenerateSession(
				activityId: template.Id,
				sessionId: Guid.NewGuid(),
				userId: UserAId));

			template.Sessions.Add(GenerateSession
				(activityId: template.Id, 
				sessionId: Guid.NewGuid(),
				userId: UserAId, 
				lastSessionEndTime: template.Sessions.Last().EndTime));

			template.Sessions.Add(GenerateSession
				(activityId: template.Id,
				sessionId: Guid.NewGuid(),
				userId: UserAId,
				lastSessionEndTime: template.Sessions.Last().EndTime));
		}

		_activityTemplates[0].Sessions.Add(GenerateSession
			(activityId: _activityTemplates[0].Id,
			sessionId: SessionIdForEnd,
			userId: UserBId,
			lastSessionEndTime: _activityTemplates[0].Sessions.Last().EndTime));

		_activityTemplates[0].Sessions.Add(GenerateSession
			(activityId: _activityTemplates[0].Id,
			sessionId: SessionIdForGet,
			userId: UserBId,
			lastSessionEndTime: SessionStartTimeForGet));

		return _activityTemplates;
	}

	private static Session GenerateSession
		(Guid activityId, Guid sessionId, Guid userId, DateTime? lastSessionEndTime = null)
	{
		var rnd = new Random();

		if (lastSessionEndTime is null)
		{
			lastSessionEndTime = new DateTime
				(23, 6, 17, rnd.Next(24), rnd.Next(60), rnd.Next(60));
		}

		var result = new Session();

		result.Id = sessionId;
		result.AppUserId = userId;
		result.StartTime = lastSessionEndTime!.Value;
		result.Duration = new TimeOnly(rnd.Next(24), rnd.Next(1, 60));
		result.EndTime = result.StartTime + result.Duration.Value.ToTimeSpan();
		result.ActivityId = activityId;

		return result;
	}
}
