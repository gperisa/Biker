using MicroOrm.Pocos.SqlGenerator.Attributes;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System;

namespace BikeGround.Models
{
    public partial class Wall
    {
        [Display(Name = "ID", ResourceType = typeof(Resources.Wall))]
        [KeyProperty(Identity = true)]
        public long ID { get; set; }

        [Display(Name = "UserID", ResourceType = typeof(Resources.Wall))]
        [JsonIgnore]
        public long UserID { get; set; }
    }
}