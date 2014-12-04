using EASNSMVC4.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using WebMatrix.WebData;

namespace EASNSMVC4.Controllers
{
    [Authorize(Roles = "Admin")]
    public class UserProfileController : Controller
    {
        //
        // GET: /UserProfile/

        public ActionResult Index(string sortOrder, string searchString)
        {
            using (var ctx = new UsersContext())
            {
                ViewBag.UserNameSortParm = String.IsNullOrEmpty(sortOrder) ? "user_desc" : "";
                ViewBag.LastNameSortParm = (sortOrder == "last") ? "last_desc" : "last";
                ViewBag.RequestedRoleSortParm = (sortOrder == "req") ? "req_desc" : "req";
                ViewBag.CitySortParm = (sortOrder == "city") ? "city_desc" : "city";

                if (!String.IsNullOrEmpty(searchString))
                {
                    var searchUser = ctx.UserProfiles.Where(c => c.UserName.ToUpper().Contains(searchString.ToUpper())
                                           || c.UserLast.ToUpper().Contains(searchString.ToUpper())).OrderBy(c => c.UserName);
                    return View(searchUser.ToList());
                }

                if (sortOrder == "user_desc")
                {
                    var userDesc = ctx.UserProfiles.OrderByDescending(c => c.UserName);
                    return View(userDesc.ToList());
                }

                else if (sortOrder == "last_desc")
                {
                    var lastDesc = ctx.UserProfiles.OrderByDescending(c => c.UserLast).ThenBy(c => c.UserFirst);
                    return View(lastDesc.ToList());
                }

                else if (sortOrder == "last")
                {
                    var last = ctx.UserProfiles.OrderBy(c => c.UserLast).ThenBy(c => c.UserFirst);
                    return View(last.ToList());
                }

                else if (sortOrder == "req_desc")
                {
                    var reqDesc = ctx.UserProfiles.OrderByDescending(c => c.UserReqRole).ThenBy(c => c.UserName);
                    return View(reqDesc.ToList());
                }

                else if (sortOrder == "req")
                {
                    var req = ctx.UserProfiles.OrderBy(c => c.UserReqRole).ThenBy(c => c.UserName);
                    return View(req.ToList());
                }

                else if (sortOrder == "city_desc")
                {
                    var cityDesc = ctx.UserProfiles.OrderByDescending(c => c.UserCity).ThenBy(c => c.UserName);
                    return View(cityDesc.ToList());
                }

                else if (sortOrder == "city")
                {
                    var city = ctx.UserProfiles.OrderBy(c => c.UserCity).ThenBy(c => c.UserName);
                    return View(city.ToList());
                }

                else
                {
                    var userAsc = ctx.UserProfiles.OrderBy(c => c.UserName);
                    return View(userAsc.ToList());
                }
            }
            //using (var ctx = new UsersContext())
            //{
            //   var thang = ctx.UserProfiles.OrderBy(s => s.UserName);
            //    return View(thang.ToList());
            //}
        }


        //
        // GET: /UserProfile/Details/5

        public ActionResult Details(int id)
        {
            var ctx = new UsersContext();
            var useDet = ctx.UserProfiles.Find(id);
            string[] useRole = System.Web.Security.Roles.GetRolesForUser(useDet.UserName);
            ViewBag.DetUseInRole = useRole[0].ToString();
            return View(useDet);
        }

        //
        // GET: /UserProfile/Create

        public ActionResult Create()
        {
            return View();
        }

        //
        // POST: /UserProfile/Create

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
        // GET: /UserProfile/Edit/5
        [Authorize(Roles="Admin")]
        public ActionResult Edit(int id)
        {
            List<SelectListItem> items = new List<SelectListItem>();

            items.Add(new SelectListItem { Text = "Admin", Value = "Admin" });
            items.Add(new SelectListItem { Text = "Educator", Value = "Educator" });
            items.Add(new SelectListItem
            {
                Text = "Agency",
                Value = "Agency",
                Selected = true
            });
            items.Add(new SelectListItem { Text = "Graduate", Value = "Graduate" });
            items.Add(new SelectListItem { Text = "Vendor", Value = "Vendor" });
            items.Add(new SelectListItem { Text = "Awaiting Approval", Value = "Awaiting Approval" });
            items.Add(new SelectListItem { Text = "Declined", Value = "Declined" });
            ViewBag.Roleos = items;
            var ctx = new UsersContext();
            return View(ctx.UserProfiles.Find(id));
        }

        //
        // POST: /UserProfile/Edit/5
        [Authorize(Roles="Admin")]
        [HttpPost]
        public ActionResult Edit(UserProfile chngUser, string Roleos)
        {
            try
            {
                string[] useRole = System.Web.Security.Roles.GetRolesForUser(chngUser.UserName);
                System.Web.Security.Roles.RemoveUserFromRole(chngUser.UserName, useRole[0]);
                System.Web.Security.Roles.AddUserToRole(chngUser.UserName, Roleos);
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        //
        // GET: /UserProfile/Delete/5

        public ActionResult Delete(int id)
        {
            return View();
        }

        //
        // POST: /UserProfile/Delete/5

        [HttpPost]
        public ActionResult Delete(UserProfile delUser)
        {
            try
            {
                //ViewBag.DelMessage = (System.Web.Security.Membership.DeleteUser(delUser.UserName)) ? "Delete Successful" : "Not able to Delete user!";
                string[] roles = System.Web.Security.Roles.GetRolesForUser(delUser.UserName);
                if (roles.Length > 0)
                {
                    System.Web.Security.Roles.RemoveUserFromRole(delUser.UserName, roles[0]);
                    ((SimpleMembershipProvider)Membership.Provider).DeleteUser(delUser.UserName, true); // deletes record from UserProfile table
                }

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
