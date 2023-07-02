using Dlbb.Track.Domain.Entities;
using Dlbb.Track.Domain.Enums;
using Dlbb.Track.Persistence.Contexts;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

namespace Dlbb.Track.Persistence.Services;
public class SeedingService : ISeedingService
{
	private readonly SeedingOptions _options;
	private readonly AppDbContext _dbContext;
	private readonly PasswordHasher _hasher;
	private readonly List<Activity> _activityTemplates;

	private List<AppUser> _userList = new();

	public SeedingService(AppDbContext dbContext,IOptions<SeedingOptions> seedingOptions, PasswordHasher hasher)
	{
		_options = seedingOptions.Value;

		_dbContext = dbContext;
		_hasher = hasher;
		InitUsers().Wait();
		InitAdmins().Wait();
		_userList = _dbContext.AppUsers.ToList();

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
		result.AppUser = _userList.ElementAt(rnd.Next(_userList.Count));

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

	private async Task InitAdmins()
	{
		if (_options.SeedEnabled == false)
		{
			return;
		}

		if (await _dbContext.AppUsers.AnyAsync(u => u.Role == RoleEnum.Admin))
		{
			Console.WriteLine("В БД уже есть админстарторы");
			return;
		}

		var admins = new List<AppUser>()
		{
			new()
			{
				Email = "admin1@mail.ru",
				PassworHash = _hasher.Hash("admin1"),
				Role = RoleEnum.Admin,
				UserName = "admin1"
			},
			new()
			{
				Email = "admin2@mail.ru",
				PassworHash = _hasher.Hash("admin2"),
				Role = RoleEnum.Admin,
				UserName = "admin2"
			},
		};

		await _dbContext.AppUsers.AddRangeAsync(admins);

		_dbContext.SaveChangesAsync().Wait();
		Console.WriteLine("В БД добавлены админстраторы");
	}

	private async Task InitUsers()
	{
		if (_options.SeedEnabled == false)
		{
			return;
		}

		if (await _dbContext.AppUsers.AnyAsync())
		{
			Console.WriteLine("В БД уже есть пользователи");
			return;
		}

		var users = new List<AppUser>()
		{
			new()
			{
				Email = "user1@mail.ru",
				PassworHash = _hasher.Hash("user1"),
				Role = RoleEnum.User,
				UserName = "user1"
			},
			new()
			{
				Id = Guid.Parse("b1ce9b6e-17c2-4041-86a0-16f3081cc299"),
				Email = "user2@mail.ru",
				PassworHash = _hasher.Hash("user2"),
				Role = RoleEnum.User,
				UserName = "user2",
			},
		};

		await _dbContext.AppUsers.AddRangeAsync(users);

		_dbContext.SaveChangesAsync().Wait();
		Console.WriteLine("В БД добавлены тестовые пользователи");
	}
}
