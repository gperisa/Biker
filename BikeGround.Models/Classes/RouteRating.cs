using MicroOrm.Pocos.SqlGenerator.Attributes;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System;

namespace BikeGround.Models
{
    public partial class RouteRating
    {
        [Display(Name = "ID", ResourceType = typeof(Resources.RouteRating))]
        [KeyProperty(Identity = true)]
        public long ID { get; set; }

        [Display(Name = "UserID", ResourceType = typeof(Resources.RouteRating))]
        [KeyProperty]
        [JsonIgnore]
        public long UserID { get; set; }

        [Display(Name = "RouteID", ResourceType = typeof(Resources.RouteRating))]
        [Required]
        public long RouteID { get; set; }

        [Display(Name = "RatingID", ResourceType = typeof(Resources.RouteRating))]
        [Required]
        public int RatingID { get; set; }

        [Display(Name = "Comment", ResourceType = typeof(Resources.RouteRating))]
        [Required]
        [StringLength(250)]
        [DataType(DataType.MultilineText)]
        public string Comment { get; set; }

        [Display(Name = "DateAdded", ResourceType = typeof(Resources.RouteRating))]
        [Required]
        public DateTime DateAdded { get; set; }

        [Display(Name = "Active", ResourceType = typeof(Resources.RouteRating))]
        [Required]
        public bool Active { get; set; }
    }
}