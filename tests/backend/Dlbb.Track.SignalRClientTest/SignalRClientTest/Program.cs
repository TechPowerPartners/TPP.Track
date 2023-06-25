using Microsoft.AspNetCore.SignalR.Client;


int timeForRecivering = 100;

HubConnection connection = new HubConnectionBuilder()
			.WithUrl("https://localhost:7234/timerhub")
			.Build();

connection.On<string>("ReceiveData",data =>
{
	Console.WriteLine("Received data: " + data);
});

try
{
	await connection.StartAsync();
	Console.WriteLine("Connection established. Start receiving data.");
	Console.WriteLine("state: " + connection.State);

	await connection.InvokeAsync("StartTimer");
	await connection.InvokeAsync("SendData");
	await connection.InvokeAsync("SendData");

	int i = 0;
	while(i < 300)
	{
		await connection.InvokeAsync("SendData");
		i++;
		await Task.Delay(1);

	}

	await connection.InvokeAsync("StopTimer");
	await connection.InvokeAsync("ResetTimer");
	Console.WriteLine("stopTimer");
	await connection.InvokeAsync("StartTimer");
	Console.WriteLine("StartTimer");

	i = 0;
	while(i < 300)
	{
		await connection.InvokeAsync("SendData");
		i++;
		await Task.Delay(1);

	}
}
catch(Exception ex)
{
	Console.WriteLine("Connection error: " + ex.Message);
}
finally
{
	await connection.DisposeAsync();
}

Console.ReadLine();
