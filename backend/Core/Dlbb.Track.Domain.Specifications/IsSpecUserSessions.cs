using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dlbb.Track.Domain.Entities;
using Dlbb.Track.Domain.Specifications.Base;

namespace Dlbb.Track.Domain.Specifications;
public class IsSpecUserSessions : Spec<Session>
{
	public IsSpecUserSessions(Guid userid) : base((s) => s.AppUserId == userid)
	{
	}

}
