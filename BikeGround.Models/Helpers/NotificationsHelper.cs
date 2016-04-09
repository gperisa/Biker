using System;

namespace BikeGround.Models.Helpers
{
    /// <summary>
    /// Helper koji služi prilikom slanja notifikacija svim korisnicima koji su pretplaćeni na SignalR grupu korisnika (e-mail korisnika)
    /// </summary>
    public class NotificationsHelper
    {
        /// <summary>
        /// Enum koji označava tipove poruka koje korisnik može postaviti
        /// </summary>
        public enum NotificationType { Request, Notification_New, Notification_Update, Notification_Delete, Message }

        /// <summary>
        /// Notification type
        /// </summary>
        public NotificationType notificationType { get; set; }

        /// <summary>
        /// Notifications message
        /// </summary>
        public string notificationMessage { get; set; }

        /// <summary>
        /// Notifications date
        /// </summary>
        public DateTime notificationsDate { get; set; }
    }
}
