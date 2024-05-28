using Microsoft.AspNetCore.SignalR;

namespace SignalR
{
    public class MessageHub : Hub
    {
        public async Task SendMessage(string message)
        {
            // Сохранение сообщения (например, в базу данных)

            // Отправка сообщения всем подключенным клиентам
            await Clients.All.SendAsync("ReceiveMessage", message);
        }
    }
}
