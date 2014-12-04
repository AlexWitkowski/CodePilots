using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using EASNSMVC4.Models;
using EASNSMVC4.DAL;
using System.Net;
using EASNSMVC4.ViewModels;
using EASNSMVC4.Classes;
using System.IO;
using System.Web.Security;

namespace EASNSMVC4.Controllers
{
    public class ReportingController : Controller
    {
        private DBEntity db = new DBEntity();

        //
        // GET: /Reporting/

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult RepResource()
        {
            var res = db.Resources.OrderByDescending(i => i.NumDownloads).ThenBy(i => i.Title).ToList();
            var numOne = res[0];
            var numTwo = res[1];
            var numThree = res[2];
            var numFour = res[3];
            var numFive = res[4];
            List<Resource> rez = new List<Resource>();
            rez.Add(numOne);
            rez.Add(numTwo);
            rez.Add(numThree);
            rez.Add(numFour);
            rez.Add(numFive);
 
            return View(rez);
        }

        public ActionResult RepResource1()
        {
            var res = db.Resources.OrderByDescending(i => i.NumDownloads).ThenBy(i => i.Title).ToList();
            var numOne = res[0];
            List<Resource> rez = new List<Resource>();
            rez.Add(numOne);

            return View(rez);
        }

        public ActionResult RepResource10()
        {
            var res = db.Resources.OrderByDescending(i => i.NumDownloads).ThenBy(i => i.Title).ToList();
            var numOne = res[0];
            var numTwo = res[1];
            var numThree = res[2];
            var numFour = res[3];
            var numFive = res[4];
            var numSix = res[5];
            var numSeven = res[6];
            var numEight = res[7];
            var numNine = res[8];
            var numTen = res[9];

            List<Resource> rez = new List<Resource>();
            rez.Add(numOne);
            rez.Add(numTwo);
            rez.Add(numThree);
            rez.Add(numFour);
            rez.Add(numFive);
            rez.Add(numSix);
            rez.Add(numSeven);
            rez.Add(numEight);
            rez.Add(numNine);
            rez.Add(numTen);

            return View(rez);
        }

        public ActionResult RepUserCities()
        {
            using (var ctx = new UsersContext())
            {
                var trial = ctx.UserProfiles.GroupBy(i => i.UserCity).ToList();

                var trialNum = trial.Count();

                var cityOne = trial[0].ToList();
                var cityOneName = cityOne[0].UserCity;

                var cityTwo = trial[0].ToList();
                var cityTwoName = cityTwo[0].UserCity;

                var cityThree = trial[0].ToList();
                var cityThreeName = cityThree[0].UserCity;

                var oneCount = 0;
                var twoCount = 0;
                var threeCount = 0;

                for (var i = 0; i < trialNum; i++ )
                {
                    if( trial[i].Count() > oneCount)
                    {
                        cityThreeName = cityTwoName;
                        threeCount = twoCount;
                        cityTwoName = cityOneName;
                        twoCount = oneCount;
                        cityOne = trial[i].ToList();
                        cityOneName = cityOne[0].UserCity;
                        oneCount = trial[i].Count();
                    }

                    else if( trial[i].Count() > twoCount)
                    {
                        cityThreeName = cityTwoName;
                        threeCount = twoCount;
                        cityTwo = trial[i].ToList();
                        cityTwoName = cityTwo[0].UserCity;
                        twoCount = trial[i].Count();
                    }

                    else if (trial[i].Count() > threeCount)
                    {
                        cityThree = trial[i].ToList();
                        cityThreeName = cityThree[0].UserCity;
                        threeCount = trial[i].Count();
                    }
                }


                @ViewBag.UseCity1 = cityOneName;
                @ViewBag.UseCity2 = cityTwoName;
                @ViewBag.UseCity3 = cityThreeName;
                @ViewBag.City1Count = oneCount;
                @ViewBag.City2Count = twoCount;
                @ViewBag.City3Count = threeCount;

                return View();
            }
                
        }

        public ActionResult RepUserCities1()
        {
            using (var ctx = new UsersContext())
            {
                var trial = ctx.UserProfiles.GroupBy(i => i.UserCity).ToList();

                var trialNum = trial.Count();

                var cityOne = trial[0].ToList();
                var cityOneName = cityOne[0].UserCity;
                var oneCount = 0;

                for (var i = 0; i < trialNum; i++)
                {
                    if (trial[i].Count() > oneCount)
                    {
                        cityOne = trial[i].ToList();
                        cityOneName = cityOne[0].UserCity;
                        oneCount = trial[i].Count();
                    }
                }

                @ViewBag.UseCity1 = cityOneName;
                @ViewBag.City1Count = oneCount;

                return View();
            }

        }

        public ActionResult RepUserCities5()
        {
            using (var ctx = new UsersContext())
            {
                var trial = ctx.UserProfiles.GroupBy(i => i.UserCity).ToList();

                var trialNum = trial.Count();

                var cityOne = trial[0].ToList();
                var cityOneName = cityOne[0].UserCity;

                var cityTwo = trial[0].ToList();
                var cityTwoName = cityTwo[0].UserCity;

                var cityThree = trial[0].ToList();
                var cityThreeName = cityThree[0].UserCity;

                var cityFour = trial[0].ToList();
                var cityFourName = cityFour[0].UserCity;

                var cityFive = trial[0].ToList();
                var cityFiveName = cityFive[0].UserCity;

                var oneCount = 0;
                var twoCount = 0;
                var threeCount = 0;
                var fourCount = 0;
                var fiveCount = 0;

                for (var i = 0; i < trialNum; i++)
                {
                    if (trial[i].Count() > oneCount)
                    {
                        cityFiveName = cityFourName;
                        fiveCount = fourCount;
                        cityFourName = cityThreeName;
                        fourCount = threeCount;
                        cityThreeName = cityTwoName;
                        threeCount = twoCount;
                        cityTwoName = cityOneName;
                        twoCount = oneCount;
                        cityOne = trial[i].ToList();
                        cityOneName = cityOne[0].UserCity;
                        oneCount = trial[i].Count();
                    }

                    else if (trial[i].Count() > twoCount)
                    {
                        cityFiveName = cityFourName;
                        fiveCount = fourCount;
                        cityFourName = cityThreeName;
                        fourCount = threeCount;
                        cityThreeName = cityTwoName;
                        threeCount = twoCount;
                        cityTwo = trial[i].ToList();
                        cityTwoName = cityTwo[0].UserCity;
                        twoCount = trial[i].Count();
                    }

                    else if (trial[i].Count() > threeCount)
                    {
                        cityFiveName = cityFourName;
                        fiveCount = fourCount;
                        cityFourName = cityThreeName;
                        fourCount = threeCount;
                        cityThree = trial[i].ToList();
                        cityThreeName = cityThree[0].UserCity;
                        threeCount = trial[i].Count();
                    }

                    else if (trial[i].Count() > fourCount)
                    {
                        cityFiveName = cityFourName;
                        fiveCount = fourCount;
                        cityFour = trial[i].ToList();
                        cityFourName = cityFour[0].UserCity;
                        fourCount = trial[i].Count();
                    }

                    else if (trial[i].Count() > fiveCount)
                    {
                        cityFive = trial[i].ToList();
                        cityFiveName = cityFive[0].UserCity;
                        fiveCount = trial[i].Count();
                    }
                }


                @ViewBag.UseCity1 = cityOneName;
                @ViewBag.UseCity2 = cityTwoName;
                @ViewBag.UseCity3 = cityThreeName;
                @ViewBag.UseCity4 = cityFourName;
                @ViewBag.UseCity5 = cityFiveName;
                @ViewBag.City1Count = oneCount;
                @ViewBag.City2Count = twoCount;
                @ViewBag.City3Count = threeCount;
                @ViewBag.City4Count = fourCount;
                @ViewBag.City5Count = fiveCount;

                return View();
            }

        }

        public ActionResult RepVendor(string sortOrder, string searchString)
        {
            ViewBag.TitleSortParm = String.IsNullOrEmpty(sortOrder) ? "title_Desc" : "";


            var vens = db.Vendors.OrderBy(i => i.Name).ToList();

            if (!String.IsNullOrEmpty(searchString))
            {
                return View(vens.Where(r => r.Name.ToUpper().Contains(searchString.ToUpper())));
            }

            if (sortOrder == "title_Desc")
            {
                return View(vens.OrderByDescending(r => r.Name).ToList());
            }

            else
            {
                return View(vens);
            }
        }

        //
        // GET: /Reporting/Details/5

        public ActionResult Details(int id)
        {
            return View();
        }

        //
        // GET: /Reporting/Create

        public ActionResult Create()
        {
            return View();
        }

        //
        // POST: /Reporting/Create

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
        // GET: /Reporting/Edit/5

        public ActionResult Edit(int id)
        {
            return View();
        }

        //
        // POST: /Reporting/Edit/5

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
        // GET: /Reporting/Delete/5

        public ActionResult Delete(int id)
        {
            return View();
        }

        //
        // POST: /Reporting/Delete/5

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
