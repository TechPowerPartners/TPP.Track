using System.Linq.Expressions;
using Dlbb.Track.Domain.Entities;
using Dlbb.Track.Domain.Enums;
using Dlbb.Track.Domain.Specifications.Base;

namespace Dlbb.Track.Domain.Specifications.Users;
public class UserByEmailSpec : Spec<AppUser>
{
	public UserByEmailSpec(string userEmail) : base((u) => u.Email == userEmail)
	{
	}
}
