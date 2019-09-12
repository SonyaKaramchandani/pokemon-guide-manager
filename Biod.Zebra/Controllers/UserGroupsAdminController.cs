using Biod.Zebra.Library.EntityModels;
using Biod.Zebra.Library.Models;
using Microsoft.AspNet.Identity.Owin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace Biod.Zebra.Controllers
{
    [Authorize(Roles = "Admin")]
    public class UserGroupsAdminController : BaseController
    {
        public UserGroupsAdminController() { }

        public UserGroupsAdminController(ApplicationUserManager userManager,
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


        // GET: /UserGroupsAdmin
        public ActionResult Index()
        {
            Logger.Info($"Loaded Admin page for list of User Groups");
            return View(DbContext.UserGroups.OrderBy(ug => ug.Name).ToList());
        }

        //
        // GET: /UserGroupsAdmin/Details/2
        public ActionResult Details(int id)
        {
            var userGroup = DbContext.UserGroups.FirstOrDefault(ug => ug.Id == id);
            if (userGroup == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.NotFound);
            }

            // Get the list of Users in this UserGroup
            var users = UserManager.Users.AsEnumerable()
                .Where(u => u.UserGroupId == userGroup.Id)
                .Select(u => new UserViewModel()
                {
                    Id = u.Id,
                    UserName = u.UserName
                })
                .OrderBy(u => u.UserName)
                .ToList();

            ViewBag.Users = users;
            ViewBag.UserCount = users.Count();

            Logger.Info($"{UserName} viewed User Group with ID {id}");
            return View(userGroup);
        }

        //
        // GET: /UserGroupsAdmin/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: /UserGroupsAdmin/Create
        [HttpPost]
        public async Task<ActionResult> Create(UserGroupViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                var group = DbContext.UserGroups.Add(new UserGroup()
                {
                    Name = viewModel.Name
                });
                await DbContext.SaveChangesAsync();

                Logger.Info($"{UserName} created UserGroup with ID {group.Id}");
                return RedirectToAction("Index");
            }
            return View();
        }

        //
        // GET: /UserGroupsAdmin/Edit/1
        public ActionResult Edit(int id)
        {
            var userGroup = DbContext.UserGroups.FirstOrDefault(ug => ug.Id == id);
            if (userGroup == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.NotFound);
            }
            var allUsers = UserManager.Users.ToList()
                .Where(u => u.UserGroupId == userGroup.Id || u.UserGroupId == null)
                .Select(u => new UserViewModel()
                {
                    Id = u.Id,
                    UserName = u.UserName,
                    UserGroupId = u.UserGroupId
                })
                .OrderBy(u => u.UserName)
                .ToList();

            return View(new UserGroupViewModel()
            {
                Id = userGroup.Id,
                Name = userGroup.Name,
                UsersList = allUsers.Select(u => new SelectListItem()
                {
                    Selected = u.UserGroupId == userGroup.Id,
                    Text = u.UserName,
                    Value = u.Id
                })
            });
        }

        //
        // POST: /UserGroupsAdmin/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "Name,Id")] UserGroupViewModel viewModel, params string[] selectedUsers)
        {
            if (ModelState.IsValid)
            {
                var userGroup = DbContext.UserGroups.FirstOrDefault(ug => ug.Id == viewModel.Id);
                if (userGroup == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.NotFound);
                }
                userGroup.Name = viewModel.Name;
                await DbContext.SaveChangesAsync();

                foreach (var user in UserManager.Users.ToList())
                {
                    if (user.UserGroupId == userGroup.Id && (selectedUsers == null || !selectedUsers.Contains(user.Id)))
                    {
                        // User has been deselected from this user group
                        user.UserGroupId = null;
                        await UserManager.UpdateAsync(user);
                    }
                    else if (user.UserGroupId == null && selectedUsers != null && selectedUsers.Contains(user.Id))
                    {
                        // User that was previously unassigned, now assigned to this user group
                        user.UserGroupId = userGroup.Id;
                        await UserManager.UpdateAsync(user);
                    }
                }

                Logger.Info($"{UserName} edited User Group with ID {viewModel.Id}");
                return RedirectToAction("Details", new { id = viewModel.Id });
            }
            return View();
        }

        //
        // GET: /UserGroupsAdmin/Delete/5
        public ActionResult Delete(int id)
        {
            var userGroup = DbContext.UserGroups.FirstOrDefault(ug => ug.Id == id);
            if (userGroup == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.NotFound);
            }

            return View(new UserGroupViewModel()
            {
                Id = userGroup.Id,
                Name = userGroup.Name
            });
        }

        //
        // POST: /UserGroupsAdmin/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            if (ModelState.IsValid)
            {
                var userGroup = DbContext.UserGroups.FirstOrDefault(ug => ug.Id == id);
                if (userGroup == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.NotFound);
                }

                foreach (var user in UserManager.Users.ToList())
                {
                    if (user.UserGroupId == userGroup.Id)
                    {
                        user.UserGroupId = null;
                        await UserManager.UpdateAsync(user);
                    }
                }

                DbContext.UserGroups.Remove(userGroup);
                await DbContext.SaveChangesAsync();

                Logger.Info($"{UserName} deleted User Group with ID {id}");
                return RedirectToAction("Index");
            }
            return View();
        }

        //
        // POST: /UserGroupsAdmin/Unassign
        [HttpPost]
        public async Task<ActionResult> Unassign(int groupId, string userId)
        {
            var userGroup = DbContext.UserGroups.FirstOrDefault(ug => ug.Id == groupId);
            var user = await UserManager.FindByIdAsync(userId);

            if (userGroup == null || user == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.NotFound);
            }

            if (user.UserGroupId == userGroup.Id)
            {
                user.UserGroupId = null;
                await UserManager.UpdateAsync(user);
            }

            Logger.Info($"{UserName} unassigned user ID {userId} from User Group with ID {groupId}");
            return new HttpStatusCodeResult(HttpStatusCode.OK);
        }
    }
}