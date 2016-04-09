using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;

namespace BikeGround.Models
{
    public partial class Register
    {
        [Display(Name = "UserName", ResourceType = typeof(Resources.Register))]
        [Required]
        [StringLength(30)]
        public string UserName { get; set; }

        [Display(Name = "Password", ResourceType = typeof(Resources.Register))]
        [Required]
        [StringLength(30)]
        [PasswordAttribute]
        public string Password { get; set; }

        [Display(Name = "RepeatPassword", ResourceType = typeof(Resources.Register))]
        [Required]
        [StringLength(30)]
        [PasswordAttribute]
        public string RepeatPassword { get; set; }
    }
}