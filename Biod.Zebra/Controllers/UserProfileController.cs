using System;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using Biod.Zebra.Library.Models;
using Biod.Zebra.Library.Infrastructures;
using System.Configuration;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.IO;
using System.Net;
using Biod.Zebra.Library.Models.DiseaseRelevance;
using DiseaseRelevanceViewModel = Biod.Zebra.Library.Models.DiseaseRelevance.DiseaseRelevanceViewModel;
using System.Data.Entity.Migrations;
using Biod.Zebra.Library.EntityModels.Zebra;

namespace Biod.Zebra.Controllers
{
    [Authorize]
    public class UserProfileController : BaseController
    {
        private ApplicationSignInManager _signInManager;
        private ApplicationRoleManager _roleManager;

        public UserProfileController()
        {
        }

        public UserProfileController(ApplicationUserManager userManager, ApplicationSignInManager signInManager)
        {
            UserManager = userManager;
            SignInManager = signInManager;
        }

        public ApplicationSignInManager SignInManager
        {
            get
            {
                return _signInManager ?? HttpContext.GetOwinContext().Get<ApplicationSignInManager>();
            }
            private set
            {
                _signInManager = value;
            }
        }
        
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
        // GET: /UserProfile/PersonalDetails
        public async Task<ActionResult> PersonalDetails(ManageMessageId? message)
        {
            ViewBag.StatusMessage =
                message == ManageMessageId.Success ? "Your profile details have been updated."
                : "";
            var userId = User.Identity.GetUserId();
            var user = UserManager.FindById(userId);

            var personalDetails = new PersonalDetailsViewModel
            {
                FirstName = user.FirstName,
                LastName = user.LastName,
                Organization = user.Organization,
                Location = user.Location,
                GeonameId = user.GeonameId,
                GridId = user.GridId,
                Email = user.Email,
                PhoneNumber = user.PhoneNumber
            };

            var customRoleFilter = new CustomRolesFilter(DbContext);
            var userRoleList = await UserManager.GetRolesAsync(user.Id);
            personalDetails.Role = customRoleFilter.GetFirstPublicRole(userRoleList);
            personalDetails.RolesList = customRoleFilter.GetPublicRoleOptions();

            return View(personalDetails);
        }

        //
        // POST: /UserProfile/PersonalDetails
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> PersonalDetails([Bind(Include = "FirstName,LastName,Role,RoleList,Organization,Location,GeonameId,GridId,Email,PhoneNumber")] PersonalDetailsViewModel model)
        {
            var userId = User.Identity.GetUserId();
            var user = UserManager.FindById(userId);

            var customRolesFilter = new CustomRolesFilter(DbContext);
            
            model.RolesList = customRolesFilter.GetPublicRoleOptions();

            if (!ModelState.IsValid)
            {
                var userRoleList = await UserManager.GetRolesAsync(user.Id);
                model.Role = customRolesFilter.GetFirstPublicRole(userRoleList);
                return View(model);
            }
            else
            {
                if (user == null)
                {
                    return HttpNotFound();
                }

                BiodZebraEntities zebraDbContext = new BiodZebraEntities();
                //if (model.GeonameId == 0)
                //{
                //    model.Location = user.Location;
                //    model.GeonameId = user.GeonameId;
                //    model.RolesList = roleList;
                //    //ModelState.AddModelError("Location", "Location is required.");
                //    return RedirectToAction("PersonalDetails", "UserProfile");
                //    //return View(model);
                //}
                //else
                //{
                //    model.GridId = zebraDbContext.usp_GetZebraGridIdByGeonameId(model.GeonameId).FirstOrDefault();
                //}

                model.GridId = zebraDbContext.usp_ZebraPlaceGetGridIdByGeonameId(model.GeonameId).FirstOrDefault();

                user.UserName = model.Email;
                user.Email = model.Email;
                user.FirstName = model.FirstName;
                user.LastName = model.LastName;
                user.Organization = model.Organization;
                user.Location = model.GeonameId == user.GeonameId ? user.Location : model.Location;
                user.GeonameId = model.GeonameId;
                user.GridId = model.GridId;
                user.PhoneNumber = model.PhoneNumber;

                if (!User.IsInRole(ConfigurationManager.AppSettings.Get("PaidUsersRole")))
                {
                    user.AoiGeonameIds = model.GeonameId.ToString();
                }

                var result = await UpdateUserPublicRoleAsync(user.Id, model.Role);
                if (!result.Succeeded)
                {
                    ModelState.AddModelError("", result.Errors.First());
                    return View(model);
                }

                if (!User.IsInRole(ConfigurationManager.AppSettings.Get("PaidUsersRole")))
                {
                    await AccountHelper.PrecalculateRisk(user.Id);
                }

                Logger.Info("User successfully changed their personal details in the Account Details page");
                return RedirectToAction("PersonalDetails", "UserProfile", new { message = ManageMessageId.Success });
            }
        }


        //
        // GET: /UserProfile/CustomSettings
        public ActionResult CustomSettings(ManageMessageId? message)
        {
            ViewBag.StatusMessage = message == ManageMessageId.Success ? "Your custom settings have been changed." : "";
            var userId = User.Identity.GetUserId();
            var user = UserManager.FindById(userId);
            var userRoles = new HashSet<string>(user.Roles.Select(ur => ur.RoleId));
            
            var publicRoleNames = new HashSet<string>(new CustomRolesFilter(DbContext).GetPublicRoleOptions().Select(r => r.Value));
            var role = DbContext.AspNetRoles.Where(r => publicRoleNames.Contains(r.Name) && userRoles.Contains(r.Id)).FirstOrDefault();

            var userCustomSettings = new CustomSettingsViewModel
            {
                AoiGeonameIds = user.AoiGeonameIds,
                UserId = userId,
                DiseaseRelevanceViewModel = DiseaseRelevanceViewModel.GetDiseaseRelevanceSettingViewModel(DbContext, RoleManager),
                RoleId = role?.Id,
                RoleDescription = role?.NotificationDescription ?? ""
            };
            
            // Get the user's settings
            var diseaseNameLookup = userCustomSettings.DiseaseRelevanceViewModel.Diseases.ToDictionary(d => d.DiseaseId, d => d.DiseaseName);
            var userDiseaseRelevance = DbContext.Xtbl_User_Disease_Relevance.Where(r => r.UserId == userId).ToList();

            // Get the role's settings
            var diseaseSettingFromRole = userCustomSettings.DiseaseRelevanceViewModel.RolesMap[userCustomSettings.RoleId].DiseaseSetting;

            userCustomSettings.UserRelevanceViewModel = new RelevanceViewModel
            {
                Id = userId,
                Name = user.UserName,
                DiseaseSetting = userDiseaseRelevance.Count == 0 && userCustomSettings.RoleId != null
                    ?
                    // If there are no existing settings and they have a role, inherit from the role
                    diseaseSettingFromRole
                    :
                    // Otherwise load from settings (can be an empty dictionary)
                    userDiseaseRelevance.Where(r => diseaseNameLookup.ContainsKey(r.DiseaseId))
                        .ToDictionary(r => r.DiseaseId, r => new RelevanceViewModel.DiseaseRelevanceSettingViewModel
                        {
                            DiseaseId = r.DiseaseId,
                            DiseaseName = diseaseNameLookup[r.DiseaseId],
                            RelevanceType = r.RelevanceId,
                            State = r.StateId
                        })
            };

            // Add all entries that are in diseaseSettingFromRole but not currently in userCustomSettings.UserRelevanceViewModel.DiseaseSetting
            var diseaseSettings = userCustomSettings.UserRelevanceViewModel.DiseaseSetting;
            var missingKeys = diseaseSettingFromRole.Keys.Except(diseaseSettings.Keys);
            userCustomSettings.UserRelevanceViewModel.DiseaseSetting = diseaseSettings
                .Concat(diseaseSettingFromRole
                        .Where(s => missingKeys.Contains(s.Key))
                        .ToDictionary(s => s.Key, s => s.Value))
                .ToDictionary(e => e.Key, e => e.Value);

            return View(userCustomSettings);
        }
        
        //
        // POST: /UserProfile/CustomSettings
        [HttpPost]
        public async Task<ActionResult> CustomSettings()
        {
            string payload;
            using (var receiveStream = Request.InputStream)
            {
                using (var readStream = new StreamReader(receiveStream, Request.ContentEncoding))
                {
                    payload = readStream.ReadToEnd();
                }
            }

            var viewModel = JsonConvert.DeserializeObject<CustomSettingsViewModel>(payload);

            // Validate user exists
            var user = UserManager.FindById(viewModel.UserId);
            if (user == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest, $"Unknown user id {viewModel.UserId}");
            }

            // Validate role exists
            if (viewModel.RoleId == null || viewModel.RoleId.Length == 0)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest, $"Please select a role");
            }
            var role = RoleManager.Roles.FirstOrDefault(r => r.Id == viewModel.RoleId);
            if (role == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest, $"Unknown role id {viewModel.RoleId}");
            }

            // Update user info
            if (viewModel.AoiGeonameIds == null || viewModel.AoiGeonameIds.Count() == 0)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest, $"Please enter at least one location");
            }
            user.AoiGeonameIds = viewModel.AoiGeonameIds;
            var result = await UserManager.UpdateAsync(user);
            if (!result.Succeeded)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest, $"Failed to update AOI to {viewModel.AoiGeonameIds}");
            }
            if (user.AoiGeonameIds.Length > 0)
            {
                await AccountHelper.PrecalculateRisk(user.Id);
            }

            var roleName = RoleManager.FindById(viewModel.RoleId).Name;
            result = await UpdateUserPublicRoleAsync(user.Id, roleName);
            if (!result.Succeeded)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest, $"Failed to update user's role to {roleName}");
            }

            if (viewModel.RolePresetSelected)
            {
                // Remove all entries for the user in User_Disease_Relevance cross table if preset is selected
                var entries = DbContext.Xtbl_User_Disease_Relevance.RemoveRange(DbContext.Xtbl_User_Disease_Relevance.Where(u => u.UserId == viewModel.UserId));
            }
            else
            {
                // Otherwise, add an entry for each disease into the User_Disease_Relevance cross table
                foreach (var diseaseSetting in viewModel.UserRelevanceViewModel.DiseaseSetting.Values)
                {
                    DbContext.Xtbl_User_Disease_Relevance.AddOrUpdate(new Xtbl_User_Disease_Relevance
                    {
                        DiseaseId = diseaseSetting.DiseaseId,
                        RelevanceId = diseaseSetting.RelevanceType,
                        UserId = viewModel.UserId,
                        StateId = 1 // Default, in the future, need to keep existing state
                    });
                }
            }
            DbContext.SaveChanges();

            Logger.Info("User successfully changed their custom settings in the Custom Settings page");
            return RedirectToAction("CustomSettings", "UserProfile", new { message = ManageMessageId.Success });
        }
            

        //
        // GET: /UserProfile/UserNotification
        public ActionResult UserNotification(ManageMessageId? message)
        {
            ViewBag.StatusMessage =
                message == ManageMessageId.Success ? "Your notification settings have been changed."
                : "";
            var userId = User.Identity.GetUserId();
            var user = UserManager.FindById(userId);
            var userNotification = new UserNotificationViewModel()
            {
                AoiGeonameIds = user.AoiGeonameIds,
                SmsNotificationEnabled = user.SmsNotificationEnabled,
                NewCaseNotificationEnabled = (!User.IsInRole(ConfigurationManager.AppSettings.Get("PaidUsersRole"))) ? false : user.NewCaseNotificationEnabled,
                NewOutbreakNotificationEnabled = user.NewOutbreakNotificationEnabled,
                WeeklyOutbreakNotificationEnabled = user.WeeklyOutbreakNotificationEnabled
            };

            return View(userNotification);
        }

        //
        // POST: /UserProfile/UserNotification
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> UserNotification(UserNotificationViewModel model)
        {
            if (ModelState.IsValid)
            {
                var userId = User.Identity.GetUserId();
                var user = await UserManager.FindByIdAsync(userId);
                if (user == null)
                {
                    return HttpNotFound();
                }

                user.AoiGeonameIds = model.AoiGeonameIds;
                user.NewCaseNotificationEnabled = model.NewCaseNotificationEnabled;
                user.SmsNotificationEnabled = model.SmsNotificationEnabled;
                user.NewOutbreakNotificationEnabled = model.NewOutbreakNotificationEnabled;
                user.WeeklyOutbreakNotificationEnabled = model.WeeklyOutbreakNotificationEnabled;
                var result = await UserManager.UpdateAsync(user);
                if (!result.Succeeded)
                {
                    ModelState.AddModelError("", result.Errors.First());
                    return View(model);
                }

                Logger.Info("User successfully updated their notification settings in the Notifications page");
                return RedirectToAction("UserNotification", "UserProfile", new { message = ManageMessageId.Success });
            }
            else
            {
                return View(model);
            }
        }




        //
        // GET: /UserProfile/ChangePassword
        public ActionResult ChangePassword(ManageMessageId? message)
        {
            ViewBag.StatusMessage =
                message == ManageMessageId.Success ? "Your password has been changed."
                : "";

            return View();
        }

        //
        // POST: /UserProfile/ChangePassword
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> ChangePassword(ChangePasswordViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            var result = await UserManager.ChangePasswordAsync(User.Identity.GetUserId(), model.OldPassword, model.NewPassword);
            if (result.Succeeded)
            {
                var user = await UserManager.FindByIdAsync(User.Identity.GetUserId());
                if (user != null)
                {
                    await SignInManager.SignInAsync(user, isPersistent: false, rememberBrowser: false);
                }

                Logger.Info("User successfully changed their password in the Change Password page");
                return RedirectToAction("ChangePassword", "UserProfile", new { message = ManageMessageId.Success }); ;
                //return RedirectToAction("Index", new { Message = ManageMessageId.ChangePasswordSuccess });
            }
            AddErrors(result);
            return View(model);
        }

        public ActionResult UpdateDiseaseRelevance(string inputJson = "")
        {
            //valid input
            //inputJson = "{\"userId\": \"a4f5aa5a-0fe4-4579-8d99-f5c78c6e176f\", \"roleId\": \"\", \"diseaseRelevanceJson\": [{ \"diseaseId\": 1, \"relevanceId\": 1, \"stateId\": 1 },{ \"diseaseId\": 2, \"relevanceId\": 1, \"stateId\": 1 }]}"

            //invalid input
            //inputJson = "{\"userId\": \"test user\", \"roleId\": \"test role\", \"diseaseRelevanceJson\": [{ \"diseaseId\": 26, \"relevanceId\": 1, \"stateId\": 1 },{ \"diseaseId\": 55, \"relevanceId\": 1, \"stateId\": 1 }]}"

            var result = JsonStringResultClass.PostJsonStringResultAsync(
                    ConfigurationManager.AppSettings.Get("ZebraApiBaseUrl"),
                    "/api/ZebraDiseaseRelevance",
                    ConfigurationManager.AppSettings.Get(@"ZebraApiUserName"),
                    ConfigurationManager.AppSettings.Get("ZebraApiPassword"),
                    inputJson
                    ).Result;

            if (!String.IsNullOrEmpty(result))
            {
                Logger.Info($"Updated disease relevance cross table");
                return Content("Success");
            }
            else {
                Logger.Info($"Failed to update disease relevance cross table");
                return Content("Failed");
            }
        }

        protected override void Dispose(bool disposing)
        {
            if (RoleManager != null)
            {
                RoleManager.Dispose();
                RoleManager = null;
            }
            
            base.Dispose(disposing);
        }

        #region Helpers
        // Used for XSRF protection when adding external logins
        private const string XsrfKey = "XsrfId";

        private IAuthenticationManager AuthenticationManager
        {
            get
            {
                return HttpContext.GetOwinContext().Authentication;
            }
        }

        private void AddErrors(IdentityResult result)
        {
            foreach (var error in result.Errors)
            {
                ModelState.AddModelError("", error);
            }
        }

        private bool HasPassword()
        {
            var user = UserManager.FindById(User.Identity.GetUserId());
            if (user != null)
            {
                return user.PasswordHash != null;
            }
            return false;
        }

        private bool HasPhoneNumber()
        {
            var user = UserManager.FindById(User.Identity.GetUserId());
            if (user != null)
            {
                return user.PhoneNumber != null;
            }
            return false;
        }


        private async Task<IdentityResult> UpdateUserPublicRoleAsync(string userId, string modelId)
        {
            var userRoles = await UserManager.GetRolesAsync(userId);
            var publicRoles = new CustomRolesFilter(DbContext).FilterOutPrivateRoles(userRoles).ToArray();
            var result = await UserManager.RemoveFromRolesAsync(userId, publicRoles);

            if (!result.Succeeded)
            {
                return result;
            }

            return await UserManager.AddToRolesAsync(userId, modelId);
        }

        public enum ManageMessageId
        {
            Success
        }
        #endregion
    }
}