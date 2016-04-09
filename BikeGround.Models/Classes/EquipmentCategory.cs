using MicroOrm.Pocos.SqlGenerator.Attributes;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System;

namespace BikeGround.Models
{
    public partial class EquipmentCategory
    {
        [Display(Name = "ID", ResourceType = typeof(Resources.EquipmentCategory))]
        [KeyProperty(Identity = true)]
        public long ID { get; set; }

        [Display(Name = "Name", ResourceType = typeof(Resources.EquipmentCategory))]
        [Required]
        [StringLength(25)]
        public string Name { get; set; }

        [Display(Name = "Description", ResourceType = typeof(Resources.EquipmentCategory))]
        [Required]
        [StringLength(250)]
        [DataType(DataType.MultilineText)]
        public string Description { get; set; }

        [Display(Name = "Active", ResourceType = typeof(Resources.EquipmentCategory))]
        [Required]
        public bool Active { get; set; }
    }
}