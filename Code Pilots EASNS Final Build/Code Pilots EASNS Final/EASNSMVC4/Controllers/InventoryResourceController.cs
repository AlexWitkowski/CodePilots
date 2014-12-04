using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using EASNSMVC4.Models;
using EASNSMVC4.DAL;
using System.IO;
using EASNSMVC4.Classes;

namespace EASNSMVC4.Controllers
{
    
    public class InventoryResourceController : Controller
    {
        private DBEntity db = new DBEntity();
    
        //
        // GET: /InventoryResource/

        public ActionResult Index()
        {
            var inventoryresources = db.InventoryResources.Include(i => i.InventoryType).Include(i => i.FileType);
            return View(inventoryresources.ToList());
        }

        //
        // GET: /InventoryResource/Details/5

        public ActionResult Details(int id = 0)
        {
            InventoryResource inventoryresource = db.InventoryResources.Find(id);
            if (inventoryresource == null)
            {
                return HttpNotFound();
            }
            return View(inventoryresource);
        }

        //
        // GET: /InventoryResource/Create
        [Authorize(Roles = "Admin")]
        public ActionResult Create()
        {
            ViewBag.InventoryTypeID = new SelectList(db.InventoryTypes, "ID", "Name");
            ViewBag.FileTypeID = new SelectList(db.FileTypes, "ID", "Desc");
            return View();
        }

        //
        // POST: /InventoryResource/Create
        [Authorize(Roles = "Admin")]
        [HttpPost]
        public ActionResult Create(InventoryResource inventoryresource)
        {
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
                    string folderPath = "~/uploads/InventoryResources/";
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

                    inventoryresource.FilePath = DLPath;

                    //add to directory

                    Request.Files[upload].SaveAs(fullPath);


                    //Initialize Download Count
                    inventoryresource.NumDownloads = 0;

                    //auditable
                    string auditUser = "Anonymous";
                    if (User.Identity.IsAuthenticated)
                    {
                        auditUser = User.Identity.Name;
                    }

                    DateTime auditDate = DateTime.Now;
                    inventoryresource.CreatedOn = auditDate;
                    inventoryresource.CreatedBy = auditUser;
                    inventoryresource.UpdatedOn = auditDate;
                    inventoryresource.UpdatedBy = auditUser;

                    //add to database
                    db.InventoryResources.Add(inventoryresource);
                    db.SaveChanges();

                    return RedirectToAction("SuccessCreate");
                }
            }
            catch (DataException)
            {
                ModelState.AddModelError("", "Unable to save changes. Try again, and if the problem persists, see your system administrator.");
            }

            ViewBag.InventoryTypeID = new SelectList(db.InventoryTypes, "ID", "Name", inventoryresource.InventoryTypeID);
            ViewBag.FileTypeID = new SelectList(db.FileTypes, "ID", "Desc", inventoryresource.FileTypeID);
            return View(inventoryresource);
        }


        public ActionResult SuccessCreate()
        {
            return View();
        }
        //
        // GET: /InventoryResource/Edit/5
        [Authorize(Roles = "Admin")]
        public ActionResult Edit(int id = 0)
        {
            InventoryResource inventoryresource = db.InventoryResources.Find(id);
            if (inventoryresource == null)
            {
                return HttpNotFound();
            }
            ViewBag.InventoryTypeID = new SelectList(db.InventoryTypes, "ID", "Name", inventoryresource.InventoryTypeID);
            ViewBag.FileTypeID = new SelectList(db.FileTypes, "ID", "Desc", inventoryresource.FileTypeID);
            return View(inventoryresource);
        }

        //
        // POST: /InventoryResource/Edit/5
        [Authorize(Roles = "Admin")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(InventoryResource inventoryresource)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    string auditUser = "Anonymous";
                    if (User.Identity.IsAuthenticated)
                    {
                        auditUser = User.Identity.Name;
                    }

                    DateTime auditDate = DateTime.Now;
                    inventoryresource.UpdatedOn = auditDate;
                    inventoryresource.UpdatedBy = auditUser;

                    db.Entry(inventoryresource).State = EntityState.Modified;
                    db.SaveChanges();
                    return RedirectToAction("SuccessCreate");
                }
            }
            catch (DataException)
            {
                ModelState.AddModelError("", "Unable to save changes. Try again, and if the problem persists, see your system administrator.");
            }
            ViewBag.InventoryTypeID = new SelectList(db.InventoryTypes, "ID", "Name", inventoryresource.InventoryTypeID);
            ViewBag.FileTypeID = new SelectList(db.FileTypes, "ID", "Desc", inventoryresource.FileTypeID);
            return View(inventoryresource);
        }

        //
        // GET: /InventoryResource/Delete/5
        [Authorize(Roles = "Admin")]
        public ActionResult Delete(int id = 0)
        {
            InventoryResource inventoryresource = db.InventoryResources.Find(id);
            if (inventoryresource == null)
            {
                return HttpNotFound();
            }
            return View(inventoryresource);
        }

        //
        // POST: /InventoryResource/Delete/5
        [Authorize(Roles = "Admin")]
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            try
            {
                InventoryResource inventoryResource = db.InventoryResources.Find(id);
                db.InventoryResources.Remove(inventoryResource);
                db.SaveChanges();


                string path = inventoryResource.FilePath;
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
        public ActionResult Download(string path)
        {
            InventoryResource resource = db.InventoryResources.SingleOrDefault(r => r.FilePath == path);
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

    }
}