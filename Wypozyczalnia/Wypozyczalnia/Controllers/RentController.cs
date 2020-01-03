using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Wypozyczalnia.Models;

namespace Wypozyczalnia.Controllers
{
    [Authorize]
    public class RentController : Controller
    {
        private Entities3 db = new Entities3();

        //// GET: Rent
        //public ActionResult Index()
        //{
        //    return View(db.Wypozyczenie.ToList());
        //}



        // GET: Rent
        public ActionResult Index()
        {
            string userId = System.Web.HttpContext.Current.User.Identity.GetUserId().ToString();

            return View(db.Wypozyczenie.Select(n => n).Where(b => b.UserId.Equals(userId)).ToList());
        }

        // GET: Rent/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Wypozyczenie wypozyczenie = db.Wypozyczenie.Find(id);
            if (wypozyczenie == null)
            {
                return HttpNotFound();
            }
            return View(wypozyczenie);
        }

        // GET: Rent/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Rent/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,UserId,SamochodId,PoczatekUmowy,KoniecUmowy")] Wypozyczenie wypozyczenie)
        {
            if (ModelState.IsValid)
            {
                db.Wypozyczenie.Add(wypozyczenie);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(wypozyczenie);
        }

        // GET: Rent/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Wypozyczenie wypozyczenie = db.Wypozyczenie.Find(id);
            if (wypozyczenie == null)
            {
                return HttpNotFound();
            }
            return View(wypozyczenie);
        }

        // POST: Rent/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,UserId,SamochodId,PoczatekUmowy,KoniecUmowy")] Wypozyczenie wypozyczenie)
        {
            if (ModelState.IsValid)
            {
                db.Entry(wypozyczenie).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(wypozyczenie);
        }

        // GET: Rent/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Wypozyczenie wypozyczenie = db.Wypozyczenie.Find(id);
            if (wypozyczenie == null)
            {
                return HttpNotFound();
            }
            return View(wypozyczenie);
        }

        // POST: Rent/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Wypozyczenie wypozyczenie = db.Wypozyczenie.Find(id);
            db.Wypozyczenie.Remove(wypozyczenie);
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
