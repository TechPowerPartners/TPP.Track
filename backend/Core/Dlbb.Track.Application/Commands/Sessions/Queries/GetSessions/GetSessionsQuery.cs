using Dlbb.Track.Application.Sessions.Queries.GetSession;
using MediatR;

namespace Dlbb.Track.Application.Sessions.Queries.GetSessions;
public class GetSessionsQuery : IRequest<List<SessionVm>>
{
}
