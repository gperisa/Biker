using MicroOrm.Pocos.SqlGenerator.Attributes;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System;

namespace BikeGround.Models
{
    public partial class Route
    {
        [Display(Name = "ID", ResourceType = typeof(Resources.Route))]
        [KeyProperty(Identity = true)]
        public long ID { get; set; }

        [Display(Name = "UserID", ResourceType = typeof(Resources.Route))]
        [KeyProperty]
        [JsonIgnore]
        public long UserID { get; set; }

        [Display(Name = "Name", ResourceType = typeof(Resources.Route))]
        [Required]
        [StringLength(250)]
        [DataType(DataType.MultilineText)]
        public string Name { get; set; }

        [Display(Name = "Description", ResourceType = typeof(Resources.Route))]
        [Required]
        [StringLength(2147483647)]
        [DataType(DataType.MultilineText)]
        public string Description { get; set; }

        [Display(Name = "Lenght", ResourceType = typeof(Resources.Route))]
        [Required]
        public decimal Lenght { get; set; }

        [Display(Name = "ElevationUp", ResourceType = typeof(Resources.Route))]
        [Required]
        public decimal ElevationUp { get; set; }

        [Display(Name = "ElevationDown", ResourceType = typeof(Resources.Route))]
        [Required]
        public decimal ElevationDown { get; set; }

        [Display(Name = "GeoJson", ResourceType = typeof(Resources.Route))]
        [Required]
        [StringLength(2147483647)]
        [DataType(DataType.MultilineText)]
        public string GeoJson { get; set; }

        [Display(Name = "GpxURL", ResourceType = typeof(Resources.Route))]
        [Required]
        [StringLength(2147483647)]
        [DataType(DataType.MultilineText)]
        public string GpxURL { get; set; }

        [Display(Name = "DateAdded", ResourceType = typeof(Resources.Route))]
        [Required]
        public DateTime DateAdded { get; set; }

        [Display(Name = "Official", ResourceType = typeof(Resources.Route))]
        [Required]
        public bool Official { get; set; }

        [Display(Name = "Active", ResourceType = typeof(Resources.Route))]
        [Required]
        public bool Active { get; set; }
    }
}