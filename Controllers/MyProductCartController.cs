using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using OnlineShoppingSystem.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;



namespace OnlineShoppingSystem.Controllers
{
    public class MyProductCartController : Controller
    {
        private readonly OnlineShoppingSystemContext db;
        private readonly IWebHostEnvironment _hostEnvironment;
      
        public MyProductCartController(OnlineShoppingSystemContext context, IWebHostEnvironment hostEnvironment)
        {
            db = context;
            this._hostEnvironment = hostEnvironment;
        }

        #region CustomerProductCart
        public ActionResult MyCart(int Id)
        {

            ViewBag.uid = HttpContext.Session.GetString("uid");
            Product p = db.Products.Where(x => x.ProductId == Id).SingleOrDefault();
            return View(p);
        }
  

        [HttpPost]
        public ActionResult MyCart(Product product, string qty, int Id, ProductOrder objvm)
        {
            var productlist = (from p in db.Products where p.ProductId == Id select p).ToList();

            var product1 = db.Products.Where(x => x.ProductId == Id).SingleOrDefault();
            List<ProductOrder> po = new List<ProductOrder>();
            foreach (var item in productlist )
            {
              //ProductOrder objvm = new ProductOrder();
                objvm.ProductId = Id;
                objvm.Productname = item.ProductName;
                objvm.Quantity= Convert.ToInt32(qty);
                objvm.OrderedDate = DateTime.UtcNow;
                objvm.unitprice = Convert.ToInt32(item.Price);
                ViewBag.Message = objvm.unitprice;
                TempData.Keep();
                objvm.Price = Convert.ToInt32(item.Price) *objvm.Quantity;
                po.Add(objvm);
                db.ProductOrders.Add(objvm);
                db.SaveChanges();
                TempData["ProductID"] = JsonConvert.SerializeObject(objvm.ProductId);
                TempData["ProductName"] = JsonConvert.SerializeObject(objvm.Productname);
                TempData["Amount"] = JsonConvert.SerializeObject(objvm.Price);
                TempData["UnitPrice"] = JsonConvert.SerializeObject(objvm.unitprice);
                objvm.unitprice = decimal.Parse((string)TempData["UnitPrice"]);
                ViewBag.Amount1 = objvm.unitprice;
                TempData.Keep();

            }


           

            return RedirectToAction("checkout");
        }
        #endregion

        #region ProductCheckout
        public ActionResult checkout()
        {
           
            ViewBag.uid = HttpContext.Session.GetString("uid");
            TempData.Keep();
            List<ProductOrder> categories = db.ProductOrders.ToList();
            return View(categories);
        }
        #endregion
        #region Cart Delete
        public IActionResult  CartDelete(int id)
        {
            ProductOrder productOrder= db.ProductOrders.Find(id);
            db.ProductOrders.Remove(productOrder);
            db.SaveChanges();
            return RedirectToAction("Checkout");
        }
        #endregion
        #region Edit Product Cart
        public IActionResult CartEdit(int id)
        {
            ProductOrder productOrder = db.ProductOrders.Find(id);

            return View(productOrder);
        }
        [HttpPost]
        public IActionResult CartEdit(ProductOrder c)
        {
           ProductOrder productOrder = db.ProductOrders.Find(c.OrderId);
            productOrder.Quantity = c.Quantity;
            productOrder.unitprice = Convert.ToInt32(productOrder.Price); ;
            productOrder.Price = Convert.ToInt32(productOrder.Price) * productOrder.Quantity;
            db.SaveChanges();

            return RedirectToAction("Checkout");
        }
        #endregion
        public ActionResult OrderConfirmation()
        {
            ViewBag.uid = HttpContext.Session.GetString("uid");
            return View();
        }

        #region Payment Module
        public IActionResult PaymentDetails()
        {
 
            BankDetail payment = new BankDetail();
            payment.Balance = decimal.Parse((string)TempData["Amount"]);
            payment.ProductName =(string) TempData["ProductName"];
            ViewBag.Amount = payment.Balance;
            ViewBag.ProductId = payment.ProductId;
            ViewBag.Productname = payment.ProductName;
            TempData.Keep();
            return View();
           
        }
        [HttpPost]
        public IActionResult PaymentDetails(BankDetail bank)
        {
            ViewBag.uid = HttpContext.Session.GetString("uid");
            BankDetail payment = new BankDetail();
            payment.Balance = decimal.Parse((string)TempData["Amount"]);
            payment.ProductName = (string)TempData["ProductName"];
            ViewBag.Amount = payment.Balance;
            TempData.Keep();
            if (ModelState.IsValid)
            {

                db.BankDetails.Add(bank);
                db.SaveChanges();
                ViewBag.Message = "Payment Done & Order Placed Successfully";
                return View();

            }
            else
            {
                return View();
            }
        }

    }
    #endregion






}
