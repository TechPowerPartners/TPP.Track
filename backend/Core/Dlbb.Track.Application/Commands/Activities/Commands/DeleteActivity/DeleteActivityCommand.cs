using MediatR;

namespace Dlbb.Track.Application.Activities.Commands.DeleteActivity;
public class DeleteActivityCommand : IRequest
{
	public Guid Id { get; set; }
	public bool IsGlobal { get; set; }
}
