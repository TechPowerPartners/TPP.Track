using Dlbb.Track.Domain.Entities;
using Dlbb.Track.Domain.Specifications.Base;

namespace Dlbb.Track.Domain.Specifications.Users;
public class ActivityByUserIdSpec : Spec<Activity>
{
	public ActivityByUserIdSpec(Guid userId) : base((a) => a.AppUserId == userId)
	{
	}
}
