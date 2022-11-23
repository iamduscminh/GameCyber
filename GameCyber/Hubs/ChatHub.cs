using GameCyber.Logics;
using Microsoft.AspNetCore.SignalR;

namespace GameCyber.Hubs
{
    public class ChatHub : Hub
    {
        public async Task SendMessage(string userReceiveId, string userSendId, string message)
        {
            if (!int.TryParse(userReceiveId, out int IdReceive)) return;
            if (!int.TryParse(userSendId, out int IdSend)) return;
            var commonLogic = new CommonLogic();
            var user = commonLogic.GetUser(IdReceive);

            if (user.Connection == null) return;
            await Clients.Caller.SendAsync("SendMessage", message);

            await Clients.Client(user.Connection).SendAsync("ReceiveMessage", message, userSendId);

            commonLogic.SendMessage(IdSend, IdReceive, message);
        }
        public void UpdateConnectionId(string userId)
        {
            if (!int.TryParse(userId, out int Id)) return;
            new CommonLogic().SetUserConnection(Id, Context.ConnectionId);
        }

    }
}
