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
        public MyProductCartController(OnlineShoppingSystemContext context)
        {
            db = context;
        }
      
        List<MyCart> li = new List<MyCart>();
       

        //new module

        public ActionResult MyOrdercart()
        {
            if (TempData["cart"] != null)
            {
                float x = 0;
                List<MyCart> li2 = TempData["cart"] as List<MyCart>;
                foreach (var item in li2)
                {
                    x += item.bill;

                }

                TempData["total"] = x;
            }
            TempData.Keep();
            return View(db.Products.OrderByDescending(x => x.ProductId).ToList());
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
         
            Product p = db.Products.Where(x => x.ProductId == Id).SingleOrDefault();

            MyCart c = new MyCart();
            c.proid =p.ProductId;
            c.proname = p.ProductName;
            c.price = Convert.ToInt32(p.Price);
            c.qty = Convert.ToInt32(qty);
            c.bill = c.price * c.qty;
            if (TempData["cart"] == null)
            {
                li.Add(c);
                TempData["cart"] = li;
                //li = TempData["cart"] as List<MyCart>;
            }
            else
            {
                List<MyCart> li2 = TempData["cart"] as List<MyCart>;
                li2.Add(c);
                TempData["cart"] = li2;
            }

          
           
            TempData.Keep();

            return RedirectToAction("checkout");
        }

        public ActionResult checkout()
        {

            ViewBag.uid = HttpContext.Session.GetString("uid");
            TempData.Keep();
            return View();
        }

//NEW MODULE ENDS
        public ActionResult remove(int? id)
        {
            if (TempData["cart"] == null)
            {
                TempData.Remove("total");
                TempData.Remove("cart");
            }
            else
            {
                List<MyCart> li2 = TempData["cart"] as List<MyCart>;
                MyCart c = li2.Where(x => x.proid == id).SingleOrDefault();
                li2.Remove(c);
                int s = 0;
                foreach (var item in li2)
                {
                    s += item.bill;
                }
                TempData["total"] = s;

            }

            return RedirectToAction("OrderConfirmation");
        }
        public ActionResult OrderConfirmation()
        {
            ViewBag.uid = HttpContext.Session.GetString("uid");
            return View();
        }

    }


 



}
