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
using Microsoft.AspNet.Identity;
using AxelHarveyStudio.UI.MVC.Utilitites;

namespace AxelHarveyStudio.UI.MVC.Controllers
{
    public class OwnerAssetsController : Controller
    {
        private ReservationSystemEntities db = new ReservationSystemEntities();

        // GET: OwnerAssets

            
        public ActionResult Index()
        {

            //Roadblock highlight 1:
            if (Request.IsAuthenticated && User.IsInRole("Admin") || User.IsInRole("Employee"))
            {
                var ownerAssets = db.OwnerAssets.Include(o => o.UserDetail);
                return View(ownerAssets.ToList());
            }
            else
            {
                var currentUser = User.Identity.GetUserId();
                string userID = User.Identity.GetUserId();
                var userAssets = db.OwnerAssets.Where(a => a.UserID == userID).Include(o => o.UserDetail);


                return View(userAssets.ToList());
              
            }
            
           
        }

        // GET: OwnerAssets/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            OwnerAsset ownerAsset = db.OwnerAssets.Find(id);
            if (ownerAsset == null)
            {
                return HttpNotFound();
            }
            return View(ownerAsset);
        }

        // GET: OwnerAssets/Create
        public ActionResult Create()
        {
            var currentUser = User.Identity.GetUserId();
            ViewBag.UserID = new SelectList(db.UserDetails, "UserID", "FirstName");
            ViewBag.AssetCount = db.OwnerAssets.Where(a => a.IsActive && a.UserID == currentUser).Count();
            return View();
        }

        // POST: OwnerAssets/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        //Image Upload step 6: add a parameter to the action result, class and the obj/field of the class you want to upload for.
        public ActionResult Create([Bind(Include = "OwnerAssetID,AssetName,UserID,AssetPhoto,OwnerNotes,EmployeeNotes,IsActive,DateAdded,Review")] OwnerAsset ownerAsset, HttpPostedFileBase AssetPhoto)
        {

            //Image Upload step 7: add a region for the file upload.
            #region File Upload
            //get a default image if none is provided. i.e. imgName = "noImage.png" (in your uploads folder)
            DateTime dateAdded = DateTime.Now;
            ownerAsset.DateAdded = dateAdded;
            string userID = User.Identity.GetUserId();
            ownerAsset.UserID = userID;
            string imgName = "noImage.png";

            if (AssetPhoto != null)
            {
                imgName = AssetPhoto.FileName;//get image and reassign the variable with it.

                string ext = imgName.Substring(imgName.LastIndexOf('.'));//We take the imgName (now file name of obj) and we want to use
                                                                         //substring method to the last index of '.' where the period is,                                           we are hunting for it's extension.

                //declare a list of valid extensions (i.e. popular image formats that browsers can take)
                string[] goodExts = { ".jpeg", ".jpg", ".gif", ".png" };
                #endregion
                //check the ext variable (ToLower()) against the valid list, this ensures we can use it whether or not the 
                //file uploaded was in caps or lower case.
                if (goodExts.Contains(ext.ToLower()) && (AssetPhoto.ContentLength <= 4194304)) //4mb max by ASP.NET
                {
                    //if it is in the list rename using a guid.
                    imgName = Guid.NewGuid() + ext;

                    #region Resize Image
                    string savePath = Server.MapPath("~/Content/assets/img/uploads/");
                    Image convertedImage = Image.FromStream(AssetPhoto.InputStream);
                    int maxImageSize = 500;
                    int maxThumbSize = 100;

                    ImageServices.ResizeImage(savePath, imgName, convertedImage, maxImageSize, maxThumbSize);
                    #endregion


                }//end if statement
            }//end if statement

            ownerAsset.AssetPhoto = imgName;




            //no matter what add the imgName to the object.

            if (ModelState.IsValid)
            {

                db.OwnerAssets.Add(ownerAsset);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.UserID = new SelectList(db.UserDetails, "UserID", "FirstName", ownerAsset.UserID);
            return View(ownerAsset);
        }

        // GET: OwnerAssets/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            OwnerAsset ownerAsset = db.OwnerAssets.Find(id);
            if (ownerAsset == null)
            {
                return HttpNotFound();
            }
            ViewBag.UserID = new SelectList(db.UserDetails, "UserID", "FirstName", ownerAsset.UserID);
            return View(ownerAsset);
        }

        // POST: OwnerAssets/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "OwnerAssetID,AssetName,UserID,AssetPhoto,OwnerNotes,EmployeeNotes,IsActive,DateAdded,Review")] OwnerAsset ownerAsset, HttpPostedFileBase AssetPhoto)
        {
             DateTime dateAdded = DateTime.Now;
            ownerAsset.DateAdded = dateAdded;
            string userID = User.Identity.GetUserId();
            ownerAsset.UserID = userID;
            if (ModelState.IsValid)
            {
                string imgName = ownerAsset.AssetPhoto;
                if (AssetPhoto != null)
                {
                    imgName = AssetPhoto.FileName;//get image and reassign the variable with it.

                    string ext = imgName.Substring(imgName.LastIndexOf('.'));//We take the imgName (now file name of obj) and we want to use
                                                                             //substring method to the last index of '.' where the period is,                                           we are hunting for it's extension.

                    //declare a list of valid extensions (i.e. popular image formats that browsers can take)
                    string[] goodExts = { ".jpeg", ".jpg", ".gif", ".png" };

                    if (goodExts.Contains(ext.ToLower()) && (AssetPhoto.ContentLength <= 4194304)) //4mb max by ASP.NET
                    {
                        //if it is in the list rename using a guid.
                        imgName = Guid.NewGuid() + ext;

                        #region Resize Image
                        string savePath = Server.MapPath("~/Content/assets/img/uploads/");
                        Image convertedImage = Image.FromStream(AssetPhoto.InputStream);
                        int maxImageSize = 500;
                        int maxThumbSize = 100;

                        ImageServices.ResizeImage(savePath, imgName, convertedImage, maxImageSize, maxThumbSize);
                        #endregion

                        if (ownerAsset.AssetPhoto != null && ownerAsset.AssetPhoto != "noImage.png")
                        {
                            string path = Server.MapPath("~/Content/assets/img/uploads/");
                            ImageServices.Delete(path, ownerAsset.AssetPhoto);
                        }//end if statment
                    }//end if statement
                }//end if statement

                //no matter what updat the AssetPhoto with the value of the file variable.
                ownerAsset.AssetPhoto = imgName;

                db.Entry(ownerAsset).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }//end if statement
            ViewBag.UserID = new SelectList(db.UserDetails, "UserID", "FirstName", ownerAsset.UserID);
            return View(ownerAsset);

        }//end EDIT POST

        // GET: OwnerAssets/Delete/5
        [Authorize]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            OwnerAsset ownerAsset = db.OwnerAssets.Find(id);
            if (ownerAsset == null)
            {
                return HttpNotFound();
            }
            return View(ownerAsset);
        }

        // POST: OwnerAssets/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            OwnerAsset ownerAsset = db.OwnerAssets.Find(id);

            if (ownerAsset.AssetPhoto != null && ownerAsset.AssetPhoto != "noImage.png")
            {
                System.IO.File.Delete(Server.MapPath("~/Content/assets/img/uploads" + Session["currentImage"].ToString()));
            }
            db.OwnerAssets.Remove(ownerAsset);
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
