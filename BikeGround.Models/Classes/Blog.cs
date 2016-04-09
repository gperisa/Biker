using MicroOrm.Pocos.SqlGenerator.Attributes;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System;

namespace BikeGround.Models
{
    public partial class Blog
    {
        [Display(Name = "ID", ResourceType = typeof(Resources.Blog))]
        [KeyProperty(Identity = true)]
        public long ID { get; set; }

        [Display(Name = "UserID", ResourceType = typeof(Resources.Blog))]
        [KeyProperty]
        [JsonIgnore]
        public long UserID { get; set; }

        [Display(Name = "Name", ResourceType = typeof(Resources.Blog))]
        [Required]
        [StringLength(30)]
        public string Name { get; set; }

        [Display(Name = "Caption", ResourceType = typeof(Resources.Blog))]
        [Required]
        [StringLength(80)]
        [DataType(DataType.MultilineText)]
        public string Caption { get; set; }

        [Display(Name = "BlogName", ResourceType = typeof(Resources.Blog))]
        [Required]
        [StringLength(30)]
        public string BlogName { get; set; }

        [Display(Name = "Active", ResourceType = typeof(Resources.Blog))]
        [Required]
        public bool Active { get; set; }
    }
}