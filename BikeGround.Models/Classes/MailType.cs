using MicroOrm.Pocos.SqlGenerator.Attributes;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System;

namespace BikeGround.Models
{
    public partial class MailType
    {
        [Display(Name = "ID", ResourceType = typeof(Resources.MailType))]
        [KeyProperty]
        public int ID { get; set; }

        [Display(Name = "Description", ResourceType = typeof(Resources.MailType))]
        [Required]
        [StringLength(50)]
        [DataType(DataType.MultilineText)]
        public string Description { get; set; }

        [Display(Name = "Active", ResourceType = typeof(Resources.MailType))]
        [Required]
        public bool Active { get; set; }
    }
}