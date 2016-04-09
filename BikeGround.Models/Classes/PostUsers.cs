using MicroOrm.Pocos.SqlGenerator.Attributes;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System;

namespace BikeGround.Models
{
    public partial class PostUsers
    {
        [Display(Name = "PostID", ResourceType = typeof(Resources.PostUsers))]
        public long PostID { get; set; }

        [Display(Name = "UserID", ResourceType = typeof(Resources.PostUsers))]
        [KeyProperty]
        [JsonIgnore]
        public long UserID { get; set; }

        [Display(Name = "Comment", ResourceType = typeof(Resources.PostUsers))]
        [Required]
        [StringLength(250)]
        [DataType(DataType.MultilineText)]
        public string Comment { get; set; }

        [Display(Name = "Active", ResourceType = typeof(Resources.PostUsers))]
        [Required]
        public bool Active { get; set; }
    }
}