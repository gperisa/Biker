using BikeGround.Models.Helpers;
using MicroOrm.Pocos.SqlGenerator.Attributes;
using Newtonsoft.Json;
using System.Collections.Generic;

namespace BikeGround.Models
{
    /// <summary>
    /// Klasa koja enkapsulira podatke koji se učitavaju prilikom učitavanja korisnika
    /// </summary>
    public partial class Starter
    {
        [NonStored]
        public long BlogID
        {
            get
            {
                if (this.Blog == null)
                {
                    return 0;
                }
                else
                {
                    return this.Blog.ID;
                }
            }
        }

        [NonStored]
        public Blog Blog { get; set; }

        [NonStored]
        public bool ChatActivity { get; set; }

        [NonStored]
        public string BlogName { get; set; }

        [NonStored]
        public IEnumerable<DDHelper> Countries { get; set; }

        [NonStored]
        public IEnumerable<DDHelper> ProfileActivities { get; set; }

        [NonStored]
        public IEnumerable<DDHelper> Trips { get; set; }

        [NonStored]
        public IEnumerable<DDHelper> MailTypes { get; set; }

        [NonStored]
        public IEnumerable<DDHelper> RouteRatings { get; set; }
    }
}