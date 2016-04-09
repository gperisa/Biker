using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;

namespace BikeGround.Models
{
    public partial class Login
    {
        [Display(Name = "UserName", ResourceType = typeof(Resources.Login))]
        [Required]
        [StringLength(30)]
        public string UserName { get; set; }

        [Display(Name = "Password", ResourceType = typeof(Resources.Login))]
        [Required]
        [StringLength(30)]
        [PasswordAttribute]
        public string Password { get; set; }
    }
}