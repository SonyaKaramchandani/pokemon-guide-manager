using System;
using System.Configuration;
using System.Linq;
using System.Security.Principal;
using System.Web;
using System.Web.Mvc;

[AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, Inherited = true, AllowMultiple = true)]
public class AuthorizeByConfig : AuthorizeAttribute
{

    /// <summary>
    /// Web.config appSetting key to get comma-delimited roles from
    /// </summary>
    public string RolesAppSettingKey { get; set; }                                                                                                                                                      

    /// <summary>
    /// Web.config appSetting key to get comma-delimited users from
    /// </summary>
    public string UsersAppSettingKey { get; set; }


    protected override bool AuthorizeCore(HttpContextBase httpContext)
    {
        if (!Convert.ToBoolean(ConfigurationManager.AppSettings.Get("AnyoneCanRegister")))
        {
            if (!String.IsNullOrEmpty(RolesAppSettingKey))
            {
                string roles = ConfigurationManager.AppSettings.Get("AdminUsersRole") + "," + ConfigurationManager.AppSettings[RolesAppSettingKey];
                if (!String.IsNullOrEmpty(roles))
                {
                    this.Roles = roles;
                }
            }

            if (!String.IsNullOrEmpty(UsersAppSettingKey))
            {
                string users = ConfigurationManager.AppSettings[UsersAppSettingKey];
                if (!String.IsNullOrEmpty(users))
                {
                    this.Users = users;
                }
            }
        }
        return base.AuthorizeCore(httpContext);
    }

    public class DoNotAuthorizeAttribute : AuthorizeAttribute
    {
        /// <summary>
        /// This is effectively a copy of the MVC source for AuthorizeCore with true/false logic swapped.
        /// </summary>
        /// <param name="httpContext">The HTTP context, which encapsulates all HTTP-specific information about an individual HTTP request.</param>
        /// <returns>true if the user is authorized; otherwise, false.</returns>
        protected override bool AuthorizeCore(HttpContextBase httpContext)
        {
            if (httpContext == null)
            {
                throw new ArgumentNullException("httpContext");
            }
            IPrincipal user = httpContext.User;
            if (!user.Identity.IsAuthenticated)
            {
                return false;
            }

            string[] usersSplit = Users.Split(',');
            if ((usersSplit.Length > 0) && usersSplit.Contains<string>(user.Identity.Name, StringComparer.OrdinalIgnoreCase))
            {
                return false;
            }

            string[] rolesSplit = Roles.Split(',');
            if ((rolesSplit.Length > 0) && rolesSplit.Any<string>(new Func<string, bool>(user.IsInRole)))
            {
                return false;
            }

            return true;
        }
    }
}