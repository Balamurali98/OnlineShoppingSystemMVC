using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OnlineShoppingSystem.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace OnlineShoppingSystem.Controllers
{
    public class CustomerController : Controller
    {
        public string userid;
        public string email { get; private set; }
      
        private readonly OnlineShoppingSystemContext db;
        public CustomerController(OnlineShoppingSystemContext context)
        {
            db = context;
        }

        public async Task<IActionResult> Index(int id)
        {
            ViewBag.uid = HttpContext.Session.GetString("uid");
            var onlineShoppingSystemContext = db.Products.Include(p => p.Category).Include(p => p.Retailer);
            return View(await onlineShoppingSystemContext.ToListAsync());
        }
        public async Task<IActionResult> SearchProduct(string searchString)
        {
            ViewBag.uid = HttpContext.Session.GetString("uid");
            var pro = from m in db.Products
                      select m;

            if (!String.IsNullOrEmpty(searchString))
            {
                pro = pro.Where(s => s.ProductName!.Contains(searchString));
            }
            else
            {
                ViewBag.Message = "Product Not Found";
                return View();
            }

            return View(await pro.ToListAsync());
        }
        //[HttpPost]
        //public string Search(string searchString, bool notUsed)
        //{
        //    return "From [HttpPost]Index: filter on " + searchString;
        //}
        public IActionResult CustomerSignup()
        {
            return View();
        }
        [HttpPost]
        public IActionResult CustomerSignup(Customer customer)
        {

            if (ModelState.IsValid)
            {
              
                    db.Customers.Add(customer);
                db.SaveChanges();
                return RedirectToAction("CustomerLogin");

            }
            else
            {
                return View();
            }
        }
    
       
        public IActionResult CustomerLogin()
        {
            return View();
        }
        [HttpPost]
        public IActionResult CustomerLogin(Customer customer)
        {
            if (customer.EmailId == null || customer.Password == null)
            {

                ViewBag.Message = "Username or Password cannot be empty";
                return View();
            }
            else
            {


                if (customer.EmailId == null)
                {

                    ViewBag.Message = "Invalid User";

                    return View();
                }
                else if (customer.Password == null)
                {
                    ViewBag.Message = "Invalid Password";

                    return View();

                }
                else
                {
                    var q = from p in db.Customers
                            where p.EmailId ==p.EmailId && p.Password == customer.Password
                            select p;
                    if (q.Any())
                    {
                        ViewBag.Message = "Login Successfull";
                        HttpContext.Session.SetString("uid",customer.EmailId);
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

        public IActionResult Display()
        {

            List<Customer> customers = db.Customers.ToList();
            return View(customers);
        }

        public IActionResult Delete(int id)
        {

            Customer customer = db.Customers.Find(id);
            db.Customers.Remove(customer);
            db.SaveChanges();
            return RedirectToAction("Display");
        }


        public IActionResult Details(int id)
        {
            ViewBag.uid = HttpContext.Session.GetString("uid");
            Customer customer= db.Customers.Find(ViewBag.uid);

            return View(customer);
        }



        public IActionResult Edit(int id)
        {
            Customer customer = db.Customers.Find(id);

            return View(customer);
        }
        [HttpPost]
        public IActionResult Edit(Customer c)
        {
            Customer customer = db.Customers.Find(c.CustomerId);

            customer.CustomerName= c.CustomerName;
            customer.EmailId= c.EmailId;
            customer.Address= c.Address;
            customer.Password = c.Password;
            customer.Confirmpassword = c.Confirmpassword;


            db.SaveChanges();

            return RedirectToAction("CustomerLogin");
        }

        [HttpGet]
        public IActionResult AccountDetails(Customer customer)

        {
          
            ViewBag.uid = HttpContext.Session.GetString("uid");
            string email = ViewBag.uid;

            Customer ad1 = (from p in db.Customers
                            where p.EmailId == email
                            select p).FirstOrDefault();
            return View(ad1);
        }
        public IActionResult Logout()
        {

            HttpContext.Session.Remove("uid");
            return RedirectToAction("CustomerLogin", "Customer");


        }
       

        [HttpGet]
        public IActionResult MyOrderConfirmation()
        {
            ViewBag.uid = HttpContext.Session.GetString("uid");
            string email = ViewBag.uid;
            var ad1 = (from p in db.Customers
                            where p.EmailId == email
                            select p.CustomerId).FirstOrDefault();
            var customerlist = (from cust in db.Customers
                                join
      or in db.ProductOrders on cust.CustomerId!=ad1 equals or.CustomerId!=ad1
                                select new { cust.CustomerId, cust.CustomerName, cust.Address, or.OrderId, or.Productname, or.Price, or.Quantity, or.OrderedDate }).ToList();
            List<ProductOrderDetail> lvm = new List<ProductOrderDetail>();
            foreach (var item in customerlist)
            {
                ProductOrderDetail objvm = new ProductOrderDetail();
                objvm.OrderId = item.OrderId;
                objvm.Productname = item.Productname;
                objvm.CustomerId = item.CustomerId;
                objvm.Customername = item.CustomerName;
                objvm.Address = item.Address;
                objvm.OrderedDate = item.OrderedDate;
                lvm.Add(objvm);
                //db.ProductOrderDetails.Add(objvm);
                //db.SaveChanges();

            }

            return View(lvm);
        }
        public IActionResult ForgetPassword(string email)
        {
            var ad1 = (from p in db.Customers
                       where p.EmailId == email
                       select p).FirstOrDefault();

            return View(ad1);
        }

        [HttpGet]
        public IActionResult ForgetPassword(Customer c)
        {
            var ad1 = (from p in db.Customers
                       where p.EmailId == email
                       select p).FirstOrDefault();
            if (ModelState.IsValid)
            {

                ad1.Password = c.Password;
                ad1.Confirmpassword = c.Confirmpassword;
                db.SaveChanges();

                ViewBag.Message = "Password updated Successfully";
                return View();
            }
            else
            {
                ViewBag.Message = "Password updated fail";
                return View();
            }

          
        }

      



    }
}
