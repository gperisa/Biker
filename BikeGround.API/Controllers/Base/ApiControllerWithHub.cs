using BikeGround.Models.Helpers;
using Microsoft.AspNet.SignalR;
using Microsoft.AspNet.SignalR.Hubs;
using System;
using System.Diagnostics;
using System.Threading;
using System.Web.Http;

namespace BikeGround.API.Controllers.Base
{
    public abstract class ApiControllerWithHub<THub> : ApiController where THub : IHub
    {
        Lazy<IHubContext> hub = new Lazy<IHubContext>(
            () => GlobalHost.ConnectionManager.GetHubContext<THub>()
        );

        protected IHubContext Hub
        {
            get { return hub.Value; }
        }

        public virtual void Broadcast_Info(string message)
        {
            try
            {
                this.Hub.Clients.Group(Thread.CurrentPrincipal.Identity.Name)
                               .pushMessage2("Petar", new NotificationsHelper()
                               {
                                   notificationType = NotificationsHelper.NotificationType.Message,
                                   notificationMessage = String.Format("Petar je ažurirao zapis: {0}", message),
                                   notificationsDate = DateTime.UtcNow
                               });
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
            }
        }

        public virtual void Broadcast_NotificationNew(string message)
        {
            try
            {
                this.Hub.Clients.Group(Thread.CurrentPrincipal.Identity.Name)
                               .pushMessage2("Petar", new NotificationsHelper()
                               {
                                   notificationType = NotificationsHelper.NotificationType.Notification_New,
                                   notificationMessage = String.Format("Petar je kreirao zapis: {0}", message),
                                   notificationsDate = DateTime.UtcNow
                               });
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
            }
        }

        public virtual void Broadcast_NotificationUpdate(string message)
        {
            try
            {
                this.Hub.Clients.Group(Thread.CurrentPrincipal.Identity.Name)
                               .pushMessage2("Petar", new NotificationsHelper()
                               {
                                   notificationType = NotificationsHelper.NotificationType.Notification_Update,
                                   notificationMessage = String.Format("Petar je ažurirao zapis: {0}", message),
                                   notificationsDate = DateTime.UtcNow
                               });
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
            }
        }

        public virtual void Broadcast_NotificationDelete(string message)
        {
            try
            {
                this.Hub.Clients.Group(Thread.CurrentPrincipal.Identity.Name)
                               .pushMessage2("Petar", new NotificationsHelper()
                               {
                                   notificationType = NotificationsHelper.NotificationType.Notification_Update,
                                   notificationMessage = String.Format("Petar je obrisao zapis: {0}", message),
                                   notificationsDate = DateTime.UtcNow
                               });
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
            }
        }

        public virtual void Broadcast_Request(string message)
        {
            try
            {
                this.Hub.Clients.Group(Thread.CurrentPrincipal.Identity.Name)
                               .pushMessage2("Petar", new NotificationsHelper()
                               {
                                   notificationType = NotificationsHelper.NotificationType.Request,
                                   notificationMessage = String.Format("Imate novi zahjtev: {0}", message),
                                   notificationsDate = DateTime.UtcNow
                               });
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
            }
        }
    }
}