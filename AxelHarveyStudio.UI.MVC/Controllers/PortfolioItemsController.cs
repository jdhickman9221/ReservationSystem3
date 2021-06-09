using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using AxelHarveyStudio.DATA.EF;

namespace AxelHarveyStudio.UI.MVC.Controllers
{
    [Authorize(Roles = "Admin")]
    public class PortfolioItemsController : Controller
    {
        private ReservationSystemEntities db = new ReservationSystemEntities();

        // GET: PortfolioItems
        public ActionResult Index()
        {
            return View(db.PortfolioItems.ToList());
        }

        // GET: PortfolioItems/Details/5
        [AllowAnonymous]
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
        public ActionResult Create([Bind(Include = "JobID,JobName,Photo,JobDescription,JobReview")] PortfolioItem portfolioItem)
        {
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
        public ActionResult Edit([Bind(Include = "JobID,JobName,Photo,JobDescription,JobReview")] PortfolioItem portfolioItem)
        {
            if (ModelState.IsValid)
            {
                db.Entry(portfolioItem).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
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
