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
    public class LoginController : Controller
    {
        // GET: Login
        public ActionResult Index()
        {
            Session.Contents["UserName"] = null;
            return View();
        }

        public ActionResult Login(Login loginModel)
        {
            if(ModelState.IsValid)
            {
                DecryptionHelper decryption = new DecryptionHelper();
                var encryptedPassword = decryption.EncryptedPassword(loginModel.Password);
                loginModel.Password = encryptedPassword;
                var loginUser = EStoreDBContext.EStoreDB.LoginUser(loginModel.UserName, loginModel.Password);

                if(loginUser.Email != null && loginUser.Email != "admin@gmail.com")
                {
                    Session["Username"] = loginUser.Email;
                    Session["CustomerID"] = loginUser.CustomerID;
                    return RedirectToAction("Account", "Account");
                }

                else if (loginUser.Email != null && loginUser.Email == "admin@gmail.com")
                {
                    Session["Username"] = loginUser.Email;
                    Session["CustomerID"] = loginUser.CustomerID;
                    return RedirectToAction("Index", "Admin");
                }

                else
                {
                    return View("Index");
                }
                
            }

            else
            {
                return View("Index");
            }
            
        }
    }
}