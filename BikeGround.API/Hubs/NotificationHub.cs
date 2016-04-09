using BikeGround.Interfaces;
using Microsoft.AspNet.SignalR;
using System;
using System.Diagnostics;

namespace BikeGround.API.Hubs
{
    /// <summary>
    /// SignalR hub za chat. Mora implementirati IChatHub zato što se .WEB strani automatski generira proxy za ChatHub klasu i zato 
    /// klasa mora postojati na oba solutiona i mora biti jednaka, a samo WEB.API ima stvarnu implementaciju logike
    /// </summary>
    public class NotificationHub : Hub, IChatHub
    {
        /// <summary>
        /// Subscribe to signalR group
        /// </summary>
        /// <param name="group">Group name</param>
        public void Subscribe(string group)
        {
            if (String.IsNullOrEmpty(group))
            {
                return;
            }

            Groups.Add(Context.ConnectionId, group);

            Debug.WriteLine(String.Format("Subscribe: {0} | {1}", group, Context.ConnectionId));
        }

        /// <summary>
        /// Unsubscribe to signalR group
        /// </summary>
        /// <param name="group">Group name</param>
        public void Unsubscribe(string group)
        {
            if (String.IsNullOrEmpty(group))
            {
                return;
            }
            
            Groups.Remove(Context.ConnectionId, group);

            Debug.WriteLine(String.Format("Unsubscribe: {0} | {1}", group, Context.ConnectionId));
        }

        public void Send(string grupa, string name, string message)
        {
            grupa = grupa.ToLower();

            if (grupa == "all")
            {
                Clients.Others.pushMessage(name, message);
            }
            else
            {
                Clients.OthersInGroup(grupa).pushMessage(name, message);
            }

            Debug.WriteLine(String.Format("Brodcast: {0} - {1} | {3} {2}", name, message, Context.ConnectionId, grupa));
        }
    }
}