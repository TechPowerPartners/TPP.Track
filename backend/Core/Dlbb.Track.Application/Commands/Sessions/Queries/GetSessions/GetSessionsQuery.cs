using Dlbb.Track.Application.Sessions.Queries.GetSession;
using MediatR;

namespace Dlbb.Track.Application.Sessions.Queries.GetSessions;
public record GetSessionsQuery(Guid userid) : IRequest<List<SessionVm>>
{
}
