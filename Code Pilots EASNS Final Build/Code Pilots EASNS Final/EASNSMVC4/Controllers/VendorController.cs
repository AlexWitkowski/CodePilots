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
    
    public class VendorController : Controller
    {
        private DBEntity db = new DBEntity();

        //
        // GET: /Vendor/

        public ActionResult Index()
        {
            return View(db.Vendors.ToList());
        }
        public ActionResult PubIndex()
        {
            return View(db.Vendors.ToList());
        }
        public ActionResult RegisterVendors()
        {
            return View();
        }

        //
        // GET: /Vendor/Details/5

        public ActionResult Details(int id = 0)
        {
            Vendor vendor = db.Vendors.Find(id);
            if (vendor == null)
            {
                return HttpNotFound();
            }
            return View(vendor);
        }
        public ActionResult PubDetails(int id = 0)
        {
            Vendor vendor = db.Vendors.Find(id);
            if(vendor == null)
            {
                return HttpNotFound();
            }
            return View(vendor);
        }

        //
        // GET: /Vendor/Create

        public ActionResult Create()
        {
            return View();
        }

        //
        // POST: /Vendor/Create

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Vendor vendor)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    db.Vendors.Add(vendor);
                    db.SaveChanges();
                    return RedirectToAction("SuccessCreate");
                }
            }
            catch (DataException)
            {
                ModelState.AddModelError("", "Unable to save changes. Try again, and if the problem persists, see your system administrator.");
            }
            return View(vendor);
        }


        public ActionResult SuccessCreate()
        {
            return View();
        }
        //
        // GET: /Vendor/Edit/5

        public ActionResult Edit(int id = 0)
        {
            Vendor vendor = db.Vendors.Find(id);
            if (vendor == null)
            {
                return HttpNotFound();
            }
            return View(vendor);
        }

        //
        // POST: /Vendor/Edit/5

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Vendor vendor)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    db.Entry(vendor).State = EntityState.Modified;
                    db.SaveChanges();
                    return RedirectToAction("SuccessCreate");
                }
            }
            catch (DataException)
            {
                ModelState.AddModelError("", "Unable to save changes. Try again, and if the problem persists, see your system administrator.");
            }
            return View(vendor);
        }

        //
        // GET: /Vendor/Delete/5

        public ActionResult Delete(int id = 0)
        {
            Vendor vendor = db.Vendors.Find(id);
            if (vendor == null)
            {
                return HttpNotFound();
            }
            return View(vendor);
        }

        //
        // POST: /Vendor/Delete/5

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            try
            {
                Vendor vendor = db.Vendors.Find(id);
                db.Vendors.Remove(vendor);
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