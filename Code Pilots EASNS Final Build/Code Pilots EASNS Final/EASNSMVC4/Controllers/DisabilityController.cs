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
    public class DisabilityController : Controller
    {
        private DBEntity db = new DBEntity();

        //
        // GET: /Disability/

        public ActionResult Index()
        {
            return View(db.Disabilities.ToList());
        }

        //
        // GET: /Disability/Details/5

        public ActionResult Details(int id = 0)
        {
            Disability disability = db.Disabilities.Find(id);
            if (disability == null)
            {
                return HttpNotFound();
            }
            return View(disability);
        }

        //
        // GET: /Disability/Create

        public ActionResult Create()
        {
            return View();
        }

        //
        // POST: /Disability/Create

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Disability disability)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    db.Disabilities.Add(disability);
                    db.SaveChanges();
                    return RedirectToAction("SuccessCreate");
                }
            }
            catch (DataException)
            {
                ModelState.AddModelError("", "Unable to save changes. Try again, and if the problem persists, see your system administrator.");
            }
            return View(disability);
        }

        public ActionResult SuccessCreate()
        {
            return View();
        }

        //
        // GET: /Disability/Edit/5

        public ActionResult Edit(int id = 0)
        {
            Disability disability = db.Disabilities.Find(id);
            if (disability == null)
            {
                return HttpNotFound();
            }
            return View(disability);
        }

        //
        // POST: /Disability/Edit/5

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Disability disability)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    db.Entry(disability).State = EntityState.Modified;
                    db.SaveChanges();
                    return RedirectToAction("SuccessCreate");
                }
            }
            catch (DataException)
            {
                ModelState.AddModelError("", "Unable to save changes. Try again, and if the problem persists, see your system administrator.");
            }
            return View(disability);
        }

        //
        // GET: /Disability/Delete/5

        public ActionResult Delete(int id = 0)
        {
            Disability disability = db.Disabilities.Find(id);
            if (disability == null)
            {
                return HttpNotFound();
            }
            return View(disability);
        }

        //
        // POST: /Disability/Delete/5

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            try
            {
                Disability disability = db.Disabilities.Find(id);
                db.Disabilities.Remove(disability);
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