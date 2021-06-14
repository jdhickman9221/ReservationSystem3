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
    [Authorize(Roles = "Admin")]
    public class PortfolioItemsController : Controller
    {

        private ReservationSystemEntities db = new ReservationSystemEntities();

        // GET: PortfolioItems
        [AllowAnonymous]
        public ActionResult Index()
        {
            return View(db.PortfolioItems.ToList());
        }

        // GET: PortfolioItems/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PortfolioItem portfolioItem = db.PortfolioItems.Find(id);
            if (portfolioItem == null)
            {
                return HttpNotFound();
            }
            return View(portfolioItem);
        }

        // GET: PortfolioItems/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: PortfolioItems/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "JobID,JobName,Photo,JobDescription,JobReview,ProjectLink")] PortfolioItem portfolioItem, HttpPostedFileBase Photo)
        {
            string imgName = "noImage.png";

            if (Photo != null)
            {
                imgName = Photo.FileName;
                string ext = imgName.Substring(imgName.LastIndexOf('.'));
                string[] goodExts = { ".jpeg", ".jpg", ".gif", ".png" };
                if (goodExts.Contains(ext.ToLower()) && (Photo.ContentLength <= 4194304))
                {
                    imgName = Guid.NewGuid() + ext;
                    string savePath = Server.MapPath("~/Content/assets/img/uploads/");
                    Image convertedImage = Image.FromStream(Photo.InputStream);
                    int maxImageSize = 500;
                    int maxThumbSize = 100;
                    ImageServices.ResizeImage(savePath, imgName, convertedImage, maxImageSize, maxThumbSize);
                }//end if statement
                else
                {
                    imgName = "noImage.png";
                }//end else
            }//end if statement

            portfolioItem.Photo = imgName;


            if (!ModelState.IsValid)//if not valid send obj back.
            {
                return View(portfolioItem);
            }
            if (ModelState.IsValid)
            {
                db.PortfolioItems.Add(portfolioItem);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(portfolioItem);
        }

        // GET: PortfolioItems/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PortfolioItem portfolioItem = db.PortfolioItems.Find(id);
            if (portfolioItem == null)
            {
                return HttpNotFound();
            }
            return View(portfolioItem);
        }

        // POST: PortfolioItems/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "JobID,JobName,Photo,JobDescription,JobReview,ProjectLink")] PortfolioItem portfolioItem, HttpPostedFileBase Photo)
        {

            if (ModelState.IsValid)
            {

                string imgName = portfolioItem.Photo;
                if (Photo != null)
                {
                    imgName = Photo.FileName;
                    string ext = imgName.Substring(imgName.LastIndexOf('.'));
                    string[] goodExts = { ".jpeg", ".jpg", ".gif", ".png" };

                    if (goodExts.Contains(ext.ToLower()) && (Photo.ContentLength <= 4194304))
                    {
                        imgName = Guid.NewGuid() + ext;
                        string savePath = Server.MapPath("~/Content/assets/img/uploads/" + imgName);
                        Image convertedImage = Image.FromStream(Photo.InputStream);
                        int maxImageSize = 500;
                        int maxThumbSize = 100;

                        ImageServices.ResizeImage(savePath, imgName, convertedImage, maxImageSize, maxThumbSize);

                        if (portfolioItem.Photo != null && portfolioItem.Photo != "noImage.png")
                        {
                            string path = Server.MapPath("~/Content/assets/img/uploads");
                            ImageServices.Delete(path, portfolioItem.Photo);
                        }//end if
                    }//end if
                }//end if

                portfolioItem.Photo = imgName;

                db.Entry(portfolioItem).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }//end if
            return View(portfolioItem);
        }

        // GET: PortfolioItems/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PortfolioItem portfolioItem = db.PortfolioItems.Find(id);
            if (portfolioItem == null)
            {
                return HttpNotFound();
            }
            return View(portfolioItem);
        }

        // POST: PortfolioItems/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            PortfolioItem portfolioItem = db.PortfolioItems.Find(id);
            if (portfolioItem.Photo != null && portfolioItem.Photo != "noImage.png")
            {
                System.IO.File.Delete(Server.MapPath("~/Content/assets/img/uploads/" + portfolioItem.Photo));
            }
            else
            {
                portfolioItem.Photo = "noImage.png";
                
            }
            db.PortfolioItems.Remove(portfolioItem);
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
