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
    public class StakeholderController : Controller
    {
        private DBEntity db = new DBEntity();

        //
        // GET: /Stakeholder/

        public ActionResult Index()
        {
            return View(db.Stakeholders.ToList());
        }

        //
        // GET: /Stakeholder/Details/5

        public ActionResult Details(int id = 0)
        {
            Stakeholder stakeholder = db.Stakeholders.Find(id);
            if (stakeholder == null)
            {
                return HttpNotFound();
            }
            return View(stakeholder);
        }

        //
        // GET: /Stakeholder/Create

        public ActionResult Create()
        {
            return View();
        }

        //
        // POST: /Stakeholder/Create

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Stakeholder stakeholder)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    db.Stakeholders.Add(stakeholder);
                    db.SaveChanges();
                    return RedirectToAction("SuccessCreate");
                }
            }
            catch (DataException)
            {
                ModelState.AddModelError("", "Unable to save changes. Try again, and if the problem persists, see your system administrator.");
            }

            return View(stakeholder);
        }

        public ActionResult SuccessCreate()
        {
            return View();
        }

        //
        // GET: /Stakeholder/Edit/5

        public ActionResult Edit(int id = 0)
        {
            Stakeholder stakeholder = db.Stakeholders.Find(id);
            if (stakeholder == null)
            {
                return HttpNotFound();
            }
            return View(stakeholder);
        }

        //
        // POST: /Stakeholder/Edit/5

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Stakeholder stakeholder)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    db.Entry(stakeholder).State = EntityState.Modified;
                    db.SaveChanges();
                    return RedirectToAction("SuccessCreate");
                }
            }
            catch (DataException)
            {
                ModelState.AddModelError("", "Unable to save changes. Try again, and if the problem persists, see your system administrator.");
            }
            return View(stakeholder);
        }

        //
        // GET: /Stakeholder/Delete/5

        public ActionResult Delete(int id = 0)
        {
            Stakeholder stakeholder = db.Stakeholders.Find(id);
            if (stakeholder == null)
            {
                return HttpNotFound();
            }
            return View(stakeholder);
        }

        //
        // POST: /Stakeholder/Delete/5

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            try
            {
                Stakeholder stakeholder = db.Stakeholders.Find(id);
                db.Stakeholders.Remove(stakeholder);
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