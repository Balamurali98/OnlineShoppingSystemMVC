using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
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
 



        public ActionResult MyCart(int Id)
        {

            ViewBag.uid = HttpContext.Session.GetString("uid");
            Product p = db.Products.Where(x => x.ProductId == Id).SingleOrDefault();
            return View(p);
        }
  

        [HttpPost]
        public ActionResult MyCart(Product product, string qty, int Id)
        {
            var productlist = (from p in db.Products where p.ProductId == Id select p).ToList();

            var product1 = db.Products.Where(x => x.ProductId == Id).SingleOrDefault();
            List<ProductOrder> po = new List<ProductOrder>();
            foreach (var item in productlist )
            {
              ProductOrder objvm = new ProductOrder();
                objvm.ProductId = Id;
                objvm.Productname = item.ProductName;
                objvm.Quantity= Convert.ToInt32(qty);
                objvm.OrderedDate = DateTime.MinValue;
                objvm.unitprice = Convert.ToInt32(item.Price);
                ViewBag.Message = objvm.unitprice;
                TempData.Keep();
                objvm.Price = Convert.ToInt32(item.Price) *objvm.Quantity;
                po.Add(objvm);
                db.ProductOrders.Add(objvm);
                db.SaveChanges();

            }

           

            return RedirectToAction("checkout");
        }

        public ActionResult checkout()
        {
           
            ViewBag.uid = HttpContext.Session.GetString("uid");
            TempData.Keep();
            List<ProductOrder> categories = db.ProductOrders.ToList();
            return View(categories);
        }

        public IActionResult  CartDelete(int id)
        {
            ProductOrder productOrder= db.ProductOrders.Find(id);
            db.ProductOrders.Remove(productOrder);
            db.SaveChanges();
            return RedirectToAction("Checkout");
        }

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
        public ActionResult OrderConfirmation()
        {
            ViewBag.uid = HttpContext.Session.GetString("uid");
            return View();
        }
        public IActionResult PaymentDetails()
        {
           
            return View();
           
        }
        [HttpPost]
        public IActionResult PaymentDetails(BankDetail bank)
        {
            ViewBag.uid = HttpContext.Session.GetString("uid");
            if (ModelState.IsValid)
            {

                db.BankDetails.Add(bank);
                db.SaveChanges();
                return RedirectToAction("OrderConfirmation");

            }
            else
            {
                return View();
            }
        }

    }


 



}
