using MicroOrm.Pocos.SqlGenerator.Attributes;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System;

namespace BikeGround.Models
{
    public partial class Rating
    {
        [Display(Name = "ID", ResourceType = typeof(Resources.Rating))]
        [KeyProperty(Identity = true)]
        public int ID { get; set; }

        [Display(Name = "Value", ResourceType = typeof(Resources.Rating))]
        [Required]
        public int Value { get; set; }

        [Display(Name = "Description", ResourceType = typeof(Resources.Rating))]
        [Required]
        [StringLength(250)]
        [DataType(DataType.MultilineText)]
        public string Description { get; set; }

        [Display(Name = "Active", ResourceType = typeof(Resources.Rating))]
        [Required]
        public bool Active { get; set; }
    }
}