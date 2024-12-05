using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR;
namespace MatrixWeb.Hubs
{
    public class ChatHub : Hub
    {
        public async Task SendMessage(string room, string user, string RutaArchivoFoto, string NombreCompleto, string message)
        {
            await Clients.Group(room).SendAsync("ReceiveMessage", user, RutaArchivoFoto, NombreCompleto, message);            
        }
        public async Task AddToGroup(string room)
        {
            await Groups.AddToGroupAsync(Context.ConnectionId, room);
            await Clients.Group(room).SendAsync("ShowWho",$"Ingreso:{ Context.ConnectionId}");
        }

    }
}
