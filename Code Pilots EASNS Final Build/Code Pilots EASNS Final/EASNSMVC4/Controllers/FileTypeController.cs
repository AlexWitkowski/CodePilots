using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using EASNSMVC4.Models;
using EASNSMVC4.DAL;

namespace EASNSMVC4.Controllers
{
    [Authorize(Roles = "Admin")]
    public class FileTypeController : Controller
    {
        private DBEntity db = new DBEntity();

        //
        // GET: /FileType/

        public ActionResult Index()
        {
            return View(db.FileTypes.ToList());
        }

        //
        // GET: /FileType/Details/5

        public ActionResult Details(int id = 0)
        {
            FileType filetype = db.FileTypes.Find(id);
            if (filetype == null)
            {
                return HttpNotFound();
            }
            return View(filetype);
        }

        //
        // GET: /FileType/Create

        public ActionResult Create()
        {
            return View();
        }

        //
        // POST: /FileType/Create

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(FileType filetype)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    db.FileTypes.Add(filetype);
                    db.SaveChanges();
                    return RedirectToAction("SuccessCreate");
                }
            }
            catch (DataException)
            {
                ModelState.AddModelError("", "Unable to save changes. Try again, and if the problem persists, see your system administrator.");
            }
            return View(filetype);
        }

        public ActionResult SuccessCreate()
        {
            return View();
        }

        //
        // GET: /FileType/Edit/5

        public ActionResult Edit(int id = 0)
        {
            FileType filetype = db.FileTypes.Find(id);
            if (filetype == null)
            {
                return HttpNotFound();
            }
            return View(filetype);
        }

        //
        // POST: /FileType/Edit/5

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(FileType filetype)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    db.Entry(filetype).State = EntityState.Modified;
                    db.SaveChanges();
                    return RedirectToAction("SuccessCreate");
                }
            }
            catch (DataException)
            {
                ModelState.AddModelError("", "Unable to save changes. Try again, and if the problem persists, see your system administrator.");
            }
            return View(filetype);
        }

        //
        // GET: /FileType/Delete/5

        public ActionResult Delete(int id = 0)
        {
            FileType filetype = db.FileTypes.Find(id);
            if (filetype == null)
            {
                return HttpNotFound();
            }
            return View(filetype);
        }

        //
        // POST: /FileType/Delete/5

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            try
            {
                FileType filetype = db.FileTypes.Find(id);
                db.FileTypes.Remove(filetype);
                db.SaveChanges();
                return RedirectToAction("DeleteSuccess");
            }
            catch (DataException)
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