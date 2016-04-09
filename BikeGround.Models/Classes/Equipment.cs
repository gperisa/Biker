using MicroOrm.Pocos.SqlGenerator.Attributes;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System;

namespace BikeGround.Models
{
    public partial class Equipment
    {
        [Display(Name = "ID", ResourceType = typeof(Resources.Equipment))]
        [KeyProperty(Identity = true)]
         public long ID { get; set; }

        [Display(Name = "CategoryID", ResourceType = typeof(Resources.Equipment))]
        [Required]
        public long CategoryID { get; set; }

        [Display(Name = "Name", ResourceType = typeof(Resources.Equipment))]
        [Required]
        [StringLength(250)]
        [DataType(DataType.MultilineText)]
        public string Name { get; set; }

        [Display(Name = "Description", ResourceType = typeof(Resources.Equipment))]
        [Required]
        [StringLength(2147483647)]
        [DataType(DataType.MultilineText)]
        public string Description { get; set; }

        [Display(Name = "DateAdded", ResourceType = typeof(Resources.Equipment))]
        [Required]
        public DateTime DateAdded { get; set; }

        [Display(Name = "ManufacturerID", ResourceType = typeof(Resources.Equipment))]
        public long ManufacturerID { get; set; }

        [Display(Name = "ImageUrl", ResourceType = typeof(Resources.Equipment))]
        [StringLength(2147483647)]
        [DataType(DataType.MultilineText)]
        public string ImageUrl { get; set; }

        [Display(Name = "Active", ResourceType = typeof(Resources.Equipment))]
        [Required]
        public bool Active { get; set; }
    }
}