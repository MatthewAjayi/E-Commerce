using E_Commerce.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace E_Commerce.Controllers
{
    public class ProductsController : Controller
    {
        // GET: Products
        public PartialViewResult CategoryDropdown()
        {
            // Retrieve the list of categories from your data source
            var categories = EStoreDBContext.EStoreDB.GetCategoryList();    

            return PartialView("_CategoryDropdown", categories);
        }

        public ActionResult ProductsByCategory(int id)
        {
            Session["CategoryName"] = EStoreDBContext.EStoreDB.GetCategoryName(id);
            // Retrieve and display products for the selected category
            var products = EStoreDBContext.EStoreDB.GetProductsByCategory(id);

            if (Session["Username"]  == null) {
                return View("NonUser", products);
            }

            else
            {
                return View(products);
            }

            
        }
    }
}