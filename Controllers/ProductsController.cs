using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using OnlineShoppingSystem.Models;

namespace OnlineShoppingSystem.Controllers
{
    public class ProductsController : Controller
    {
       
        private readonly OnlineShoppingSystemContext _context;
        private readonly IWebHostEnvironment _hostEnvironment;
        public ProductsController(OnlineShoppingSystemContext context,IWebHostEnvironment hostEnvironment)
        {
            _context = context;
            this._hostEnvironment = hostEnvironment;
           
        }
        public async Task<IActionResult> Search(string searchString,Product product)
        {
            var pro = from m in _context.Products
                         select m;
            if (ModelState.IsValid)
            {
                if (searchString == null)
                {

                    ViewBag.Message = "Product not found";
                    return View();
                }
                else
                {
                    if (!String.IsNullOrEmpty(searchString))
                    {
                        pro = pro.Where(s => s.ProductName.Contains(searchString));

                    }
                }
            }
     
            return View(await pro.ToListAsync());
           
        }
        [HttpPost]
        public string Search(string searchString, bool notUsed)
        {
            return "From [HttpPost]Index: filter on " + searchString;
        }
        public async Task<IActionResult> ProductHomePage()
        {
            var onlineShoppingSystemContext = _context.Products.Include(p => p.Category).Include(p => p.Retailer);
            return View(await onlineShoppingSystemContext.ToListAsync());
        }
        // GET: Products

        public async Task<IActionResult> Index(Retailer retailer)
        {
            ViewBag.uid = HttpContext.Session.GetString("uid");
            Retailer ad = _context.Retailers.Find(ViewBag.uid);
            var onlineShoppingSystemContext = _context.Products.Include(p => p.Category).Include(p => p.Retailer);
            return View(await onlineShoppingSystemContext.ToListAsync());
        }

        // GET: Products/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var product = await _context.Products
                .Include(p => p.Category)
                .Include(p => p.Retailer)
                .FirstOrDefaultAsync(m => m.ProductId == id);
            if (product == null)
            {
                return NotFound();
            }

            return View(product);
        }

        // GET: Products/Create
        public IActionResult Create()
        {
            ViewBag.uid = HttpContext.Session.GetString("uid");
      
            ViewData["CategoryId"] = new SelectList(_context.Categories, "CategoryId", "CategoryId");
            ViewData["RetailerId"] = new SelectList(_context.Retailers, "RetailerId", "RetailerId");
            return View();
        }

        // POST: Products/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ProductId,ProductName,CategoryId,RetailerId,Description,ImageFile,Features,AvailableProduct,Price")] Product product)
        {
            if (ModelState.IsValid)
            {
              
                //save image
                string wwwRootPath = _hostEnvironment.WebRootPath;
                string fileName = Path.GetFileNameWithoutExtension(product.ImageFile.FileName);
                string extension=Path.GetExtension(product.ImageFile.FileName);
                product.ProductImage=fileName=fileName + DateTime.Now.ToString("yymmssff") + extension;
                string path = Path.Combine(wwwRootPath + "/Image", fileName);
                using(var filestream=new FileStream(path,FileMode.Create))
                {
                    await product.ImageFile.CopyToAsync(filestream);
                       
                }
                _context.Add(product);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["CategoryId"] = new SelectList(_context.Categories, "CategoryId", "CategoryId", product.CategoryId);
            ViewData["RetailerId"] = new SelectList(_context.Retailers, "RetailerId", "RetailerId", product.RetailerId);
            return View(product);
        }

        // GET: Products/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var product = await _context.Products.FindAsync(id);
            if (product == null)
            {
                return NotFound();
            }
            ViewData["CategoryId"] = new SelectList(_context.Categories, "CategoryId", "CategoryId", product.CategoryId);
            ViewData["RetailerId"] = new SelectList(_context.Retailers, "RetailerId", "RetailerId", product.RetailerId);
            return View(product);
        }

        // POST: Products/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ProductId,ProductName,CategoryId,RetailerId,Description,ImageFile,Features,AvailableProduct,Price")] Product product)
        {
            ViewBag.uid = HttpContext.Session.GetString("uid");

            if (id != product.ProductId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    string wwwRootPath = _hostEnvironment.WebRootPath;
                    string fileName = Path.GetFileNameWithoutExtension(product.ImageFile.FileName);
                    string extension = Path.GetExtension(product.ImageFile.FileName);
                    product.ProductImage = fileName = fileName + DateTime.Now.ToString("yymmssff") + extension;
                    string path = Path.Combine(wwwRootPath + "/Image", fileName);
                    using (var filestream = new FileStream(path, FileMode.Create))
                    {
                        await product.ImageFile.CopyToAsync(filestream);

                    }

                    _context.Update(product);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ProductExists(product.ProductId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["CategoryId"] = new SelectList(_context.Categories, "CategoryId", "CategoryId", product.CategoryId);
            ViewData["RetailerId"] = new SelectList(_context.Retailers, "RetailerId", "RetailerId", product.RetailerId);
            return View(product);
        }

        // GET: Products/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            ViewBag.uid = HttpContext.Session.GetString("uid");
            if (id == null)
            {
                return NotFound();
            }

            var product = await _context.Products
                .Include(p => p.Category)
                .Include(p => p.Retailer)
                .FirstOrDefaultAsync(m => m.ProductId == id);
            if (product == null)
            {
                return NotFound();
            }

            return View(product);
        }

        // POST: Products/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            ViewBag.uid = HttpContext.Session.GetString("uid");
            var product = await _context.Products.FindAsync(id);
            //delete Image
            var imagepath = Path.Combine(_hostEnvironment.WebRootPath, "Image", product.ProductImage);
                if (System.IO.File.Exists(imagepath))
                System.IO.File.Delete(imagepath);
            _context.Products.Remove(product);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ProductExists(int id)
        {
            ViewBag.uid = HttpContext.Session.GetString("uid");
            return _context.Products.Any(e => e.ProductId == id);
        }
    }
}
