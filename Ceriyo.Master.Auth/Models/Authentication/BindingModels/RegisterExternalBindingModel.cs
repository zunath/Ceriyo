using System.ComponentModel.DataAnnotations;

namespace Ceriyo.Master.Auth.Models.Authentication.BindingModels
{
    public class RegisterExternalBindingModel
    {
        [Required]
        [Display(Name = "Email")]
        public string Email { get; set; }
    }

}