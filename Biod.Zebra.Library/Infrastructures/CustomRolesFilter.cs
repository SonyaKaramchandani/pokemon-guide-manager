using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using Biod.Zebra.Library.EntityModels.Zebra;
using Microsoft.AspNet.Identity.EntityFramework;

namespace Biod.Zebra.Library.Infrastructures
{
    public class CustomRolesFilter
    {
        private readonly List<AspNetRole> _privateRoles;
        private readonly List<AspNetRole> _publicRoles;
        
        public CustomRolesFilter(BiodZebraEntities dbContext)
        {
            var roles = dbContext.AspNetRoles.ToList();
            _privateRoles= roles.Where(r => !r.IsPublic).ToList();
            _publicRoles = roles.Where(r => r.IsPublic).ToList();
        }
        
        /// <summary>
        /// Removes all the private roles from the list of role names 
        /// </summary>
        public IEnumerable<string> FilterOutPrivateRoles(IEnumerable<string> roleNames)
        {
            var privateRoleNames = new HashSet<string>(_privateRoles.Select(r => r.Name).ToList());
            return roleNames.Where(n => !privateRoleNames.Contains(n));
        }

        /// <summary>
        /// Filters out the private roles and return the first public role from the list of role names
        /// </summary>
        public string GetFirstPublicRole(IEnumerable<string> roleNames)
        {
            return FilterOutPrivateRoles(roleNames).FirstOrDefault();
        }
        
        /// <summary>
        /// Removes all the private roles from the list of role names 
        /// </summary>
        public IEnumerable<IdentityUserRole> FilterOutPrivateRoles(IEnumerable<IdentityUserRole> roleIds)
        {
            var privateRoleIds = new HashSet<string>(_privateRoles.Select(r => r.Id).ToList());
            return roleIds.Where(r => !privateRoleIds.Contains(r.RoleId));
        }

        /// <summary>
        /// Filters out the private roles and return the first public role from the list of role names
        /// </summary>
        public IdentityUserRole GetFirstPublicRole(IEnumerable<IdentityUserRole> roleNames)
        {
            return FilterOutPrivateRoles(roleNames).FirstOrDefault();
        }

        /// <summary>
        /// Gets the list of Public Role Names
        /// </summary>
        public IEnumerable<string> GetPublicRoleNames()
        {
            return _publicRoles.Select(r => r.Name).AsEnumerable();
        }

        /// <summary>
        /// Gets the list of Private Role Names
        /// </summary>
        public IEnumerable<string> GetPrivateRoleNames()
        {
            return _privateRoles.Select(r => r.Name).AsEnumerable();
        }

        public IEnumerable<SelectListItem> GetPublicRoleOptions()
        {
            return _publicRoles.Select(r => new SelectListItem
            {
                Text = r.Name,
                Value = r.Name
            }).AsEnumerable();
        }
    }
}