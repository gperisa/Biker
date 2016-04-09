using MicroOrm.Pocos.SqlGenerator.Attributes;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System;

namespace BikeGround.Models
{
    public partial class ProfileActivity
    {
        [Display(Name = "ID", ResourceType = typeof(Resources.ProfileActivity))]
        [KeyProperty(Identity = true)]
        public int ID { get; set; }

        [Display(Name = "ActivityType", ResourceType = typeof(Resources.ProfileActivity))]
        [Required]
        [StringLength(15)]
        public string ActivityType { get; set; }

        [Display(Name = "Active", ResourceType = typeof(Resources.ProfileActivity))]
        [Required]
        public bool Active { get; set; }
    }
}