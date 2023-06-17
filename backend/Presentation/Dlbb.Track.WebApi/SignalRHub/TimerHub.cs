using Microsoft.AspNetCore.SignalR;
using SignalRSwaggerGen.Attributes;

namespace Dlbb.Track.WebApi.SignalRHub;


public class TimerHub : Hub
{
	private readonly IHubContext<TimerHub> _hubContext;
	private Timer _timerForTask;
	private CustomTimer _userTimer;

	public TimerHub(IHubContext<TimerHub> hubContext)
	{
		_hubContext = hubContext;
	}

	public void StartSendingData()
	{
		_timerForTask = new Timer(SendData, null, TimeSpan.Zero, TimeSpan.FromSeconds(1));
		_userTimer = new CustomTimer(1000);
		_userTimer.Start();
}

	public void StopSendingData()
	{
		_timerForTask?.Dispose();
		_userTimer?.Reset();
	}

	private void SendData(object state)
	{
		var data = _userTimer.GetFormatTime();
		// Отправка информации всем подключенным клиентам
		_hubContext.Clients.All.SendAsync("ReceiveData", data);
	}
}

public class CustomTimer
{
	private int interval;
	private bool isRunning;
	private Thread timerThread;
	private int elapsedTime;

	public CustomTimer(int interval)
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
