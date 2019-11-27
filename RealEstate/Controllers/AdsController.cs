using System;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using RealEstate.Models;
using Microsoft.AspNet.Identity;

namespace RealEstate.Controllers
{
    public class AdsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Ads
        public ActionResult Index(string Category,string search)
        {
            var ads = db.Ads;
            return View(ads.Where(x => x.Property.Category == Category && (x.Property.Location.ToLower() == search.ToLower() || x.Property.City.ToLower() == search.ToLower()) && x.Status == "Available").ToList());
        }
        public ActionResult PostedAds()
        {
            var user = User.Identity.GetUserId();
            return View(db.Ads.Where(x => x.Owner_Id == user));
        }
        // GET: Ads/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Ad ad = db.Ads.Find(id);
            Property property = db.Properties.Find(ad.Property_Id);
            Plot plot;
            Residential residential;
            plot = db.Plots.Find(ad.Property_Id);
            residential = db.Residentials.Find(ad.Property_Id);
            AdViewModel a = new AdViewModel(ad, property, plot, residential);
            if(ModelState.IsValid)
            {
                TempData["payeeID"] = a.Property.Owner_Id;
                TempData["payerID"] = User.Identity.GetUserId();
                TempData["type"] = a.Ad.Ad_Type;
                TempData["propertyID"] = a.Property.Id;
            }
            if (ad == null)
            {
                return HttpNotFound();
            }
            return View(a);
        }
        

        // GET: Ads/Create
        public ActionResult Create()
        {
            ViewBag.Owner_Id = new SelectList(db.Users, "Id", "Email");
            ViewBag.Property_Id = new SelectList(db.Properties, "Id", "Category");
            return View();
        }

        // POST: Ads/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Property_Id,Owner_Id,Property_Type,Date_Posted,Ad_Type,Price")] Ad ad)
        {
            if (ModelState.IsValid)
            {
                db.Ads.Add(ad);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.Owner_Id = new SelectList(db.Users, "Id", "Email", ad.Owner_Id);
            ViewBag.Property_Id = new SelectList(db.Properties, "Id", "Category", ad.Property_Id);
            return View(ad);
        }

        // GET: Ads/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Ad ad = db.Ads.Find(id);
            if (ad == null)
            {
                return HttpNotFound();
            }
            ViewBag.Owner_Id = new SelectList(db.Users, "Id", "Email", ad.Owner_Id);
            ViewBag.Property_Id = new SelectList(db.Properties, "Id", "Category", ad.Property_Id);
            return View(ad);
        }

        // POST: Ads/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Property_Id,Owner_Id,Property_Type,Date_Posted,Ad_Type,Price")] Ad ad)
        {
            if (ModelState.IsValid)
            {
                db.Entry(ad).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.Owner_Id = new SelectList(db.Users, "Id", "Email", ad.Owner_Id);
            ViewBag.Property_Id = new SelectList(db.Properties, "Id", "Category", ad.Property_Id);
            return View(ad);
        }


        // GET: Ads/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Ad ad = db.Ads.Find(id);
            if (ad == null)
            {
                return HttpNotFound();
            }
            return View(ad);
        }

        // POST: Ads/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Ad ad = db.Ads.Find(id);
            db.Ads.Remove(ad);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult PostAd()
        {
            ViewBag.Owner_Id = new SelectList(db.Users, "Id", "Email");
            ViewBag.Property_Id = new SelectList(db.Properties, "Id", "Category");
            return View();
        }

        // POST: Ads/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult PostAd(AdViewModel adView,HttpPostedFileBase Image)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    adView.Property.image1 = new byte[Image.ContentLength];
                    Image.InputStream.Read(adView.Property.image1, 0, Image.ContentLength);
                    adView.Property.Owner_Id = User.Identity.GetUserId();
                    adView.Ad.Status = "Available";
                    adView.Property.Id = Guid.NewGuid().ToString();
                    TempData["Propid"] = adView.Property.Id;
                    db.Properties.Add(adView.Property);
                    adView.Ad.Owner_Id = User.Identity.GetUserId();
                    adView.Ad.Date_Posted = DateTime.Now;
                    adView.Ad.Property_Id = adView.Property.Id;
                    db.Ads.Add(adView.Ad);
                    db.SaveChanges();
                    TempData["AdId"] = adView.Ad.Id;



                    if (adView.Property.Category == "Residential")
                        return RedirectToAction("ResAd", "Residentials");
                    return RedirectToAction("PlotAd", "Plots");

                }
            }
            catch(Exception e)
            {
                return RedirectToAction("ErrorPage", "Ads");
            }
            return View(adView);
        }

        public ActionResult ErrorPage()
        {
            return View();
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
