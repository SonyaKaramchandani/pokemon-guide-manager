using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web.Mvc;
using Biod.Zebra.Library.EntityModels;
using Biod.Zebra.Controllers;
using Biod.Zebra.Library.Models.DiseaseRelevance;

namespace Biod.Zebra.Library.Controllers
{
    [Authorize(Roles = "Admin")]
    public class DiseaseGroupController : BaseController
    {
        //
        // GET: /DiseaseGroup/
        public ActionResult Index()
        {
            var diseaseGroups = DbContext.CustomGroups.Include(dg => dg.Xtbl_Disease_CustomGroup).ToList();
            
            Logger.Info($"Loaded Admin page for list of Disease Groups");
            return View(diseaseGroups.Select(dg => new DiseaseGroupViewModel()
            {
                Id = dg.GroupId,
                Name = dg.GroupName
            }).OrderBy(g => g.Name));
        }

        //
        // GET: /DiseaseGroup/Details/5
        public ActionResult Details(int id)
        {
            var diseaseGroup = DbContext.CustomGroups.Include(dg => dg.Xtbl_Disease_CustomGroup).FirstOrDefault(dg => dg.GroupId == id);
            if (diseaseGroup == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var diseaseLookUp = DbContext.usp_ZebraDashboardGetDiseases().ToDictionary(d => d.DiseaseId, d => d.DiseaseName);

            Logger.Info($"{UserName} viewed Disease Group with ID {id}");
            return View(new DiseaseGroupViewModel
            {
                Id = diseaseGroup.GroupId,
                Name = diseaseGroup.GroupName,
                DiseaseList = diseaseGroup.Xtbl_Disease_CustomGroup
                    .Where(d => diseaseLookUp.ContainsKey(d.DiseaseId))
                    .Select(d => new SelectListItem
                    {
                       Text = diseaseLookUp[d.DiseaseId],
                       Value = d.DiseaseId.ToString()
                    })
                    .OrderBy(d => d.Text)
                    .ToList()
            });
        }

        //
        // GET: /DiseaseGroup/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: /DiseaseGroup/Create
        [HttpPost]
        public async Task<ActionResult> Create(DiseaseGroupViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                var group = DbContext.CustomGroups.Add(new CustomGroup
                {
                    GroupName = viewModel.Name
                });
                await DbContext.SaveChangesAsync();

                Logger.Info($"{UserName} created Disease Group with ID {group.GroupId}");
                return RedirectToAction("Index");
            }
            return View();
        }

        //
        // GET: /DiseaseGroup/Edit/1
        public ActionResult Edit(int id)
        {
            var diseaseGroup = DbContext.CustomGroups.Include(dg => dg.Xtbl_Disease_CustomGroup).FirstOrDefault(dg => dg.GroupId == id);
            if (diseaseGroup == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var diseaseIdsInGroup = new HashSet<int>(diseaseGroup.Xtbl_Disease_CustomGroup.Select(d => d.DiseaseId));
            var allDiseases = DbContext.usp_ZebraDashboardGetDiseases().OrderBy(d => d.DiseaseName).ToList();

            return View(new DiseaseGroupViewModel
            {
                Id = diseaseGroup.GroupId,
                Name = diseaseGroup.GroupName,
                DiseaseList = allDiseases.Select(d => new SelectListItem()
                {
                    Selected = diseaseIdsInGroup.Contains(d.DiseaseId),
                    Text = d.DiseaseName,
                    Value = d.DiseaseId.ToString()
                })
            });
        }

        //
        // POST: /DiseaseGroup/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "Name,Id")] DiseaseGroupViewModel viewModel, params int[] selectedDiseases)
        {
            if (ModelState.IsValid)
            {
                var diseaseGroup = DbContext.CustomGroups.Include(dg => dg.Xtbl_Disease_CustomGroup).FirstOrDefault(dg => dg.GroupId == viewModel.Id);
                if (diseaseGroup == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.NotFound);
                }
                diseaseGroup.GroupName = viewModel.Name;
                diseaseGroup.Xtbl_Disease_CustomGroup.Clear();

                foreach (var diseaseId in selectedDiseases)
                {
                    diseaseGroup.Xtbl_Disease_CustomGroup.Add(new Xtbl_Disease_CustomGroup
                    {
                        DiseaseId = diseaseId,
                        GroupId = viewModel.Id
                    });
                }
                await DbContext.SaveChangesAsync();

                Logger.Info($"{UserName} edited Disease Group with ID {viewModel.Id}");
                return RedirectToAction("Details", new { id = viewModel.Id });
            }
            return View();
        }

        //
        // GET: /DiseaseGroup/Delete/5
        public ActionResult Delete(int id)
        {
            var diseaseGroup = DbContext.CustomGroups.FirstOrDefault(dg => dg.GroupId == id);
            if (diseaseGroup == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            return View(new DiseaseGroupViewModel
            {
                Id = diseaseGroup.GroupId,
                Name = diseaseGroup.GroupName
            });
        }

        //
        // POST: /DiseaseGroup/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            if (ModelState.IsValid)
            {
                var diseaseGroup = DbContext.CustomGroups.FirstOrDefault(dg => dg.GroupId == id);
                if (diseaseGroup == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.NotFound);
                }

                DbContext.Xtbl_Disease_CustomGroup.RemoveRange(DbContext.Xtbl_Disease_CustomGroup.Where(g => g.GroupId == id));
                DbContext.CustomGroups.Remove(diseaseGroup);
                await DbContext.SaveChangesAsync();

                Logger.Info($"{UserName} deleted Disease Group with ID {id}");
                return RedirectToAction("Index");
            }
            return View();
        }

        //
        // POST: /DiseaseGroup/Unassign
        [HttpPost]
        public async Task<ActionResult> Unassign(int groupId, string diseaseId)
        {
            var assignment = DbContext.Xtbl_Disease_CustomGroup.FirstOrDefault(g => g.DiseaseId.ToString() == diseaseId && g.GroupId == groupId);
            if (assignment == null)
            {
                // Assignment already doesn't exist
                return new HttpStatusCodeResult(HttpStatusCode.OK);
            }

            DbContext.Xtbl_Disease_CustomGroup.Remove(assignment);
            await DbContext.SaveChangesAsync();

            Logger.Info($"{UserName} unassigned user ID {diseaseId} from Disease Group with ID {groupId}");
            return new HttpStatusCodeResult(HttpStatusCode.OK);
        }
    }
}
