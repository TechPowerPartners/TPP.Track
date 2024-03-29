﻿using System.Security.Claims;
using Dlbb.Track.Application.Accounts.Shared;
using Dlbb.Track.Domain.Entities;
using Dlbb.Track.Persistence.Contexts;
using Dlbb.Track.Persistence.Services;
using Microsoft.EntityFrameworkCore;

namespace Dlbb.Application.Tests.Common;
public class AppDbContextFactory
{
	//public static Guid ActivityIdForDelete = Guid.NewGuid();

	//public static Guid ActivityIdForUpdate = Guid.NewGuid();

	//public static Guid ActivityIdForGet = Guid.NewGuid();

	//public static Guid SessionIdForEnd = Guid.NewGuid();

	//public static Guid SessionIdForGet = Guid.NewGuid();
	//public static DateTime SessionStartTimeForGet = new DateTime(23, 2, 2, 2, 23, 23);

	//public static Guid UserAId = Guid.NewGuid();
	//public static Guid UserBId = Guid.NewGuid();
	//public static List<Claim> UserAClaims;
	//public static List<Claim> UserBClaims;

	//public static AppDbContext Create()
	//{
	//	var options = new DbContextOptionsBuilder<AppDbContext>()
	//		.UseInMemoryDatabase(Guid.NewGuid().ToString())
	//		.Options;

	//	var context = new AppDbContext(options);

	//	context.Database.EnsureCreated();

	//	context.AppUsers.AddRange(CreateUsers());

	//	context.Activities.AddRange(CreateActivities());

	//	context.SaveChanges();

	//	return context;
	//}

	//private static List<AppUser> CreateUsers()
	//{
	//	var users = new List<AppUser>()
	//	{
	//		new AppUser()
	//		{
	//			Id = UserAId,
	//			Email = "zalupa@gmail.com",
	//			UserName = "Stas",
	//			PassworHash = new PasswordHasher().Hash("UserAPassword"),
	//			Role = Track.Domain.Enums.RoleEnum.User,
	//		},
	//		new AppUser()
	//		{
	//			Id = UserBId,
	//			Email = "hui@gmail.com",
	//			UserName = "Makson",
	//			PassworHash = new PasswordHasher().Hash("UserBPassword"),
	//			Role = Track.Domain.Enums.RoleEnum.User,
	//		}
	//	};

	//	UserAClaims = AutorizeUtils.GetClaimsFor(users[0]);
	//	UserBClaims = AutorizeUtils.GetClaimsFor(users[1]);

	//	return users;
	//}

	//public static void Destroy(AppDbContext context)
	//{
	//	context.Database.EnsureDeleted();
	//	context.Dispose();
	//}

	//private static List<Activity> CreateActivities()
	//{
	//	List<Activity> _activityTemplates = new()
	//	{
	//		new()
	//		{
	//			Name = "Pause",
	//			Description = "nichego ne delau",
	//			Id= ActivityIdForUpdate,
	//		},
	//		new()
	//		{
	//			Name = "Dota 2",
	//			Description="degradiruu",
	//			Id = ActivityIdForDelete,
	//		},
	//		new()
	//		{
	//			Name = "Food",
	//			Description = "em",
	//			Id = Guid.NewGuid()
	//		},
	//		new()
	//		{
	//			Name = "Cleaning",
	//			Description = "ubiraus",
	//			Id = ActivityIdForGet,
	//		}
	//	};

	//	foreach (var template in _activityTemplates)
	//	{
	//		template.Sessions.Add(GenerateSession(template.Id,Guid.NewGuid()));
	//		template.Sessions.Add(GenerateSession
	//			(template.Id,Guid.NewGuid(), template.Sessions[template.Sessions.Count() - 1].StartTime));
	//		template.Sessions.Add(GenerateSession
	//			(template.Id,Guid.NewGuid(), template.Sessions[template.Sessions.Count() - 1].StartTime));
	//	}

	//	_activityTemplates[0].Sessions.Add(GenerateSession
	//		(_activityTemplates[0].Id,
	//		SessionIdForEnd, 
	//		_activityTemplates[0].Sessions.Last().EndTime));
		
	//	_activityTemplates[0].Sessions.Add(GenerateSession
	//		(activityId: _activityTemplates[0].Id,
	//		sessionId: SessionIdForGet,
	//		userId: UserBId,
	//		lastSessionEndTime: SessionStartTimeForGet));

	//	return _activityTemplates;
	//}

	//private static Session GenerateSession
	//	(Guid activityId, Guid sessionId, Guid userId, DateTime? lastSessionEndTime = null)
	//{
	//	var rnd = new Random();

	//	if (lastSessionEndTime is null)
	//	{
	//		lastSessionEndTime = new DateTime
	//			(23, 6, 17, rnd.Next(24), rnd.Next(60), rnd.Next(60));
	//	}

	//	var result = new Session();

	//	result.Id = sessionId;
	//	result.AppUserId = userId;
	//	result.StartTime = lastSessionEndTime!.Value;
	//	result.Duration = new TimeOnly(rnd.Next(24), rnd.Next(1, 60));
	//	result.ActivityId = activityId;

	//	return result;
	//}
}
