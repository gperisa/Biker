using MicroOrm.Pocos.SqlGenerator.Attributes;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System;

namespace BikeGround.Models
{
    public partial class FavoriteRoute
    {
        [Display(Name = "ID", ResourceType = typeof(Resources.FavoriteRoute))]
        [KeyProperty(Identity = true)]
        public long ID { get; set; }

        [Display(Name = "RouteID", ResourceType = typeof(Resources.FavoriteRoute))]
        [Required]
        public long RouteID { get; set; }

        [Display(Name = "UserID", ResourceType = typeof(Resources.FavoriteRoute))]
        [KeyProperty]
        [JsonIgnore]
        public long UserID { get; set; }

        [Display(Name = "DateAdded", ResourceType = typeof(Resources.FavoriteRoute))]
        [Required]
        public DateTime DateAdded { get; set; }

        [Display(Name = "Active", ResourceType = typeof(Resources.FavoriteRoute))]
        [Required]
        public bool Active { get; set; }
    }
}