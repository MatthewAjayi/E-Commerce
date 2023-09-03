using E_Commerce.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace E_Commerce.Controllers
{
    public class AccountController : Controller
    {
        // Display account information after registering
        public ActionResult Account(Customer customer)
        {
            return View(customer);
        }
    }
}