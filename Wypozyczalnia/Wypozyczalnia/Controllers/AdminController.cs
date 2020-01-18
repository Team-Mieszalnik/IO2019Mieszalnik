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
         * @brief
         * AdminController to kontroler administratora, pozwala na moderowanie użytkownikami strony        
         */
    [Authorize(Roles = "Admin")]
    public class AdminController : Controller
    {
        /**
         * @brief
         * Połączenie z bazą danych 
         */
        private Entities5 db = new Entities5();


        /**
         * @brief
         * Metoda zwraca widok z listą urzytkowników
         * 
         * @return ActionResult
         */
        // GET: Admin
        public ActionResult Index()
        {
            return View(db.AspNetUsers.ToList());
        }

        /**
         * @brief
         * Metoda zwraca widok ze szczegółami użytkownika z pasującym ID.
         * Może zwrócić informacje o braku takiego użytkownika w formie HttpNotFound().
         * 
         * @param id Jest to id identyfikujące użytkownika
         * @return ActionResult
         */
        // GET: Admin/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AspNetUsers aspNetUsers = db.AspNetUsers.Find(id);
            if (aspNetUsers == null)
            {
                return HttpNotFound();
            }
            return View(aspNetUsers);
        }

        /**
         * @brief
         * Metoda szuka użytkownika o podanym ID.
         * Może zwrócić informacje o braku takiego użytkownika w formie HttpNotFound().
         * 
         * @param id Jest to id identyfikujące użytkownika
         * @return ActionResult
         */
        // GET: Admin/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AspNetUsers aspNetUsers = db.AspNetUsers.Find(id);
            if (aspNetUsers == null)
            {
                return HttpNotFound();
            }
            return View(aspNetUsers);
        }



        /**
         * @brief
         * Metoda modyfikuje modyfikuje podanego użytkownika i zapisuje zmiany w bazie danych.
         * Zwraca widok z listą użytkowników
         * 
         * @param aspNetUsers Jest to obiekt zawierający użytkownika
         * @return ActionResult
         */
        // POST: Admin/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Email,EmailConfirmed,PasswordHash,SecurityStamp,PhoneNumber,PhoneNumberConfirmed,TwoFactorEnabled,LockoutEndDateUtc,LockoutEnabled,AccessFailedCount,UserName")] AspNetUsers aspNetUsers)
        {
            if (ModelState.IsValid)
            {
                db.Entry(aspNetUsers).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(aspNetUsers);
        }

        /**
         * @brief
         * Metoda szuka użytkownika o podanym ID.
         * Może zwrócić informacje o braku takiego użytkownika w formie HttpNotFound().
         * 
         * @param id Jest to id identyfikujące użytkownika
         * @return ActionResult
         */
        // GET: Admin/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AspNetUsers aspNetUsers = db.AspNetUsers.Find(id);
            if (aspNetUsers == null)
            {
                return HttpNotFound();
            }
            return View(aspNetUsers);
        }


        /**
        * @brief
        * Metoda usuwa użytkownika o podanym ID.        
        * 
        * @param id Jest to id identyfikujące użytkownika
        * @return ActionResult
        */
        // POST: Admin/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            AspNetUsers aspNetUsers = db.AspNetUsers.Find(id);
            db.AspNetUsers.Remove(aspNetUsers);
            db.SaveChanges();
            return RedirectToAction("Index");
        }




       /**
       * @brief
       * Metoda zamyka połączenie z bazą danych        
       * 
       * @param disposing Jest to bool informujący, czy chcemy zamknąć połączenie z bazą danych, czy nie.
       * @return void
       */
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
