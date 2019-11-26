using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using RealEstate.Models;

namespace RealEstate.Controllers
{
    public class AgreementsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Agreements
        public ActionResult Index()
        {
            var user = User.Identity.GetUserId();
            var agreements = db.Agreements.Where(x => x.Payee_Id == user).ToList();
            agreements.AddRange(db.Agreements.Where(x => x.Payer_Id == user).ToList());
            return View(agreements);
        }

        // GET: Agreements/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Agreement agreement = db.Agreements.Find(id);
            if (agreement == null)
            {
                return HttpNotFound();
            }
            return View(agreement);
        }
        [HttpPost]
        [MultipleButton(Name = "action", Argument = "Accept")]
        public ActionResult Accept(Agreement agreement)
        {
            if (ModelState.IsValid)
            {
                using (ApplicationDbContext db = new ApplicationDbContext())
                {
                    var result = db.Agreements.SingleOrDefault(b => b.Id == agreement.Id);
                    if (result != null)
                    {
                        result.Status = "Accepted";
                        db.SaveChanges();
                    }
                }
                return RedirectToAction("Index");
            }
            return View();
        }

        [HttpPost]
        [MultipleButton(Name = "action", Argument = "Reject")]
        public ActionResult Reject(Agreement agreement)
        {
            if (ModelState.IsValid)
            {
                using(ApplicationDbContext db = new ApplicationDbContext())
                {
                    var result = db.Agreements.SingleOrDefault(b => b.Id == agreement.Id);
                    if (result != null)
                    {
                        result.Status = "Rejected";
                        db.SaveChanges();
                    }
                }
                return RedirectToAction("Index");
            }
            return View();
        }
        // GET: Agreements/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Agreements/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Type,Agreement_Terms,Token_Advance,Total_Amt,Agreement_Duration,Payer_Id,Payee_Id,Status,Property_Id")] Agreement agreement)
        {
            if (ModelState.IsValid)
            {
                agreement.Payee_Id = TempData["payeeID"].ToString();
                agreement.Payer_Id = TempData["payerID"].ToString();
                agreement.Property_Id = TempData["propertyID"].ToString();
                agreement.Type = TempData["type"].ToString();
                agreement.Status = "pending";
                db.Agreements.Add(agreement);
                db.SaveChanges();
                return RedirectToAction("Details",new { id = agreement.Id });
            }

            ViewBag.Payee_Id = new SelectList(db.Users, "Id", "Email", agreement.Payee_Id);
            ViewBag.Payer_Id = new SelectList(db.Users, "Id", "Email", agreement.Payer_Id);
            ViewBag.Property_Id = new SelectList(db.Properties, "Id", "Category", agreement.Property_Id);
            return View(agreement);
        }

        // GET: Agreements/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Agreement agreement = db.Agreements.Find(id);
            if (agreement == null)
            {
                return HttpNotFound();
            }
            ViewBag.Payee_Id = new SelectList(db.Users, "Id", "Email", agreement.Payee_Id);
            ViewBag.Payer_Id = new SelectList(db.Users, "Id", "Email", agreement.Payer_Id);
            ViewBag.Property_Id = new SelectList(db.Properties, "Id", "Category", agreement.Property_Id);
            return View(agreement);
        }

        // POST: Agreements/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Type,Agreement_Terms,Token_Advance,Total_Amt,Agreement_Duration,Payer_Id,Payee_Id,Status,Property_Id")] Agreement agreement)
        {
            if (ModelState.IsValid)
            {
                db.Entry(agreement).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.Payee_Id = new SelectList(db.Users, "Id", "Email", agreement.Payee_Id);
            ViewBag.Payer_Id = new SelectList(db.Users, "Id", "Email", agreement.Payer_Id);
            ViewBag.Property_Id = new SelectList(db.Properties, "Id", "Category", agreement.Property_Id);
            return View(agreement);
        }

        // GET: Agreements/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Agreement agreement = db.Agreements.Find(id);
            if (agreement == null)
            {
                return HttpNotFound();
            }
            return View(agreement);
        }

        // POST: Agreements/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Agreement agreement = db.Agreements.Find(id);
            db.Agreements.Remove(agreement);
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
