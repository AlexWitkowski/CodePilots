using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using EASNSMVC4.Models;
using EASNSMVC4.DAL;
using System.Net;
using EASNSMVC4.ViewModels;
using EASNSMVC4.Classes;
using System.IO;
using System.Web.Security;

namespace EASNSMVC4.Controllers
{
    public class ResourceController : Controller
    {
        private DBEntity db = new DBEntity();

        //
        // GET: /Resource/

        public ActionResult Index(string sortOrder, string searchString)
        {
            ViewBag.TitleSortParm = (sortOrder == "title") ? "title_Desc" : "title";
            ViewBag.DateSortParm = (sortOrder == "date") ? "date_Desc" : "date";
            ViewBag.DownloadSortParm = (String.IsNullOrEmpty(sortOrder)) ? "download_Desc" : "";

            if(User.Identity.IsAuthenticated)
            {
                string[] rolesArray;
                RolePrincipal r = (RolePrincipal)User;
                rolesArray = r.GetRoles();
                var userRoles = Roles.GetRolesForUser();
            }

            var resources = db.Resources.Include(r => r.FileType).Include(r => r.ActiveState).Where(r => r.ActiveStateID.Equals(2));//.Where(r => r.Stakeholders = Roles.GetRolesForUser());
            if (User.IsInRole("Admin"))
            {
                resources = db.Resources.Include(r => r.FileType).Include(r => r.ActiveState);
            }

            if (!String.IsNullOrEmpty(searchString))
            {
                return View(resources.Where(r => r.Title.ToUpper().Contains(searchString.ToUpper())));
            }

            if (sortOrder == "title_Desc")
            {
                return View(resources.OrderByDescending(r => r.Title).ThenBy(r => r.CreatedOn).ToList());
            }

            if (sortOrder == "date")
            {
                return View(resources.OrderBy(r => r.CreatedOn).ThenBy(r => r.Title).ToList());
            }

            if (sortOrder == "date_Desc")
            {
                return View(resources.OrderByDescending(r => r.CreatedOn).ThenBy(r => r.Title).ToList());
            }
            if (sortOrder == "title")
            {
                return View(resources.OrderBy(r => r.Title).ThenByDescending(r => r.NumDownloads).ToList());
            }
            if (sortOrder == "download_Desc")
            {
                return View(resources.OrderBy(r => r.NumDownloads).ThenBy(r => r.Title).ToList());
            }

            else
            {
                return View(resources.OrderByDescending(r => r.NumDownloads).ThenBy(r => r.Title).ThenBy(r => r.CreatedOn).ToList());
            }
        }

        //
        // GET: /Resource/Details/5

        public ActionResult Details(int id = 0)
        {
            Resource resource = db.Resources.Find(id);
            if (resource == null)
            {
                return HttpNotFound();
            }
            return View(resource);
        }

        public ActionResult Download(string path)
        {
            Resource resource = db.Resources.SingleOrDefault(r => r.FilePath == path);
                    if (resource != null)
                    {
                        //build dl link
                        string name = path.Split('/').ToList<string>().Last();
                        string url = Url.Content(path);

                        //count DL
                        int numDLNum = Convert.ToInt32(resource.NumDownloads) + 1;
                        resource.NumDownloads = numDLNum;
                        db.Entry(resource).State = EntityState.Modified;
                        db.SaveChanges();

                        //return file
                        return File(url, System.Net.Mime.MediaTypeNames.Application.Octet, name);
                    }
                    else
                    {
                        return RedirectToAction("Index");
                    }

        }

        //
        // GET: /Resource/Create
        [Authorize]
        public ActionResult Create()
        {
            //create new resource
            var resource = new Resource();

            //generate text boxes.
            resource.Disabilities = new List<Disability>();
            resource.Stakeholders = new List<Stakeholder>();

            //generate drop down lists.
            ViewBag.FileTypeID = new SelectList(db.FileTypes, "ID", "Desc");
            ViewBag.ActiveStateID = new SelectList(db.ActiveStates, "ID", "Desc");

            //populate check boxes.
            PopulateAssignedDisabilityData(resource);
            PopulateAssignedStakeholderData(resource);


            return View();
        }

        //
        // POST: /Resource/Create
        [Authorize]
        [HttpPost]
        public ActionResult Create(Resource resource, string[] selectedDisabilities, string[] selectedStakeholders)
        {
            //check selected checkboxes based on the database.
            if (selectedDisabilities != null)
            {
                resource.Disabilities = new List<Disability>();
                foreach (var disability in selectedDisabilities)
                {
                    var disabilityToAdd = db.Disabilities.Find(int.Parse(disability));
                    resource.Disabilities.Add(disabilityToAdd);
                }
            }
            if (selectedStakeholders != null)
            {
                resource.Stakeholders = new List<Stakeholder>();
                foreach (var stakeholder in selectedStakeholders)
                {
                    var stakeholderToAdd = db.Stakeholders.Find(int.Parse(stakeholder));
                    resource.Stakeholders.Add(stakeholderToAdd);
                }
            }
            try
            {
                foreach (string upload in Request.Files)
                {
                    //Check for File

                    if (!Request.Files[upload].HasFile()) continue;


                    //Build Unique ID
                    //to create a new archive with new time spans change the folderPath as well (may result in 
                    DateTime centuryBegin = new DateTime(2014, 1, 1);
                    DateTime currentDate = DateTime.Now;
                    long elapsedTicks = currentDate.Ticks - centuryBegin.Ticks;
                    TimeSpan elapsedSpan = new TimeSpan(elapsedTicks);
                    string uniqueID = Convert.ToString(Math.Round(elapsedSpan.TotalMinutes, 2));

                    //Build Folder and save path
                    string folderPath = "~/uploads/Resources/";
                    string newfolder = Server.MapPath(folderPath + uniqueID);
                    if (Directory.Exists(newfolder))
                    {
                        continue;
                    }
                    else
                    {
                        Directory.CreateDirectory(newfolder);
                    }
                    string path = newfolder + "/";
                    string filename = Path.GetFileName(Request.Files[upload].FileName);
                    string fullPath = Path.Combine(path, filename);
                    string DLPath = folderPath + uniqueID + "/" + filename;

                    //Save filepath

                    resource.FilePath = DLPath;

                    //add to directory

                    Request.Files[upload].SaveAs(fullPath);

                   //active state
                    if (!User.IsInRole("Admin"))
                    {
                        resource.ActiveStateID = 1;
                    }

                    //auditable
                    string auditUser = "Anonymous";
                    if (User.Identity.IsAuthenticated)
                    {
                        auditUser = User.Identity.Name;
                    }

                    DateTime auditDate = DateTime.Now;
                    resource.CreatedOn = auditDate;
                    resource.CreatedBy = auditUser;
                    resource.UpdatedOn = auditDate;
                    resource.UpdatedBy = auditUser;

                    //Initialize Download Count
                    resource.NumDownloads = 0;

                    //add to database
                    db.Resources.Add(resource);
                    db.SaveChanges();

                    return RedirectToAction("SuccessCreate");
                }
            }
            catch (DataException)
            {
                ModelState.AddModelError("", "Unable to save changes. Try again, and if the problem persists, see your system administrator.");
            }

            ViewBag.FileTypeID = new SelectList(db.FileTypes, "ID", "Desc", resource.FileTypeID);
            ViewBag.ActiveStateID = new SelectList(db.ActiveStates, "ID", "Desc", resource.ActiveStateID);
            PopulateAssignedDisabilityData(resource);
            PopulateAssignedStakeholderData(resource);
            return View(resource);
        }

        public ActionResult SuccessCreate()
        {
            return View();
        }

        //
        // GET: /Resource/Edit/5
        [Authorize(Roles = "Admin")]
        public ActionResult Edit(int? id)
        {
            Resource resource = db.Resources
                .Include(p => p.Disabilities).Include(p => p.Stakeholders)
                .Where(i => i.ID == id)
                .Single();
            if (resource == null)
            {
                return HttpNotFound();
            }

            ViewBag.FileTypeID = new SelectList(db.FileTypes, "ID", "Desc", resource.FileTypeID);
            ViewBag.ActiveStateID = new SelectList(db.ActiveStates, "ID", "Desc", resource.ActiveStateID);
            PopulateAssignedDisabilityData(resource);
            PopulateAssignedStakeholderData(resource);
            return View(resource);
        }

        //
        // POST: /Resource/Edit/5
        [Authorize(Roles = "Admin")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int? id, string[] selectedDisabilities, string[] selectedStakeholders, Resource resource)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var resourceToUpdate = db.Resources
                .Include(p => p.Disabilities).Include(p => p.Stakeholders)
                .Where(i => i.ID == id)
                .Single();
            try
            {
                resourceToUpdate.Title = resource.Title;
                resourceToUpdate.Tags = resource.Tags;
                resourceToUpdate.FilePath = resource.FilePath;
                resourceToUpdate.FileTypeID = resource.FileTypeID;
                resourceToUpdate.ActiveStateID = resource.ActiveStateID;
                resourceToUpdate.Desc = resource.Desc;
                resourceToUpdate.UpdatedOn = DateTime.Now;
                string updateUser = "Anonymous";

                if (User.Identity.IsAuthenticated)
                {
                    updateUser = User.Identity.Name;
                }

                resourceToUpdate.UpdatedBy = updateUser;

                UpdateResourceDisabilities(selectedDisabilities, resourceToUpdate);
                UpdateResourceStakeholders(selectedStakeholders, resourceToUpdate);

                db.Entry(resourceToUpdate).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("SuccessCreate");
            }
            catch (DataException)
            {
                ModelState.AddModelError("", "Unable to save changes. Try again, and if the problem persists, see your system administrator.");
            }
            ViewBag.FileTypeID = new SelectList(db.FileTypes, "ID", "Desc", resourceToUpdate.FileTypeID);
            ViewBag.ActiveStateID = new SelectList(db.ActiveStates, "ID", "Desc", resourceToUpdate.ActiveStateID);
            PopulateAssignedDisabilityData(resourceToUpdate);
            PopulateAssignedStakeholderData(resourceToUpdate);
            return View(resourceToUpdate);
        }

        //Update Resources based on checkboxes
        private void UpdateResourceDisabilities(string[] selectedDisabilities, Resource resourceToUpdate)
        {
            if (selectedDisabilities == null)
            {
                resourceToUpdate.Disabilities = new List<Disability>();
                return;
            }

            var selectedDisabilitiesHS = new HashSet<string>(selectedDisabilities);
            var ResourceDisabilities = new HashSet<int>
                (resourceToUpdate.Disabilities.Select(b => b.ID));
            foreach (var disability in db.Disabilities)
            {
                if (selectedDisabilitiesHS.Contains(disability.ID.ToString()))
                {
                    if (!ResourceDisabilities.Contains(disability.ID))
                    {
                        resourceToUpdate.Disabilities.Add(disability);
                    }
                }
                else
                {
                    if (ResourceDisabilities.Contains(disability.ID))
                    {
                        resourceToUpdate.Disabilities.Remove(disability);
                    }
                }
            }
        }
        private void UpdateResourceStakeholders(string[] selectedStakeholders, Resource resourceToUpdate)
        {
            if (selectedStakeholders == null)
            {
                resourceToUpdate.Stakeholders = new List<Stakeholder>();
                return;
            }

            var selectedStakeholdersHS = new HashSet<string>(selectedStakeholders);
            var ResourceStakeholders = new HashSet<int>
                (resourceToUpdate.Stakeholders.Select(b => b.ID));
            foreach (var stakeholder in db.Stakeholders)
            {
                if (selectedStakeholdersHS.Contains(stakeholder.ID.ToString()))
                {
                    if (!ResourceStakeholders.Contains(stakeholder.ID))
                    {
                        resourceToUpdate.Stakeholders.Add(stakeholder);
                    }
                }
                else
                {
                    if (ResourceStakeholders.Contains(stakeholder.ID))
                    {
                        resourceToUpdate.Stakeholders.Remove(stakeholder);
                    }
                }
            }
        }

        //Populate check boxes
        private void PopulateAssignedDisabilityData(Resource resource)
        {
            var allDisabilities = db.Disabilities;
            var ResDis = new HashSet<int>(resource.Disabilities.Select(b => b.ID));
            var viewModel = new List<ResourceDisabilityVM>();
            foreach (var disability in allDisabilities)
            {
                viewModel.Add(new ResourceDisabilityVM
                {
                    ID = disability.ID,
                    Desc = disability.Desc,
                    assigned = ResDis.Contains(disability.ID)
                });
            }
            ViewBag.Disabilities = viewModel;
        }
        private void PopulateAssignedStakeholderData(Resource resource)
        {
            var allStakeholders = db.Stakeholders;
            var ResStak = new HashSet<int>(resource.Stakeholders.Select(b => b.ID));
            var viewModel = new List<ResourceStakeholderVM>();
            foreach (var Stakeholder in allStakeholders)
            {
                viewModel.Add(new ResourceStakeholderVM
                {
                    ID = Stakeholder.ID,
                    Desc = Stakeholder.Desc,
                    assigned = ResStak.Contains(Stakeholder.ID)
                });
            }
            ViewBag.Stakeholders = viewModel;
        }

        //
        // GET: /Resource/Delete/5
        [Authorize(Roles = "Admin")]
        public ActionResult Delete(int id = 0)
        {
            Resource resource = db.Resources.Find(id);
            if (resource == null)
            {
                return HttpNotFound();
            }
            return View(resource);
        }

        //
        // POST: /Resource/Delete/5
        [Authorize(Roles = "Admin")]
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            try
            {
                Resource resource = db.Resources.Find(id);
                db.Resources.Remove(resource);
                db.SaveChanges();


                string path = resource.FilePath;
                string[] pathSplit = path.Split('/');
                string buildDir = pathSplit[0] + "/" + pathSplit[1] + "/" + pathSplit[2] + "/" + pathSplit[3];

                System.IO.File.Delete(Server.MapPath(path));

                DirectoryInfo info = new DirectoryInfo(Server.MapPath(buildDir));

                var listfiles = info.GetFiles();


                if (listfiles.Count() < 1)
                {
                    System.IO.Directory.Delete(Server.MapPath(buildDir));
                }


                return RedirectToAction("DeleteSuccess");
            }

            catch
            {
                return RedirectToAction("DeleteFailed");
            }

        }

        public ActionResult DeleteSuccess()
        {
            return View();
        }

        public ActionResult DeleteFailed()
        {
            return View();
        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }
    }
}