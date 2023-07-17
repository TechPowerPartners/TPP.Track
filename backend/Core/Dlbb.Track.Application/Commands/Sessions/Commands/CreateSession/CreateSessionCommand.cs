using MediatR;

namespace Dlbb.Track.Application.Sessions.Commands.CreateSession;
public class CreateSessionCommand : IRequest<Guid>
{
	public Guid AppUserId { get; set; }
	public DateTime StartTime { get; set; }
	public string? Description { get; set; }
	public Guid ActivityId { get; set; }
}
