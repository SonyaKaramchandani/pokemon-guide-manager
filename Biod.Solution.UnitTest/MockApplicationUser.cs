using Biod.Zebra.Library.Models;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Biod.Solution.UnitTest.Api.Analytics
{
    public class MockApplicationUser : ApplicationUser
    {
        private readonly ICollection<IdentityUserRole> roles;

        public MockApplicationUser(List<IdentityUserRole> roles) : base()
        {
            this.roles = roles;
        }

        public override ICollection<IdentityUserRole> Roles
        {
            get { return roles; }
        }
    }
}
