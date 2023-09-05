using E_Commerce.DAL;
using E_Commerce.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace E_Commerce.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            //var listCustomers = GetCustomers();
            return View();
        }

    }
}