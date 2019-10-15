using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin;
using Microsoft.Owin.Security;
using Biod.Zebra.Library.Models;
using System.Net.Mail;
using System.Configuration;
using Twilio;
using System.Diagnostics;
using Biod.Zebra.Library.Infrastructures.Notification;

namespace Biod.Zebra
{
    // Configure the application user manager used in this application. UserManager is defined in ASP.NET Identity and is used by the application.

    public class ApplicationUserManager : UserManager<ApplicationUser>
    {
        public ApplicationUserManager(IUserStore<ApplicationUser> store)
            : base(store)
        {
        }

        public static ApplicationUserManager Create(IdentityFactoryOptions<ApplicationUserManager> options,
            IOwinContext context)
        {
            var manager = new ApplicationUserManager(new UserStore<ApplicationUser>(context.Get<ApplicationDbContext>()));
            // Configure validation logic for usernames
            manager.UserValidator = new UserValidator<ApplicationUser>(manager)
            {
                AllowOnlyAlphanumericUserNames = Convert.ToBoolean(ConfigurationManager.AppSettings.Get("AllowOnlyAlphanumericUserNames")),
                RequireUniqueEmail = Convert.ToBoolean(ConfigurationManager.AppSettings.Get("RequireUniqueEmail"))
            };
            // Configure validation logic for passwords
            manager.PasswordValidator = new PasswordValidator
            {
                RequiredLength = Convert.ToInt32(ConfigurationManager.AppSettings.Get("RequiredLength")),
                RequireNonLetterOrDigit = Convert.ToBoolean(ConfigurationManager.AppSettings.Get("RequireNonLetterOrDigit")),
                RequireDigit = Convert.ToBoolean(ConfigurationManager.AppSettings.Get("RequireDigit")),
                RequireLowercase = Convert.ToBoolean(ConfigurationManager.AppSettings.Get("RequireLowercase")),
                RequireUppercase = Convert.ToBoolean(ConfigurationManager.AppSettings.Get("RequireUppercase")),
            };
            // Configure user lockout defaults
            manager.UserLockoutEnabledByDefault = Convert.ToBoolean(ConfigurationManager.AppSettings.Get("UserLockoutEnabledByDefault"));
            manager.DefaultAccountLockoutTimeSpan = TimeSpan.FromMinutes(Convert.ToInt32(ConfigurationManager.AppSettings.Get("DefaultAccountLockoutTimeSpan")));
            manager.MaxFailedAccessAttemptsBeforeLockout = Convert.ToInt32(ConfigurationManager.AppSettings.Get("MaxFailedAccessAttemptsBeforeLockout"));
            // Register two factor authentication providers. This application uses Phone and Emails as a step of receiving a code for verifying the user
            // You can write your own provider and plug it in here.
            manager.RegisterTwoFactorProvider("Phone Code", new PhoneNumberTokenProvider<ApplicationUser>
            {
                MessageFormat = "Your security code is {0}"
            });
            manager.RegisterTwoFactorProvider("Email Code", new EmailTokenProvider<ApplicationUser>
            {
                Subject = "Security Code",
                BodyFormat = "Your security code is {0}"
            });
            manager.EmailService = new EmailService();
            manager.SmsService = new SmsService();
            var dataProtectionProvider = options.DataProtectionProvider;
            if (dataProtectionProvider != null)
            {
                var userTokenProvider = new DataProtectorTokenProvider<ApplicationUser>(dataProtectionProvider.Create("ASP.NET Identity"))
                {
                    TokenLifespan = TimeSpan.FromDays(Convert.ToDouble(ConfigurationManager.AppSettings.Get("IdentityTokenLifespanInDays")))
                };
                manager.UserTokenProvider = userTokenProvider;
            }
            return manager;
        }
    }

    // Configure the RoleManager used in the application. RoleManager is defined in the ASP.NET Identity core assembly
    public class ApplicationRoleManager : RoleManager<IdentityRole>
    {
        public ApplicationRoleManager(IRoleStore<IdentityRole, string> roleStore)
            : base(roleStore)
        {
        }

        public static ApplicationRoleManager Create(IdentityFactoryOptions<ApplicationRoleManager> options, IOwinContext context)
        {
            return new ApplicationRoleManager(new RoleStore<IdentityRole>(context.Get<ApplicationDbContext>()));
        }
    }

    
    public class SmsService : IIdentityMessageService
    {
        public Task SendAsync(IdentityMessage message)
        {
            // Twilio Begin
            var Twilio = new TwilioRestClient(
                ConfigurationManager.AppSettings["SMSAccountIdentification"],
                ConfigurationManager.AppSettings["SMSAuthToken"]
            );
            var result = Twilio.SendMessage(
              ConfigurationManager.AppSettings["SMSAccountFrom"],
              message.Destination, message.Body
            );
            // Status is one of Queued, Sending, Sent, Failed or null if the number is not valid
            Trace.TraceInformation(result.Status);
            // Twilio doesn't currently have an async API, so return success.
            return Task.FromResult(0);
            // Twilio End

            //// Plug in your sms service here to send a text message.
            //// Use your account SID and authentication token instead
            //// of the placeholders shown here.
            //string accountSID = ConfigurationManager.AppSettings["SMSAccountIdentification"];
            //string authToken = ConfigurationManager.AppSettings["SMSAuthToken"];

            //// Initialize the TwilioClient.
            //TwilioClient.Init(accountSID, authToken);

            //// Send an SMS message.
            //var smsMessage = MessageResource.Create(
            //    to: new PhoneNumber("+6474704944"),
            //    from: new PhoneNumber(ConfigurationManager.AppSettings["SMSAccountFrom"]),
            //    body: message.Body);

            //return Task.FromResult(0);

            //// Twilio Begin
            //var Twilio = new TwilioRestClient(
            //   ConfigurationManager.AppSettings["SMSSID"],
            //   ConfigurationManager.AppSettings["SMSAuthToken"]
            //);
            //var result = Twilio..SendMessage(
            //    ConfigurationManager.AppSettings["SMSPhoneNumber"],
            //   message.Destination, message.Body);

            //// Status is one of Queued, Sending, Sent, Failed or null if the number is not valid
            //Trace.TraceInformation(result.Status);

            //// Twilio doesn't currently have an async API, so return success.
            //return Task.FromResult(0);
        }
    }

    // This is useful if you do not want to tear down the database each time you run the application.
    // public class ApplicationDbInitializer : DropCreateDatabaseAlways<ApplicationDbContext>
    // This example shows you how to create a new database if the Model changes
    public class ApplicationDbInitializer : DropCreateDatabaseIfModelChanges<ApplicationDbContext>
    {
        protected override void Seed(ApplicationDbContext context)
        {
            InitializeIdentityForEF(context);
            base.Seed(context);
        }

        //Create User=AdminUserEmail with password=AdminUserPassword in the Admin role        
        public static void InitializeIdentityForEF(ApplicationDbContext db)
        {
            var userManager = HttpContext.Current.GetOwinContext().GetUserManager<ApplicationUserManager>();
            var roleManager = HttpContext.Current.GetOwinContext().Get<ApplicationRoleManager>();
            //Admin user
            string name = ConfigurationManager.AppSettings.Get("AdminUserEmail");
            string password = ConfigurationManager.AppSettings.Get("AdminUserPassword");
            string roleName = "Admin";

            //Create Role Admin if it does not exist
            var role = roleManager.FindByName(roleName);
            if (role == null)
            {
                role = new IdentityRole(roleName);
                var roleresult = roleManager.Create(role);
            }

            var user = userManager.FindByName(name);
            if (user == null)
            {
                user = new ApplicationUser { UserName = name, Email = name };
                var result = userManager.Create(user, password);
                result = userManager.SetLockoutEnabled(user.Id, false);
            }

            // Add user admin to Role Admin if not already added
            var rolesForUser = userManager.GetRoles(user.Id);
            if (!rolesForUser.Contains(role.Name))
            {
                var result = userManager.AddToRole(user.Id, role.Name);
            }
        }
    }

    public class ApplicationSignInManager : SignInManager<ApplicationUser, string>
    {
        public ApplicationSignInManager(ApplicationUserManager userManager, IAuthenticationManager authenticationManager) :
            base(userManager, authenticationManager)
        { }

        public override Task<ClaimsIdentity> CreateUserIdentityAsync(ApplicationUser user)
        {
            return user.GenerateUserIdentityAsync((ApplicationUserManager)UserManager);
        }

        public static ApplicationSignInManager Create(IdentityFactoryOptions<ApplicationSignInManager> options, IOwinContext context)
        {
            return new ApplicationSignInManager(context.GetUserManager<ApplicationUserManager>(), context.Authentication);
        }
    }
}
