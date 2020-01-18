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
    /*! 
     * \brief Kontroler wiadomosci - powiadomien, problemow i indywidualnych ofert
     * 
     * Kontroler wiadomości połączony z widokiem Powiadomienie
     */

    [Authorize]
    public class MessageController : Controller
    {
        private Entities5 db = new Entities5();

        // GET: Message
        /*! 
         * \brief Zwraca widok powiadomien
         * \return zwraca widok powiadomienia zalogowanego uzytkownika
         */
        public ActionResult Index()
        {
            string userId = System.Web.HttpContext.Current.User.Identity.GetUserId().ToString();

            return View(db.Powiadomienie.Select(n => n).Where(s => s.UserId.Equals(userId)).ToList());
        }

        /*! 
         * \brief Zwraca widok szczegolow powiadomienia
         * \param[id] id powiadomienia
         * \return zwraca widok danego powiadomienia lub kod bledu
         */
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

        /*! 
         * \brief Usuwa powiadomienie
         * \param[id] id powiadomienia
         * \return zwraca widok danego powiadomienia lub kod bledu
         */
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
        /*! 
         * \brief Zwraca potwierdzenie o usunieciu powiadomienia
         * \param[id] id powiadomienia
         * \return zwraca widok index (strona glowna)
         */
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

        /*! 
         * \brief Tworzy nowe powiadomienie
         * \return zwraca widok powiadomienia
         */
        // GET: Rent/CreateProblem
        public ActionResult CreateProblem()
        {
            Problem problem = new Problem();

            problem.UserId = System.Web.HttpContext.Current.User.Identity.GetUserId().ToString();
            problem.Data = DateTime.Today;

            return View(problem);
        }

        /*! 
         * \brief Tworzy nowy problem
         * \return zwraca widok problemu
         */
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


        /*! 
         * \brief Tworzy indywidualna oferte
         * \return zwraca widok indywidualnej oferty
         */
        // GET: Rent/CreateProblem
        public ActionResult CreateIndividualOffer()
        {
            IndywidualnaOferta indywidualnaOferta = new IndywidualnaOferta();

            indywidualnaOferta.UserId = System.Web.HttpContext.Current.User.Identity.GetUserId().ToString();

            return View(indywidualnaOferta);
        }

        // POST: Rent/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreateIndividualOffer([Bind(Include = "Id,Marka,Model,Rok,LimitKilometrow,Opony,AC,Opis,UserId")] IndywidualnaOferta indywidualnaOferta)
        {
            if (ModelState.IsValid)
            {
                db.IndywidualnaOferta.Add(indywidualnaOferta);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(indywidualnaOferta);
        }
    }
}
