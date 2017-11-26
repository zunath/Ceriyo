using Microsoft.AspNet.Identity;

namespace Ceriyo.Master.Auth.Services.Contracts
{
    interface IEmailService
    {
        void SendMessageAsync(IdentityMessage identityMessage);
    }
}
