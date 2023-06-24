using System.Text.Json.Serialization;

namespace Dlbb.Track.Domain.TrackTimer;


/// <summary>
/// Таймер для пользователя
/// </summary>
public class UserTimer
{
	private int _interval;
	private bool _isRunning;
	private Thread _timerThread;
	private int _elapsedTime;


	/// <summary>
	/// Таймер для пользовтеля
	/// </summary>
	/// <param name="interval">Число в миллисекундах.</param>
	public UserTimer(int interval = 1000)
	{
		_interval = interval;
		_isRunning = false;
		_timerThread = null;
		_elapsedTime = 0;
	}


	/// <summary>
	/// Получить получить статус таймера
	/// </summary>
	public bool IsRunning
	{
		get { return _isRunning; }
	}


	/// <summary>
	/// Получить значение таймера в миллисекундах
	/// </summary>
	public int ElapsedTime => _elapsedTime;


	/// <summary>
	/// Получить значение таймера в формате hh:mm:ss
	/// </summary>
	/// <returns> <see cref="string"/> </returns>
	public string GetFormatTime()
	{
		var totalSeconds = ElapsedTime / 1000;
		var hours = totalSeconds / 3600;
		var minutes = totalSeconds % 3600 / 60;
		var seconds = totalSeconds % 60;

		return $"{hours:00}:{minutes:00}:{seconds:00}";
	}


	/// <summary>
	/// Обнулить и остановить таймер
	/// </summary>
	public void Reset()
	{
		_elapsedTime = 0;
		Stop();
	}


	/// <summary>
	/// Стартануть таймер
	/// </summary>
	public void Start()
	{
		if (_isRunning)
		{
			return;
		}

		_isRunning = true;
		_timerThread = new Thread(TimerThreadMethod);
		_timerThread.Start();
	}


	private void Stop()
	{
		if (!_isRunning)
		{
			return;
		}

		_isRunning = false;
		_timerThread.Join();
	}

	private void TimerThreadMethod()
	{
		while (_isRunning)
		{
			_elapsedTime += _interval;
			Thread.Sleep(_interval);
		}
	}
}
