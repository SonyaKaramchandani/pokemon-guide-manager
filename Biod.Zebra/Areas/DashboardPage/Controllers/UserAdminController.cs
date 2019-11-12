using Microsoft.AspNet.Identity.Owin;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Biod.Zebra.Library.Models;
using Biod.Zebra.Controllers;
using Biod.Zebra.Library.Infrastructures;
using Biod.Zebra.Library.EntityModels.Zebra;

namespace Biod.Zebra.Library.Controllers
{
    [Authorize(Roles = "Admin")]
    public class UserAdminController : BaseController
    {
        private readonly CustomRolesFilter _customRolesFilter;
        
        public UserAdminController()
        {
            _customRolesFilter = new CustomRolesFilter(DbContext);
        }

        public UserAdminController(ApplicationUserManager userManager, ApplicationRoleManager roleManager)
        {
            UserManager = userManager;
            RoleManager = roleManager;
            _customRolesFilter = new CustomRolesFilter(DbContext);
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
        // GET: /Users/
        public async Task<ActionResult> Index()
        {
            List<UserGroup> userGroups = await DbContext.UserGroups.ToListAsync();
            List<ApplicationUser> users = await UserManager.Users.ToListAsync();
            List<usp_GetFirstLoginDateByUser_Result> firstLoginDates = DbContext.usp_GetFirstLoginDateByUser(null).ToList();
            var allRoles = RoleManager.Roles.ToList();

            Logger.Info($"Loaded Admin page for list of Users");
            return View(users.Select(u => new UserViewModel()
            {
                Id = u.Id,
                UserName = u.UserName,
                FirstName = u.FirstName,
                LastName = u.LastName,
                Organization = u.Organization,
                Location = u.Location,
                PhoneNumber = u.PhoneNumber,
                UserGroup = u.UserGroupId == null ? null : userGroups.FirstOrDefault(ug => ug.Id == u.UserGroupId)?.Name,
                DoNotTrackEnabled = u.DoNotTrackEnabled,
                EmailConfirmed = u.EmailConfirmed,
                AoiGeonameIds = u.AoiGeonameIds,
                UserGroupId = u.UserGroupId,
                RoleNames = string.Join(",", u.Roles.Select(r => allRoles.FirstOrDefault(ar => ar.Id.Equals(r.RoleId))?.Name)),
                CreationDate = firstLoginDates.FirstOrDefault(l => l.Id == u.Id)?.FirstLoginDate?.ToString("yyyy-MM-ddTHH:mm:ss"),
                NewCaseNotificationEnabled = u.NewCaseNotificationEnabled,
                NewOutbreakNotificationEnabled = u.NewOutbreakNotificationEnabled,
                PeriodicNotificationEnabled = u.PeriodicNotificationEnabled,
                WeeklyOutbreakNotificationEnabled = u.WeeklyOutbreakNotificationEnabled
            }).OrderBy(u => u.UserName));
        }

        //
        // GET: /Users/Details/5
        public async Task<ActionResult> Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var user = await UserManager.FindByIdAsync(id);

            ViewBag.RoleNames = await UserManager.GetRolesAsync(user.Id);

            Logger.Info($"{UserName} viewed User with ID {id}");
            return View(user);
        }

        //
        // GET: /Users/Create
        public ActionResult Create()
        {
            //Get the list of Roles
            ViewBag.Roles = _customRolesFilter.GetPublicRoleOptions().ToArray();
            return View();
        }

        //
        // POST: /Users/Create
        [HttpPost]
        [Obsolete("This API was used in MVC based registration and is now deprecated. Use the UserController WebAPI instead.")]
        public async Task<ActionResult> Create(RegisterViewModel userViewModel, params string[] selectedRoles)
        {
            if (ModelState.IsValid)
            {
                BiodZebraEntities zebraDbContext = new BiodZebraEntities();
                userViewModel.GridId = zebraDbContext.usp_ZebraPlaceGetGridIdByGeonameId(userViewModel.GeonameId).FirstOrDefault();
                var user = new ApplicationUser
                {
                    UserName = userViewModel.Email,
                    Email = userViewModel.Email,
                    FirstName = userViewModel.FirstName,
                    LastName = userViewModel.LastName,
                    Location = userViewModel.Location,
                    GeonameId = userViewModel.GeonameId,
                    GridId = userViewModel.GridId,
                    Organization = userViewModel.Organization,
                    PhoneNumber = userViewModel.PhoneNumber
                };
                var adminresult = await UserManager.CreateAsync(user, userViewModel.Password);
                //var adminresult = await UserManager.CreateAsync(user, userViewModel.Password ?? ConfigurationManager.AppSettings.Get("GenericPassword"));

                //Add User to the selected Roles 
                if (adminresult.Succeeded)
                {
                    if (selectedRoles != null)
                    {
                        var result = await UserManager.AddToRolesAsync(user.Id, selectedRoles);
                        if (!result.Succeeded)
                        {
                            ModelState.AddModelError("", result.Errors.First());
                            ViewBag.RoleId = new SelectList(await RoleManager.Roles.ToListAsync(), "Name", "Name");

                            Logger.Info($"{UserName} created User with user name {user.UserName}");
                            return View();
                        }
                    }
                }
                else
                {
                    ModelState.AddModelError("", adminresult.Errors.First());
                    ViewBag.RoleId = new SelectList(RoleManager.Roles, "Name", "Name");
                    return View();

                }
                return RedirectToAction("Index");
            }
            ViewBag.RoleId = new SelectList(RoleManager.Roles, "Name", "Name");
            return View();
        }

        //
        // GET: /Users/Edit/1
        public async Task<ActionResult> Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var user = await UserManager.FindByIdAsync(id);
            if (user == null)
            {
                return HttpNotFound();
            }

            var userRoles = await UserManager.GetRolesAsync(user.Id);
            ViewBag.UserGroups = DbContext.UserGroups.ToList();

            return View(new EditUserViewModel()
            {
                Id = user.Id,
                Email = user.Email,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Location = user.Location,
                GeonameId = user.GeonameId,
                Organization = user.Organization,
                PhoneNumber = user.PhoneNumber,
                RolesList = RoleManager.Roles.ToList().Select(x => new SelectListItem()
                {
                    Selected = userRoles.Contains(x.Name),
                    Text = x.Name,
                    Value = x.Name
                }),
                UserGroupId = user.UserGroupId,
                DoNotTrackEnabled = user.DoNotTrackEnabled,
                EmailConfirmed = user.EmailConfirmed
            });
        }

        //
        // POST: /Users/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "Email,Id,FirstName,LastName,Location,GeonameId,GridId,Organization,PhoneNumber,UserGroupId,UserGroup,DoNotTrackEnabled,EmailConfirmed")] EditUserViewModel editUser, params string[] selectedRole)
        {
            if (ModelState.IsValid)
            {
                var user = await UserManager.FindByIdAsync(editUser.Id);
                if (user == null)
                {
                    return HttpNotFound();
                }

                user.UserName = editUser.Email;
                user.Email = editUser.Email;
                user.FirstName = editUser.FirstName;
                user.LastName = editUser.LastName;
                user.Organization = editUser.Organization;
                user.PhoneNumber = editUser.PhoneNumber;
                user.Location = editUser.Location;
                user.GeonameId = editUser.GeonameId;
                user.UserGroupId = editUser.UserGroupId;
                user.DoNotTrackEnabled = editUser.DoNotTrackEnabled;
                user.EmailConfirmed = editUser.EmailConfirmed;

                BiodZebraEntities zebraDbContext = new BiodZebraEntities();
                user.GridId = zebraDbContext.usp_ZebraPlaceGetGridIdByGeonameId(editUser.GeonameId).FirstOrDefault();//editUser.GridId;

                var userRoles = await UserManager.GetRolesAsync(user.Id);
                selectedRole = selectedRole ?? new string[] { };
                editUser.RolesList = RoleManager.Roles.ToList().Select(x => new SelectListItem()
                {
                    Selected = selectedRole.Contains(x.Name),
                    Text = x.Name,
                    Value = x.Name
                });

                if (!_customRolesFilter.FilterOutPrivateRoles(selectedRole.ToList()).Any())
                {
                    ModelState.AddModelError("", "Must provide a public role.");
                    return View(editUser);
                }

                var result = await UserManager.AddToRolesAsync(user.Id, selectedRole.Except(userRoles).ToArray());

                if (!result.Succeeded)
                {
                    ModelState.AddModelError("", result.Errors.First());
                    return View(editUser);
                }
                result = await UserManager.RemoveFromRolesAsync(user.Id, userRoles.Except(selectedRole).ToArray());

                if (!result.Succeeded)
                {
                    ModelState.AddModelError("", result.Errors.First());
                    return View(editUser);
                }

                Logger.Info($"{UserName} edited User with ID {editUser.Id}");
                return RedirectToAction("Index");
            }
            ModelState.AddModelError("", "Something failed.");
            return View();
        }

        //
        // GET: /Users/Delete/5
        public async Task<ActionResult> Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var user = await UserManager.FindByIdAsync(id);
            if (user == null)
            {
                return HttpNotFound();
            }
            return View(user);
        }

        //
        // POST: /Users/Delete/5
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

                var user = await UserManager.FindByIdAsync(id);
                if (user == null)
                {
                    return HttpNotFound();
                }
                var result = await UserManager.DeleteAsync(user);
                if (!result.Succeeded)
                {
                    ModelState.AddModelError("", result.Errors.First());
                    return View();
                }

                Logger.Info($"{UserName} deleted User with ID {id}");
                return RedirectToAction("Index");
            }
            return View();
        }
    }
}
