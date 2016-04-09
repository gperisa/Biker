using MicroOrm.Pocos.SqlGenerator.Attributes;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System;

namespace BikeGround.Models
{
    public partial class Friend
    {
        [Display(Name = "UserID", ResourceType = typeof(Resources.Friend))]
        [KeyProperty]
        [JsonIgnore]
        public long UserID { get; set; }

        [Display(Name = "FriendID", ResourceType = typeof(Resources.Friend))]
        [Required]
        public long FriendID { get; set; }

        [Display(Name = "ProfileActivityID", ResourceType = typeof(Resources.Friend))]
        [Required]
        public int ProfileActivityID { get; set; }

        [Display(Name = "Active", ResourceType = typeof(Resources.Friend))]
        [Required]
        public bool Active { get; set; }
    }
}