using System.Linq.Expressions;
using Dlbb.Track.Domain.Entities;
using Dlbb.Track.Domain.Specifications.Base;

namespace Dlbb.Track.Domain.Specifications;
public class IsSpecActivity : Spec<Activity>
{
	public IsSpecActivity(Guid activityId) : base((a) => a.Id == activityId)
	{
	}

	public IsSpecActivity(bool isGlobal) : base((a) => a.IsGlobal == isGlobal)
	{
	}
}
