using Dlbb.Track.Application.Activities.Queries.GetActivity;
using MediatR;

namespace Dlbb.Track.Application.Activities.Queries.GetActivities;
public class GetActivitiesCommand : IRequest<List<ActivityVm>>
{
}
