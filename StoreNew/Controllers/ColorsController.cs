using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.CodeAnalysis;
using Microsoft.EntityFrameworkCore;
using StoreNew.Models;

namespace StoreNew.Controllers
{
    public class ColorsController : Controller
    {
        private readonly ModelContext _context;
        private readonly IWebHostEnvironment _webHostEnvironment;
        public ColorsController(ModelContext context, IWebHostEnvironment webHostEnvironment)
        {
            _context = context;
            _webHostEnvironment= webHostEnvironment;
        }

        // GET: Colors
        public async Task<IActionResult> Index()
        {
            var modelContext = _context.Colors.Include(c => c.Product);
            return View(await modelContext.ToListAsync());
        }

        // GET: Colors/Details/5
        public async Task<IActionResult> Details(decimal? id,int productid)
        {
            if (id == null || _context.Colors == null)
            {
                return NotFound();
            }

            var color = await _context.Colors
                .Include(c => c.Product)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (color == null)
            {
                return NotFound();
            }
            ViewBag.prpductid = productid;
            return View(color);
        }

        // GET: Colors/Create
        public IActionResult Create()
        {
            //ViewData["Productid"] = new SelectList(_context.Products, "Id", "Id");
            ViewBag.Productname = _context.Products
        .Select(c => new SelectListItem
        {
            Value = c.Id.ToString(),           // القيمة التي سترجع عند الاختيار
            Text = c.Name              // الاسم المعروض في القائمة
        })
        .ToList();
            return View();
        }

        // POST: Colors/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(int productid,[Bind("Id,Namecolors,Imagefile,Productid")] Color color)
        {
            if (ModelState.IsValid)
            {
                if (color.Imagefile != null)
                {
                    string wwwRootpath = _webHostEnvironment.WebRootPath;
                    string filename = Guid.NewGuid().ToString() + "_" + color.Imagefile.FileName;
                    string path = Path.Combine(wwwRootpath + "/Images/", filename);
                    using (var filestream = new FileStream(path, FileMode.Create))
                    {
                        await color.Imagefile.CopyToAsync(filestream);
                    }
                    color.Imagepath = filename;
                }
                _context.Add(color);
                await _context.SaveChangesAsync();
                return RedirectToAction("Colorproduct", "Admin", new {id= productid });
            }
            // ViewData["Productid"] = new SelectList(_context.Products, "Id", "Id", color.Productid);
            ViewBag.Productname = _context.Products
         .Select(c => new SelectListItem
         {
             Value = c.Id.ToString(),           // القيمة التي سترجع عند الاختيار
             Text = c.Name              // الاسم المعروض في القائمة
         })
         .ToList();
     
            return View(color);
        }

        // GET: Colors/Edit/5
        public async Task<IActionResult> Edit(decimal? id, int productid)
        {
            if (id == null || _context.Colors == null)
            {
                return NotFound();
            }

            var color = await _context.Colors.FindAsync(id);
            if (color == null)
            {
                return NotFound();
            }
            // ViewData["Productid"] = new SelectList(_context.Products, "Id", "Id", color.Productid);
            ViewBag.Productname = _context.Products
        .Select(c => new SelectListItem
        {
            Value = c.Id.ToString(),           // القيمة التي سترجع عند الاختيار
            Text = c.Name              // الاسم المعروض في القائمة
        })
        .ToList();

            ViewBag.idproduct = productid;
            return View(color);
        }

        // POST: Colors/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(decimal id,int productid, [Bind("Id,Namecolors,Imagefile,Productid")] Color color)
        {
            if (id != color.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    if (color.Imagefile != null)
                    {
                        string wwwRootpath = _webHostEnvironment.WebRootPath;
                        string filename = Guid.NewGuid().ToString() + "_" + color.Imagefile.FileName;
                        string path = Path.Combine(wwwRootpath + "/Images/", filename);
                        using (var filestream = new FileStream(path, FileMode.Create))
                        {
                            await color.Imagefile.CopyToAsync(filestream);
                        }
                        color.Imagepath = filename;
                    }
                    _context.Update(color);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ColorExists(color.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                //   return RedirectToAction(nameof(Index));
                return RedirectToAction("Colorproduct", "Admin", new { id = productid });

            }
            // ViewData["Productid"] = new SelectList(_context.Products, "Id", "Id", color.Productid);
            ViewBag.Productname = _context.Products
         .Select(c => new SelectListItem
         {
             Value = c.Id.ToString(),           // القيمة التي سترجع عند الاختيار
             Text = c.Name              // الاسم المعروض في القائمة
         })
         .ToList();
            return View(color);
        }

        // GET: Colors/Delete/5
        public async Task<IActionResult> Delete(decimal? id)
        {
            if (id == null || _context.Colors == null)
            {
                return NotFound();
            }

            var color = await _context.Colors
                .Include(c => c.Product)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (color == null)
            {
                return NotFound();
            }

            return View(color);
        }

        // POST: Colors/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(decimal id,int productid)
        {
            if (_context.Colors == null)
            {
                return Problem("Entity set 'ModelContext.Colors'  is null.");
            }
            var color = await _context.Colors.FindAsync(id);
            if (color != null)
            {
                _context.Colors.Remove(color);
            }
            
            await _context.SaveChangesAsync();
            // return RedirectToAction(nameof(Index));
            return RedirectToAction("Colorproduct", "Admin", new { id = productid });

        }

        private bool ColorExists(decimal id)
        {
          return (_context.Colors?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
