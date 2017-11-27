using System.ComponentModel.DataAnnotations;

namespace Ceriyo.Master.Server.Models.Authentication.BindingModels
{
    public class LoginBindingModel
    {
        [Required]
        [Display(Name = "Username")]
        [StringLength(256, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 4)]
        public string Username { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }
    }
}