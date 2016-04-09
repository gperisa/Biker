using MicroOrm.Pocos.SqlGenerator.Attributes;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System;

namespace BikeGround.Models
{
    public partial class Post
    {
        [Display(Name = "ID", ResourceType = typeof(Resources.Post))]
        [KeyProperty(Identity = true)]
        public long ID { get; set; }

        [Display(Name = "UserID", ResourceType = typeof(Resources.Post))]
        [KeyProperty]
        [JsonIgnore]
        public long UserID { get; set; }

        [Display(Name = "TripID", ResourceType = typeof(Resources.Post))]
        [Required]
        public long TripID { get; set; }

        [Display(Name = "Title", ResourceType = typeof(Resources.Post))]
        [Required]
        [StringLength(30)]
        public string Title { get; set; }

        [Display(Name = "Description", ResourceType = typeof(Resources.Post))]
        [Required]
        [StringLength(2147483647)]
        [TextEditorAttribute]
        public string Description { get; set; }

        [Display(Name = "DateAdded", ResourceType = typeof(Resources.Post))]
        [Required]
        public DateTime DateAdded { get; set; }

        [Display(Name = "ModifiedDate", ResourceType = typeof(Resources.Post))]
        [Required]
        public DateTime ModifiedDate { get; set; }

        [Display(Name = "MapRoute", ResourceType = typeof(Resources.Post))]
        [Required]
        [StringLength(2147483647)]
        [DataType(DataType.MultilineText)]
        public string MapRoute { get; set; }

        [Display(Name = "LastLocation", ResourceType = typeof(Resources.Post))]
        [StringLength(2147483647)]
        [DataType(DataType.MultilineText)]
        public string LastLocation { get; set; }

        [Display(Name = "Active", ResourceType = typeof(Resources.Post))]
        [Required]
        public bool Active { get; set; }
    }
}