using OrdersApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.UI.WebControls;

namespace OrdersApp.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            if (Session["UserName"] == null)
            {
                ViewBag.LoggedStatus = "Logged Out";
            }
            else ViewBag.LoggedStatus = (@Session["UserName"]);
            
            return View();
        }

        public ActionResult About()
        {
            if (Session["UserName"] == null)
            {
                ViewBag.LoggedStatus = "Logged Out";
            }
            else ViewBag.LoggedStatus = (@Session["UserName"]);


            return View();
        }

        public ActionResult Contact()
        {
            if (Session["UserName"] == null)
            {
                ViewBag.LoggedStatus = "Logged Out";
            }
            else ViewBag.LoggedStatus = (@Session["UserName"]);


            ViewBag.Message = "Contact Information";

            return View();
        }


    }
}