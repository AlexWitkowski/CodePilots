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

    public class InventoryTypeController : Controller
    {
        private DBEntity db = new DBEntity();

        //
        // GET: /InventoryType/

        public ActionResult Index()
        {
            var inventorytypes = db.InventoryTypes.Include(i => i.Vendor).Include(i => i.Peripheral);
            return View(inventorytypes.ToList());
        }

        //
        // GET: /InventoryType/Details/5

        public ActionResult Details(int id = 0)
        {
            InventoryType inventorytype = db.InventoryTypes.Find(id);
            if (inventorytype == null)
            {
                return HttpNotFound();
            }
            return View(inventorytype);
        }

        //
        // GET: /InventoryType/Create
        [Authorize(Roles = "Admin")]
        public ActionResult Create()
        {
            var InventoryType = new InventoryType();

            InventoryType.Disabilities = new List<Disability>();

            ViewBag.VendorID = new SelectList(db.Vendors, "ID", "Name");
            ViewBag.PeripheralID = new SelectList(db.Peripherals, "ID", "Desc");

            PopulateAssignedDisabilityData(InventoryType);

            return View();
        }

        //
        // POST: /InventoryType/Create
        [Authorize(Roles = "Admin")]
        [HttpPost]
        public ActionResult Create(InventoryType inventorytype, string[] selectedDisabilities)
        {

            //check selected checkboxes based on the database.
            if (selectedDisabilities != null)
            {
                inventorytype.Disabilities = new List<Disability>();
                foreach (var disability in selectedDisabilities)
                {
                    var disabilityToAdd = db.Disabilities.Find(int.Parse(disability));
                    inventorytype.Disabilities.Add(disabilityToAdd);
                }
            }
            try
            {
                foreach (string upload in Request.Files)
                {
                    //Check for File

                    if (!Request.Files[upload].HasFile()) continue;


                    //Build Unique ID

                    DateTime centuryBegin = new DateTime(2014, 1, 1);
                    DateTime currentDate = DateTime.Now;
                    long elapsedTicks = currentDate.Ticks - centuryBegin.Ticks;
                    TimeSpan elapsedSpan = new TimeSpan(elapsedTicks);
                    string uniqueID = Convert.ToString(Math.Round(elapsedSpan.TotalMinutes, 2));

                    //Build Folder and save path
                    string folderPath = "~/uploads/InventoryDisplay/";
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

                    inventorytype.Display = DLPath;

                    //add to directory

                    Request.Files[upload].SaveAs(fullPath);


                    //add to database
                    db.InventoryTypes.Add(inventorytype);
                    db.SaveChanges();

                    return RedirectToAction("SuccessCreate");
                }
            }
            catch (DataException)
            {
                ModelState.AddModelError("", "Unable to save changes. Try again, and if the problem persists, see your system administrator.");
            }
            ViewBag.VendorID = new SelectList(db.Vendors, "ID", "Name", inventorytype.VendorID);
            ViewBag.TechLevelID = new SelectList(db.TechLevels, "ID", "Desc", inventorytype.PeripheralID);
            PopulateAssignedDisabilityData(inventorytype);
            return View(inventorytype);
        }

        public ActionResult SuccessCreate()
        {
            return View();
        }
        //
        // GET: /InventoryType/Edit/5
        [Authorize(Roles = "Admin")]
        public ActionResult Edit(int? id)
        {

            InventoryType inventorytype = db.InventoryTypes
                .Include(p => p.Disabilities)
                .Where(i => i.ID == id)
                .Single();

            if (inventorytype == null)
            {
                return HttpNotFound();
            }
            ViewBag.VendorID = new SelectList(db.Vendors, "ID", "Name", inventorytype.VendorID);
            ViewBag.TechLevelID = new SelectList(db.TechLevels, "ID", "Desc", inventorytype.PeripheralID);
            PopulateAssignedDisabilityData(inventorytype);
            return View(inventorytype);
        }

        //
        // POST: /InventoryType/Edit/5
        [Authorize(Roles = "Admin")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int? id, string[] selectedDisabilities, InventoryType inventorytype)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var inventoryTypeToUpdate = db.InventoryTypes
                .Include(p => p.Disabilities)
                .Where(i => i.ID == id)
                .Single();
            try
            {

                inventoryTypeToUpdate.Desc = inventorytype.Desc;
                inventoryTypeToUpdate.Display = inventorytype.Display;
                inventoryTypeToUpdate.Name = inventorytype.Name;
                inventoryTypeToUpdate.ModelNumber = inventorytype.ModelNumber;
                inventoryTypeToUpdate.VendorID = inventorytype.VendorID;
                inventoryTypeToUpdate.PeripheralID = inventorytype.PeripheralID;


                UpdateResourceDisabilities(selectedDisabilities, inventoryTypeToUpdate);

                db.Entry(inventoryTypeToUpdate).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("SuccessCreate");
            
            }
            catch (DataException)
            {
                ModelState.AddModelError("", "Unable to save changes. Try again, and if the problem persists, see your system administrator.");
            }

            ViewBag.VendorID = new SelectList(db.Vendors, "ID", "Name", inventorytype.VendorID);
            ViewBag.TechLevelID = new SelectList(db.TechLevels, "ID", "Desc", inventorytype.PeripheralID);
            PopulateAssignedDisabilityData(inventorytype);
            return View(inventoryTypeToUpdate);
        }
        //Update database by checkboxes
        private void UpdateResourceDisabilities(string[] selectedDisabilities, InventoryType inventoryTypeToUpdate)
        {
            if (selectedDisabilities == null)
            {
                inventoryTypeToUpdate.Disabilities = new List<Disability>();
                return;
            }

            var selectedDisabilitiesHS = new HashSet<string>(selectedDisabilities);
            var ResourceDisabilities = new HashSet<int>
                (inventoryTypeToUpdate.Disabilities.Select(b => b.ID));
            foreach (var disability in db.Disabilities)
            {
                if (selectedDisabilitiesHS.Contains(disability.ID.ToString()))
                {
                    if (!ResourceDisabilities.Contains(disability.ID))
                    {
                        inventoryTypeToUpdate.Disabilities.Add(disability);
                    }
                }
                else
                {
                    if (ResourceDisabilities.Contains(disability.ID))
                    {
                        inventoryTypeToUpdate.Disabilities.Remove(disability);
                    }
                }
            }
        }
        //Populate check boxes
        private void PopulateAssignedDisabilityData(InventoryType inventoryType)
        {
            var allDisabilities = db.Disabilities;
            var invDis = new HashSet<int>(inventoryType.Disabilities.Select(b => b.ID));
            var viewModel = new List<ResourceDisabilityVM>();
            foreach (var disability in allDisabilities)
            {
                viewModel.Add(new ResourceDisabilityVM
                {
                    ID = disability.ID,
                    Desc = disability.Desc,
                    assigned = invDis.Contains(disability.ID)
                });
            }
            ViewBag.Disabilities = viewModel;
        }


        //
        // GET: /InventoryType/Delete/5
        [Authorize(Roles = "Admin")]
        public ActionResult Delete(int id = 0)
        {
            InventoryType inventorytype = db.InventoryTypes.Find(id);
            if (inventorytype == null)
            {
                return HttpNotFound();
            }
            return View(inventorytype);
        }

        //
        // POST: /InventoryType/Delete/5
        [Authorize(Roles = "Admin")]
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            try
            {
                InventoryType inventoryType = db.InventoryTypes.Find(id);
                db.InventoryTypes.Remove(inventoryType);
                db.SaveChanges();


                string path = inventoryType.Display;
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