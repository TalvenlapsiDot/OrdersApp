using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using OrdersApp.Models;
using System.Data.Entity;

namespace OrdersApp.Controllers
{
    public class ProductController : Controller
    {
        // GET: Product
        OrdersDBEntities2 db = new OrdersDBEntities2();

        public ActionResult Index()
        {
            if (Session["UserName"] == null)
            {
                return RedirectToAction("login", "login");
            } else
            {
                ViewBag.LoggedStatus = (@Session["UserName"]);
                List<Tuotteet> model = db.Tuotteet.ToList();

                return View(model);
            }
        }

        #region Edit
        public ActionResult Edit(int? id)
        {
            if (id == null) return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            Tuotteet product = db.Tuotteet.Find(id);
            if (product == null) return HttpNotFound();
            return View(product);

        }
        [HttpPost]
        [ValidateAntiForgeryToken]

        public ActionResult Edit([Bind(Include = "TuoteID, Nimi, Ahinta")] Tuotteet products)
        {
            if (ModelState.IsValid)
            {
                db.Entry(products).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(products);
        }

        #endregion Edit

        #region Create
        public ActionResult Create()
        {

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]

        public ActionResult Create([Bind(Include = "TuoteID, Nimi, Ahinta")] Tuotteet products)
        {
            if (ModelState.IsValid)
            {
                db.Tuotteet.Add(products);
                db.SaveChanges();
                return RedirectToAction("Index");   
            }

            return View(products);
        }

        #endregion Create

        #region Delete
        public ActionResult Delete(int? id)
        {
            if (id == null) return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            Tuotteet products = db.Tuotteet.Find(id);
            if (products == null) return HttpNotFound();

            return View(products);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]

        public ActionResult DeleteConfirmed(int id)
        {
            Tuotteet products = db.Tuotteet.Find(id);
            db.Tuotteet.Remove(products);
            db.SaveChanges();

            return RedirectToAction("Index");
        }
        #endregion
    }
}