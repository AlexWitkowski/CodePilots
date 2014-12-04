using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace EASNSMVC4.Controllers
{
    public class TechController : Controller
    {
        //
        // GET: /Tech/

        public ActionResult ViewTech()
        {
            return View();
        }

        //
        // GET: /Tech/Details/5

        public ActionResult Details(int id)
        {
            return View();
        }

        //
        // GET: /Tech/Create

        public ActionResult Create()
        {
            return View();
        }

        //
        // POST: /Tech/Create

        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        //
        // GET: /Tech/Edit/5

        public ActionResult Edit(int id)
        {
            return View();
        }

        //
        // POST: /Tech/Edit/5

        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        //
        // GET: /Tech/Delete/5

        public ActionResult Delete(int id)
        {
            return View();
        }

        //
        // POST: /Tech/Delete/5

        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
