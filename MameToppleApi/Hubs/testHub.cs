using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR;

namespace MameToppleApi.Hubs
{
    public class testHub : Hub
    {
        public async Task SendMessage(string user, string message)
        {
            //多工
            //if (true)
            //{
            //    await Clients.All.SendAsync("ReceiveMessage", user, message);
            //}
            await Clients.All.SendAsync("ReceiveMessage", user, message);
        }
    }
}
