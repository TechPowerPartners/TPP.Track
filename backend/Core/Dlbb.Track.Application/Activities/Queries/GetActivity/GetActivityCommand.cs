using MediatR;

namespace Dlbb.Track.Application.Activities.Queries.GetActivity;
public class GetActivityCommand : IRequest<ActivityVm>
{
	public Guid Id { get; set; }
}
