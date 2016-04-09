using MicroOrm.Pocos.SqlGenerator.Attributes;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System;

namespace BikeGround.Models
{
    public partial class TripBuget
    {
        [Display(Name = "ID", ResourceType = typeof(Resources.TripBuget))]
        [KeyProperty(Identity = true)]
        public long ID { get; set; }

        [Display(Name = "UserID", ResourceType = typeof(Resources.TripBuget))]
        [KeyProperty]
        [JsonIgnore]
        public long UserID { get; set; }

        [Display(Name = "TripID", ResourceType = typeof(Resources.TripBuget))]
        [Required]
        public long TripID { get; set; }

        [Display(Name = "PostID", ResourceType = typeof(Resources.TripBuget))]
        public long PostID { get; set; }

        [Display(Name = "Description", ResourceType = typeof(Resources.TripBuget))]
        [Required]
        [StringLength(250)]
        [DataType(DataType.MultilineText)]
        public string Description { get; set; }

        [Display(Name = "Price", ResourceType = typeof(Resources.TripBuget))]
        [Required]
        public decimal Price { get; set; }

        [Display(Name = "DateAdded", ResourceType = typeof(Resources.TripBuget))]
        [Required]
        public DateTime DateAdded { get; set; }

        [Display(Name = "Active", ResourceType = typeof(Resources.TripBuget))]
        [Required]
        public bool Active { get; set; }
    }
}