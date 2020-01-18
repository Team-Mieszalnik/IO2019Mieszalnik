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
    /**
     * @brief Kontroler do zarzadzania wypozyczonymi samochodami
     */
    [Authorize]
    public class RentController : Controller
    {
        private Entities5 db = new Entities5();

        /**
         * 
         * @brief Akcja do wyswietlania listy wypozyczonych samochodow
         * @return ActionResult Widok z lista wypozyczonych samochodow
         */
        // GET: Rent
        public ActionResult Index()
        {
            string userId = System.Web.HttpContext.Current.User.Identity.GetUserId().ToString();

            return View(db.Samochod.Select(n => n).Where(s => s.UserId.Equals(userId)).ToList());
        }

        /**
         * @brief Akcja do wyswietlania szczegolow wypozyczonego samochodu
         * @param id Identyfikator wybranego samochodu
         * @return ActionResult Widok z szczegolami danego wyswietlenia lub blad
         */
        // GET: Rent/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Samochod samochod = db.Samochod.Find(id);
            if (samochod == null)
            {
                return HttpNotFound();
            }
            return View(samochod);
        }

        //// GET: Rent/Create
        //public ActionResult Create()
        //{
        //    return View();
        //}

        //// POST: Rent/Create
        //// To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        //// more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult Create([Bind(Include = "Id,Marka,Model,Rok,LimitKilometrow,Opony,AC,NrRejestracyjny,Zdjecie,Cena,PoczatekUmowy,KoniecUmowy,UserId,Opis")] Samochod samochod)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        db.Samochod.Add(samochod);
        //        db.SaveChanges();
        //        return RedirectToAction("Index");
        //    }

        //    return View(samochod);
        //}

        /**
         * @brief Akcja HttpGet do tworzenia nowego wypozyczenia
         * @param id Identyfikator wybranego samochodu
         * @return ActionResult Widok do tworzenia wypozyczenia lub blad
         */
        // GET: Rent/CreateRent/5
        public ActionResult CreateRent(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Samochod samochod = db.Samochod.Find(id);
            if (samochod == null)
            {
                return HttpNotFound();
            }

            samochod.UserId = System.Web.HttpContext.Current.User.Identity.GetUserId().ToString();
            samochod.PoczatekUmowy = DateTime.Today;

            return View(samochod);
        }

        /**
         * @brief Akcja HttpPost do tworzenia nowego wypozyczenia
         * @param samochod Zmodyfikowany samochod do wypozyczenia
         * @return ActionResult Widok do tworzenia wypozyczenia lub przekierowanie do akcji Index
         */
        // POST: Rent/CreateRent/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreateRent([Bind(Include = "Id,Marka,Model,Rok,LimitKilometrow,Opony,AC,NrRejestracyjny,Zdjecie,Cena,PoczatekUmowy,KoniecUmowy,UserId,Opis")] Samochod samochod)
        {

            if (ModelState.IsValid && samochod.KoniecUmowy > samochod.PoczatekUmowy)
            {
                db.Entry(samochod).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(samochod);
        }

        /**
         * @brief Akcja HttpGet do modyfikowania wypozyczenia
         * @param id Identyfikator wybranego samochodu
         * @return ActionResult Widok do modyfikowania wypozyczenia lub blad
         */
        // GET: Rent/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Samochod samochod = db.Samochod.Find(id);
            if (samochod == null)
            {
                return HttpNotFound();
            }
            return View(samochod);
        }

        /**
         * @brief Akcja HttpPost do modyfikowania wypozyczenia
         * @param samochod Zmodyfikowany samochod
         * @return ActionResult Widok do modyfikowania wypozyczenia lub przekierowanie do akcji Index
         */
        // POST: Rent/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Marka,Model,Rok,LimitKilometrow,Opony,AC,NrRejestracyjny,Zdjecie,Cena,PoczatekUmowy,KoniecUmowy,UserId,Opis")] Samochod samochod)
        {
            if (ModelState.IsValid)
            {
                db.Entry(samochod).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(samochod);
        }

        /**
         * @brief Akcja HttpGet do usuwania wypozyczenia
         * @param id Identyfikator wybranego samochodu
         * @return ActionResult Widok potwiedzenia usuniecia wypozyczenia lub blad
         */
        // GET: Rent/DeleteRent/5
        public ActionResult DeleteRent(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Samochod samochod = db.Samochod.Find(id);
            if (samochod == null)
            {
                return HttpNotFound();
            }
            return View(samochod);
        }

        /**
         * @brief Akcja HttpGet do usuwania wypozyczenia
         * @param id Identyfikator wybranego samochodu
         * @return ActionResult Przekierowanie do akcji Index lub blad
         */
        // POST: Rent/DeleteRent/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteRent(int id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Samochod samochod = db.Samochod.Find(id);
            if (samochod == null)
            {
                return HttpNotFound();
            }

            samochod.UserId = null;
            samochod.PoczatekUmowy = null;
            samochod.KoniecUmowy = null;

            db.Entry(samochod).State = EntityState.Modified;
            db.Configuration.ValidateOnSaveEnabled = false;
            db.SaveChanges();
            db.Configuration.ValidateOnSaveEnabled = true;

            return RedirectToAction("Index");
        }

        //// GET: Rent/Delete/5
        //public ActionResult Delete(int? id)
        //{
        //    if (id == null)
        //    {
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }
        //    Samochod samochod = db.Samochod.Find(id);
        //    if (samochod == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    return View(samochod);
        //}

        //// POST: Rent/Delete/5
        //[HttpPost, ActionName("Delete")]
        //[ValidateAntiForgeryToken]
        //public ActionResult DeleteConfirmed(int id)
        //{
        //    Samochod samochod = db.Samochod.Find(id);
        //    db.Samochod.Remove(samochod);
        //    db.SaveChanges();
        //    return RedirectToAction("Index");
        //}

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
