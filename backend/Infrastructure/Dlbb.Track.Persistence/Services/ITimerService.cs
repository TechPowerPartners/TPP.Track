using System.Diagnostics;

namespace Dlbb.Track.Persistence.Services;
public interface ITimerService
{
	Timer Timer { get; set; }
	Dictionary<string, Stopwatch> Timers { get; }
	string Time(string connectionId);
	void Start(string connectionId);
	void Stop(string connectionId);
	void Reset(string connectionId);
}
