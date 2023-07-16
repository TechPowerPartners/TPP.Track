using MediatR;

namespace Dlbb.Track.Application.Sessions.Commands.EndSession;
public class EndSessionCommand : IRequest
{
	public Guid Id { get; set; }
	public string? Description { get; set; }
	public TimeOnly Duration { get; set; }
}
