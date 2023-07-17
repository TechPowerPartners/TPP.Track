using System.Linq.Expressions;
using Dlbb.Track.Domain.Entities;
using Dlbb.Track.Domain.Specifications.Base;

namespace Dlbb.Track.Domain.Specifications;
public class IsSpecSession : Spec<Session>
{
	public IsSpecSession(Expression<Func<Session, bool>> expression) : base(expression)
	{
	}

	public IsSpecSession(Guid sessionId) : base((s)=>s.Id == sessionId)
	{
	}

	public IsSpecSession(TimeOnly? sessionDuration) : 
		base((s) => s.Duration == sessionDuration)
	{
	}

}
