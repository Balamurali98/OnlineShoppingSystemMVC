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




    }
}
