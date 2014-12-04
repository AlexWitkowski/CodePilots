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
    
    public class PeripheralController : Controller
    {
        private DBEntity db = new DBEntity();

        //
        // GET: /Peripheral/

        public ActionResult Index()
        {
            var peripherals = db.Peripherals.Include(p => p.TechLevel);
            return View(peripherals.ToList());
        }

        //
        // GET: /Peripheral/Details/5

        public ActionResult Details(int id = 0)
        {
            Peripheral peripheral = db.Peripherals.Find(id);
            if (peripheral == null)
            {
                return HttpNotFound();
            }
            return View(peripheral);
        }

        //
        // GET: /Peripheral/Create
        [Authorize(Roles = "Admin")]
        public ActionResult Create()
        {
            ViewBag.TechLevelID = new SelectList(db.TechLevels, "ID", "Desc");
            return View();
        }

        //
        // POST: /Peripheral/Create
        [Authorize(Roles = "Admin")]
        [HttpPost]
        public ActionResult Create(Peripheral peripheral)
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
                    string folderPath = "~/uploads/PeripheralDisplay/";
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

                    peripheral.Display = DLPath;

                    //add to directory

                    Request.Files[upload].SaveAs(fullPath);

                    //add to database
                    db.Peripherals.Add(peripheral);
                    db.SaveChanges();

                    return RedirectToAction("SuccessCreate");
                }
            }
            catch (DataException)
            {
                ModelState.AddModelError("", "Unable to save changes. Try again, and if the problem persists, see your system administrator.");
            }
            ViewBag.TechLevelID = new SelectList(db.TechLevels, "ID", "Desc", peripheral.TechLevelID);
            return View(peripheral);
        }

        public ActionResult SuccessCreate()
        {
            return View();
        }

        //
        // GET: /Peripheral/Edit/5
        [Authorize(Roles = "Admin")]
        public ActionResult Edit(int id = 0)
        {
            Peripheral peripheral = db.Peripherals.Find(id);
            if (peripheral == null)
            {
                return HttpNotFound();
            }
            ViewBag.TechLevelID = new SelectList(db.TechLevels, "ID", "Desc", peripheral.TechLevelID);
            return View(peripheral);
        }

        //
        // POST: /Peripheral/Edit/5
        [Authorize(Roles = "Admin")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Peripheral peripheral)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    db.Entry(peripheral).State = EntityState.Modified;
                    db.SaveChanges();
                    return RedirectToAction("SuccessCreate");
                }
            }
            catch (DataException)
            {
                ModelState.AddModelError("", "Unable to save changes. Try again, and if the problem persists, see your system administrator.");
            }
            ViewBag.TechLevelID = new SelectList(db.TechLevels, "ID", "Desc", peripheral.TechLevelID);
            return View(peripheral);
        }

        //
        // GET: /Peripheral/Delete/5
        [Authorize(Roles = "Admin")]
        public ActionResult Delete(int id = 0)
        {
            Peripheral peripheral = db.Peripherals.Find(id);
            if (peripheral == null)
            {
                return HttpNotFound();
            }
            return View(peripheral);

        }

        public ActionResult DeleteSuccess()
        {
            return View();
        }

        public ActionResult DeleteFailed()
        {
            return View();
        }

        //
        // POST: /Peripheral/Delete/5
        [Authorize(Roles = "Admin")]
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            try
            {
                Peripheral peripheral = db.Peripherals.Find(id);
                db.Peripherals.Remove(peripheral);
                db.SaveChanges();


                string path = peripheral.Display;
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

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }
    }
}