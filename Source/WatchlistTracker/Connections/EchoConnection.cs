using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using SignalR;

namespace WatchlistTracker.Connections
{
    public class EchoConnection : PersistentConnection
    {
        protected override Task OnReceivedAsync(IRequest request, string connectionId, string data)
        {
            // Broadcast data to all clients
            return Connection.Broadcast(new EchoResponse{ Text = data});
        }
    }


    public class EchoResponse
    {
        public string Text { get; set; }
    }
}