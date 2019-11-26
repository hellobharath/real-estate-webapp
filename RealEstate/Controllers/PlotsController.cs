using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using RealEstate.Models;

namespace RealEstate.Controllers
{
    public class PlotsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Plots
        public ActionResult Index()
        {
            var plots = db.Plots;
            return View(plots.ToList());
        }

        // GET: Plots/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Plot plot = db.Plots.Find(id);
            if (plot == null)
            {
                return HttpNotFound();
            }
            return View(plot);
        }

        // GET: Plots/Create
        public ActionResult Create()
        {
            ViewBag.Id = new SelectList(db.Properties, "Id", "Category");
            return View();
        }

        // POST: Plots/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Length,Width,Location,Price_Per_Sqft")] Plot plot)
        {
            if (ModelState.IsValid)
            {
                db.Plots.Add(plot);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.Id = new SelectList(db.Properties, "Id", "Category", plot.Id);
            return View(plot);
        }

        // GET: Plots/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Plot plot = db.Plots.Find(id);
            if (plot == null)
            {
                return HttpNotFound();
            }
            ViewBag.Id = new SelectList(db.Properties, "Id", "Category", plot.Id);
            return View(plot);
        }

        // POST: Plots/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Length,Width,Location,Price_Per_Sqft")] Plot plot)
        {
            if (ModelState.IsValid)
            {
                db.Entry(plot).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.Id = new SelectList(db.Properties, "Id", "Category", plot.Id);
            return View(plot);
        }

        // GET: Plots/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Plot plot = db.Plots.Find(id);
            if (plot == null)
            {
                return HttpNotFound();
            }
            return View(plot);
        }

        public ActionResult PlotAd()
        {
            return View();
        }

        // POST: Plots/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult PlotAd(Plot plot)
        {
            if (ModelState.IsValid)
            {
                plot.Id = TempData["Propid"].ToString();
                db.Plots.Add(plot);
                db.SaveChanges();
                return RedirectToAction("Details","Ads",new { Id = TempData["AdId"].ToString() });
            }

            return View(plot);
        }

        // POST: Plots/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Plot plot = db.Plots.Find(id);
            db.Plots.Remove(plot);
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
