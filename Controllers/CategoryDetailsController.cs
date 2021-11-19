using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OnlineShoppingSystem.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OnlineShoppingSystem.Controllers
{
    public class CategoryDetailsController : Controller
    {
        private readonly OnlineShoppingSystemContext db;
        public CategoryDetailsController(OnlineShoppingSystemContext context)
        {
            db = context;
        }
        public async Task<IActionResult> Search(string searchString)
        {
            ViewBag.uid = HttpContext.Session.GetString("uid");
            var pro = from m in db.Categories
                      select m;

            if (!String.IsNullOrEmpty(searchString))
            {
                pro = pro.Where(s => s.CategoryName!.Contains(searchString));
            }

            return View(await pro.ToListAsync());
        }
        [HttpPost]
        public string Search(string searchString, bool notUsed)
        {
            return "From [HttpPost]Index: filter on " + searchString;
        }
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult AddCategory()
        {
            return View();
        }
        [HttpPost]
        public IActionResult AddCategory(Category category)
        {

            ViewBag.uid = HttpContext.Session.GetString("uid");
            if (ModelState.IsValid)
            {
               Category category1 = db.Categories.Where(x => x.CategoryId == category.CategoryId).FirstOrDefault();
                if (category1 != null)
                {
                    ViewBag.Message = "Category Id Exist";
                    return View();
                }
                else
                {
                    db.Categories.Add(category);
                    db.SaveChanges();
                    return RedirectToAction("Display");
                }
            }
            else
            {
                return View();
            }      
        }
        public IActionResult Display()
        {
            ViewBag.uid = HttpContext.Session.GetString("uid");
            List<Category> categories = db.Categories.ToList();
            return View(categories);
        }

        public IActionResult Delete(string id)
        {
            Category category = db.Categories.Find(id);
            db.Categories.Remove(category);
            db.SaveChanges();
            return RedirectToAction("Display");
        }

        
        public IActionResult Details(string id)
        {
            Category category = db.Categories.Find(id);

            return View(category);
        }

    

        public IActionResult Edit(string id)
        {
            Category category = db.Categories.Find(id);

            return View(category);
        }
        [HttpPost]
        public IActionResult Edit(Category c)
        {
            Category category = db.Categories.Find(c.CategoryId);

            category.CategoryName = c.CategoryName;
            category.Descriptions = c.Descriptions;
           
            db.SaveChanges();

            return RedirectToAction("Display");
        }



    }
}
