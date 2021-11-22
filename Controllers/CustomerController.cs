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
        #region SearchProduct
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
        #endregion

        #region CustomerSignup
        public IActionResult CustomerSignup()
        {
            return View();
        }
        [HttpPost]
        public IActionResult CustomerSignup(Customer customer)
        {

            if (ModelState.IsValid)
            {
               Customer customer1 = db.Customers.Where(x => x.EmailId == customer.EmailId).FirstOrDefault();
                Customer customer2 = db.Customers.Where(x => x.Phonenumber== customer.Phonenumber).FirstOrDefault();
                if (customer1 != null)
                {
                    ViewBag.Message = "Email Id Exist";
                    return View();
                }
                else if (customer2 != null)
                {
                    ViewBag.Message1 = "Phone number Already Exist";
                    return View();
                }
                else
                {
                    db.Customers.Add(customer);
                    db.SaveChanges();
                    return RedirectToAction("CustomerLogin");
                }    

            }
            else
            {
                return View();
            }
        }

        #endregion
        #region Customerlogin Module
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

        #endregion

        #region DisplayCustomer
        public IActionResult Display()
        {

            List<Customer> customers = db.Customers.ToList();
            return View(customers);
        }
        #endregion

        #region CustomerDelete
        public IActionResult Delete(int id)
        {

            Customer customer = db.Customers.Find(id);
            db.Customers.Remove(customer);
            db.SaveChanges();
            return RedirectToAction("Display");
        }

        #endregion
        public IActionResult Details(int id)
        {
            ViewBag.uid = HttpContext.Session.GetString("uid");
            Customer customer= db.Customers.Find(ViewBag.uid);

            return View(customer);
        }


        #region Customer Edit Module
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

            ViewBag.Message = "Account Updated Successfully";
            return View();
        }
        #endregion

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

        #region Product OrderModule
        [HttpGet]
        public IActionResult MyOrderConfirmation(Customer c)
        {
            ViewBag.uid = HttpContext.Session.GetString("uid");
            string email = ViewBag.uid;
            var ad1 = (from p in db.Customers
                            where p.EmailId == email
                            select p.CustomerId).FirstOrDefault();
            var customerlist = (from cust in db.Customers
                             join
      or in db.ProductOrders  on cust.CustomerId==ad1 equals or.CustomerId== ad1
                                join m in db.Products on or.ProductId equals m.ProductId
                                select new { cust.CustomerId, cust.CustomerName, cust.Address, or.OrderId, or.Productname, or.Quantity, or.OrderedDate,m.Price}).ToList();
            List<ProductOrderDetail> lvm = new List<ProductOrderDetail>();
            foreach (var item in customerlist)
            {
                ProductOrderDetail objvm = new ProductOrderDetail();
                objvm.OrderId = item.OrderId;
                objvm.Productname = item.Productname;
                objvm.quantity = item.Quantity;
                objvm.Price = (int)item.Price;
                objvm.TotalPrice = (objvm.quantity)*(int)objvm.Price;
                objvm.CustomerId = item.CustomerId;
                objvm.Address = item.Address;
                objvm.OrderedDate = item.OrderedDate;
                objvm.PaymentStatus = "paid";
                objvm.Deliverydate = DateTime.Today.AddDays(10);
                lvm.Add(objvm);
                //db.ProductOrderDetails.Add(objvm);
                //db.SaveChanges();

            }

            return View(lvm);
        }

        #endregion

        #region ChangePasswordModule
        public IActionResult ChangePassword()
        {
            return View();
        }

        [HttpPost]
        public IActionResult ChangePassword(ForgetPassword pass)
        {
            try
            {
                var query = db.Customers.Where(customer => customer.EmailId == pass.EmailId).SingleOrDefault();
                if (query != null)
                {
                    query.EmailId = pass.EmailId;
                    query.Password = pass.Password;

                    db.SaveChanges();
                    ViewBag.Message = "Password Updated Successfully";
                    return View();
                }
                else
                {
                    TempData["msg"] = "Password Not updated";
                }
            }
            catch (Exception ex)
            {
                TempData["msg"] = ex;
            }

            return View();
        }
    }


    #endregion



}

