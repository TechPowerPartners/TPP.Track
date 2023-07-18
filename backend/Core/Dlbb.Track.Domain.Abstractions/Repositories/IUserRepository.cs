using System.Linq.Expressions;
using Dlbb.Track.Domain.Abstractions.Repositories.Base;
using Dlbb.Track.Domain.Entities;

namespace Dlbb.Track.Domain.Abstractions.Repositories;
public interface IUserRepository : IGenericRepository<AppUser>
{
}
