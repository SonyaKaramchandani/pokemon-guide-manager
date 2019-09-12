using System;
using System.Configuration;
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


}