using System.Linq.Expressions;
using Dlbb.Track.Domain.Abstractions.Repositories;
using Dlbb.Track.Domain.Entities;
using Dlbb.Track.Persistence.Contexts;
using Dlbb.Track.Repositories.Base;
using Microsoft.EntityFrameworkCore;

namespace Dlbb.Track.Repositories;
public class UserRepository : GenericRepository<AppUser>, IUserRepository
{
	public UserRepository(AppDbContext dbContext) : base(dbContext)
	{
	}
}
