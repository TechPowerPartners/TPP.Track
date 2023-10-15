using Dlbb.Track.Application.Activities.Queries.GetActivity;
using MediatR;

namespace Dlbb.Track.Application.Activities.Queries.GetActivities;
public record GetActivitiesQuery(Guid userId) : IRequest<List<ActivityVm>>
{
}
