using MicroOrm.Pocos.SqlGenerator.Attributes;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System;

namespace BikeGround.Models
{
    public partial class Messages
    {
        [Display(Name = "ID", ResourceType = typeof(Resources.Messages))]
        [KeyProperty]
        public long ID { get; set; }

        [Display(Name = "From", ResourceType = typeof(Resources.Messages))]
        [Required]
        public long From { get; set; }

        [Display(Name = "To", ResourceType = typeof(Resources.Messages))]
        [Required]
        public long To { get; set; }

        [Display(Name = "Message", ResourceType = typeof(Resources.Messages))]
        [Required]
        [StringLength(500)]
        [DataType(DataType.MultilineText)]
        public string Message { get; set; }

        [Display(Name = "MessagesVersionID", ResourceType = typeof(Resources.Messages))]
        [Required]
        [StringLength(5)]
        public string MessagesVersionID { get; set; }

        [Display(Name = "DateAdded", ResourceType = typeof(Resources.Messages))]
        [Required]
        public DateTime DateAdded { get; set; }

        [Display(Name = "Active", ResourceType = typeof(Resources.Messages))]
        [Required]
        public bool Active { get; set; }
    }
}