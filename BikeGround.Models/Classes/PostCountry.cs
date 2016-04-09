using MicroOrm.Pocos.SqlGenerator.Attributes;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System;

namespace BikeGround.Models
{
    public partial class PostCountry
    {
        [Display(Name = "ID", ResourceType = typeof(Resources.PostCountry))]
        [KeyProperty(Identity = true)]
        public long ID { get; set; }

        [Display(Name = "PostID", ResourceType = typeof(Resources.PostCountry))]
        [Required]
        public long PostID { get; set; }

        [Display(Name = "CountryID", ResourceType = typeof(Resources.PostCountry))]
        [Required]
        public long CountryID { get; set; }

        [Display(Name = "Active", ResourceType = typeof(Resources.PostCountry))]
        [Required]
        public bool Active { get; set; }
    }
}