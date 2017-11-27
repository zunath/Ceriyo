using Ceriyo.Master.Server.Models.Authentication;
using Microsoft.AspNet.Identity.EntityFramework;

namespace Ceriyo.Master.Server.Models
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