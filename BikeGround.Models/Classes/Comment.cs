using MicroOrm.Pocos.SqlGenerator.Attributes;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System;

namespace BikeGround.Models
{
    public partial class Comment
    {
        [Display(Name = "ID", ResourceType = typeof(Resources.Comment))]
        [KeyProperty(Identity = true)]
        public long ID { get; set; }

        [Display(Name = "UserID", ResourceType = typeof(Resources.Comment))]
        [KeyProperty]
        [JsonIgnore]
        public long UserID { get; set; }

        [Display(Name = "PostID", ResourceType = typeof(Resources.Comment))]
        [Required]
        public long PostID { get; set; }

        [Display(Name = "CommentText", ResourceType = typeof(Resources.Comment))]
        [Required]
        [StringLength(300)]
        [DataType(DataType.MultilineText)]
        public string CommentText { get; set; }

        [Display(Name = "CommentID", ResourceType = typeof(Resources.Comment))]
        public long CommentID { get; set; }

        [Display(Name = "Active", ResourceType = typeof(Resources.Comment))]
        [Required]
        public bool Active { get; set; }
    }
}