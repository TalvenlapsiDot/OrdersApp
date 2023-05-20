using OrdersApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.UI.WebControls;

namespace OrdersApp.Controllers
{
    public class LoginController : Controller
    {
        // GET: Login
        public ActionResult Login()
        {
            if (Session["UserName"] == null)
            {
                ViewBag.LoggedStatus = "Logged Out";
            }
            else ViewBag.LoggedStatus = (@Session["UserName"]);

            return View();
        }
        [HttpPost]
        public ActionResult Authorize(Logins LoginModel)
        {
            OrdersDBEntities2 db = new OrdersDBEntities2();
            var LoggedUser = db.Logins.SingleOrDefault(x => x.UserName == LoginModel.UserName && x.PassWord == LoginModel.PassWord);

            if (LoggedUser != null)
            {
                ViewBag.LoginMessage = "Successfull login";
                ViewBag.LoggedStatus = "Logged In";
                Session["UserName"] = LoggedUser.UserName;
                return RedirectToAction("Index", "Home");
            }
            else
            {
                ViewBag.LoginMessage = "Login unsuccessfull";
                ViewBag.LoggedStatus = "Logged Out";
                LoginModel.LoginErrorMessage = "Invalid Username or Password.";
                return View("Login", LoginModel);
            }
        }

        public ActionResult LogOut()
        {
            Session.Abandon();
            ViewBag.LoggedStatus = "Logged Out";
            return RedirectToAction("Index", "Home");
        }
    }
}