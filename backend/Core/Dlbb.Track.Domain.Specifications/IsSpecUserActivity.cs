using Dlbb.Track.Domain.Entities;
using Dlbb.Track.Domain.Specifications.Base;

namespace Dlbb.Track.Domain.Specifications;
public class IsSpecUserActivity : Spec<Activity>
{
	public IsSpecUserActivity(Guid userId) : base((a) => a.AppUserId == userId)
	{
	}
}
