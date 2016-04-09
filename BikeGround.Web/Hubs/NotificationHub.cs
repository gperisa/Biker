using BikeGround.Interfaces;
using BikeGround.Models;
using Microsoft.AspNet.SignalR;
using System;

namespace BikeGround.Web.Hubs
{
    /// <summary>
    /// Chat hub, ova klasa isključivo za implementaciju hub-a na klijentskoj strani. Tj., kreira se .js signalR proxy koji
    /// u sebi sadrži sve metode navedene u hubu s odgovarajućim potpisima metode. Iz toga razloga koristi se IChatHub interface koji osigurava
    /// da je .js proxy hub jednak onome hub-u koji se nalazi na web.api sloju i služi kao pravi hub s logičkom implementacijom koda
    /// </summary>
    public class NotificationHub : Hub, IChatHub
    {
        public void Subscribe(string group)
        {
            throw new NotImplementedException();
        }

        public void Unsubscribe(string group)
        {
            throw new NotImplementedException();
        }

        public void Send(string grupa, string name, string message)
        {
            throw new NotImplementedException();
        }
    }
}