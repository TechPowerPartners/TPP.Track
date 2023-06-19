using Dlbb.Track.Domain.Entities;
using Dlbb.Track.Domain.Enums;
using Dlbb.Track.Persistence.Contexts;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

namespace Dlbb.Track.Persistence.Services;
public class SeedingService : ISeedingService
{
	private readonly SeedingOptions _options;
	private readonly Random _rnd;
	private readonly AppDbContext _dbContext;
	private readonly PasswordHasher _hasher;
	private List<Activity> _activityTemplates;


	public SeedingService(AppDbContext dbContext,IOptions<SeedingOptions> seedingOptions, PasswordHasher hasher)
	{
		_options = seedingOptions.Value;
		_rnd = new Random();
		_dbContext = dbContext;
		_hasher = hasher;

	}


	public async Task Initialize()
	{
		if (_options.SeedEnabled == false)
		{
			return;
		}

		//Должны идти строго в такой последовательности!
		InitUsers().Wait();
		InitAdmins().Wait();
		InitActivities().Wait();
		InitSessions().Wait();
		InitGlobalActivities().Wait();
		InitGlobalSessions().Wait();
	}

	private async Task InitGlobalSessions()
	{
		if (await _dbContext.Sessions.AnyAsync())
		{
			Console.WriteLine("В БД уже есть Глобальные Сессии");
			return;
		}

		var globActivities = _dbContext.GlobalActivities.ToList();
		var users = _dbContext.AppUsers.ToList();
		var gsList = new List<GlobalSessions>()
		{
			new()
			{
				AppUser = users.ElementAt(_rnd.Next(users.Count)),
				GlobalActivity = globActivities.ElementAt(_rnd.Next(globActivities.Count)),
				StartTime = new DateTime(23, 6, 17, _rnd.Next(24), _rnd.Next(60), _rnd.Next(60)),
				Duration = new TimeOnly(_rnd.Next(24),_rnd.Next(1,60))
			},
			new()
			{
				AppUser = users.ElementAt(_rnd.Next(users.Count)),
				GlobalActivity = globActivities.ElementAt(_rnd.Next(globActivities.Count)),
				StartTime = new DateTime(23, 6, 17, _rnd.Next(24), _rnd.Next(60), _rnd.Next(60)),
				Duration = new TimeOnly(_rnd.Next(24),_rnd.Next(1,60))
			},
			new()
			{
				AppUser = users.ElementAt(_rnd.Next(users.Count)),
				GlobalActivity = globActivities.ElementAt(_rnd.Next(globActivities.Count)),
				StartTime = new DateTime(23, 6, 17, _rnd.Next(24), _rnd.Next(60), _rnd.Next(60)),
				Duration = new TimeOnly(_rnd.Next(24),_rnd.Next(1,60))
			}
		};

		await _dbContext.GlobalSessions.AddRangeAsync(gsList);
		_dbContext.SaveChanges();

		await _dbContext.SaveChangesAsync();
	}

	private async Task InitGlobalActivities()
	{
		if (await _dbContext.GlobalActivities.AnyAsync())
		{
			Console.WriteLine("В БД уже есть Глобальные Активности");
			return;
		}

		var users = await _dbContext.AppUsers.ToListAsync();

		var _globalActivityTemplates = new List<GlobalActivity>
		{
			new()
			{
				Name = "CS GO",
				Description="degradiruu",
			},
			new()
			{
				Name = "Global Pause",
				Description = "nichego ne delau",
			},
			new()
			{
				Name = "Смотрю грифинов",
				Description = "фывфыв",
			}
		};

		await _dbContext.GlobalActivities.AddRangeAsync(_globalActivityTemplates);
		_dbContext.SaveChanges();
	}

	private async Task InitSessions()
	{
		if (await _dbContext.Sessions.AnyAsync())
		{
			Console.WriteLine("В БД уже есть Сессии");
			return;
		}

		var activities = _dbContext.Activities.ToList();
		for (int i = 0; i < 10; i++)
		{
			_dbContext.Add(GenerateSession(activities));
		};

		await _dbContext.SaveChangesAsync();
	}


	private async Task InitActivities()
	{
		if (await _dbContext.Activities.AnyAsync())
		{
			Console.WriteLine("В БД уже есть Активности");
			return;
		}

		var users = await _dbContext.AppUsers.ToListAsync();

		_activityTemplates = new()
		{
			new()
			{
				Name = "Dota 2",
				Description="degradiruu",
				AppUser = users.ElementAt(_rnd.Next(users.Count))
			},
			new()
			{
				Name = "Pause",
				Description = "nichego ne delau",
				AppUser = users.ElementAt(_rnd.Next(users.Count))
			},
			new()
			{
				Name = "Food",
				Description = "em",
				AppUser = users.ElementAt(_rnd.Next(users.Count))
			},
			new()
			{
				Name = "Cleaning",
				Description = "ubiraus",
				AppUser = users.ElementAt(_rnd.Next(users.Count))
			}
		};

		await _dbContext.AddRangeAsync(_activityTemplates);
		_dbContext.SaveChanges();
	}


	private Session GenerateSession(List<Activity> activities, DateTime? startTime = null)
	{
		if (startTime is null)
		{
			startTime = new DateTime
				(23, 6, 17, _rnd.Next(24), _rnd.Next(60), _rnd.Next(60));
		}

		var result = new Session();


		result.StartTime = startTime!.Value;
		result.Duration = new TimeOnly(_rnd.Next(24), _rnd.Next(1,60));
		result.Activity = activities.ElementAt(_rnd.Next(activities.Count));

		return result;
	}


	private async Task InitAdmins()
	{
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
				PasswordHash = _hasher.Hash("admin1"),
				Role = RoleEnum.Admin,
				UserName = "admin1"
			},
			new()
			{
				Email = "admin2@mail.ru",
				PasswordHash = _hasher.Hash("admin2"),
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
				PasswordHash = _hasher.Hash("user1"),
				Role = RoleEnum.User,
				UserName = "user1"
			},
			new()
			{
				Email = "user2@mail.ru",
				PasswordHash = _hasher.Hash("user2"),
				Role = RoleEnum.User,
				UserName = "user2",
			},
		};

		await _dbContext.AppUsers.AddRangeAsync(users);

		_dbContext.SaveChangesAsync().Wait();
		Console.WriteLine("В БД добавлены тестовые пользователи");
	}
}
