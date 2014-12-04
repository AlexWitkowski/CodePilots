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
    public class ActiveStateController : Controller
    {
        private DBEntity db = new DBEntity();

        //
        // GET: /ActiveState/

        public ActionResult Index()
        {
            return View(db.ActiveStates.ToList());
        }

        //
        // GET: /ActiveState/Details/5

        public ActionResult Details(int id = 0)
        {
            ActiveState activestate = db.ActiveStates.Find(id);
            if (activestate == null)
            {
                return HttpNotFound();
            }
            return View(activestate);
        }

        //
        // GET: /ActiveState/Create

        public ActionResult Create()
        {
            return View();
        }

        //
        // POST: /ActiveState/Create

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(ActiveState activestate)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    db.ActiveStates.Add(activestate);
                    db.SaveChanges();
                    return RedirectToAction("SuccessCreate");
                }
            }
            catch (DataException)
            {
                ModelState.AddModelError("", "Unable to save changes. Try again, and if the problem persists, see your system administrator.");
            }
            return View(activestate);
        }

        public ActionResult SuccessCreate()
        {
            return View();
        }

        //
        // GET: /ActiveState/Edit/5

        public ActionResult Edit(int id = 0)
        {
                ActiveState activestate = db.ActiveStates.Find(id);
                if (activestate == null)
                {
                    return HttpNotFound();
                }
            return View(activestate);
        }

        //
        // POST: /ActiveState/Edit/5

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(ActiveState activestate)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    db.Entry(activestate).State = EntityState.Modified;
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
            }
            catch (DataException)
            {
                ModelState.AddModelError("", "Unable to save changes. Try again, and if the problem persists, see your system administrator.");
            }
            return View(activestate);
        }

        //
        // GET: /ActiveState/Delete/5

        public ActionResult Delete(int id = 0)
        {
                ActiveState activestate = db.ActiveStates.Find(id);
                if (activestate == null)
                {
                    return HttpNotFound();
                }
                return View(activestate);
            
        }

        //
        // POST: /ActiveState/Delete/5

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            try
            {
                ActiveState activestate = db.ActiveStates.Find(id);
                db.ActiveStates.Remove(activestate);
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