using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using OnlineShoppingSystem.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;


namespace OnlineShoppingSystem.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly OnlineShoppingSystemContext _context;
     
    

        public HomeController(ILogger<HomeController> logger, OnlineShoppingSystemContext context)
        {
            _logger = logger;
            _context = context;
        }
        public async Task<IActionResult> Search(string searchString,Product product)
        {
            var pro = from m in _context.Products
                      select m;
            if (searchString == null)
            {
                ViewBag.Message = "Product Not Found";
                return View();
            }
            else
            {
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
        }
        public async Task<IActionResult> Index()
        {
            var onlineShoppingSystemContext = _context.Products.Include(p => p.Category).Include(p => p.Retailer);
            return View(await onlineShoppingSystemContext.ToListAsync());
        }
        //public IActionResult Index()
        //{
        //    return View(/*_context.Products.Include(c => c.ProductName).Include(c => c.ImageFile).ToList()*/);
        //}

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
