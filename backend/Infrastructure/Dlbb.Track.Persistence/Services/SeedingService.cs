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


	public SeedingService(AppDbContext dbContext, IOptions<SeedingOptions> seedingOptions, PasswordHasher hasher)
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
		await InitUsers();
		await InitAdmins();
		await InitActivities();
		await InitGlobalActivities();
		await InitSessions();
		await InitCategories();
		await InitGlobalCategories();
	}

	private async Task InitGlobalCategories()
	{
		if (await _dbContext.Categories.AnyAsync(c=>c.IsGlobal))
		{
			return;
		}
		var activities = await _dbContext.Activities.Where(a=>a.IsGlobal).ToListAsync();
		var users = _dbContext.AppUsers.Where(u=>u.Role == RoleEnum.Admin);
		var categoryTemplates = new List<Category>()
		{
			new()
			{
				Name= "Igry",
				Description = "igrau",
				IsGlobal = true,
				Activities = new List<Activity>(),
				AppUser = await users.FirstAsync()
			},
			new()
			{
				Name = "Food",
				Description = "havau",
				IsGlobal = true,
				Activities = new List<Activity>(),
				AppUser = await users.FirstAsync()
			}
		};

		foreach (var category in categoryTemplates)
		{
			category.Activities.Add(activities[_rnd.Next(activities.Count)]);
			category.Activities.Add(activities[_rnd.Next(activities.Count)]);
		}

		await _dbContext.Categories.AddRangeAsync(categoryTemplates);
		await _dbContext.SaveChangesAsync();
	}

	private async Task InitCategories()
	{
		if (await _dbContext.Categories.AnyAsync())
		{
			return;
		}
		var activities =await _dbContext.Activities.ToListAsync();
		var users = _dbContext.AppUsers;
		var categoryTemplates = new List<Category>()
		{
			new()
			{
				Name= "Igry",
				Description = "igrau",
				IsGlobal = false,
				Activities = new List<Activity>(),
				AppUser = await users.FirstAsync()
			},
			new()
			{
				Name = "Food",
				Description = "havau",
				IsGlobal = false,
				Activities = new List<Activity>(),
				AppUser = await users.FirstAsync()
			}
		};

		foreach (var category in categoryTemplates)
		{
			category.Activities.Add(activities[_rnd.Next(activities.Count)]);
			category.Activities.Add(activities[_rnd.Next(activities.Count)]);
		}

		await _dbContext.Categories.AddRangeAsync(categoryTemplates);
		await _dbContext.SaveChangesAsync();
	}

	private async Task InitGlobalActivities()
	{
		if (await _dbContext.Activities.AnyAsync(a => a.IsGlobal))
		{
			Console.WriteLine("В БД уже есть Глобальные Активности");
			return;
		}

		var admin = await _dbContext.AppUsers.FirstAsync(u => u.Role == RoleEnum.Admin);

		var _globalActivityTemplates = new List<Activity>
		{
			new()
			{
				Name = "CS GO",
				Description="degradiruu",
				IsGlobal = true,
				AppUser = admin,
				AppUserId = admin.Id
			},
			new()
			{
				Name = "Global Pause",
				Description = "nichego ne delau",
				IsGlobal = true,
				AppUser = admin,
				AppUserId = admin.Id
			},
			new()
			{
				Name = "Смотрю грифинов",
				Description = "фывфыв",
				IsGlobal = true,
				AppUser = admin,
				AppUserId = admin.Id
			}
		};

		await _dbContext.Activities.AddRangeAsync(_globalActivityTemplates);
		await _dbContext.SaveChangesAsync();
	}

	private async Task InitSessions()
	{
		if (await _dbContext.Sessions.AnyAsync())
		{
			Console.WriteLine("В БД уже есть Сессии");
			return;
		}

		var activities = await _dbContext.Activities.ToListAsync();
		for (int i = 0; i < 10; i++)
		{
			await _dbContext.AddAsync(await GenerateSessionAsync(activities));
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
				AppUser = users.ElementAt(_rnd.Next(users.Count)),
				IsGlobal = false
			},
			new()
			{
				Name = "Pause",
				Description = "nichego ne delau",
				AppUser = users.ElementAt(_rnd.Next(users.Count)),
				IsGlobal = false
			},
			new()
			{
				Name = "Food",
				Description = "em",
				AppUser = users.ElementAt(_rnd.Next(users.Count)),
				IsGlobal = false
			},
			new()
			{
				Name = "Cleaning",
				Description = "ubiraus",
				AppUser = users.ElementAt(_rnd.Next(users.Count)),
				IsGlobal = false
			}
		};

		await _dbContext.AddRangeAsync(_activityTemplates);
		await _dbContext.SaveChangesAsync();
	}


	private async Task<Session> GenerateSessionAsync(List<Activity> activities, DateTime? startTime = null)
	{
		if (startTime is null)
		{
			startTime = new DateTime
				(23, 6, 17, _rnd.Next(24), _rnd.Next(60), _rnd.Next(60));
		}

		var result = new Session();

		List<string> descriptionTemplates = new()
		{
			"huinei stradal",
			"drochil",
			"v dotu gamal",
			"progal",
			"ebashil v cs",
			"v zhopu ebalsya",
			"haval",
			"obshalsya",
			"gulyal",
			"sral",
			"ssal",
			"zakupalsya rashodnikami"
		};

		result.StartTime = startTime!.Value;
		result.Duration = new TimeOnly(_rnd.Next(24), _rnd.Next(1, 60));
		result.Activity = activities.ElementAt(_rnd.Next(activities.Count));
		result.Description = descriptionTemplates[_rnd.Next(descriptionTemplates.Count)];
		result.AppUser = await _dbContext.AppUsers.FirstAsync();

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

		await _dbContext.SaveChangesAsync();
		Console.WriteLine("В БД добавлены админстраторы");
	}


	private async Task InitUsers()
	{
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
				Id = Guid.Parse("b1ce9b6e-17c2-4041-86a0-16f3081cc299"),
				Email = "user2@mail.ru",
				PasswordHash = _hasher.Hash("user2"),
				Role = RoleEnum.User,
				UserName = "user2",
			},
		};

		await _dbContext.AppUsers.AddRangeAsync(users);

		await _dbContext.SaveChangesAsync();
		Console.WriteLine("В БД добавлены тестовые пользователи");
	}
}
