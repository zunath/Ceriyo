using System;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using Ceriyo.Master.Server.Models.Authentication;
using Ceriyo.Master.Server.Models.Authentication.BindingModels;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using Microsoft.Owin.Security.Cookies;

namespace Ceriyo.Master.Server.Controllers.Api
{
    [System.Web.Http.Authorize]
    [System.Web.Http.RoutePrefix("api/Account")]
    [RequireHttps]
    public class AccountController : ApiController
    {
        private ApplicationUserManager _userManager;
        private ApplicationSignInManager _signInManager;

        public AccountController()
        {
        }

        public AccountController(ApplicationUserManager userManager,
            ApplicationSignInManager signInManager,
            ISecureDataFormat<AuthenticationTicket> accessTokenFormat)
        {
            UserManager = userManager;
            _signInManager = signInManager;
            AccessTokenFormat = accessTokenFormat;
        }

        public ApplicationUserManager UserManager
        {
            get => _userManager ?? Request.GetOwinContext().GetUserManager<ApplicationUserManager>();
            private set => _userManager = value;
        }

        public ISecureDataFormat<AuthenticationTicket> AccessTokenFormat { get; private set; }


        // POST api/Account/Register
        [System.Web.Http.AllowAnonymous]
        [System.Web.Http.Route("Register")]
        public async Task<IHttpActionResult> Register(RegisterBindingModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var user = new ApplicationUser
            {
                UserName = model.Username,
                Email = model.Email
            };

            var result = await UserManager.CreateAsync(user, model.Password);

            if (result.Succeeded)
            {
                var token = await UserManager.GenerateEmailConfirmationTokenAsync(user.Id);
                var encodedToken = HttpUtility.UrlEncode(token);

                string callbackUrl = $@"{Url.Content("~/")}api/Account/ConfirmEmail?userId={user.Id}&token={encodedToken}";

                await UserManager.SendEmailAsync(
                    user.Id,
                    "Please confirm your account",
                    "Please confirm your Ceriyo Game Engine account by clicking this " +
                    "<a href=\" " + callbackUrl + " \">link</a>");
            }

            return !result.Succeeded ? GetErrorResult(result) : Ok();
        }
        [System.Web.Http.AllowAnonymous]
        [System.Web.Http.Route("ConfirmEmail")]
        [System.Web.Http.HttpGet]
        public HttpResponseMessage ConfirmEmail(string userId, string token)
        {
            if (userId == null || token == null)
            {
                return new HttpResponseMessage()
                {
                    Content = new StringContent("Invalid user ID or code. Please try registering again.")
                };
            }

            var result = UserManager.ConfirmEmailAsync(userId, token);
            string message = result.Result.Succeeded ?
                "Success! Your account was created. Please log in to Ceriyo whenever you're ready." :
                "There was an error creating your account. Reason: " + string.Join(Environment.NewLine, result.Result.Errors);

            return new HttpResponseMessage()
            {
                Content = new StringContent(message)
            };
        }
        
        // POST api/Account/Logout
        [System.Web.Http.Route("Logout")]
        public IHttpActionResult Logout()
        {
            Authentication.SignOut(CookieAuthenticationDefaults.AuthenticationType);
            return Ok();
        }
        
        // POST api/Account/ChangePassword
        [System.Web.Http.Route("ChangePassword")]
        public async Task<IHttpActionResult> ChangePassword(ChangePasswordBindingModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            IdentityResult result = await UserManager.ChangePasswordAsync(User.Identity.GetUserId(), model.OldPassword,
                model.NewPassword);
            
            if (!result.Succeeded)
            {
                return GetErrorResult(result);
            }

            return Ok();
        }
        
        protected override void Dispose(bool disposing)
        {
            if (disposing && _userManager != null)
            {
                _userManager.Dispose();
                _userManager = null;
            }

            base.Dispose(disposing);
        }

        #region Helpers

        private IAuthenticationManager Authentication => Request.GetOwinContext().Authentication;

        private IHttpActionResult GetErrorResult(IdentityResult result)
        {
            if (result == null)
            {
                return InternalServerError();
            }

            if (!result.Succeeded)
            {
                if (result.Errors != null)
                {
                    foreach (string error in result.Errors)
                    {
                        ModelState.AddModelError("", error);
                    }
                }

                if (ModelState.IsValid)
                {
                    // No ModelState errors are available to send, so just return an empty BadRequest.
                    return BadRequest();
                }

                return BadRequest(ModelState);
            }

            return null;
        }
        
        #endregion
    }
}
