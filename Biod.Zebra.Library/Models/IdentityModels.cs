using Biod.Zebra.Library.EntityModels.Zebra;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Biod.Zebra.Library.Models
{
    // You can add profile data for the user by adding more properties to your ApplicationUser class, please visit https://go.microsoft.com/fwlink/?LinkID=317594 to learn more.
    public class ApplicationUser : IdentityUser
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Organization { get; set; }
        public string Location { get; set; }
        public int GeonameId { get; set; }
        public string AoiGeonameIds { get; set; }
        public string GridId { get; set; }
        public bool SmsNotificationEnabled { get; set; }
        public bool NewCaseNotificationEnabled { get; set; }
        public bool NewOutbreakNotificationEnabled { get; set; }
        public bool PeriodicNotificationEnabled { get; set; }
        public bool WeeklyOutbreakNotificationEnabled { get; set; }
        public int? UserGroupId { get; set; }
        public bool DoNotTrackEnabled { get; set; }
        public bool OnboardingCompleted { get; set; }
        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<ApplicationUser> manager)
        {
            // Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
            var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
            // Add custom user claims here
            userIdentity.AddClaim(new Claim("Id", this.Id));
            userIdentity.AddClaim(new Claim("FirstName", this.FirstName));
            userIdentity.AddClaim(new Claim("LastName", this.LastName));
            //userIdentity.AddClaim(new Claim("Organization", this.Organization));
            userIdentity.AddClaim(new Claim("UserGroupId", this.UserGroupId == null ? "" : this.UserGroupId.ToString()));
            userIdentity.AddClaim(new Claim("UserProfileGeonameId", this.GeonameId.ToString()));
            userIdentity.AddClaim(new Claim("UserProfileLocation", this.Location));
            userIdentity.AddClaim(new Claim("DoNotTrackEnabled", this.DoNotTrackEnabled.ToString()));
            userIdentity.AddClaim(new Claim("EmailConfirmed", this.EmailConfirmed.ToString()));
            //log the UTC datetime of user login
            BiodZebraEntities zebraDbContext = new BiodZebraEntities();
            zebraDbContext.usp_SetZebraUserLoginTrans(userIdentity.GetUserId());

            return userIdentity;
        }
    }

    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext()
            : base("DefaultConnection", throwIfV1Schema: false)
        {
        }

        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }
    }
}