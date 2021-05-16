using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using RuilWinkelVaals.Models;

namespace RuilWinkelVaals.Controllers
{
    public class ProfileDataController : Controller
    {
        private UserDataModel db = new UserDataModel();

        // GET: ProfileData
        public ActionResult Index()
        {
            var profileData = db.ProfileData.Include(p => p.AccountType_LT).Include(p => p.Ledenpas_LT);
            return View(profileData.ToList());
        }

        // GET: ProfileData/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ProfileData profileData = db.ProfileData.Find(id);
            if (profileData == null)
            {
                return HttpNotFound();
            }
            return View(profileData);
        }

        // GET: ProfileData/Create
        public ActionResult Create()
        {
            ViewBag.AccountType = new SelectList(db.AccountType_LT, "Id", "AccountType");
            ViewBag.Ledenpas = new SelectList(db.Ledenpas_LT, "Id", "Status");
            return View();
        }

        // POST: ProfileData/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Email,Voornaam,Achternaam,Balans,AccountType,Ledenpas,Straat,Huisnummer,Woonplaats,Postcode,DateCreated,Geboortedatum")] ProfileData profileData)
        {
            if (ModelState.IsValid)
            {
                db.ProfileData.Add(profileData);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.AccountType = new SelectList(db.AccountType_LT, "Id", "AccountType", profileData.AccountType);
            ViewBag.Ledenpas = new SelectList(db.Ledenpas_LT, "Id", "Status", profileData.Ledenpas);
            return View(profileData);
        }

        // GET: ProfileData/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ProfileData profileData = db.ProfileData.Find(id);
            if (profileData == null)
            {
                return HttpNotFound();
            }
            ViewBag.AccountType = new SelectList(db.AccountType_LT, "Id", "AccountType", profileData.AccountType);
            ViewBag.Ledenpas = new SelectList(db.Ledenpas_LT, "Id", "Status", profileData.Ledenpas);
            return View(profileData);
        }

        // POST: ProfileData/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Email,Voornaam,Achternaam,Balans,AccountType,Ledenpas,Straat,Huisnummer,Woonplaats,Postcode,DateCreated,Geboortedatum")] ProfileData profileData)
        {
            if (ModelState.IsValid)
            {
                db.Entry(profileData).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.AccountType = new SelectList(db.AccountType_LT, "Id", "AccountType", profileData.AccountType);
            ViewBag.Ledenpas = new SelectList(db.Ledenpas_LT, "Id", "Status", profileData.Ledenpas);
            return View(profileData);
        }

        // GET: ProfileData/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ProfileData profileData = db.ProfileData.Find(id);
            if (profileData == null)
            {
                return HttpNotFound();
            }
            return View(profileData);
        }

        // POST: ProfileData/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            ProfileData profileData = db.ProfileData.Find(id);
            db.ProfileData.Remove(profileData);
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
