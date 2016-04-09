using MicroOrm.Pocos.SqlGenerator.Attributes;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System;

namespace BikeGround.Models
{
    public partial class Country
    {
        [Display(Name = "ID", ResourceType = typeof(Resources.Country))]
        [KeyProperty(Identity = true)]
        public long ID { get; set; }

        [Display(Name = "Name", ResourceType = typeof(Resources.Country))]
        [Required]
        [StringLength(100)]
        [DataType(DataType.MultilineText)]
        public string Name { get; set; }

        [Display(Name = "Active", ResourceType = typeof(Resources.Country))]
        [Required]
        public bool Active { get; set; }
    }
}