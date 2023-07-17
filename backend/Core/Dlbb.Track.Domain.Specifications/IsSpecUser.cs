using System.Linq.Expressions;
using Dlbb.Track.Domain.Entities;
using Dlbb.Track.Domain.Enums;
using Dlbb.Track.Domain.Specifications.Base;

namespace Dlbb.Track.Domain.Specifications;
public class IsSpecUser : Spec<AppUser>
{
	public IsSpecUser(Guid userId) : base((u)=>u.Id == userId)
	{
	}

	public IsSpecUser(string userEmail) : base((u) => u.Email == userEmail)
	{
	}

	public IsSpecUser(Guid userId, string userEmail) : 
		base((u) => u.Id == userId && u.Email == userEmail)
	{
	}

	public IsSpecUser(RoleEnum userRole) : base((u) => u.Role == userRole)
	{
	}

}
