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
    public class ResidentialsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Residentials
        public ActionResult Index()
        {
            var residentials = db.Residentials.Include(r => r.Property);
            return View(residentials.ToList());
        }

        // GET: Residentials/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Residential residential = db.Residentials.Find(id);
            if (residential == null)
            {
                return HttpNotFound();
            }
            return View(residential);
        }

        // GET: Residentials/Create
        public ActionResult Create()
        {
            ViewBag.Id = new SelectList(db.Properties, "Id", "Category");
            return View();
        }

        // POST: Residentials/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Type,Built_Up_Length,Built_Up_Width,Land_Length,Land_Width,Location,NoOfBedrooms,Price")] Residential residential)
        {
            if (ModelState.IsValid)
            {
                db.Residentials.Add(residential);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.Id = new SelectList(db.Properties, "Id", "Category", residential.Id);
            return View(residential);
        }

        // GET: Residentials/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Residential residential = db.Residentials.Find(id);
            if (residential == null)
            {
                return HttpNotFound();
            }
            ViewBag.Id = new SelectList(db.Properties, "Id", "Category", residential.Id);
            return View(residential);
        }

        // POST: Residentials/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Type,Built_Up_Length,Built_Up_Width,Land_Length,Land_Width,Location,NoOfBedrooms,Price")] Residential residential)
        {
            if (ModelState.IsValid)
            {
                db.Entry(residential).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.Id = new SelectList(db.Properties, "Id", "Category", residential.Id);
            return View(residential);
        }

        // GET: Residentials/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Residential residential = db.Residentials.Find(id);
            if (residential == null)
            {
                return HttpNotFound();
            }
            return View(residential);
        }

        // POST: Residentials/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Residential residential = db.Residentials.Find(id);
            db.Residentials.Remove(residential);
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
