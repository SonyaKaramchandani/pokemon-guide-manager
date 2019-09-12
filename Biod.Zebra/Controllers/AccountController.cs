﻿using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using Biod.Zebra.Library.Models;
using System.Configuration;
using Biod.Zebra.Library.Infrastructures;
using Biod.Zebra.Library.Infrastructures.Notification;
using Biod.Zebra.Library.Models.Notification.Email;

namespace Biod.Zebra.Controllers
{
    [Authorize]
    public class AccountController : BaseController
    {
        private ApplicationSignInManager _signInManager;

        public AccountController()
        {
        }

        public AccountController(ApplicationUserManager userManager, ApplicationSignInManager signInManager)
        {
            UserManager = userManager;
            SignInManager = signInManager;
        }

        public ApplicationSignInManager SignInManager
        {
            get => _signInManager ?? HttpContext.GetOwinContext().Get<ApplicationSignInManager>();
            private set => _signInManager = value;
        }

        //
        // GET: /Account/Login
        [AllowAnonymous]
        public ActionResult Login(string returnUrl)
        {
            ViewBag.ReturnUrl = returnUrl;
            return View();
        }

        // POST: /Account/Login
        [HttpPost]
        [AllowAnonymous]
        //[ValidateAntiForgeryToken]
        public async Task<ActionResult> Login(LoginViewModel model, string returnUrl)
        {
            //This if statement is to handle the Anti-Forgery validation by redirect to the 
            //main dashboard or to sign out if AntiForgery is invalid
            if (User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Index", "Dashboard", new { area = "DashboardPage" });
            }
            try
            {
                System.Web.Helpers.AntiForgery.Validate();
            }
            catch (System.Exception)
            {
                AuthenticationManager.SignOut(DefaultAuthenticationTypes.ApplicationCookie);
                Logger.Info($"{UserName} has signed out. AntiForgery was invalid.");
                return RedirectToAction("Index", "Dashboard", new { area = "DashboardPage" });
            }

            if (!ModelState.IsValid)
            {
                return View(model);
            }

            // This doesn't count login failures towards account lockout
            // To enable password failures to trigger account lockout, change to shouldLockout: true
            var result = await SignInManager.PasswordSignInAsync(model.Email, model.Password, model.RememberMe, shouldLockout: false);
            //var result = await SignInManager.PasswordSignInAsync(model.Email, ConfigurationManager.AppSettings.Get("GenericPassword"), model.RememberMe, shouldLockout: false);
            switch (result)
            {
                case SignInStatus.Success:
                    Logger.Info($"{model.Email} successfully logged in");
                    return RedirectToLocal(returnUrl);
                case SignInStatus.LockedOut:
                    return View("Lockout");
                case SignInStatus.RequiresVerification:
                    return RedirectToAction("SendCode", new { ReturnUrl = returnUrl, model.RememberMe });
                case SignInStatus.Failure:
                    Logger.Warning($"{model.Email} failed to login: sign in status of {result}");
                    ModelState.AddModelError("", "Invalid login attempt.");
                    return View(model);
                default:
                    return View(model);
            }
        }

        //
        // GET: /Account/VerifyCode
        [AllowAnonymous]
        public async Task<ActionResult> VerifyCode(string provider, string returnUrl, bool rememberMe)
        {
            // Require that the user has already logged in via username/password or external login
            if (!await SignInManager.HasBeenVerifiedAsync())
            {
                return View("Error");
            }
            return View(new VerifyCodeViewModel { Provider = provider, ReturnUrl = returnUrl, RememberMe = rememberMe });
        }

        //
        // POST: /Account/VerifyCode
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> VerifyCode(VerifyCodeViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            // The following code protects for brute force attacks against the two factor codes. 
            // If a user enters incorrect codes for a specified amount of time then the user account 
            // will be locked out for a specified amount of time. 
            // You can configure the account lockout settings in IdentityConfig
            var result = await SignInManager.TwoFactorSignInAsync(model.Provider, model.Code, isPersistent: model.RememberMe, rememberBrowser: model.RememberBrowser);
            switch (result)
            {
                case SignInStatus.Success:
                    return RedirectToLocal(model.ReturnUrl);
                case SignInStatus.LockedOut:
                    return View("Lockout");
                case SignInStatus.Failure:
                    ModelState.AddModelError("", "Invalid code.");
                    return View(model);
                default:
                    return View(model);
            }
        }

        //
        // GET: /Account/Register
        [AllowAnonymous]
        public ActionResult Register()
        {
            return View(new RegisterViewModel
            {
                RolesList = new CustomRolesFilter(DbContext).GetPublicRoleNames()
            });
        }

        // GET: /Account/Unsubscribe
        [AllowAnonymous]
        public ActionResult Unsubscribe(string _)
        {
            return Redirect(Url.Action("UserNotification", "UserProfile", new { area = "" }));
        }

        //
        // POST: /Account/Register
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Register(RegisterViewModel model)
        {
            model.RolesList = new CustomRolesFilter(DbContext).GetPublicRoleNames();

            if (ModelState.IsValid)
            {
                if (model.GeonameId == 0)
                {
                    model.Location = "";
                    return View(model);
                }
                else
                {
                    model.GridId = DbContext.usp_ZebraPlaceGetGridIdByGeonameId(model.GeonameId).FirstOrDefault();
                    model.Location = DbContext.usp_ZebraPlaceGetLocationNameByGeonameId(model.GeonameId).FirstOrDefault();
                }

                var user = new ApplicationUser
                {
                    UserName = model.Email,
                    Email = model.Email,
                    FirstName = model.FirstName,
                    LastName = model.LastName,
                    Location = model.Location,
                    GeonameId = model.GeonameId,
                    GridId = model.GridId,
                    Organization = model.Organization,
                    PhoneNumber = model.PhoneNumber,
                    NewCaseNotificationEnabled = model.NewCaseNotificationEnabled,
                    NewOutbreakNotificationEnabled = model.NewOutbreakNotificationEnabled,
                    PeriodicNotificationEnabled = model.PeriodicNotificationEnabled,
                    WeeklyOutbreakNotificationEnabled = model.WeeklyOutbreakNotificationEnabled,
                    SmsNotificationEnabled = model.SmsNotificationEnabled,
                    AoiGeonameIds = model.GeonameId.ToString()
                };
                var result = await UserManager.CreateAsync(user, model.Password); //with password
                //var result = await UserManager.CreateAsync(user, ConfigurationManager.AppSettings.Get("GenericPassword"));
                if (result.Succeeded)
                {
                    UserManager.AddToRole(user.Id, model.Role);
                    // For more information on how to enable account confirmation and password reset please visit https://go.microsoft.com/fwlink/?LinkID=320771
                    // Send an email with this link
                    string code = await UserManager.GenerateEmailConfirmationTokenAsync(user.Id);
                    var callbackUrl = Url.Action("ConfirmEmail", "Account", new
                    {
                        userId = user.Id,
                        utm_source = Library.Infrastructures.Constants.GoogleAnalytics.UrlTracking.UTM_SOURCE_EMAIL,
                        utm_medium = Library.Infrastructures.Constants.GoogleAnalytics.UrlTracking.UTM_MEDIUM_EMAIL,
                        utm_campaign = Library.Infrastructures.Constants.GoogleAnalytics.UrlTracking.UTM_CAMPAIGN_CONFIRMATION,
                        code
                    }, protocol: Request?.Url?.Scheme);

                    await new NotificationHelper(DbContext, UserManager).SendZebraNotification(new ConfirmationEmailViewModel()
                    {
                        UserId = user.Id,
                        FirstName = user.FirstName,
                        LastName = user.LastName,
                        Email = user.Email,
                        DoNotTrackEnabled = user.DoNotTrackEnabled,
                        EmailConfirmed = user.EmailConfirmed,
                        CallbackUrl = callbackUrl,
                        AoiGeonameIds = user.AoiGeonameIds,
                        Title = ConfigurationManager.AppSettings.Get("ZebraConfirmationEmailSubject")
                    });

                    await AccountHelper.PrecalculateRisk(user.Id);

                    Logger.Info($"New user ID {user.Id} has been successfully registered");

                    // Since this page is only accessible by admin and most likely visited from the UserAdmin page
                    // redirect it to that page after the user has been created
                    return RedirectToAction("Index", "UserAdmin", new { area = "DashboardPage" });
                }
                AddErrors(result);
            }

            // If we got this far, something failed, redisplay form
            return View(model);
        }

        //
        // GET: /Account/ConfirmEmail
        [AllowAnonymous]
        public async Task<ActionResult> ConfirmEmail(string userId, string code)
        {
            if (userId == null || code == null)
            {
                return View("Error");
            }
            var result = await UserManager.ConfirmEmailAsync(userId, code);
            if (result.Succeeded)
            {
                var user = await UserManager.FindByIdAsync(userId);

                await new NotificationHelper(DbContext, UserManager).SendZebraNotification(new WelcomeEmailViewModel()
                {
                    UserId = user.Id,
                    FirstName = user.FirstName,
                    LastName = user.LastName,
                    Email = user.Email,
                    AoiGeonameIds = user.AoiGeonameIds,
                    Title = ConfigurationManager.AppSettings.Get("ZebraWelcomeEmailSubject"),
                    UserLocationName = user.Location
                }.Compute(DbContext));

                Logger.Info($"Email has been successfully confirmed for user ID {user.Id}");
            }
            return View(result.Succeeded ? "ConfirmEmail" : "Error");
        }

        //
        // GET: /Account/ForgotPassword
        [AllowAnonymous]
        public ActionResult ForgotPassword()
        {
            return View();
        }

        //
        // POST: /Account/ForgotPassword
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> ForgotPassword(ForgotPasswordViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = await UserManager.FindByNameAsync(model.Email);
                if (user == null || !(await UserManager.IsEmailConfirmedAsync(user.Id)))
                {
                    // Don't reveal that the user does not exist or is not confirmed
                    Logger.Warning($"Forgot password request received for {model.Email}, but the user does not exist or the user email is not confirmed");

                    // TODO: Add a random timeout to simulate processing
                    return View("ForgotPasswordConfirmation");
                }

                // For more information on how to enable account confirmation and password reset please visit https://go.microsoft.com/fwlink/?LinkID=320771
                // Send an email with this link
                string code = await UserManager.GeneratePasswordResetTokenAsync(user.Id);
                var callbackUrl = Url.Action("ResetPassword", "Account", new
                {
                    userId = user.Id,
                    utm_source = Library.Infrastructures.Constants.GoogleAnalytics.UrlTracking.UTM_SOURCE_EMAIL,
                    utm_medium = Library.Infrastructures.Constants.GoogleAnalytics.UrlTracking.UTM_MEDIUM_EMAIL,
                    utm_campaign = Library.Infrastructures.Constants.GoogleAnalytics.UrlTracking.UTM_CAMPAIGN_RESET_PASSWORD,
                    code
                }, protocol: Request?.Url?.Scheme);

                await new NotificationHelper(DbContext, UserManager).SendZebraNotification(new ResetPasswordEmailViewModel()
                {
                    FirstName = user.FirstName,
                    UserId = user.Id,
                    Email = user.Email,
                    CallbackUrl = callbackUrl,
                    Title = "Reset Password",
                    AoiGeonameIds = user.AoiGeonameIds
                });

                Logger.Info($"Forgot password email sent for user ID {user.Id}");
                return RedirectToAction("ForgotPasswordConfirmation", "Account");
            }

            // If we got this far, something failed, redisplay form
            return View(model);
        }

        //
        // GET: /Account/ForgotPasswordConfirmation
        [AllowAnonymous]
        public ActionResult ForgotPasswordConfirmation()
        {
            return View();
        }

        //
        // GET: /Account/ResetPassword
        [AllowAnonymous]
        public ActionResult ResetPassword(string code)
        {
            return code == null ? View("Error") : View();
        }

        //
        // POST: /Account/ResetPassword
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> ResetPassword(ResetPasswordViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var user = await UserManager.FindByNameAsync(model.Email);
            if (user == null)
            {
                Logger.Warning($"Reset Password request for {model.Email} was received, but no such user exists");
                // Don't reveal that the user does not exist
                return RedirectToAction("ResetPasswordConfirmation", "Account");
            }
            var result = await UserManager.ResetPasswordAsync(user.Id, model.Code, model.Password);
            if (result.Succeeded)
            {
                Logger.Info($"Password has been reset for user ID {user.Id}");
                return RedirectToAction("ResetPasswordConfirmation", "Account");
            }
            AddErrors(result);
            return View();
        }

        //
        // GET: /Account/ResetPasswordConfirmation
        [AllowAnonymous]
        public ActionResult ResetPasswordConfirmation()
        {
            return View();
        }

        //
        // POST: /Account/ExternalLogin
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult ExternalLogin(string provider, string returnUrl)
        {
            // Request a redirect to the external login provider
            return new ChallengeResult(provider, Url.Action("ExternalLoginCallback", "Account", new { ReturnUrl = returnUrl }));
        }

        //
        // GET: /Account/SendCode
        [AllowAnonymous]
        public async Task<ActionResult> SendCode(string returnUrl, bool rememberMe)
        {
            var userId = await SignInManager.GetVerifiedUserIdAsync();
            if (userId == null)
            {
                return View("Error");
            }
            var userFactors = await UserManager.GetValidTwoFactorProvidersAsync(userId);
            var factorOptions = userFactors.Select(purpose => new SelectListItem { Text = purpose, Value = purpose }).ToList();
            return View(new SendCodeViewModel { Providers = factorOptions, ReturnUrl = returnUrl, RememberMe = rememberMe });
        }

        //
        // POST: /Account/SendCode
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> SendCode(SendCodeViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }

            // Generate the token and send it
            if (!await SignInManager.SendTwoFactorCodeAsync(model.SelectedProvider))
            {
                return View("Error");
            }
            return RedirectToAction("VerifyCode", new { Provider = model.SelectedProvider, model.ReturnUrl, model.RememberMe });
        }

        //
        // GET: /Account/ExternalLoginCallback
        [AllowAnonymous]
        public async Task<ActionResult> ExternalLoginCallback(string returnUrl)
        {
            var loginInfo = await AuthenticationManager.GetExternalLoginInfoAsync();
            if (loginInfo == null)
            {
                return RedirectToAction("Login");
            }

            // Sign in the user with this external login provider if the user already has a login
            var result = await SignInManager.ExternalSignInAsync(loginInfo, isPersistent: false);
            switch (result)
            {
                case SignInStatus.Success:
                    return RedirectToLocal(returnUrl);
                case SignInStatus.LockedOut:
                    return View("Lockout");
                case SignInStatus.RequiresVerification:
                    return RedirectToAction("SendCode", new { ReturnUrl = returnUrl, RememberMe = false });
                case SignInStatus.Failure:
                    // If the user does not have an account, then prompt the user to create an account
                    ViewBag.ReturnUrl = returnUrl;
                    ViewBag.LoginProvider = loginInfo.Login.LoginProvider;
                    return View("ExternalLoginConfirmation", new ExternalLoginConfirmationViewModel { Email = loginInfo.Email });
                default:
                    return RedirectToAction("Login");
            }
        }

        //
        // POST: /Account/ExternalLoginConfirmation
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> ExternalLoginConfirmation(ExternalLoginConfirmationViewModel model, string returnUrl)
        {
            if (User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Index", "Manage");
            }

            if (ModelState.IsValid)
            {
                // Get the information about the user from the external login provider
                var info = await AuthenticationManager.GetExternalLoginInfoAsync();
                if (info == null)
                {
                    return View("ExternalLoginFailure");
                }
                var user = new ApplicationUser { UserName = model.Email, Email = model.Email };
                var result = await UserManager.CreateAsync(user);
                if (result.Succeeded)
                {
                    result = await UserManager.AddLoginAsync(user.Id, info.Login);
                    if (result.Succeeded)
                    {
                        await SignInManager.SignInAsync(user, isPersistent: false, rememberBrowser: false);
                        return RedirectToLocal(returnUrl);
                    }
                }
                AddErrors(result);
            }

            ViewBag.ReturnUrl = returnUrl;
            return View(model);
        }

        //
        // POST: /Account/LogOff
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult LogOff()
        {
            AuthenticationManager.SignOut(DefaultAuthenticationTypes.ApplicationCookie);
            Logger.Info($"{UserName} has signed out");
            return RedirectToAction("Index", "Dashboard", new { area = "DashboardPage" });
        }

        //
        // GET: /Account/ExternalLoginFailure
        [AllowAnonymous]
        public ActionResult ExternalLoginFailure()
        {
            return View();
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (_signInManager != null)
                {
                    _signInManager.Dispose();
                    _signInManager = null;
                }
            }

            base.Dispose(disposing);
        }

        #region Helpers
        // Used for XSRF protection when adding external logins
        private const string XsrfKey = "XsrfId";

        private IAuthenticationManager AuthenticationManager
        {
            get
            {
                return HttpContext.GetOwinContext().Authentication;
            }
        }

        private void AddErrors(IdentityResult result)
        {
            foreach (var error in result.Errors)
            {
                //https://stackoverflow.com/questions/26109965/mvc-5-identity-and-error-message
                if (!(error.StartsWith("Name") && error.EndsWith("is already taken."))
                    && !(error.StartsWith("The GeonameId")))
                {
                    ModelState.AddModelError("", error);
                }
            }
        }

        private ActionResult RedirectToLocal(string returnUrl)
        {
            if (Url.IsLocalUrl(returnUrl))
            {
                return Redirect(returnUrl);
            }
            return RedirectToAction("Index", "Dashboard", new { area = "DashboardPage" });
        }

        internal class ChallengeResult : HttpUnauthorizedResult
        {
            public ChallengeResult(string provider, string redirectUri)
                : this(provider, redirectUri, null)
            {
            }

            public ChallengeResult(string provider, string redirectUri, string userId)
            {
                LoginProvider = provider;
                RedirectUri = redirectUri;
                UserId = userId;
            }

            public string LoginProvider { get; set; }
            public string RedirectUri { get; set; }
            public string UserId { get; set; }

            public override void ExecuteResult(ControllerContext context)
            {
                var properties = new AuthenticationProperties { RedirectUri = RedirectUri };
                if (UserId != null)
                {
                    properties.Dictionary[XsrfKey] = UserId;
                }
                context.HttpContext.GetOwinContext().Authentication.Challenge(properties, LoginProvider);
            }
        }
        #endregion
    }
}