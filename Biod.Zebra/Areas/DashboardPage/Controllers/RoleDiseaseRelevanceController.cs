using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Biod.Zebra.Controllers;
using Biod.Zebra.Library.EntityModels;
using Biod.Zebra.Library.Models.DiseaseRelevance;
using Microsoft.AspNet.Identity.Owin;
using Newtonsoft.Json;

namespace Biod.Zebra.Library.Controllers
{
    [Authorize(Roles = "Admin")]
    public class RoleDiseaseRelevanceController : BaseController
    {
        private ApplicationRoleManager _roleManager;
        public ApplicationRoleManager RoleManager
        {
            get => _roleManager ?? HttpContext.GetOwinContext().Get<ApplicationRoleManager>();
            private set => _roleManager = value;
        }

        // GET: DashboardPage/RoleDiseaseRelevance
        public ActionResult Index()
        {
            return View(DiseaseRelevanceViewModel.GetDiseaseRelevanceAdminViewModel(DbContext, RoleManager));
        }
        
        // POST: DashboardPage/RoleDiseaseRelevance
        [HttpPost]
        public ActionResult Update()
        {
            string payload;
            using (var receiveStream = Request.InputStream)
            {
                using (var readStream = new StreamReader(receiveStream, Request.ContentEncoding))
                {
                    payload = readStream.ReadToEnd();
                }
            }
            
            var viewModel = JsonConvert.DeserializeObject<RelevanceViewModel>(payload);
            
            // Validate role exists
            var role = RoleManager.Roles.FirstOrDefault(r => r.Id == viewModel.Id);
            if (role == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest, $"Unknown role id {viewModel.Id}");
            }

            // Validate diseases exists
            var diseases = DbContext.usp_ZebraDashboardGetDiseases();
            var diseaseIds = new HashSet<int>(diseases.Select(d => d.DiseaseId));
            var unknownDiseaseIds = viewModel.DiseaseSetting.Values.Where(d => !diseaseIds.Contains(d.DiseaseId)).Select(d => d.DiseaseId).ToArray();
            if (unknownDiseaseIds.Any())
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest, $"Unknown disease ids {string.Join(",", unknownDiseaseIds)}");
            }
            
            // Validate Relevance Types
            var relevanceTypes = new HashSet<int>(DbContext.RelevanceTypes.Select(r => r.RelevanceId));
            var unknownRelevanceTypes = new HashSet<int>(viewModel.DiseaseSetting.Values.Select(d => d.RelevanceType)).Except(relevanceTypes).ToArray();
            if (unknownRelevanceTypes.Any())
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest, $"Unknown relevance types {string.Join(",", unknownRelevanceTypes)}");
            }

            foreach (var diseaseSetting in viewModel.DiseaseSetting.Values)
            {
                DbContext.Xtbl_Role_Disease_Relevance.AddOrUpdate(new Xtbl_Role_Disease_Relevance
                {
                    DiseaseId = diseaseSetting.DiseaseId,
                    RelevanceId = diseaseSetting.RelevanceType,
                    RoleId = viewModel.Id,
                    StateId = 1 // Default, in the future, need to keep existing state
                });
            }

            DbContext.SaveChanges();

            return new HttpStatusCodeResult(HttpStatusCode.OK);
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
    }
}