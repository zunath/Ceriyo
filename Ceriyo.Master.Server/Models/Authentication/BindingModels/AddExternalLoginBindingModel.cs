using System.ComponentModel.DataAnnotations;

namespace Ceriyo.Master.Server.Models.Authentication.BindingModels
{
    public class AddExternalLoginBindingModel
    {
        [Required]
        [Display(Name = "External access token")]
        public string ExternalAccessToken { get; set; }
    }

}