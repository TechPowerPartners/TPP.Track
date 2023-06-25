using System.Diagnostics;

namespace Dlbb.Track.Persistence.Services;
public class TimerService : ITimerService
{
	private Stopwatch _stopwatch = new();

	public string Time => _stopwatch.Elapsed.ToString();

	public void Reset()
	{
		_stopwatch.Reset();
	}

	public void Start()
	{
		_stopwatch.Start();
	}

	public void Stop()
	{
		_stopwatch.Stop();
	}
}
