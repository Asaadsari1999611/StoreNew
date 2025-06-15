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
    public class FeedbackStoresController : Controller
    {
        private readonly ModelContext _context;

        public FeedbackStoresController(ModelContext context)
        {
            _context = context;
        }

        // GET: FeedbackStores
        public async Task<IActionResult> Index()
        {
            var modelContext = _context.FeedbackStores.Include(f => f.User).OrderByDescending(f=>f.Id);
            ViewBag.customers = _context.Users.ToList();
            return View(await modelContext.ToListAsync());
        }

        // GET: FeedbackStores/Details/5
        public async Task<IActionResult> Details(decimal? id)
        {
            if (id == null || _context.FeedbackStores == null)
            {
                return NotFound();
            }

            var feedbackStore = await _context.FeedbackStores
                .Include(f => f.Product)
                .Include(f => f.User)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (feedbackStore == null)
            {
                return NotFound();
            }

            return View(feedbackStore);
        }

        // GET: FeedbackStores/Create
        public IActionResult Create()
        {
            ViewData["Productid"] = new SelectList(_context.Products, "Id", "Id");
            ViewData["Userid"] = new SelectList(_context.Users, "Id", "Id");
            return View();
        }

        // POST: FeedbackStores/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Ratingsatars,Ratingdetails,Productid,Userid,Shared")] FeedbackStore feedbackStore)
        {
            if (ModelState.IsValid)
            {
                _context.Add(feedbackStore);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["Productid"] = new SelectList(_context.Products, "Id", "Id", feedbackStore.Productid);
            ViewData["Userid"] = new SelectList(_context.Users, "Id", "Id", feedbackStore.Userid);
            return View(feedbackStore);
        }

        // GET: FeedbackStores/Edit/5
        public async Task<IActionResult> Edit(decimal? id)
        {
            if (id == null || _context.FeedbackStores == null)
            {
                return NotFound();
            }

            var feedbackStore = await _context.FeedbackStores.FindAsync(id);
            if (feedbackStore == null)
            {
                return NotFound();
            }
            ViewData["Productid"] = new SelectList(_context.Products, "Id", "Id", feedbackStore.Productid);
            ViewData["Userid"] = new SelectList(_context.Users, "Id", "Id", feedbackStore.Userid);
            return View(feedbackStore);
        }

        // POST: FeedbackStores/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(decimal id, [Bind("Id,Ratingsatars,Ratingdetails,Productid,Userid,Shared")] FeedbackStore feedbackStore)
        {
            if (id != feedbackStore.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(feedbackStore);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!FeedbackStoreExists(feedbackStore.Id))
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
            ViewData["Productid"] = new SelectList(_context.Products, "Id", "Id", feedbackStore.Productid);
            ViewData["Userid"] = new SelectList(_context.Users, "Id", "Id", feedbackStore.Userid);
            return View(feedbackStore);
        }

        // GET: FeedbackStores/Delete/5
        public async Task<IActionResult> Delete(decimal? id)
        {
            if (id == null || _context.FeedbackStores == null)
            {
                return NotFound();
            }

            var feedbackStore = await _context.FeedbackStores
                .Include(f => f.Product)
                .Include(f => f.User)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (feedbackStore == null)
            {
                return NotFound();
            }

            return View(feedbackStore);
        }

        // POST: FeedbackStores/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(decimal id)
        {
            if (_context.FeedbackStores == null)
            {
                return Problem("Entity set 'ModelContext.FeedbackStores'  is null.");
            }
            var feedbackStore = await _context.FeedbackStores.FindAsync(id);
            if (feedbackStore != null)
            {
                _context.FeedbackStores.Remove(feedbackStore);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool FeedbackStoreExists(decimal id)
        {
          return (_context.FeedbackStores?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
