using MediatR;

namespace Dlbb.Track.Application.Activities.Queries.GetActivity;
public class GetActivityQuery:IRequest<ActivityVm>
{
	public Guid Id { get; set; }
}
