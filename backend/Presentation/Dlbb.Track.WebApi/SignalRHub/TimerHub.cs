using Dlbb.Track.Domain.TrackTimer;
using Microsoft.AspNetCore.SignalR;

namespace Dlbb.Track.WebApi.SignalRHub;

/// <summary>
/// Хаб для подключения к таймеру
/// </summary>
public class TimerHub : Hub
{
	private readonly IHubContext<TimerHub> _hubContext;
	private Timer _timerForTask;
	private UserTimer _userTimer;

	public TimerHub(IHubContext<TimerHub> hubContext)
	{
		_hubContext = hubContext;
	}


	/// <summary>
	/// Начать получать значение таймера
	/// </summary>
	public void StartSendingData()
	{
		_timerForTask = new Timer(SendData, null, TimeSpan.Zero, TimeSpan.FromSeconds(1));
		_userTimer = new UserTimer();
		_userTimer.Start();
	}


	/// <summary>
	/// Остоновить и обнулить таймер
	/// </summary>
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
