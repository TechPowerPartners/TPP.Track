using Dlbb.Track.Persistence.Services;
using Microsoft.AspNetCore.SignalR;

namespace Dlbb.Track.WebApi.SignalRHub;

public class TimerHub : Hub
{
  private readonly ITimerService _timer;
  public TimerHub(ITimerService timer)
  {
	_timer = timer;
  }

  public Task SendData() => Clients.Caller.SendAsync("ReceiveData", _timer.Time);

  public void StartTimer()
  {
	_timer.Start();
  }

  public void StopTimer() => _timer.Stop();
  public void ResetTimer() => _timer.Reset();
}
