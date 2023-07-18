using System.Diagnostics;

namespace Dlbb.Track.Persistence.Services;

public class TimerService : ITimerService 
{
	public Dictionary<string, Stopwatch> Timers { get; } = new();
	public Timer Timer { get; set; }

	public string Time(string connectionId)
	{
		return Timers[connectionId].Elapsed.ToString();
	}

	public void Reset(string connectionId)
	{
		Timers[connectionId].Reset();
	}

	public void Start(string connectionId)
	{
		if (Timers.ContainsKey(connectionId) == false)
		{
			Timers[connectionId] = Stopwatch.StartNew();
		}

		Timers[connectionId].Start();
	}

	public void Stop(string connectionId)
	{
		Timers.Remove(connectionId);
	}
}
