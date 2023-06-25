using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;

namespace Dlbb.Track.Application.Sessions.Commands.EndSession;
public class EndSessionCommand : IRequest
{
	public Guid Id { get; set; }
	public TimeOnly Duration { get; set; }
}
