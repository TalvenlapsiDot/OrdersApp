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
    public class OrderRowsController : Controller
    {
        // GET: OrderRows
        OrdersDBEntities2 db = new OrdersDBEntities2();

        public ActionResult Index()
        {
            if (Session["UserName"] == null)
            {
                return RedirectToAction("login", "login");
            } else
            {
                ViewBag.LoggedStatus = (@Session["UserName"]);

                List<Tilausrivit> model = db.Tilausrivit.ToList();

                return View(model);
            }

        }

        #region Edit
        public ActionResult Edit(int? id)
        {
            if (id == null) return new HttpStatusCodeResult(HttpStatusCode.BadRequest);


            Tilausrivit orderRow = db.Tilausrivit.Find(id);
            if (orderRow == null) return HttpNotFound();

            ViewBag.Tilaukset = db.Tilaukset.Select(p => p.TilausID).ToList();
            ViewBag.Tuotteet = db.Tuotteet.Select(p => p.TuoteID).ToList();

            return View(orderRow);

        }
        [HttpPost]
        [ValidateAntiForgeryToken]

        public ActionResult Edit([Bind(Include = "TilausriviID, TilausID, TuoteID, Maara, Ahinta")] Tilausrivit orderRow)
        {
            if (ModelState.IsValid)
            {
                db.Entry(orderRow).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(orderRow);
        }

        #endregion Edit

        #region Create
        public ActionResult Create()
        {
            ViewBag.Tilaukset = db.Tilaukset.Select(p => p.TilausID).ToList();
            ViewBag.Tuotteet = db.Tuotteet.Select(p => p.TuoteID).ToList();

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]

        public ActionResult Create([Bind(Include = "TilausriviID, TilausID, TuoteID, Maara, Ahinta")] Tilausrivit orderRow)
        {
            if (ModelState.IsValid)
            {
                db.Tilausrivit.Add(orderRow);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(orderRow);
        }

        #endregion Create

        #region Delete
        public ActionResult Delete(int? id)
        {
            if (id == null) return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            Tilausrivit orderRow = db.Tilausrivit.Find(id);
            if (orderRow == null) return HttpNotFound();

            return View(orderRow);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]

        public ActionResult DeleteConfirmed(int id)
        {
            Tilausrivit orderRow = db.Tilausrivit.Find(id);
            db.Tilausrivit.Remove(orderRow);
            db.SaveChanges();

            return RedirectToAction("Index");
        }
        #endregion
    }
}