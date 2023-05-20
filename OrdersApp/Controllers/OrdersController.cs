using OrdersApp.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace OrdersApp.Controllers
{
    public class OrdersController : Controller
    {
        // GET: Orders
        OrdersDBEntities2 db = new OrdersDBEntities2();

        public ActionResult Index()
        {
            if (Session["UserName"] == null)
            {
                return RedirectToAction("login", "login");
            } else
            {
                ViewBag.LoggedStatus = (@Session["UserName"]);
                List<Tilaukset> model = db.Tilaukset.ToList();

                return View(model);
            }

        }

        #region Edit
        public ActionResult Edit(int? id)
        {
            if (id == null) return new HttpStatusCodeResult(HttpStatusCode.BadRequest);


            Tilaukset order  = db.Tilaukset.Find(id);
            if (order == null) return HttpNotFound();

            ViewBag.Asiakkaat = db.Asiakkaat.Select(p => p.AsiakasID).ToList();
            ViewBag.Postitoimipaikat = db.Postitoimipaikat.Select(p => p.Postinumero).ToList();
            
            return View(order);

        }
        [HttpPost]
        [ValidateAntiForgeryToken]

        public ActionResult Edit([Bind(Include = "TilausID, AsiakasID, Toimitusosoite, Postinumero, Tilauspvm, Toimituspvm")] Tilaukset order)
        {
            if (ModelState.IsValid)
            {
                db.Entry(order).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(order);
        }

        #endregion Edit

        #region Create
        public ActionResult Create()
        {
            ViewBag.Asiakkaat = db.Asiakkaat.Select(p => p.AsiakasID).ToList();
            ViewBag.Postitoimipaikat = db.Postitoimipaikat.Select(p => p.Postinumero).ToList();

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]

        public ActionResult Create([Bind(Include = "TilausID, AsiakasID, Toimitusosoite, Postinumero, Tilauspvm, Toimituspvm")] Tilaukset order)
        {
            if (ModelState.IsValid)
            {
                db.Tilaukset.Add(order);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(order);
        }

        #endregion Create

        #region Delete
        public ActionResult Delete(int? id)
        {
            if (id == null) return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            Tilaukset order = db.Tilaukset.Find(id);
            if (order == null) return HttpNotFound();

            return View(order);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]

        public ActionResult DeleteConfirmed(int id)
        {
            Tilaukset order = db.Tilaukset.Find(id);
            db.Tilaukset.Remove(order);
            db.SaveChanges();

            return RedirectToAction("Index");
        }
        #endregion
    }
}