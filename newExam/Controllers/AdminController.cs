using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using newExam.Models;

namespace newExam.Controllers
{
    [Authorize(Roles = "admin")]
    public class AdminController : Controller
    {
        TourContext db = new TourContext();
        ApplicationDbContext adb = new ApplicationDbContext();
        // GET: Admin
        public ActionResult Index()
        {
            return View(db.Tours.ToList());
        }

        [HttpGet]
        public ActionResult EditTour(int? id)
        {
            if (id == null)
            {
                return HttpNotFound();
            }
            Tour tour = db.Tours.Find(id);
            if (tour != null)
            {
                return View(tour);
            }
            return HttpNotFound();
        }
        [HttpPost]
        public ActionResult EditTour(Tour tour)
        {
            if (ModelState.IsValid)
            {
                if (tour.Id == 0)
                    db.Tours.Add(tour);
                else
                {
                    Tour dbEntry = db.Tours.Find(tour.Id);
                    if (dbEntry != null)
                    {
                        dbEntry.City = tour.City;
                        dbEntry.Country = tour.Country;
                        dbEntry.Type = tour.Type;
                        dbEntry.Description = tour.Description;
                        dbEntry.Price = tour.Price;
                    }
                }

                db.SaveChanges();
                TempData["message"] = string.Format($"Изменения в туре № \"{tour.Id}\" были сохранены");
                return RedirectToAction("Index");
            }

            return View(tour);
        }
        public ViewResult Create()
        {
            return View("EditTour", new Tour());
        }
        [HttpPost]
        public ActionResult Delete(int Id)
        {
            List<Tour> deletedTour = db.Tours.Where(t => t.Id == Id).ToList();
            if (deletedTour[0] != null)
            {
                TempData["message"] = string.Format($"Тур \"{deletedTour[0].Country} - {deletedTour[0].City} - {deletedTour[0].Type}\" была удален");
                db.Tours.Remove(deletedTour[0]);
                db.SaveChanges();
            }
            return RedirectToAction("Index");
        }

        public ActionResult Orders()
        {
            var orders = db.Orders.Include("Tour");
            ViewBag.User = adb.Users;
            return View(orders.ToList());
        }
        public ActionResult EditOrder()
        {
            IEnumerable<Order> orders = db.Orders;
            return null;
        }
        public ActionResult DeleteOrder(int Id)
        {
            var order = db.Orders.Find(Id);
            db.Orders.Remove(order);
            db.SaveChanges();
            TempData["message"] = string.Format($"Заказ был удалён");

            var orders = db.Orders.Include("Tour");

            return View("Orders", orders);
        }
    }
}