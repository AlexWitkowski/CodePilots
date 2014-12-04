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
    public class InventoryController : Controller
    {
        private DBEntity db = new DBEntity();

        //
        // GET: /Inventory/

        public ActionResult Index()
        {
            var inventories = db.Inventories.Include(i => i.InventoryType);
            return View(inventories.ToList());
        }

        //
        // GET: /Inventory/Details/5

        public ActionResult Details(int id = 0)
        {
            Inventory inventory = db.Inventories.Find(id);
            if (inventory == null)
            {
                return HttpNotFound();
            }
            return View(inventory);
        }

        //
        // GET: /Inventory/Create

        public ActionResult Create()
        {
            ViewBag.InventoryTypeID = new SelectList(db.InventoryTypes, "ID", "Name");
            return View();
        }

        //
        // POST: /Inventory/Create

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Inventory inventory)
        {
            if (ModelState.IsValid)
            {
                db.Inventories.Add(inventory);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.InventoryTypeID = new SelectList(db.InventoryTypes, "ID", "Name", inventory.InventoryTypeID);
            return View(inventory);
        }

        //
        // GET: /Inventory/Edit/5

        public ActionResult Edit(int id = 0)
        {
            Inventory inventory = db.Inventories.Find(id);
            if (inventory == null)
            {
                return HttpNotFound();
            }
            ViewBag.InventoryTypeID = new SelectList(db.InventoryTypes, "ID", "Name", inventory.InventoryTypeID);
            return View(inventory);
        }

        //
        // POST: /Inventory/Edit/5

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Inventory inventory)
        {
            if (ModelState.IsValid)
            {
                db.Entry(inventory).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.InventoryTypeID = new SelectList(db.InventoryTypes, "ID", "Name", inventory.InventoryTypeID);
            return View(inventory);
        }

        //
        // GET: /Inventory/Delete/5

        public ActionResult Delete(int id = 0)
        {
            Inventory inventory = db.Inventories.Find(id);
            if (inventory == null)
            {
                return HttpNotFound();
            }
            return View(inventory);
        }

        //
        // POST: /Inventory/Delete/5

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Inventory inventory = db.Inventories.Find(id);
            db.Inventories.Remove(inventory);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }
    }
}