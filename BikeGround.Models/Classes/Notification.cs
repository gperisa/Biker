using MicroOrm.Pocos.SqlGenerator.Attributes;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System;

namespace BikeGround.Models
{
    public partial class Notification
    {
        [Display(Name = "ID", ResourceType = typeof(Resources.Notification))]
        public long ID { get; set; }

        [Display(Name = "ToUserID", ResourceType = typeof(Resources.Notification))]
        [Required]
        public long ToUserID { get; set; }

        [Display(Name = "Message", ResourceType = typeof(Resources.Notification))]
        [Required]
        [StringLength(80)]
        [DataType(DataType.MultilineText)]
        public string Message { get; set; }

        [Display(Name = "Navigation", ResourceType = typeof(Resources.Notification))]
        [Required]
        [StringLength(80)]
        [DataType(DataType.MultilineText)]
        public string Navigation { get; set; }

        [Display(Name = "Unread", ResourceType = typeof(Resources.Notification))]
        [Required]
        public bool Unread { get; set; }

        [Display(Name = "NotificationTypeID", ResourceType = typeof(Resources.Notification))]
        [Required]
        public int NotificationTypeID { get; set; }

        [Display(Name = "NotificationVersionID", ResourceType = typeof(Resources.Notification))]
        [Required]
        [StringLength(5)]
        public string NotificationVersionID { get; set; }

        [Display(Name = "DateAdded", ResourceType = typeof(Resources.Notification))]
        [Required]
        public DateTime DateAdded { get; set; }

        [Display(Name = "Active", ResourceType = typeof(Resources.Notification))]
        [Required]
        public bool Active { get; set; }
    }
}