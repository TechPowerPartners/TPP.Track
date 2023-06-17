using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;

namespace Dlbb.Track.Application.Sessions.Queries.GetSession;
public class GetSessionQuery:IRequest<SessionVm>
{
	public Guid Id { get; set; }
}
