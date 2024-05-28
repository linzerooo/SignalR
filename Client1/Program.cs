using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Client1
{
    internal class Program
    {
        static async Task Main(string[] args)
        {
            // Подключиться к SignalR хабу
            var connection = new HubConnectionBuilder()
                .WithUrl("http://localhost:5000/messages")
                .Build();
            await connection.StartAsync();

            // Отправить сообщение
            await connection.InvokeAsync("SendMessage", "Привет из Клиента 1");

            // Слушать получение сообщений
            connection.On<string>("ReceiveMessage", (message) =>
            {
                Console.WriteLine($"Получено сообщение: {message}");
            });
        }
    }
}
