using Microsoft.AspNetCore.SignalR;
using System.Threading.Tasks;

namespace CoreDBPackage.Hubs {
    public class ChatHub : Hub {
        //public Task JoinGroup(string group) {
        //    return Groups.AddToGroupAsync(Context.ConnectionId, group);
        //}

        //public async Task SendMessage(string sender, string message) {
        //    await Clients.All.SendAsync("SendMessage", sender, message);
        //}

        //public Task SendMessageToGroup(string groupname, string sender, string message) {
        //    return Clients.Group(groupname).SendAsync("SendMessage", sender, message);
        //}
    }
}
