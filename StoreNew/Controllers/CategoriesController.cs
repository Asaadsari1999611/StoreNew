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
    public class CategoriesController : Controller
    {
        private readonly ModelContext _context;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public CategoriesController(ModelContext context, IWebHostEnvironment webHostEnvironment)
        {
            _context = context;
            _webHostEnvironment= webHostEnvironment;
        }

        // GET: Categories
        public async Task<IActionResult> Index()
        {
            return _context.Categories != null ? 
                          View(await _context.Categories.ToListAsync()) :
                          Problem("Entity set 'ModelContext.Categories'  is null.");
        }
        /* public IActionResult Index()
           {
               return View(_context.Categories.ToList());
           }
     */

        // GET: Categories/Details/5
        public async Task<IActionResult> Details(decimal? id)
        {
            if (id == null || _context.Categories == null)
            {
                return NotFound();
            }

            var category = await _context.Categories
                .FirstOrDefaultAsync(m => m.Id == id);
            if (category == null)
            {
                return NotFound();
            }

            return View(category);
        }

        // GET: Categories/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Categories/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Gategoryname,Imagefile")] Category category)
        {
            if (ModelState.IsValid)
            {
                if (category.Imagefile != null)
                {
                    string wwwRootpath = _webHostEnvironment.WebRootPath;
                    string filename = Guid.NewGuid().ToString() + "_" + category.Imagefile.FileName;
                    string path = Path.Combine(wwwRootpath + "/Images/", filename);
                    using (var filestream = new FileStream(path, FileMode.Create))
                    {
                        await category.Imagefile.CopyToAsync(filestream);
                    }
                    category.Imagepath = filename;
                }
                _context.Add(category);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View();
        }

        // GET: Categories/Edit/5
        public async Task<IActionResult> Edit(decimal? id)
        {
            if (id == null || _context.Categories == null)
            {
                return NotFound();
            }

            var category = await _context.Categories.FindAsync(id);
            if (category == null)
            {
                return NotFound();
            }
            return View(category);
        }

        // POST: Categories/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(decimal id, [Bind("Id,Gategoryname,Imagefile")] Category category)
        {
            if (id != category.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    if (category.Imagefile != null)
                    {
                        string wwwRootpath = _webHostEnvironment.WebRootPath;
                        string filename = Guid.NewGuid().ToString() + "_" + category.Imagefile.FileName;
                        string path = Path.Combine(wwwRootpath + "/Images/", filename);
                        using (var filestream = new FileStream(path, FileMode.Create))
                        {
                            await category.Imagefile.CopyToAsync(filestream);
                        }
                        category.Imagepath = filename;
                    }
                        _context.Update(category);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CategoryExists(category.Id))
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
            return View(category);
        }

        // GET: Categories/Delete/5
        public async Task<IActionResult> Delete(decimal? id)
        {
            if (id == null || _context.Categories == null)
            {
                return NotFound();
            }

            var category = await _context.Categories
                .FirstOrDefaultAsync(m => m.Id == id);
            if (category == null)
            {
                return NotFound();
            }

            return View(category);
        }

        // POST: Categories/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(decimal id)
        {
            if (_context.Categories == null)
            {
                return Problem("Entity set 'ModelContext.Categories'  is null.");
            }
            var category = await _context.Categories.FindAsync(id);
            if (category != null)
            {
                _context.Categories.Remove(category);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CategoryExists(decimal id)
        {
          return (_context.Categories?.Any(e => e.Id == id)).GetValueOrDefault();
        }
        [HttpGet]
        public IActionResult Deleteall()
        {
            return View();
        }
        [HttpPost, ActionName("Deleteall")]
        public async Task<IActionResult> Deleteall(int a)
        {
            foreach (Category item in _context.Categories)
            {
                //_context.Categories.Remove(item);
                await _context.Database.ExecuteSqlRawAsync($"delete from  Category where GategoryName='{item.Gategoryname}'");
                await _context.SaveChangesAsync();
           
            }
            return RedirectToAction(nameof(Index));
        }
    }
}
