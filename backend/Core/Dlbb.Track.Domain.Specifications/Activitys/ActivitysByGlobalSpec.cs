using System.Linq.Expressions;
using Dlbb.Track.Domain.Entities;
using Dlbb.Track.Domain.Specifications.Base;

namespace Dlbb.Track.Domain.Specifications.Activitys;
public class ActivitysByGlobalSpec : Spec<Activity>
{
	public ActivitysByGlobalSpec(Guid activityId) : base((a) => a.Id == activityId)
	{
	}

	public ActivitysByGlobalSpec(bool isGlobal) : base((a) => a.IsGlobal == isGlobal)
	{
	}
}
