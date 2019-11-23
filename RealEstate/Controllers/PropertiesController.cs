﻿using System;
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
    public class PropertiesController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Properties
        public ActionResult Index()
        {
            var properties = db.Properties;
            return View(properties.ToList());
        }

        // GET: Properties/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Property property = db.Properties.Find(id);
            if (property == null)
            {
                return HttpNotFound();
            }
            return View(property);
        }

        // GET: Properties/Create
        public ActionResult Create()
        {
            ViewBag.Owner_Id = new SelectList(db.Users, "Id", "Email");
            ViewBag.Id = new SelectList(db.Plots, "Id", "Location");
            ViewBag.Id = new SelectList(db.Residentials, "Id", "Type");
            return View();
        }

        // POST: Properties/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Category,Location,City,Owner_Id,Status,image1,image2,image3")] Property property)
        {
            if (ModelState.IsValid)
            {
                db.Properties.Add(property);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.Owner_Id = new SelectList(db.Users, "Id", "Email", property.Owner_Id);
            ViewBag.Id = new SelectList(db.Plots, "Id", "Location", property.Id);
            ViewBag.Id = new SelectList(db.Residentials, "Id", "Type", property.Id);
            return View(property);
        }

        // GET: Properties/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Property property = db.Properties.Find(id);
            if (property == null)
            {
                return HttpNotFound();
            }
            ViewBag.Owner_Id = new SelectList(db.Users, "Id", "Email", property.Owner_Id);
            ViewBag.Id = new SelectList(db.Plots, "Id", "Location", property.Id);
            ViewBag.Id = new SelectList(db.Residentials, "Id", "Type", property.Id);
            return View(property);
        }

        // POST: Properties/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Category,Location,City,Owner_Id,Status,image1,image2,image3")] Property property)
        {
            if (ModelState.IsValid)
            {
                db.Entry(property).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.Owner_Id = new SelectList(db.Users, "Id", "Email", property.Owner_Id);
            ViewBag.Id = new SelectList(db.Plots, "Id", "Location", property.Id);
            ViewBag.Id = new SelectList(db.Residentials, "Id", "Type", property.Id);
            return View(property);
        }

        // GET: Properties/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Property property = db.Properties.Find(id);
            if (property == null)
            {
                return HttpNotFound();
            }
            return View(property);
        }

        // POST: Properties/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Property property = db.Properties.Find(id);
            db.Properties.Remove(property);
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
