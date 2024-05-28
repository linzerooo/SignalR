using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
using SignalR.Model;

namespace SignalR
{
    public class MessageHub : Hub
    {
        public readonly ApplicationDbContext _context;

        public MessageHub(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task SendMessage(string user, string message)
        {
            var chatMessage = new Message
            {
                UserName = user,
                Content = message
            };

            _context.Add(chatMessage);

            await _context.SaveChangesAsync();

            await Clients.All.SendAsync("ReceiveMessage", user, message);
        }

        public override async Task OnConnectedAsync()
        {
            var messages = _context.Messages.OrderBy(m => m.Timestamp).ToList();

            foreach (var message in messages)
            {
                await Clients.Caller.SendAsync("ReceiveMessage", message.UserName, message.Content);
            }
            await base.OnConnectedAsync();
        }
        public bool GetAllMessages()
        {
            return _context.Messages.ToListAsync() == null;
        }
    }
}
