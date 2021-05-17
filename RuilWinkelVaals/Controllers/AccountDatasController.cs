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
    public class AccountDatasController : Controller
    {
        private UserDataModel db = new UserDataModel();

        // GET: AccountDatas
        public ActionResult Index()
        {
            var accountData = db.AccountData.Include(a => a.ProfileData);
            return View(accountData.ToList());
        }

        // GET: AccountDatas/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AccountData accountData = db.AccountData.Find(id);
            if (accountData == null)
            {
                return HttpNotFound();
            }
            return View(accountData);
        }

        // GET: AccountDatas/Create
        public ActionResult Create()
        {
            ViewBag.ProfileId = new SelectList(db.ProfileData, "Id", "Email");
            return View();
        }

        // POST: AccountDatas/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,ProfileId,Hash,Salt,Blocked,DateBlocked")] AccountData accountData)
        {
            if (ModelState.IsValid)
            {
                db.AccountData.Add(accountData);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.ProfileId = new SelectList(db.ProfileData, "Id", "Email", accountData.ProfileId);
            return View(accountData);
        }

        // GET: AccountDatas/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AccountData accountData = db.AccountData.Find(id);
            if (accountData == null)
            {
                return HttpNotFound();
            }
            ViewBag.ProfileId = new SelectList(db.ProfileData, "Id", "Email", accountData.ProfileId);
            return View(accountData);
        }

        // POST: AccountDatas/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,ProfileId,Hash,Salt,Blocked,DateBlocked")] AccountData accountData)
        {
            if (ModelState.IsValid)
            {
                db.Entry(accountData).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.ProfileId = new SelectList(db.ProfileData, "Id", "Email", accountData.ProfileId);
            return View(accountData);
        }

        // GET: AccountDatas/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AccountData accountData = db.AccountData.Find(id);
            if (accountData == null)
            {
                return HttpNotFound();
            }
            return View(accountData);
        }

        // POST: AccountDatas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            AccountData accountData = db.AccountData.Find(id);
            db.AccountData.Remove(accountData);
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
