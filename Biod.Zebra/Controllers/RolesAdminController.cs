using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.AspNet.Identity.EntityFramework;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System.Collections.Generic;
using Biod.Zebra.Library.Infrastructures;
using Biod.Zebra.Library.Models;

namespace Biod.Zebra.Controllers
{
    [Authorize(Roles = "Admin")]
    public class RolesAdminController : BaseController
    {
        public RolesAdminController()
        {
        }

        public RolesAdminController(ApplicationUserManager userManager,
            ApplicationRoleManager roleManager)
        {
            UserManager = userManager;
            RoleManager = roleManager;
        }

        private ApplicationRoleManager _roleManager;
        public ApplicationRoleManager RoleManager
        {
            get
            {
                return _roleManager ?? HttpContext.GetOwinContext().Get<ApplicationRoleManager>();
            }
            private set
            {
                _roleManager = value;
            }
        }

        //
        // GET: /Roles/
        public ActionResult Index()
        {
            Logger.Info($"Loaded Admin page for list of Roles");
            return View(DbContext.AspNetRoles.Select(r => new RoleViewModel
            {
                Id = r.Id,
                Name = r.Name,
                IsPublic = r.IsPublic,
                NotificationDescription = r.NotificationDescription
            }).ToList());
        }

        //
        // GET: /Roles/Details/5
        public async Task<ActionResult> Details(string id)
        {
            var role = DbContext.AspNetRoles.FirstOrDefault(r => r.Id == id);
            if (role == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.NotFound);
            }
            
            // Get the list of Users in this Role
            var users = new List<ApplicationUser>();

            // Get the list of Users in this Role
            foreach (var user in UserManager.Users.ToList())
            {
                if (await UserManager.IsInRoleAsync(user.Id, role.Name))
                {
                    users.Add(user);
                }
            }

            ViewBag.Users = users.OrderBy(u => u.UserName).ToList();
            ViewBag.UserCount = users.Count;

            Logger.Info($"{UserName} viewed Role with ID {id}");
            return View(new RoleViewModel
            {
                Id = role.Id,
                Name = role.Name,
                IsPublic = role.IsPublic,
                NotificationDescription = role.NotificationDescription
            });
        }

        //
        // GET: /Roles/Create
        public ActionResult Create()
        {
            return View();
        }

        //
        // POST: /Roles/Create
        [HttpPost]
        public async Task<ActionResult> Create(RoleViewModel roleViewModel)
        {
            if (ModelState.IsValid)
            {
                var role = new IdentityRole(roleViewModel.Name);
                var roleresult = await RoleManager.CreateAsync(role);
                if (!roleresult.Succeeded)
                {
                    ModelState.AddModelError("", roleresult.Errors.First());
                    return View();
                }
                Logger.Info($"{UserName} created Role with ID {roleViewModel.Id}");
                return RedirectToAction("Index");
            }
            return View();
        }

        //
        // GET: /Roles/Edit/Admin
        public ActionResult Edit(string id)
        {
            var role = DbContext.AspNetRoles.FirstOrDefault(r => r.Id == id);
            if (role == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.NotFound);
            }
            
            var allUsers = UserManager.Users.ToList()
                .Select(u => new SelectListItem
                {
                    Text = u.UserName,
                    Value = u.Id,
                    Selected = u.Roles.FirstOrDefault(r => r.RoleId == role.Id) != null
                })
                .OrderBy(i => i.Text)
                .ToList();
            
            var roleModel = new RoleViewModel
            {
                Id = role.Id,
                Name = role.Name,
                IsPublic = role.IsPublic,
                NotificationDescription = role.NotificationDescription,
                UsersList = allUsers
            };
            return View(roleModel);
        }

        //
        // POST: /Roles/Edit/5
        [HttpPost, ValidateInput(false)]

        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "Name,Id,NotificationDescription,IsPublic")] RoleViewModel roleModel, params string[] selectedUsers)
        {
            if (ModelState.IsValid)
            {
                var role = DbContext.AspNetRoles.FirstOrDefault(r => r.Id == roleModel.Id);
                if (role == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.NotFound);
                }
                role.Name = roleModel.Name;
                role.IsPublic = roleModel.IsPublic;
                role.NotificationDescription = roleModel.NotificationDescription;

                foreach (var user in UserManager.Users.ToList())
                {
                    if (user.Roles.FirstOrDefault(r => r.RoleId == role.Id) != null && CanRemove(user, role.Id) && (selectedUsers == null || !selectedUsers.Contains(user.Id)))
                    {
                        if (CanRemove(user, role.Id))
                        // User has been unassigned from this role
                        await UserManager.RemoveFromRoleAsync(user.Id, role.Name);
                    }
                    else if (user.Roles.FirstOrDefault(r => r.RoleId == role.Id) == null && selectedUsers != null && selectedUsers.Contains(user.Id))
                    {
                        // User that was previously unassigned, now assigned with this role
                        await UserManager.AddToRoleAsync(user.Id, role.Name);
                    }
                }
                
                await DbContext.SaveChangesAsync();
                
                Logger.Info($"{UserName} edited Role with ID {roleModel.Id}");
                return RedirectToAction("Details", new { id = roleModel.Id });
            }
            return View();
        }

        //
        // GET: /Roles/Delete/5
        public async Task<ActionResult> Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var role = await RoleManager.FindByIdAsync(id);
            if (role == null)
            {
                return HttpNotFound();
            }
            return View(role);
        }

        //
        // POST: /Roles/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(string id)
        {
            if (ModelState.IsValid)
            {
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                var role = await RoleManager.FindByIdAsync(id);
                if (role == null)
                {
                    return HttpNotFound();
                }

                if (role.Users.Any())
                {
                    ViewBag.Error = "Cannot delete role that is assigned to users";
                    return View(role);
                }
                
                var result = await RoleManager.DeleteAsync(role);
                if (!result.Succeeded)
                {
                    ModelState.AddModelError("", result.Errors.First());
                    return View();
                }

                Logger.Info($"{UserName} deleted Role with ID {id}");
                return RedirectToAction("Index");
            }
            return View();
        }

        //
        // POST: /RolesAdmin/Unassign
        [HttpPost]
        public async Task<ActionResult> Unassign(string roleId, string userId)
        {
            var role = DbContext.AspNetRoles.FirstOrDefault(r => r.Id == roleId);
            var user = await UserManager.FindByIdAsync(userId);

            if (role == null || user == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.NotFound);
            }

            if (!CanRemove(user, roleId))
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            await UserManager.RemoveFromRoleAsync(userId, role.Name);
            
            Logger.Info($"{UserName} unassigned role with Id {roleId} from user ID {userId}");
            return new HttpStatusCodeResult(HttpStatusCode.OK);
        }

        private bool CanRemove(ApplicationUser user, string roleId)
        {
            var publicRoles = new CustomRolesFilter(DbContext).FilterOutPrivateRoles(user.Roles);
            return publicRoles.Any(r => r.RoleId != roleId);
        }
    }
}
