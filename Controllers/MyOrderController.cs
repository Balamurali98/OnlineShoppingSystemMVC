using Microsoft.AspNetCore.Mvc;
using OnlineShoppingSystem.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OnlineShoppingSystem.Controllers
{
    public class MyOrderController : Controller
    {
        private readonly OnlineShoppingSystemContext db;
        public MyOrderController(OnlineShoppingSystemContext context)
        {
            db = context;
        }
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult MyOrder()
        {
            return View();
        }

        [HttpPost]
        public IActionResult MyOrder(OrderDetail orderDetail)
        {


            db.OrderDetails.Add(orderDetail);
            db.SaveChanges();
            return RedirectToAction("Display");

        }

        public IActionResult Display()
        {
            List<OrderDetail> orderDetails = db.OrderDetails.ToList();
            return View(orderDetails);
        }

        public IActionResult Delete(int id)
        {
            OrderDetail orderDetail = db.OrderDetails.Find(id);
            db.OrderDetails.Remove(orderDetail);
            db.SaveChanges();
            return RedirectToAction("Display");
        }

        public IActionResult OrderPlaced()
        {
            return View();
        }
    }
}
