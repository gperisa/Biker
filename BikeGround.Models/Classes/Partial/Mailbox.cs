using MicroOrm.Pocos.SqlGenerator.Attributes;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace BikeGround.Models
{
    public partial class Mailbox
    {
        [Display(Name = "From", ResourceType = typeof(Resources.Mailbox))]
        [NonStored]
        public string Sender { get; set; }
        
        [Display(Name = "To", ResourceType = typeof(Resources.Mailbox))]
        [NonStored]
        public string Receiver { get; set; }
    }
}
