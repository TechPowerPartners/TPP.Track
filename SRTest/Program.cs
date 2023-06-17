using Microsoft.AspNetCore.SignalR.Client;
namespace SRTest;

internal class Program
{
	static HubConnection _connection;

	static void Main(string[] args)
	{
		Console.WriteLine("Hello, World!");
		_connection = new HubConnectionBuilder()
			.WithUrl("/timerHub")
			.Build();

		_connection.On<string>("GetTime", (s) => Console.WriteLine(s));
	}
}
