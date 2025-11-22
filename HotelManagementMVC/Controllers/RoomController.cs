using HotelManagementMVC.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace HotelManagementMVC.Controllers
{
    public class RoomController : Controller
    {
        HotelContext db = new HotelContext();
        // GET: Room
        public ActionResult Index()
        {
            return View(db.Rooms.ToList());
        }

        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Create(Room model)
        {
            if(ModelState.IsValid)
            {
                db.Rooms.Add(model);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(model);
        }

        [HttpGet]
        public ActionResult Edit(int id)
        {
            return View(db.Rooms.Find(id));
        }
        [HttpPost]
        public ActionResult Edit(Room model)
        {
            var data = db.Rooms.Find(model.RoomId);
            if (TryUpdateModel(data))
            {
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(data);
        }
        public ActionResult Details(int id)
        {
            var data = db.Rooms.Find(id);
            return View(data); 
        }
        public ActionResult Delete(int id)
        {
            var data = db.Rooms.Find(id);
            db.Rooms.Remove(data);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}