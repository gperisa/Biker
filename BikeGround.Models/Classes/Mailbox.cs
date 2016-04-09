using MicroOrm.Pocos.SqlGenerator.Attributes;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System;

namespace BikeGround.Models
{
    public partial class Mailbox
    {
        [Display(Name = "ID", ResourceType = typeof(Resources.Mailbox))]
        [KeyProperty(Identity = true)]
        public long ID { get; set; }

        [Display(Name = "FromID", ResourceType = typeof(Resources.Mailbox))]
        [Required]
        public long FromID { get; set; }

        [Display(Name = "ToID", ResourceType = typeof(Resources.Mailbox))]
        [Required]
        public long ToID { get; set; }

        [Display(Name = "MailTypeID", ResourceType = typeof(Resources.Mailbox))]
        [Required]
        public int MailTypeID { get; set; }

        [Display(Name = "Subject", ResourceType = typeof(Resources.Mailbox))]
        [Required]
        [StringLength(250)]
        [DataType(DataType.MultilineText)]
        public string Subject { get; set; }

        [Display(Name = "Body", ResourceType = typeof(Resources.Mailbox))]
        [Required]
        [StringLength(2147483647)]
        [DataType(DataType.MultilineText)]
        public string Body { get; set; }

        [Display(Name = "DateAdded", ResourceType = typeof(Resources.Mailbox))]
        [Required]
        public DateTime DateAdded { get; set; }

        [Display(Name = "IsRead", ResourceType = typeof(Resources.Mailbox))]
        [Required]
        public bool IsRead { get; set; }

        [Display(Name = "Active", ResourceType = typeof(Resources.Mailbox))]
        [Required]
        public bool Active { get; set; }
    }
}