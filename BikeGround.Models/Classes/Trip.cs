using MicroOrm.Pocos.SqlGenerator.Attributes;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System;

namespace BikeGround.Models
{
    public partial class Trip
    {
        [Display(Name = "ID", ResourceType = typeof(Resources.Trip))]
        [KeyProperty(Identity = true)]
        public long ID { get; set; }

        [Display(Name = "UserID", ResourceType = typeof(Resources.Trip))]
        [KeyProperty]
        [JsonIgnore]
        public long UserID { get; set; }

        [Display(Name = "BlogID", ResourceType = typeof(Resources.Trip))]
        [Required]
        public long BlogID { get; set; }

        [Display(Name = "Title", ResourceType = typeof(Resources.Trip))]
        [Required]
        [StringLength(30)]
        public string Title { get; set; }

        [Display(Name = "ShortDescription", ResourceType = typeof(Resources.Trip))]
        [Required]
        [StringLength(500)]
        [DataType(DataType.MultilineText)]
        public string ShortDescription { get; set; }

        [Display(Name = "Distance", ResourceType = typeof(Resources.Trip))]
        [Required]
        public decimal Distance { get; set; }

        [Display(Name = "StartDate", ResourceType = typeof(Resources.Trip))]
        [Required]
        public DateTime StartDate { get; set; }

        [Display(Name = "EndDate", ResourceType = typeof(Resources.Trip))]
        [Required]
        public DateTime EndDate { get; set; }

        [Display(Name = "DateAdded", ResourceType = typeof(Resources.Trip))]
        [Required]
        public DateTime DateAdded { get; set; }

        [Display(Name = "Active", ResourceType = typeof(Resources.Trip))]
        [Required]
        public bool Active { get; set; }
    }
}