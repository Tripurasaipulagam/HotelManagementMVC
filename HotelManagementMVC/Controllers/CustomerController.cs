using HotelManagementMVC.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace HotelManagementMVC.Controllers
{
    public class CustomerController : Controller
    {
        HotelContext db = new HotelContext();
        // GET: Customer
        public ActionResult Index()
        {
            return View(db.Customers.ToList());
        }

        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Create(Customer model)
        {
            if(ModelState.IsValid)
            {
                db.Customers.Add(model);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View();
        }

        [HttpGet]
        public ActionResult Edit(int id)
        {
            return View(db.Customers.Find(id));
        }
        [HttpPost]
        public ActionResult Edit(Customer model)
        {
            var data = db.Customers.Find(model.CustomerId);
            if(TryUpdateModel(data))
            {
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(data);
        }
        public ActionResult Delete(int id)
        {
            var data = db.Customers.Find(id);
            db.Customers.Remove(data);  
            db.SaveChanges();
            return RedirectToAction("Index");
        }
        public ActionResult Details(int id)
        {
            return View(db.Customers.Find(id));
        }
    }
}