// Подключиться к SignalR хабу
using Microsoft.AspNetCore.SignalR.Client;

var connection = new HubConnectionBuilder()
    .WithUrl("http://localhost:5000/messages")
    .Build();
await connection.StartAsync();

var clientMessage = Console.ReadLine();

// Отправить сообщение

// Слушать получение сообщений
while (true)
{
    await connection.InvokeAsync("SendMessage", $"{clientMessage}");

    connection.On<string>("ReceiveMessage", (message) =>
    {
        Console.WriteLine($"Получено сообщение: {message}");
    });
}
