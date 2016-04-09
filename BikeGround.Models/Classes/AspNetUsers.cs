using MicroOrm.Pocos.SqlGenerator.Attributes;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System;

namespace BikeGround.Models
{
    public partial class AspNetUsers
    {
        [Display(Name = "Id", ResourceType = typeof(Resources.AspNetUsers))]
        [KeyProperty]
        [Required]
        public long Id { get; set; }

        [Display(Name = "UserName", ResourceType = typeof(Resources.AspNetUsers))]
        [StringLength(256)]
        [DataType(DataType.MultilineText)]
        public string UserName { get; set; }

        [Display(Name = "Email", ResourceType = typeof(Resources.AspNetUsers))]
        [Required]
        [StringLength(256)]
        [DataType(DataType.MultilineText)]
        public string Email { get; set; }

        [Display(Name = "EmailConfirmed", ResourceType = typeof(Resources.AspNetUsers))]
        [Required]
        public bool EmailConfirmed { get; set; }

        [Display(Name = "PasswordHash", ResourceType = typeof(Resources.AspNetUsers))]
        [Required]
        [StringLength(2000)]
        [DataType(DataType.MultilineText)]
        public string PasswordHash { get; set; }

        [Display(Name = "SecurityStamp", ResourceType = typeof(Resources.AspNetUsers))]
        [Required]
        [StringLength(2000)]
        [DataType(DataType.MultilineText)]
        public string SecurityStamp { get; set; }

        [Display(Name = "PhoneNumber", ResourceType = typeof(Resources.AspNetUsers))]
        [StringLength(2000)]
        [DataType(DataType.MultilineText)]
        public string PhoneNumber { get; set; }

        [Display(Name = "PhoneNumberConfirmed", ResourceType = typeof(Resources.AspNetUsers))]
        [Required]
        public bool PhoneNumberConfirmed { get; set; }

        [Display(Name = "TwoFactorEnabled", ResourceType = typeof(Resources.AspNetUsers))]
        [Required]
        public bool TwoFactorEnabled { get; set; }

        [Display(Name = "LockoutEndDateUtc", ResourceType = typeof(Resources.AspNetUsers))]
        public DateTime LockoutEndDateUtc { get; set; }

        [Display(Name = "LockoutEnabled", ResourceType = typeof(Resources.AspNetUsers))]
        [Required]
        public bool LockoutEnabled { get; set; }

        [Display(Name = "AccessFailedCount", ResourceType = typeof(Resources.AspNetUsers))]
        [Required]
        public int AccessFailedCount { get; set; }
    }
}