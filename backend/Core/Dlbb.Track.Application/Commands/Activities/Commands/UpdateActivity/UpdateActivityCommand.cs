using System.Security.Claims;
using MediatR;

namespace Dlbb.Track.Application.Activities.Commands.UpdateActivity;
public class UpdateActivityCommand : IRequest
{
	public Guid Id { get; set; }
	public string Name { get; set; }
	public string? Description { get; set; }
	public List<Claim> Cliams { get; set; }
}
