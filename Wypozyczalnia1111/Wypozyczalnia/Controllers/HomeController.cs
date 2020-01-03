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
    public class HomeController : Controller
    {
        private Entities4 db = new Entities4();

        //// GET: Home
        //public ActionResult Index()
        //{
        //    return View(db.Samochod.ToList());
        //}

        // GET: Home
        public ActionResult Index()
        {
            return View(db.Samochod.Select(n => n).Where(c => c.UserId == null));
        }

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
