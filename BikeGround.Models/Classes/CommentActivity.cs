using MicroOrm.Pocos.SqlGenerator.Attributes;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System;

namespace BikeGround.Models
{
    public partial class CommentActivity
    {
        [Display(Name = "ID", ResourceType = typeof(Resources.CommentActivity))]
        [KeyProperty(Identity = true)]
        public int ID { get; set; }

        [Display(Name = "UserID", ResourceType = typeof(Resources.CommentActivity))]
        [KeyProperty]
        [JsonIgnore]
        public long UserID { get; set; }

        [Display(Name = "CommentID", ResourceType = typeof(Resources.CommentActivity))]
        [Required]
        public long CommentID { get; set; }

        [Display(Name = "ProfileActivityID", ResourceType = typeof(Resources.CommentActivity))]
        [Required]
        public int ProfileActivityID { get; set; }

        [Display(Name = "PositiveComment", ResourceType = typeof(Resources.CommentActivity))]
        [Required]
        public int PositiveComment { get; set; }

        [Display(Name = "NegativeComment", ResourceType = typeof(Resources.CommentActivity))]
        [Required]
        public int NegativeComment { get; set; }
    }
}