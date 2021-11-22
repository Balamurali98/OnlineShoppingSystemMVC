using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OnlineShoppingSystem.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OnlineShoppingSystem.Controllers
{
    public class AdminLoginController : Controller
    {
        private readonly OnlineShoppingSystemContext db;
        public AdminLoginController(OnlineShoppingSystemContext context)
        {
            db = context;
        }
        public IActionResult Index()
        {
            return View();
        }
        #region AdminLogin Module
        public IActionResult ALogin()
        {
            return View();
        }

        [HttpPost]
        public IActionResult ALogin(AdminLogin adminLogin)
        {
            if (adminLogin.AdminId == null || adminLogin.AdminPassword == null)
            {

                ViewBag.Message = "Username or Password cannot be empty";
                return View();
            }
            else
            {


                if (adminLogin.AdminId== null)
                {

                    ViewBag.Message = "Invalid User";

                    return View();
                }
                else if (adminLogin.AdminPassword == null)
                {
                    ViewBag.Message = "Invalid Password";

                    return View();

                }
                else
                {
                    var q = from p in db.AdminLogins
                            where p.AdminId ==p.AdminId && p.AdminPassword == adminLogin.AdminPassword
                            select p;
                    if (q.Any())
                    {
                        HttpContext.Session.SetString("uid",adminLogin.AdminId);
                        ViewBag.Message = "Login Successfull";
                        return RedirectToAction("Index");


                    }
                    else
                    {
                        ViewBag.Message = "Invalid Password";
                        return View();
                    }

                }
            }
        }
        #endregion

        #region AdminLogout
        public IActionResult Logout()
        {

            HttpContext.Session.Remove("uid");
            return RedirectToAction("ALogin", "AdminLogin");


        }
        #endregion
        #region CustomerDisplay
        public IActionResult CustomerDisplay()
        {
            ViewBag.uid = HttpContext.Session.GetString("uid");
            List<Customer> customers = db.Customers.ToList();
            return View(customers);
        }
        #endregion

        #region CustomerDelete Module
        public IActionResult CustomerDelete(int id)
        {

            Customer customer = db.Customers.Find(id);
            db.Customers.Remove(customer);
            db.SaveChanges();
            return RedirectToAction("CustomerDisplay");
        }
        #endregion

        #region ProductDisplay
        public IActionResult ProductDisplay()
        {
            ViewBag.uid = HttpContext.Session.GetString("uid");
            List<Product> Product = db.Products.ToList();
            return View(Product);
        }
        #endregion

        #region ProductEdit

        public IActionResult ProductDelete(int id)
        {

            Product product = db.Products.Find(id);
            db.Products.Remove(product);
            db.SaveChanges();
            return RedirectToAction("ProductDisplay");
        }
        #endregion




    }
}
