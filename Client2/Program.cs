using Microsoft.AspNetCore.SignalR.Client;
using Microsoft.Extensions.Logging;

var connection = new HubConnectionBuilder()
    .WithUrl("http://localhost:5000/messages")
    .ConfigureLogging(logging =>
    {
        logging.SetMinimumLevel(LogLevel.Information);
    })
    .Build();

connection.On<string, string>("ReceiveMessage", (user, message) =>
{
    Console.WriteLine($"{user}: {message}");
});

await connection.StartAsync();

Console.WriteLine("Connected to the server.");

Console.Write("Enter your name: ");
var userName = Console.ReadLine();

string message;
while ((message = Console.ReadLine()) != null)
{
    await connection.SendAsync("SendMessage", userName, message);
}