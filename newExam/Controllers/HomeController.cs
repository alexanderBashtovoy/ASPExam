using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.DynamicData;
using System.Web.Mvc;
using System.Web.Script.Serialization;
using System.Web.WebPages;
using Microsoft.AspNet.Identity;
using newExam.Models;
using Filter = System.Web.Mvc.Filter;

namespace newExam.Controllers
{
    public class HomeController : Controller
    {
        TourContext db = new TourContext();

        public ActionResult Index()
        {
            IEnumerable<Tour> tour = db.Tours;
            return View(tour.ToList());
        }

        public string GetTour(int? Id)
        {
            Tour tour = (db.Tours.Where(t => t.Id == Id) as IEnumerable<Tour>).First();
            return new JavaScriptSerializer().Serialize(tour);
        }
        // public ActionResult GetItems(newExam.Models.Filter filter)
        //{
        //if (filter == null)
        //{
        //    return View("Tours", tours);
        //}

        //var toursFilter = from t in tours select t;            

        //if (!filter.Country.IsEmpty())
        //{
        //    toursFilter = from t in toursFilter where t.Country == filter.Country select t;
        //}
        //if (!filter.Country.IsEmpty())
        //{
        //    toursFilter = from t in toursFilter where t.Country == filter.Country select t;
        //}

        //   return View("Tours");
        //}
        [HttpGet]
        public ActionResult SetOrder(int? id)
        {
            if (id == null)
            {
                return HttpNotFound();
            }

            var tour = db.Tours.Find(id);

            if (tour == null)
            {
                return HttpNotFound();
            }

            var order = new Order() {ApplicationUserId = User.Identity.GetUserId(), UserName = User.Identity.Name, Tour = tour, TourId = tour.Id, Date = DateTime.Now};

            ViewBag.Tours = db.Tours as IEnumerable<Tour>;

            return View(order);
        }
        [HttpPost]
        public ActionResult AddOrder(int? Id)
        {
            if (Id == null)
            {
                return HttpNotFound();
            }

            var tour = db.Tours.Find(Id);

            if (tour == null)
            {
                return HttpNotFound();
            }

            var order = new Order() { ApplicationUserId = User.Identity.GetUserId(), Tour = tour, TourId = tour.Id };

            ViewBag.Tours = db.Tours as IEnumerable<Tour>;

            return View("SetOrder");
        
        }

        [HttpPost]
        public ActionResult SetOrder(Order order)
        {
            //if (ModelState.IsValid)
            {
                var tour = db.Tours.Find(order.TourId);
                order.Tour = tour;

                //order.ApplicationUser = (adb.Users.Where(user => user.Id == order.ApplicationUserId) as IEnumerable<ApplicationUser>).First();

                if (order.Id == 0)
                {
                    db.Orders.Add(order);
                    TempData["message"] = string.Format($"Тур №\"{order.Tour.Id}\" были оформлен");
                }
                    
                else
                {
                    Order dbEntry = db.Orders.Find(order.Id);
                    if (dbEntry != null)
                    {
                        dbEntry.Tour = tour;
                        dbEntry.TourId = order.TourId;
                        dbEntry.Date = order.Date;
                        dbEntry.NPersons = order.NPersons;

                        TempData["message"] = string.Format($"Тур №\"{order.Tour.Id}\" были переоформлен");
                    }
                }
            }

            db.SaveChanges();
            return RedirectToAction("Index", "Home");
        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }
    }
}