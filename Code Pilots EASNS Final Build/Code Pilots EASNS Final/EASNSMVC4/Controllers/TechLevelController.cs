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
    public class TechLevelController : Controller
    {
        private DBEntity db = new DBEntity();

        //
        // GET: /TechLevel/

        public ActionResult Index()
        {
            return View(db.TechLevels.ToList());
        }

        //
        // GET: /TechLevel/Details/5

        public ActionResult Details(int id = 0)
        {
            TechLevel techlevel = db.TechLevels.Find(id);
            if (techlevel == null)
            {
                return HttpNotFound();
            }
            return View(techlevel);
        }

        //
        // GET: /TechLevel/Create

        public ActionResult Create()
        {
            return View();
        }

        //
        // POST: /TechLevel/Create

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(TechLevel techlevel)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    db.TechLevels.Add(techlevel);
                    db.SaveChanges();
                    return RedirectToAction("SuccessCreate");
                }
            }
            catch (DataException)
            {
                ModelState.AddModelError("", "Unable to save changes. Try again, and if the problem persists, see your system administrator.");
            }
            return View(techlevel);
        }


        public ActionResult SuccessCreate()
        {
            return View();
        }

        //
        // GET: /TechLevel/Edit/5

        public ActionResult Edit(int id = 0)
        {
            TechLevel techlevel = db.TechLevels.Find(id);
            if (techlevel == null)
            {
                return HttpNotFound();
            }
            return View(techlevel);
        }

        //
        // POST: /TechLevel/Edit/5

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(TechLevel techlevel)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    db.Entry(techlevel).State = EntityState.Modified;
                    db.SaveChanges();
                    return RedirectToAction("SuccessCreate");
                }
            }
            catch (DataException)
            {
                ModelState.AddModelError("", "Unable to save changes. Try again, and if the problem persists, see your system administrator.");
            }
            return View(techlevel);
        }

        //
        // GET: /TechLevel/Delete/5

        public ActionResult Delete(int id = 0)
        {
            TechLevel techlevel = db.TechLevels.Find(id);
            if (techlevel == null)
            {
                return HttpNotFound();
            }
            return View(techlevel);
        }

        //
        // POST: /TechLevel/Delete/5

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            try
            {
                TechLevel techlevel = db.TechLevels.Find(id);
                db.TechLevels.Remove(techlevel);
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