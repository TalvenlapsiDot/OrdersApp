using OrdersApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Net;
using System.Data.Entity;

namespace OrdersApp.Controllers
{
    public class CustomersController : Controller
    {
        // GET: Customers
        OrdersDBEntities2 db = new OrdersDBEntities2();

        public ActionResult Index()
        {
            if (Session["UserName"] == null)
            {
                return RedirectToAction("login", "login");
            } else
            {
               ViewBag.LoggedStatus = (@Session["UserName"]);

                List<Asiakkaat> model = db.Asiakkaat.ToList();
                return View(model);
            }
        }

        #region Edit
        public ActionResult Edit(int? id)
        {
            if (id == null) return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            Asiakkaat customer = db.Asiakkaat.Find(id);
            if (customer == null) return HttpNotFound();

            ViewBag.Postitoimipaikat = db.Postitoimipaikat.Select(p => p.Postinumero).ToList();

            return View(customer);

        }
        [HttpPost]
        [ValidateAntiForgeryToken]

        public ActionResult Edit([Bind(Include = "AsiakasID, Nimi, Osoite, Postinumero")] Asiakkaat customer)
        {
            if (ModelState.IsValid)
            {
                db.Entry(customer).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(customer);
        }
        #endregion

        #region Create
        public ActionResult Create()
        {
            ViewBag.Postitoimipaikat = db.Postitoimipaikat.Select(p => p.Postinumero).ToList();
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]

        public ActionResult Create([Bind(Include = "AsiakasID, Nimi, Osoite, Postinumero")] Asiakkaat customer)
        {
            if (ModelState.IsValid)
            {
                db.Asiakkaat.Add(customer);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(customer);
        }
        #endregion

        #region Delete
        public ActionResult Delete(int? id)
        {
            if (id == null) return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            Asiakkaat customer = db.Asiakkaat.Find(id);
            if (customer == null) return HttpNotFound();

            return View(customer);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]

        public ActionResult DeleteConfirmed(int id)
        {
            Asiakkaat customer = db.Asiakkaat.Find(id);
            db.Asiakkaat.Remove(customer);
            db.SaveChanges();

            return RedirectToAction("Index");
        }
        #endregion
    }
}