using BikeGround.Interfaces;
using MicroOrm.Pocos.SqlGenerator.Attributes;
using System.ComponentModel.DataAnnotations;

namespace BikeGround.Models
{
    /// <summary>
    /// Klasa koja se koristi prilikom pretraživanja profila
    /// </summary>
    public partial class Subscribe : IDynamicQuery
    {
        [Display(Name = "FirstName", ResourceType = typeof(Resources.Profile))]
        [StringLength(30)]
        [SourceTableAttribute(TableName = "Profile")]
        public string FirstName { get; set; }

        [Display(Name = "LastName", ResourceType = typeof(Resources.Profile))]
        [StringLength(30)]
        [SourceTableAttribute(TableName = "Profile")]
        public string LastName { get; set; }

        [Display(Name = "Email", ResourceType = typeof(Resources.AspNetUsers))]
        [Required]
        [StringLength(256)]
        [SourceTableAttribute(TableName = "AspNetUsers")]
        public string Email { get; set; }
    }
}