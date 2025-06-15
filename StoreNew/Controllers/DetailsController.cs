using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using StoreNew.Models;

namespace StoreNew.Controllers
{
    public class DetailsController : Controller
    {
        private readonly ModelContext _context;

        public DetailsController(ModelContext context)
        {
            _context = context;
        }

        // GET: Details
        public async Task<IActionResult> Index()
        {
            var modelContext = _context.Details.Include(d => d.Product);
            return View(await modelContext.ToListAsync());
        }

        // GET: Details/Details/5
        public async Task<IActionResult> Details(decimal? id)
        {
            if (id == null || _context.Details == null)
            {
                return NotFound();
            }

            var detail = await _context.Details
                .Include(d => d.Product)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (detail == null)
            {
                return NotFound();
            }

            return View(detail);
        }

        // GET: Details/Create
        public IActionResult Create()
        {
           // ViewData["Productid"] = new SelectList(_context.Products, "Id", "Id");
            ViewBag.Productname = _context.Products
       .Select(c => new SelectListItem
       {
           Value = c.Id.ToString(),           // القيمة التي سترجع عند الاختيار
           Text = c.Name              // الاسم المعروض في القائمة
       })
       .ToList();
            return View();
        }

        // POST: Details/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Launch,Dimensions,Weight,Typee,Sizee,Os,Memoryy,Maincamera,Selfecamera,Battery,Productid")] Detail detail)
        {
            if (ModelState.IsValid)
            {
                _context.Add(detail);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            //ViewData["Productid"] = new SelectList(_context.Products, "Id", "Id", detail.Productid);
            ViewBag.Productname = _context.Products
         .Select(c => new SelectListItem
         {
             Value = c.Id.ToString(),           // القيمة التي سترجع عند الاختيار
             Text = c.Name              // الاسم المعروض في القائمة
         })
         .ToList();
            return View(detail);
        }

        // GET: Details/Edit/5
        public async Task<IActionResult> Edit(decimal? id)
        {
            if (id == null || _context.Details == null)
            {
                return NotFound();
            }

            var detail = await _context.Details.FindAsync(id);
            if (detail == null)
            {
                return NotFound();
            }
            ViewData["Productid"] = new SelectList(_context.Products, "Id", "Id", detail.Productid);
            return View(detail);
        }

        // POST: Details/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(decimal id, [Bind("Id,Launch,Dimensions,Weight,Typee,Sizee,Os,Memoryy,Maincamera,Selfecamera,Battery,Productid")] Detail detail)
        {
            if (id != detail.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(detail);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!DetailExists(detail.Id))
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
            ViewData["Productid"] = new SelectList(_context.Products, "Id", "Id", detail.Productid);
            return View(detail);
        }

        // GET: Details/Delete/5
        public async Task<IActionResult> Delete(decimal? id)
        {
            if (id == null || _context.Details == null)
            {
                return NotFound();
            }

            var detail = await _context.Details
                .Include(d => d.Product)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (detail == null)
            {
                return NotFound();
            }

            return View(detail);
        }

        // POST: Details/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(decimal id)
        {
            if (_context.Details == null)
            {
                return Problem("Entity set 'ModelContext.Details'  is null.");
            }
            var detail = await _context.Details.FindAsync(id);
            if (detail != null)
            {
                _context.Details.Remove(detail);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool DetailExists(decimal id)
        {
          return (_context.Details?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
