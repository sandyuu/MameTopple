using MameToppleApi.Models;
using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MameToppleApi.Hubs
{
    public class MameHub : Hub
    {
        public static Dictionary<string, User> users = new Dictionary<string, User>();

        //for connection test
        public async Task test (string test)
        {
            await Clients.All.SendAsync("test", test);
        }

    }
}
