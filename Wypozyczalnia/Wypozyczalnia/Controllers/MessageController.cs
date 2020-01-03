﻿using Microsoft.AspNet.Identity;
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
    public class MessageController : Controller
    {
        private Entities5 db = new Entities5();

        // GET: Message
        public ActionResult Index()
        {
            string userId = System.Web.HttpContext.Current.User.Identity.GetUserId().ToString();

            return View(db.Powiadomienie.Select(n => n).Where(s => s.UserId.Equals(userId)).ToList());
        }

        // GET: Message/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Powiadomienie powiadomienie = db.Powiadomienie.Find(id);
            if (powiadomienie == null)
            {
                return HttpNotFound();
            }
            return View(powiadomienie);
        }


        // GET: Message/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Powiadomienie powiadomienie = db.Powiadomienie.Find(id);
            if (powiadomienie == null)
            {
                return HttpNotFound();
            }
            return View(powiadomienie);
        }

        // POST: Message/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Powiadomienie powiadomienie = db.Powiadomienie.Find(id);
            db.Powiadomienie.Remove(powiadomienie);
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




        // GET: Rent/CreateProblem
        public ActionResult CreateProblem()
        {
            Problem problem = new Problem();

            problem.UserId = System.Web.HttpContext.Current.User.Identity.GetUserId().ToString();
            problem.Data = DateTime.Today;

            return View(problem);
        }

        // POST: Rent/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreateProblem([Bind(Include = "Id,Tytul,Opis,NrRejestracyjny,Data,UserId")] Problem problem)
        {
            if (ModelState.IsValid)
            {
                db.Problem.Add(problem);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(problem);
        }
    }
}