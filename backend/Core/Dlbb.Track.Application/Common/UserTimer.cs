using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dlbb.Track.Application.Common;


/// <summary>
/// Таймер для пользователя
/// </summary>
public class UserTimer
{
	private int interval;
	private bool isRunning;
	private Thread timerThread;
	private int elapsedTime;


	/// <summary>
	/// Таймер для пользовтеля
	/// </summary>
	/// <param name="interval">Число в миллисекундах.</param>
	public UserTimer(int interval)
	{
		this.interval = interval;
		isRunning = false;
		timerThread = null;
		elapsedTime = 0;
	}


	/// <summary>
	/// Получить получить статус таймера
	/// </summary>
	public bool IsRunning
	{
		get { return isRunning; }
	}


	/// <summary>
	/// Получить значение таймера в миллисекундах
	/// </summary>
	public int ElapsedTime
	{
		get
		{
			return elapsedTime;
		}
	}


	/// <summary>
	/// Получить значение таймера в формате hh:mm:ss
	/// </summary>
	/// <returns> <see cref="string"/> </returns>
	public string GetFormatTime()
	{
		int totalSeconds = ElapsedTime / 1000;
		int hours = totalSeconds / 3600;
		int minutes = (totalSeconds % 3600) / 60;
		int seconds = totalSeconds % 60;

		return $"{hours:00}:{minutes:00}:{seconds:00}";
	}


	/// <summary>
	/// Обнулить и остановить таймер
	/// </summary>
	public void Reset()
	{
		elapsedTime = 0;
		Stop();
	}


	/// <summary>
	/// Стартануть таймер
	/// </summary>
	public void Start()
	{
		if (!isRunning)
		{
			isRunning = true;
			timerThread = new Thread(TimerThreadMethod);
			timerThread.Start();
		}
	}


	/// <summary>
	/// Остановить таймер
	/// </summary>
	private void Stop()
	{
		if (isRunning)
		{
			isRunning = false;
			timerThread.Join();
		}
	}

	private void TimerThreadMethod()
	{
		while (isRunning)
		{
			elapsedTime += interval;
			Thread.Sleep(interval);
		}
	}
}
