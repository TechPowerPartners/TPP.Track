using Dlbb.Track.Application.Common;
using Microsoft.AspNetCore.SignalR;
using SignalRSwaggerGen.Attributes;

namespace Dlbb.Track.WebApi.SignalRHub;


public class TimerHub : Hub
{
	private readonly IHubContext<TimerHub> _hubContext;
	private Timer _timerForTask;
	private UserTimer _userTimer;

	public TimerHub(IHubContext<TimerHub> hubContext)
	{
		Console.WriteLine("New Hub");
		_hubContext = hubContext;
	}

	public void StartSendingData()
	{
		Console.WriteLine("Client fetch method");
		_timerForTask = new Timer(SendData, null, TimeSpan.Zero, TimeSpan.FromSeconds(1));
		_userTimer = new UserTimer(1000);
		_userTimer.Start();
	}

	public void StopSendingData()
	{
		_timerForTask?.Dispose();
		_userTimer?.Reset();
	}

	private void SendData(object state)
	{
		var data = _userTimer.GetFormatTime();
		_hubContext.Clients.All.SendAsync("ReceiveData", data);
	}
}
