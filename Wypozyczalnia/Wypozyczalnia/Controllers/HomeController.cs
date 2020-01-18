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
     * Kontroler do kontrolowania strony głównej z wszystkimi ofertami wypozyczalni
     * 
     */
    public class HomeController : Controller
    {
        /**
        * @brief 
        * Połączenie z bazą danych
        */
        private Entities5 db = new Entities5();

        //// GET: Home
        //public ActionResult Index()
        //{
        //    return View(db.Samochod.ToList());
        //}

        /**
        * @brief 
        * Zwraca index html strony głownej czyli liste wszsytkich dostepnych ofert wypozyczalni
        * @return View
        * 
        */
        // GET: Home
        public ActionResult Index()
        {
            return View(db.Samochod.Select(n => n).Where(c => c.UserId == null));
        }

        /**
        * @brief 
        * Zwraca szczegoly poszczegolnego wypozyczenia
        * @return View
        * 
        */
        // GET: Home/Details/5
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
