using System;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.AspNet.Identity.EntityFramework;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System.Collections.Generic;
using System.Data.Entity;
using Biod.Zebra.Library.EntityModels.Zebra;
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
            return View(DbContext.UserTypes.Select(r => new RoleViewModel
            {
                Id = r.Id.ToString(),
                Name = r.Name,
                IsPublic = true,
                NotificationDescription = r.NotificationDescription
            }).ToList());
        }

        //
        // GET: /Roles/Details/5
        public async Task<ActionResult> Details(string id)
        {
            var role = DbContext.UserTypes.FirstOrDefault(r => r.Id.ToString() == id);
            if (role == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.NotFound);
            }

            var users = DbContext.AspNetUsers.Join(
                    DbContext.UserProfiles.Where(up => up.UserTypeId.ToString() == id),
                    u => u.Id,
                    up => up.Id,
                    (u, up) => new {u.UserName})
                .OrderBy(u => u.UserName)
                .ToList();

            ViewBag.Users = users;
            ViewBag.UserCount = users.Count;

            Logger.Info($"{UserName} viewed Role with ID {id}");
            return View(new RoleViewModel
            {
                Id = role.Id.ToString(),
                Name = role.Name,
                IsPublic = true,
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
                DbContext.UserTypes.Add(new UserType
                {
                    Id = Guid.NewGuid(),
                    Name = roleViewModel.Name
                });

                await DbContext.SaveChangesAsync();
                
                Logger.Info($"{UserName} created Role with ID {roleViewModel.Id}");
                return RedirectToAction("Index");
            }
            return View();
        }

        //
        // GET: /Roles/Edit/Admin
        public ActionResult Edit(string id)
        {
            var role = DbContext.UserTypes.FirstOrDefault(r => r.Id.ToString() == id);
            if (role == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.NotFound);
            }
            
            var allUsers = DbContext.AspNetUsers
                .Join(
                    DbContext.UserProfiles,
                    u => u.Id,
                    up => up.Id,
                    (u, up) => new {User = u, UserProfile = up})
                .Select(uup => new SelectListItem
                {
                    Text = uup.User.UserName,
                    Value = uup.UserProfile.Id,
                    Selected = uup.UserProfile.UserTypeId.ToString() == id
                })
                .OrderBy(i => i.Text)
                .ToList();
            
            var roleModel = new RoleViewModel
            {
                Id = role.Id.ToString(),
                Name = role.Name,
                IsPublic = true,
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
                var role = DbContext.UserTypes.FirstOrDefault(r => r.Id.ToString() == roleModel.Id);
                if (role == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.NotFound);
                }
                role.Name = roleModel.Name;
                role.NotificationDescription = roleModel.NotificationDescription;

                foreach (var userProfile in DbContext.UserProfiles.Where(up => selectedUsers.Contains(up.Id)))
                {
                    userProfile.UserTypeId = role.Id;
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

            var role = await DbContext.UserTypes.FirstOrDefaultAsync(ut => ut.Id.ToString() == id);
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

                var role = await DbContext.UserTypes.FirstOrDefaultAsync(ut => ut.Id.ToString() == id);
                if (role == null)
                {
                    return HttpNotFound();
                }

                if (role.UserProfiles.Any())
                {
                    ViewBag.Error = "Cannot delete role that is assigned to users";
                    return View(role);
                }

                DbContext.UserTypes.Remove(role);
                await DbContext.SaveChangesAsync();

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
            return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
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
