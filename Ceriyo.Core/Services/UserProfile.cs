using Ceriyo.Core.Contracts;

namespace Ceriyo.Core.Services
{
    /// <inheritdoc />>
    public class UserProfile: IUserProfile
    {
        /// <inheritdoc />>
        public string MasterServerToken { get; set; }
    }
}
