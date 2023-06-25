using Dlbb.Track.Persistence.Services;
using Microsoft.AspNetCore.SignalR;

namespace Dlbb.Track.WebApi.SignalRHub;

public class TimerHub : Hub
{
	private readonly ITimerService _timerService;
	private readonly IHubContext<TimerHub> _hubContext;

	public TimerHub(ITimerService timer, IHubContext<TimerHub> hubContext)
	{
		_timerService = timer;
		_hubContext = hubContext;
	}

	public Task StartSendingData()
	{
		_timerService.Timer = new Timer(callback: state =>
		{
			foreach (var connectionId in _timerService.Timers.Keys)
			{
				_hubContext
				.Clients
				.Client(connectionId)
				.SendAsync("ReceiveData", _timerService.Time(connectionId));
			}
		},
		null,
		dueTime: 0,
		period: 250
		);

		return Task.CompletedTask;
	}

	public Task SendData() =>
		  Clients.Caller.SendAsync("ReceiveData", _timerService.Time(Context.ConnectionId));

	public void StartTimer()
	{
		_timerService.Start(Context.ConnectionId);
	}

	public ValueTask? StopSendingData()
	{
		return  _timerService.Timer?.DisposeAsync();
	}

	public void StopTimer() => _timerService.Stop(Context.ConnectionId);
	public void ResetTimer() => _timerService.Reset(Context.ConnectionId);
}
