using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using OnlineShoppingSystem.Models;

namespace OnlineShoppingSystem.Controllers
{
    public class RetailersController : Controller
    {
        public string userid { get; private set; }
        private readonly OnlineShoppingSystemContext _context;

        public RetailersController(OnlineShoppingSystemContext context)
        {
            _context = context;
        }

        public IActionResult Welcome()
        {
            ViewBag.uid = HttpContext.Session.GetString("uid");
            return View();
        }
        public IActionResult RLogin()
        {
            return View();
        }
        [HttpGet]
        public IActionResult AccountDetails(Retailer retailer)

        {
            ViewBag.uid = HttpContext.Session.GetString("uid");
            Retailer ad =_context.Retailers.Find(ViewBag.uid);
            return View(ad);
        }

        [HttpPost]
        public IActionResult RLogin(Retailer retailer)
        {
            if (retailer.RetailerId == null ||retailer.Password == null)
            {

                ViewBag.Message = "Username or Password cannot be empty";
                return View();
            }
            else
            {


                if (retailer.RetailerId == null)
                {

                    ViewBag.Message = "Invalid User";

                    return View();
                }
                else if (retailer.Password == null)
                {
                    ViewBag.Message = "Invalid Password";

                    return View();

                }
                else
                {
                    var q = from p in _context.Retailers
                            where p.RetailerId == p.RetailerId && p.Password ==retailer.Password
                            select p;
                    if (q.Any())
                    {
                        ViewBag.Message = "Login Successfull";
                        HttpContext.Session.SetString("uid",retailer.RetailerId);
                        return RedirectToAction("Welcome");
                    


                    }
                    else
                    {
                        ViewBag.Message = "Invalid Password";
                        return View();
                    }

                }
            }
        }

        // GET: Retailers
        public async Task<IActionResult> Index()
        {
          
            return View(await _context.Retailers.ToListAsync());
        }

        // GET: Retailers/Details/5
        public async Task<IActionResult> Details(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var retailer = await _context.Retailers
                .FirstOrDefaultAsync(m => m.RetailerId == id);
            if (retailer == null)
            {
                return NotFound();
            }

            return View(retailer);
        }

        // GET: Retailers/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Retailers/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("RetailerId,RetailerName,Gender,Address,Phonenumber,EmailId,Password,Confirmpassword")] Retailer retailer)
        {
            //if (retailer.RetailerId==retailer.RetailerId)
            //{
            //    ViewBag.Message = "Retailer ID Already Exist";
            //    return View();
            //}
           if (ModelState.IsValid)
            {
                _context.Add(retailer);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(retailer);
        }

        // GET: Retailers/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var retailer = await _context.Retailers.FindAsync(id);
            if (retailer == null)
            {
                return NotFound();
            }
            return View(retailer);
        }

        // POST: Retailers/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("RetailerId,RetailerName,Gender,Address,Phonenumber,EmailId,Password,Confirmpassword")] Retailer retailer)
        {
            if (id != retailer.RetailerId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(retailer);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!RetailerExists(retailer.RetailerId))
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
            return View(retailer);
        }

        // GET: Retailers/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var retailer = await _context.Retailers
                .FirstOrDefaultAsync(m => m.RetailerId == id);
            if (retailer == null)
            {
                return NotFound();
            }

            return View(retailer);
        }

        // POST: Retailers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            var retailer = await _context.Retailers.FindAsync(id);
            _context.Retailers.Remove(retailer);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool RetailerExists(string id)
        {
            return _context.Retailers.Any(e => e.RetailerId == id);
        }
    }
}
