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
    
    public class StaffController : Controller
    {
        private DBEntity db = new DBEntity();

        //
        // GET: /Staff/

        public ActionResult Index()
        {
            return View(db.Staffs.ToList());
        }

        //
        // GET: /Staff/Details/5

        public ActionResult Details(int id = 0)
        {
            Staff staff = db.Staffs.Find(id);
            if (staff == null)
            {
                return HttpNotFound();
            }
            return View(staff);
        }

        //
        // GET: /Staff/Create
        [Authorize(Roles = "Admin")]
        public ActionResult Create()
        {
            return View();
        }

        //
        // POST: /Staff/Create
        [Authorize(Roles = "Admin")]
        [HttpPost]
        public ActionResult Create(Staff staff)
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
                    string folderPath = "~/uploads/StaffAvatars/";
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

                    staff.Avatar = DLPath;

                    //add to directory

                    Request.Files[upload].SaveAs(fullPath);

                    //add to database
                    db.Staffs.Add(staff);
                    db.SaveChanges();

                    return RedirectToAction("SuccessCreate");
                }
            }
            catch (DataException)
            {
                ModelState.AddModelError("", "Unable to save changes. Try again, and if the problem persists, see your system administrator.");
            }
            return View(staff);
        }

        public ActionResult SuccessCreate()
        {
            return View();
        }

        //
        // GET: /Staff/Edit/5
        [Authorize(Roles = "Admin")]
        public ActionResult Edit(int id = 0)
        {
            Staff staff = db.Staffs.Find(id);
            if (staff == null)
            {
                return HttpNotFound();
            }
            return View(staff);
        }

        //
        // POST: /Staff/Edit/5
        [Authorize(Roles = "Admin")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Staff staff)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    db.Entry(staff).State = EntityState.Modified;
                    db.SaveChanges();
                    return RedirectToAction("SuccessCreate");
                }
            }
            catch (DataException)
            {
                ModelState.AddModelError("", "Unable to save changes. Try again, and if the problem persists, see your system administrator.");
            }
            return View(staff);
        }

        //
        // GET: /Staff/Delete/5
        [Authorize(Roles = "Admin")]
        public ActionResult Delete(int id = 0)
        {
            Staff staff = db.Staffs.Find(id);
            if (staff == null)
            {
                return HttpNotFound();
            }
            return View(staff);
        }

        //
        // POST: /Staff/Delete/5
        [Authorize(Roles = "Admin")]
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            try
            {
                Staff staff = db.Staffs.Find(id);
                db.Staffs.Remove(staff);
                db.SaveChanges();


                string path = staff.Avatar;
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