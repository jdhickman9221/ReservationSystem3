using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using AxelHarveyStudio.DATA.EF;
using AxelHarveyStudio.UI.MVC.Utilitites;

namespace AxelHarveyStudio.UI.MVC.Controllers
{
    public class LocationsController : Controller
    {
        private ReservationSystemEntities db = new ReservationSystemEntities();

        // GET: Locations
        public ActionResult Index()
        {
            return View(db.Locations.ToList());
        }

        // GET: Locations/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Location location = db.Locations.Find(id);
            if (location == null)
            {
                return HttpNotFound();
            }
            return View(location);
        }

        // GET: Locations/Create
        [Authorize(Roles = "Admin")]
        public ActionResult Create()
        {
            return View();
        }

        // POST: Locations/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "LocationID,LocationName,Address,City,State,ZipCode,ReservationLimit,Description,LocationPhoto")] Location location, HttpPostedFileBase LocationPhoto)
        {
            #region FileUpload
            string imgName = "noImage.png";

            if (LocationPhoto != null)
            {
                imgName = LocationPhoto.FileName;
                string ext = imgName.Substring(imgName.LastIndexOf('.'));
                string[] goodExts = { ".jpg", ".jpg", ".gif", ".png" };

                if (goodExts.Contains(ext.ToLower()) && (LocationPhoto.ContentLength <= 4194304))
                {
                    imgName = Guid.NewGuid() + ext;

                    #region Resize Image
                    string savePath = Server.MapPath("~/Content/assets/img/uploads/");
                    Image convertedImage = Image.FromStream(LocationPhoto.InputStream);
                    int maxImageSize = 500;
                    int maxThumbSize = 100;
                    #endregion

                    ImageServices.ResizeImage(savePath, imgName, convertedImage, maxImageSize, maxThumbSize);
                }//end if statement
            }//end if statement
            location.LocationLogo = imgName;
            if (!ModelState.IsValid)
            {
                return View(location);
            }
            if (ModelState.IsValid)
            {
                db.Locations.Add(location);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(location);
        }
        #endregion

        // GET: Locations/Edit/5
        [Authorize(Roles = "Admin")]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Location location = db.Locations.Find(id);
            if (location == null)
            {
                return HttpNotFound();
            }
            return View(location);
        }

        // POST: Locations/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "LocationID,LocationName,Address,City,State,ZipCode,ReservationLimit,Description,LocationPhoto")] Location location, HttpPostedFileBase LocationPhoto)
        {


            if (ModelState.IsValid)
            {

                string imgName = location.LocationLogo;
                if (LocationPhoto != null)
                {
                    imgName = LocationPhoto.FileName;
                    string ext = imgName.Substring(imgName.LastIndexOf('.'));
                    string[] goodExts = { ".jpeg", ".jpg", ".gif", ".png" };

                    if (goodExts.Contains(ext.ToLower()) && (LocationPhoto.ContentLength <= 4194304))
                    {
                        imgName = Guid.NewGuid() + ext;

                        #region Resize Image
                        string savePath = Server.MapPath("~/Content/assets/img/uploads/");
                        Image convertedImage = Image.FromStream(LocationPhoto.InputStream);
                        int maxImageSize = 500;
                        int maxThumbSize = 100;

                        ImageServices.ResizeImage(savePath, imgName, convertedImage, maxImageSize, maxThumbSize);
                        #endregion

                        if (location.LocationLogo != null && location.LocationLogo != "noImage.png")
                        {
                            string path = Server.MapPath("~/Content/assets/img/uploads/");
                            ImageServices.Delete(path, location.LocationLogo);
                        }//end if statement
                    }//end if statement
                }//end if statment

                location.LocationLogo = imgName;


                db.Entry(location).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }//end if statement
            return View(location);
        }
        // GET: Locations/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Location location = db.Locations.Find(id);
            if (location == null)
            {
                return HttpNotFound();
            }
            return View(location);
        }

        // POST: Locations/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Location location = db.Locations.Find(id);

            if (location.LocationLogo != null && location.LocationLogo != "noImage.png")
            {
                System.IO.File.Delete(Server.MapPath("~/Content/assets/img/uploads/" + Session["currentImage"].ToString()));
            }
            db.Locations.Remove(location);
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
