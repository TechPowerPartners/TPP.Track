using Microsoft.AspNetCore.SignalR;

namespace Dlbb.Track.WebApi.SignalRHub;

public class TimerHub : Hub
{
	public async Task SendMessage(string user, string message)
	{
		await Clients.All.SendAsync("ReceiveMessage", user, message);
	}
}
