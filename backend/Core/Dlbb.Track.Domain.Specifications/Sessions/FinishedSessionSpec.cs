using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dlbb.Track.Domain.Entities;
using Dlbb.Track.Domain.Specifications.Base;

namespace Dlbb.Track.Domain.Specifications.Sessions;
public class FinishedSessionSpec : Spec<Session>
{
	public FinishedSessionSpec(Guid userid) : base((s) => s.AppUserId == userid && s.Duration != null)
	{
	}

}
