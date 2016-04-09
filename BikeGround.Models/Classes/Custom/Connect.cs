using BikeGround.Interfaces;
using MicroOrm.Pocos.SqlGenerator.Attributes;
using System.ComponentModel.DataAnnotations;

namespace BikeGround.Models
{
    /// <summary>
    /// Klasa koja se koristi prilikom pretraživanja profila
    /// </summary>
    public partial class Connect : IDynamicQuery
    {
        [Display(Name = "ID", ResourceType = typeof(Resources.Profile))]
        [KeyProperty(Identity = true)]
        [SourceTableAttribute(TableName="Profile")]
        public long ID { get; set; }

        [Display(Name = "FirstName", ResourceType = typeof(Resources.Profile))]
        [StringLength(30)]
        [SourceTableAttribute(TableName = "Profile")]
        public string FirstName { get; set; }

        [Display(Name = "LastName", ResourceType = typeof(Resources.Profile))]
        [StringLength(30)]
        [SourceTableAttribute(TableName = "Profile")]
        public string LastName { get; set; }

        [Display(Name = "About", ResourceType = typeof(Resources.Profile))]
        [StringLength(250)]
        [DataType(DataType.MultilineText)]
        [SourceTableAttribute(TableName = "Profile")]
        public string About { get; set; }

        [Display(Name = "CountryID", ResourceType = typeof(Resources.Profile))]
        [SourceTableAttribute(TableName = "Profile")]
        public long CountryID { get; set; }

        [Display(Name = "City", ResourceType = typeof(Resources.Profile))]
        [SourceTableAttribute(TableName = "Profile")]
        [StringLength(85)]
        public string City { get; set; }

        [Display(Name = "Accommodation", ResourceType = typeof(Resources.Profile))]
        [SourceTableAttribute(TableName = "Profile")]
        public bool Accommodation { get; set; }

        [Display(Name = "AccDescription", ResourceType = typeof(Resources.Profile))]
        [SourceTableAttribute(TableName = "Profile")]
        [StringLength(250)]
        public string AccDescription { get; set; }

        /*** Blog data ***/

        [Display(Name = "Name", ResourceType = typeof(Resources.Blog))]
        [StringLength(30)]
        [SourceTableAttribute(TableName = "Blog")]
        public string Name { get; set; }

        [Display(Name = "Caption", ResourceType = typeof(Resources.Blog))]
        [StringLength(80)]
        [SourceTableAttribute(TableName = "Blog")]
        public string Caption { get; set; }

        [Display(Name = "BlogName", ResourceType = typeof(Resources.Blog))]
        [StringLength(30)]
        [SourceTableAttribute(TableName = "Blog")]
        public string BlogName { get; set; }
    }
}