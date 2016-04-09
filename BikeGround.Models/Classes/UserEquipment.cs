using MicroOrm.Pocos.SqlGenerator.Attributes;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System;

namespace BikeGround.Models
{
    public partial class UserEquipment
    {
        [Display(Name = "ID", ResourceType = typeof(Resources.UserEquipment))]
        [KeyProperty(Identity = true)]
        public long ID { get; set; }

        [Display(Name = "UserID", ResourceType = typeof(Resources.UserEquipment))]
        [KeyProperty]
        [JsonIgnore]
        public long UserID { get; set; }

        [Display(Name = "EquipmentID", ResourceType = typeof(Resources.UserEquipment))]
        [Required]
        public long EquipmentID { get; set; }

        [Display(Name = "Description", ResourceType = typeof(Resources.UserEquipment))]
        [Required]
        [StringLength(250)]
        [DataType(DataType.MultilineText)]
        public string Description { get; set; }

        [Display(Name = "RatingID", ResourceType = typeof(Resources.UserEquipment))]
        [Required]
        public int RatingID { get; set; }

        [Display(Name = "DateAdded", ResourceType = typeof(Resources.UserEquipment))]
        [Required]
        public DateTime DateAdded { get; set; }

        [Display(Name = "Active", ResourceType = typeof(Resources.UserEquipment))]
        [Required]
        public bool Active { get; set; }
    }
}