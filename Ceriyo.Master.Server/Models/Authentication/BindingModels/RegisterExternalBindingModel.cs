using System.ComponentModel.DataAnnotations;

namespace Ceriyo.Master.Server.Models.Authentication.BindingModels
{
    public class RegisterExternalBindingModel
    {
        [Required]
        [Display(Name = "Email")]
        public string Email { get; set; }
    }

}