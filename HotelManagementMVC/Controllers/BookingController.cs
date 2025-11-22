using HotelManagementMVC.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.Entity;

namespace HotelManagementMVC.Controllers
{
    public class BookingController : Controller
    {
        private HotelContext db = new HotelContext();
        // GET: Booking
        public ActionResult Index()
        {
            var data = db.Bookings
                .Include(b => b.Room)
                .Include(b => b.Customer)
                .ToList();
            return View(data);
        }
        [HttpGet]
        public ActionResult Create()
        {
            ViewBag.RoomId = new SelectList(db.Rooms.Where(r  => r.IsAvailable == true),"RoomId", "RoomNumber");
            ViewBag.CustomerId = new SelectList(db.Customers, "CustomerId", "Name");
            return View();
        }
        [HttpPost]
        public ActionResult Create(Booking model)
        {
            if(ModelState.IsValid)
            {
                var data = db.Rooms.Find(model.RoomId);
                if (data == null)
                {
                    ModelState.AddModelError("", "Invalid Room selected.");
                    return View(model);
                }

                var totalDays = (model.CheckOutDate.Date - model.CheckInDate.Date).TotalDays;
                if (totalDays < 1)
                    totalDays = 1;

                model.TotalAmount = data.PricePerNight * (decimal)totalDays;

                db.Bookings.Add(model);

                data.IsAvailable = false;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.RoomId = new SelectList(db.Rooms.Where(r => r.IsAvailable == true), "RoomId", "RoomNumber", model.RoomId);
            ViewBag.CustomerId = new SelectList(db.Customers, "CustomerId", "Name", model.CustomerId);
            return View();
        }

        [HttpGet]
        public ActionResult Edit(int id)
        {
            var data = db.Bookings.Find(id);
            if (data == null)
                return HttpNotFound();

            ViewBag.RoomId = new SelectList(db.Rooms, "RoomId", "RoomNumber", data.RoomId);
            ViewBag.CustomerId = new SelectList(db.Customers, "CustomerId", "Name", data.CustomerId);
            return View(data);
        }
        [HttpPost]
        public ActionResult Edit(Booking model)
        {
            if (ModelState.IsValid)
            {
                var booking = db.Bookings.Find(model.BookingId);
                if (booking != null)
                {
                    booking.RoomId = model.RoomId;
                    booking.CustomerId = model.CustomerId;
                    booking.CheckInDate = model.CheckInDate;
                    booking.CheckOutDate = model.CheckOutDate;
                    booking.TotalAmount = model.TotalAmount;

                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
            }
            ViewBag.RoomId = new SelectList(db.Rooms, "RoomId", "RoomNumber", model.RoomId);
            ViewBag.CustomerId = new SelectList(db.Customers, "CustomerId", "Name", model.CustomerId);
            return View(model);
        }

       public ActionResult Delete(int id)
{
    var booking = db.Bookings.Find(id);
    if (booking == null)
        return HttpNotFound(); 


    var room = db.Rooms.Find(booking.RoomId);

    db.Bookings.Remove(booking); 
    if (room != null)
        room.IsAvailable = true;

    db.SaveChanges();

    return RedirectToAction("Index");
}
        public ActionResult Details(int id)
        {
            var data = db.Bookings
                .Include(b => b.Room)
                .Include(b => b.Customer)
                .FirstOrDefault(b => b.BookingId == id);

            if (data == null)
                return HttpNotFound();

            return View(data);
        }
    }
}