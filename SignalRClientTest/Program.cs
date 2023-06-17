using Microsoft.AspNetCore.SignalR.Client;
using System;
using System.Threading.Tasks;
using System.Windows;
using Microsoft.AspNetCore.SignalR.Client;

namespace SignalRClientTest;
class Program
{
	static async Task Main(string[] args)
	{
		HubConnection connection = new HubConnectionBuilder()
			.WithUrl("https://localhost:7234/timerhub")
			.Build();

		connection.On<string>("ReceiveData", data =>
		{
			Console.WriteLine("Received data: " + data);
		});

		try
		{
			await connection.StartAsync();
			Console.WriteLine("Connection established. Start receiving data.");
			Console.WriteLine("state: " + connection.State);

			await connection.InvokeAsync("StartSendingData");

			// Ждем 10 секунд, чтобы получить данные
			await Task.Delay(TimeSpan.FromSeconds(10));

			// Останавливаем получение данных
			await connection.InvokeAsync("StopSendingData");

			Console.WriteLine("Data receiving stopped.");
		}
		catch (Exception ex)
		{
			Console.WriteLine("Connection error: " + ex.Message);
		}
		finally
		{
			await connection.DisposeAsync();
		}
	}
}
