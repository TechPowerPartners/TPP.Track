namespace Dlbb.Track.Persistence.Services;
public interface ITimerService
{
  string Time { get; }
  void Start();
  void Stop();
  void Reset();
}
