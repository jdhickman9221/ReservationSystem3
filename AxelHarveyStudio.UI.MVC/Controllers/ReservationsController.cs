using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Web;
using System.Web.Mvc;
using AxelHarveyStudio.DATA.EF;
using Microsoft.AspNet.Identity;

namespace AxelHarveyStudio.UI.MVC.Controllers
{
    public class ReservationsController : Controller
    {
        private ReservationSystemEntities db = new ReservationSystemEntities();

        // GET: Reservations
        [Authorize]
        public ActionResult Index()
        {
            var currentUser = User.Identity.GetUserId();
            var assets = db.OwnerAssets.Where(i => i.UserID == currentUser && i.IsActive).ToList().Count;

            if (Request.IsAuthenticated && (User.IsInRole("Admin") || User.IsInRole("Employee")))
            {
                ViewBag.LocationID = new SelectList(db.Locations, "LocationID", "LocationName");
                ViewBag.OwnerAssetID = new SelectList(db.OwnerAssets, "OwnerAssetID", "AssetName");
                var reservations = db.Reservations.Include(r => r.Location).Include(r => r.OwnerAsset);
                return View(reservations.ToList());
            }
            else if (assets != 0)//if they do have assets and are not in roles admin or emp
            {
                ViewBag.LocationID = new SelectList(db.Locations, "LocationID", "LocationName");
                ViewBag.OwnerAssetID = new SelectList(db.OwnerAssets.Where(o => o.UserID == currentUser), "OwnerAssetID", "AssetName");
                var reservations = db.Reservations.Where(u => u.OwnerAsset.UserID == currentUser);
                return View(reservations.ToList());
            }
            else
            {
                return RedirectToAction("Create", "OwnerAssets");
            }

            //if (Request.IsAuthenticated && !User.IsInRole("Admin") || !User.IsInRole("Employee"))//If they are not Admin or Employee and Logged in.
            //{
            //    string userID = User.Identity.GetUserId();
            //    var userReservations = db.Reservations.Where(a => a.OwnerAsset.UserID == userID).Include(r => r.OwnerAsset);
            //    return View(userReservations.ToList());
            //}
            //else//Admin/Employee Query.
            //{
            //    if (true)
            //    {

            //    }
            //    var reservations = db.Reservations.Include(r => r.Location).Include(r => r.OwnerAsset);
            //    return View(reservations.ToList());
            //}

        }

        // GET: Reservations/Details/5
        [Authorize(Roles = "Admin")]
        public ActionResult Details(int? id)

        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Reservation reservation = db.Reservations.Find(id);
            if (reservation == null)
            {
                return HttpNotFound();
            }
            return View(reservation);
        }

        // GET: Reservations/Create
        public ActionResult Create()
        {
            if (Request.IsAuthenticated && User.IsInRole("Admin") || User.IsInRole("Employee"))
            {
                ViewBag.LocationID = new SelectList(db.Locations, "LocationID", "LocationName");
                ViewBag.OwnerAssetID = new SelectList(db.OwnerAssets, "OwnerAssetID", "AssetName");
               
            }
            else
            {
                var currentUser = User.Identity.GetUserId();
                ViewBag.LocationID = new SelectList(db.Locations, "LocationID", "LocationName");
                ViewBag.OwnerAssetID = new SelectList(db.OwnerAssets.Where(o => o.UserID == currentUser && o.IsActive), "OwnerAssetID", "AssetName");
            }
           
            return View();
        }

        // POST: Reservations/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ReserationID,OwnerAssetID,LocationID,ReservationDate")] Reservation reservation)
        {

            if (ModelState.IsValid)
            {
                int reservationCount = db.Reservations.Where(x => x.ReservationDate == reservation.ReservationDate && x.LocationID == reservation.LocationID).Count();//gets the count where bookings are at the same place and the same date.
                Location locations = db.Locations.Find(reservation.LocationID);//this goes to the db for locations and finds the value for
                //what the current model's location is. We can access the props from this. Such as the reservatin limit prop.

                if (locations.ReservationLimit > reservationCount)//if the count (which is the bookings at the same date and same place)
                    //are less than the daily limit, then it will be created.
                {
                    var currentUser = User.Identity.GetUserName();
                    db.Reservations.Add(reservation);
                    db.SaveChanges();

                    string subject = $"{currentUser} added a new reservation";
                    string message = $"You have a new reservation! On Date:{reservation.ReservationDate.ToShortDateString()}. At Studio: {reservation.Location.LocationName}. Customer: {currentUser}.";
                    MailMessage mm = new MailMessage(
                        ConfigurationManager.AppSettings["EmailUser"].ToString(),
                        ConfigurationManager.AppSettings["EmailTo"].ToString(),
                        subject,
                        message
                        );

                    SmtpClient client = new SmtpClient(ConfigurationManager.AppSettings["EmailClient"].ToString());
                    client.Port = 8889; //This is for testing locally.
                    client.Credentials = new NetworkCredential(ConfigurationManager.AppSettings["EmailUser"].ToString(), ConfigurationManager.AppSettings["EmailPass"].ToString());


                    client.Send(mm);

                    MailMessage mm2 = new MailMessage(
                        ConfigurationManager.AppSettings["EmailUser"].ToString(),
                        currentUser,
                        subject,
                        message
                        );
                    client.Send(mm2);

                    

                }
                else
                {
                   
                    if (locations.ReservationLimit <= reservationCount && User.IsInRole("Admin"))//if the booking limit has been met, 
                        //and the user is in the create booking AND is admin role, then they are able to override it. 
                    {

                        db.Reservations.Add(reservation);
                        db.SaveChanges();
                        return RedirectToAction("Index");
                    }
                    ViewBag.LocationID = new SelectList(db.Locations, "LocationID", "LocationName", reservation.LocationID);
                    ViewBag.OwnerAssetID = new SelectList(db.OwnerAssets, "OwnerAssetID", "AssetName", reservation.OwnerAssetID);
                    return View("ErrorView");
                }
                return RedirectToAction("Index");
            }
            

            ViewBag.LocationID = new SelectList(db.Locations, "LocationID", "LocationName", reservation.LocationID);
            ViewBag.OwnerAssetID = new SelectList(db.OwnerAssets, "OwnerAssetID", "AssetName", reservation.OwnerAssetID);
            return View(reservation);
        }

        // GET: Reservations/Edit/5
        [Authorize(Roles = "Admin")]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Reservation reservation = db.Reservations.Find(id);
            if (reservation == null)
            {
                return HttpNotFound();
            }
            ViewBag.LocationID = new SelectList(db.Locations, "LocationID", "LocationName", reservation.LocationID);
            ViewBag.OwnerAssetID = new SelectList(db.OwnerAssets, "OwnerAssetID", "AssetName", reservation.OwnerAssetID);
            return View(reservation);
        }

        // POST: Reservations/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ReserationID,OwnerAssetID,LocationID,ReservationDate")] Reservation reservation)
        {
            if (ModelState.IsValid)
            {
                db.Entry(reservation).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.LocationID = new SelectList(db.Locations, "LocationID", "LocationName", reservation.LocationID);
            ViewBag.OwnerAssetID = new SelectList(db.OwnerAssets, "OwnerAssetID", "AssetName", reservation.OwnerAssetID);
            return View(reservation);
        }

        // GET: Reservations/Delete/5
        [Authorize(Roles = "Admin")]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Reservation reservation = db.Reservations.Find(id);
            if (reservation == null)
            {
                return HttpNotFound();
            }
            return View(reservation);
        }

        // POST: Reservations/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Reservation reservation = db.Reservations.Find(id);
            db.Reservations.Remove(reservation);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
