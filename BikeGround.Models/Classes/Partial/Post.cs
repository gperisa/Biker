using MicroOrm.Pocos.SqlGenerator.Attributes;
using System.Collections.Generic;

namespace BikeGround.Models
{
    public partial class Post
    {
        [NonStored]
        public IEnumerable<Trip> ListTrips { get; set; }

        [NonStored]
        public IEnumerable<Comment> ListComments { get; set; }
    }
}
