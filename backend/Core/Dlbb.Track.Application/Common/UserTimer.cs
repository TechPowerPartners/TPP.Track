using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dlbb.Track.Application.Common;
public class UserTimer
{
	private int interval;
	private bool isRunning;
	private Thread timerThread;
	private int elapsedTime;

	public UserTimer(int interval)
	{
		this.interval = interval;
		this.isRunning = false;
		this.timerThread = null;
		this.elapsedTime = 0;
	}

	public bool IsRunning
	{
		get { return isRunning; }
	}

	public int ElapsedTime
	{
		get
		{
			return elapsedTime;
		}
	}

	public string GetFormatTime()
	{
		int totalSeconds = ElapsedTime / 1000;
		int hours = totalSeconds / 3600;
		int minutes = (totalSeconds % 3600) / 60;
		int seconds = totalSeconds % 60;

		return $"{hours:00}:{minutes:00}:{seconds:00}";
	}

	public void Reset()
	{
		elapsedTime = 0;
		Stop();
	}

	public void Start()
	{
		if (!isRunning)
		{
			isRunning = true;
			timerThread = new Thread(TimerThreadMethod);
			timerThread.Start();
		}
	}

	public void Stop()
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
