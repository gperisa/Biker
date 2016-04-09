//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace BikeGround.Tester
{
    using System;
    using System.Collections.Generic;
    
    public partial class Equipment
    {
        public Equipment()
        {
            this.TripEquipment = new HashSet<TripEquipment>();
            this.UserEquipment = new HashSet<UserEquipment>();
        }
    
        public long ID { get; set; }
        public long CategoryID { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public System.DateTime DateAdded { get; set; }
        public Nullable<long> ManufacturerID { get; set; }
        public string ImageUrl { get; set; }
        public bool Active { get; set; }
    
        public virtual EquipmentCategory EquipmentCategory { get; set; }
        public virtual ICollection<TripEquipment> TripEquipment { get; set; }
        public virtual ICollection<UserEquipment> UserEquipment { get; set; }
    }
}
