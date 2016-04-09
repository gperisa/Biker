using MicroOrm.Pocos.SqlGenerator.Attributes;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System;

namespace BikeGround.Models
{
    public partial class Profile
    {
        [Display(Name = "ID", ResourceType = typeof(Resources.Profile))]
        [KeyProperty(Identity = true)]
        public long ID { get; set; }

        [Display(Name = "UserID", ResourceType = typeof(Resources.Profile))]
        [KeyProperty]
        [JsonIgnore]
        public long UserID { get; set; }

        [Display(Name = "FirstName", ResourceType = typeof(Resources.Profile))]
        [Required]
        [StringLength(30)]
        public string FirstName { get; set; }

        [Display(Name = "LastName", ResourceType = typeof(Resources.Profile))]
        [Required]
        [StringLength(30)]
        public string LastName { get; set; }

        [Display(Name = "SecundaryEMail", ResourceType = typeof(Resources.Profile))]
        [Required]
        [StringLength(30)]
        public string SecundaryEMail { get; set; }

        [Display(Name = "About", ResourceType = typeof(Resources.Profile))]
        [Required]
        [StringLength(250)]
        [DataType(DataType.MultilineText)]
        public string About { get; set; }

        [Display(Name = "CountryID", ResourceType = typeof(Resources.Profile))]
        [Required]
        public long CountryID { get; set; }

        [Display(Name = "City", ResourceType = typeof(Resources.Profile))]
        [Required]
        [StringLength(85)]
        public string City { get; set; }

        [Display(Name = "Accommodation", ResourceType = typeof(Resources.Profile))]
        [Required]
        public bool Accommodation { get; set; }

        [Display(Name = "AccDescription", ResourceType = typeof(Resources.Profile))]
        [StringLength(250)]
        [DataType(DataType.MultilineText)]
        public string AccDescription { get; set; }

        [Display(Name = "ChatActivity", ResourceType = typeof(Resources.Profile))]
        [Required]
        public bool ChatActivity { get; set; }

        [Display(Name = "ProfileActivityID", ResourceType = typeof(Resources.Profile))]
        [Required]
        public int ProfileActivityID { get; set; }
    }
}