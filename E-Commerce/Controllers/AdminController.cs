using E_Commerce.DAL;
using E_Commerce.Helper;
using E_Commerce.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace E_Commerce.Controllers
{
    public class AdminController : Controller
    {
        //Dashboard
        public ActionResult Index()
        {
            Session["NumberOfUsers"] = EStoreDBContext.EStoreDB.GetNumberOfUsers();
            return View();
        }

        public ActionResult Users() 
        {
            if (Session["Username"] != null)
            {
                var listCustomers = EStoreDBContext.EStoreDB.GetCustomers();
                return View(listCustomers);
            }
            return View();  
        }

        public ActionResult CreateUser() 
        {
            return View();
        }

        [HttpPost]
        public ActionResult CreateUser(Customer customer)
        {
            if (ModelState.IsValid)
            {
                //Add it into the DB
                //Check for duplicates
                var duplicateExist = EStoreDBContext.EStoreDB.CheckForDuplicates(customer.Email);
                if (duplicateExist)
                {
                    return View();
                }

                else
                {
                    var registeredCustomer = EStoreDBContext.EStoreDB.AddCustomer(customer);
                    Session["UserName"] = registeredCustomer.Email;
                    Session["FirstName"] = registeredCustomer.FirstName;
                    Session["CustomerID"] = registeredCustomer.CustomerID;
                    return RedirectToAction("Users", "Admin");
                }

            }

            return View();
        }

        public ActionResult GetCategories()
        {
            if (Session["Username"] != null)
            {
                var listCategories = EStoreDBContext.EStoreDB.GetCategoryList();
                return View(listCategories);
            }
            return View();
        }

        public ActionResult Category()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Category(Category category)
        {
            if (ModelState.IsValid)
            {
                EStoreDBContext.EStoreDB.AddCategory(category);
                return RedirectToAction("GetCategories", "Admin");
            }

            else
            {
                return View();
            }
            
        }

        public ActionResult GetProducts()
        {
            if (Session["Username"] != null)
            {
                var listProducts = EStoreDBContext.EStoreDB.GetProductList();
                return View(listProducts);
            }

            return View();
            
        }

        public ActionResult Products()
        {
            var categoriesList = EStoreDBContext.EStoreDB.GetCategoryList();
            ViewBag.Categories = new SelectList(categoriesList, "CategoryId", "CategoryName");
            return View();
        }

        [HttpPost]
        public ActionResult Products(Products product)
        {
            if (ModelState.IsValid)
            {
                product.CategoryID = Convert.ToInt32(product.Category);
                EStoreDBContext.EStoreDB.AddProduct(product);
                return RedirectToAction("GetProducts", "Admin");
            }

            else
            {
                return View();
            }
        }

        public ActionResult GetInventory()
        {
            if (Session["Username"] != null)
            {
                var inventoryList = EStoreDBContext.EStoreDB.GetInventoryList();
                return View(inventoryList);
            }

            return View();

        }

        public ActionResult Inventory()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Inventory(Inventory inventory)
        {

            if (ModelState.IsValid)
            {
                //inventory.ProductID = Convert.ToInt32(product.Category);
                EStoreDBContext.EStoreDB.AddInventory(inventory);
                return RedirectToAction("GetInventory", "Admin");
            }

            else
            {
                return View();
            }
        }

        public ActionResult UpdateInventory(int id)
        {
            var currentInventory = EStoreDBContext.EStoreDB.GetCurrentInventory(id);
            if (currentInventory == null )
            {
                return View();
            }

            return View(currentInventory);
        }

        [HttpPost]
        public ActionResult UpdateInventory(Inventory inventory)
        {
            int rowsAffected = 0;
            rowsAffected = EStoreDBContext.EStoreDB.UpdateInventory(inventory);
            
            if (rowsAffected > 0) {

                return RedirectToAction("GetInventory");
            }

            else
            {
                return View();
            }
        }
    }
}