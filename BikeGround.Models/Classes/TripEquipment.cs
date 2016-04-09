using MicroOrm.Pocos.SqlGenerator.Attributes;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System;

namespace BikeGround.Models
{
    public partial class TripEquipment
    {
        [Display(Name = "ID", ResourceType = typeof(Resources.TripEquipment))]
        [KeyProperty(Identity = true)]
        public long ID { get; set; }

        [Display(Name = "UserID", ResourceType = typeof(Resources.TripEquipment))]
        [KeyProperty]
        [JsonIgnore]
        public long UserID { get; set; }

        [Display(Name = "TripID", ResourceType = typeof(Resources.TripEquipment))]
        [Required]
        public long TripID { get; set; }

        [Display(Name = "EquipmentID", ResourceType = typeof(Resources.TripEquipment))]
        [Required]
        public long EquipmentID { get; set; }

        [Display(Name = "Description", ResourceType = typeof(Resources.TripEquipment))]
        [Required]
        [StringLength(250)]
        [DataType(DataType.MultilineText)]
        public string Description { get; set; }

        [Display(Name = "DateAdded", ResourceType = typeof(Resources.TripEquipment))]
        [Required]
        public DateTime DateAdded { get; set; }

        [Display(Name = "Active", ResourceType = typeof(Resources.TripEquipment))]
        [Required]
        public bool Active { get; set; }
    }
}