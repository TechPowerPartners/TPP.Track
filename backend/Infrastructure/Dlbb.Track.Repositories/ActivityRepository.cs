using System.Linq.Expressions;
using Dlbb.Track.Domain.Abstractions.Repositories;
using Dlbb.Track.Domain.Entities;
using Dlbb.Track.Persistence.Contexts;
using Dlbb.Track.Repositories.Base;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace Dlbb.Track.Repositories;
public class ActivityRepository : GenericRepository<Activity>, IActivityRepository
{
	public ActivityRepository(AppDbContext dbContext) : base(dbContext)
	{
	}
}
