using Ceriyo.Master.Auth.Models.Authentication;
using Microsoft.AspNet.Identity.EntityFramework;

namespace Ceriyo.Master.Auth.Models
{

    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext()
            : base("DefaultConnection", false)
        {
        }

        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }
    }
}