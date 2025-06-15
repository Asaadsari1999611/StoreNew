using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.CodeAnalysis;
using Microsoft.EntityFrameworkCore;
using StoreNew.Models;

namespace StoreNew.Controllers
{
    public class ProductsController : Controller
    {
        private readonly ModelContext _context;
        private readonly IWebHostEnvironment _webHostEnvironment;
        public ProductsController(ModelContext context, IWebHostEnvironment webHostEnvironment)
        {
            _context = context;
            _webHostEnvironment= webHostEnvironment;

        }

        // GET: Products
        public async Task<IActionResult> Index()
        {
            var modelContext = _context.Products.Include(p => p.Category);
            ViewBag.category = _context.Categories.ToList();
            //check details for product found or no
            ViewBag.details = _context.Details.ToList();
            return View(await modelContext.ToListAsync());
        }

        // GET: Products/Details/5
        public async Task<IActionResult> Details(decimal? id)
        {
            if (id == null || _context.Products == null)
            {
                return NotFound();
            }

            var product = await _context.Products
                .Include(p => p.Category)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (product == null)
            {
                return NotFound();
            }
            ViewBag.categoryname=await _context.Categories.Where(a=>a.Id==product.Categoryid).Select(a=>a.Gategoryname).FirstOrDefaultAsync();
            return View(product);
        }

        // GET: Products/Create
        public IActionResult Create()
        {
            //ViewData["Categoryid"] = new SelectList(_context.Categories, "Id", "Id");
            ViewBag.Categoryname = _context.Categories
        .Select(c => new SelectListItem
        {
            Value = c.Id.ToString(),           // القيمة التي سترجع عند الاختيار
            Text = c.Gategoryname              // الاسم المعروض في القائمة
        })
        .ToList();
            return View();
        }

        // POST: Products/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,Price,Sale,Stochquntity,Imagefile,Categoryid")] Product product)
        {
            if (ModelState.IsValid)
            {
                if (product.Imagefile != null)
                {
                    string wwwRootpath = _webHostEnvironment.WebRootPath;
                    string filename = Guid.NewGuid().ToString() + "_" + product.Imagefile.FileName;
                    string path = Path.Combine(wwwRootpath + "/Images/", filename);
                    using (var filestream = new FileStream(path, FileMode.Create))
                    {
                        await product.Imagefile.CopyToAsync(filestream);
                    }
                    product.Imagepath = filename;
                }
                if (product.Stochquntity >= 15)
                    product.Status = "Valiable";
                else if (product.Stochquntity <= 15 && product.Stochquntity > 0)
                    product.Status = "must be recharged";
                else product.Status = "empty";
                _context.Add(product);
                await _context.SaveChangesAsync();

                //add to statistic product
                StatisticsProduct statisticsProduct = new StatisticsProduct();
                statisticsProduct.Productid = product.Id;
                statisticsProduct.Topnumorderforproduct = 0;
                statisticsProduct.Topevaluateforproduct = 0;
                _context.StatisticsProducts.Add(statisticsProduct);
                await _context.SaveChangesAsync();

                return RedirectToAction(nameof(Index));
            }
           // ViewData["Categoryid"] = new SelectList(_context.Categories, "Id", "Id", product.Categoryid);
            //name of category
            ViewBag.Categoryname = _context.Categories
             .Select(c => new SelectListItem
             {
                 Value = c.Id.ToString(),           // القيمة التي سترجع عند الاختيار
                 Text = c.Gategoryname              // الاسم المعروض في القائمة
             })
             .ToList();

            return View(product);
        }

        // GET: Products/Edit/5
        public async Task<IActionResult> Edit(decimal? id)
        {
            if (id == null || _context.Products == null)
            {
                return NotFound();
            }

            var product = await _context.Products.FindAsync(id);
            if (product == null)
            {
                return NotFound();
            }
            ViewData["Categoryid"] = new SelectList(_context.Categories, "Id", "Id", product.Categoryid);
            ViewBag.Categoryname = _context.Categories
         .Select(c => new SelectListItem
         {
             Value = c.Id.ToString(),           // القيمة التي سترجع عند الاختيار
             Text = c.Gategoryname              // الاسم المعروض في القائمة
         })
         .ToList();
            return View(product);
        }

        // POST: Products/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(decimal id, [Bind("Id,Name,Price,Sale,Stochquntity,Imagefile,Categoryid")] Product product)
        {
            if (id != product.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    if (product.Imagefile != null)
                    {
                        string wwwRootpath = _webHostEnvironment.WebRootPath;
                        string filename = Guid.NewGuid().ToString() + "_" + product.Imagefile.FileName;
                        string path = Path.Combine(wwwRootpath + "/Images/", filename);
                        using (var filestream = new FileStream(path, FileMode.Create))
                        {
                            await product.Imagefile.CopyToAsync(filestream);
                        }
                        product.Imagepath = filename;
                    }
                    if (product.Stochquntity >= 15)
                        product.Status = "Valiable";
                    else if (product.Stochquntity <= 15 && product.Stochquntity > 0)
                        product.Status = "must be recharged";
                    else product.Status = "empty";
                    _context.Update(product);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ProductExists(product.Id))
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
            ViewData["Categoryid"] = new SelectList(_context.Categories, "Id", "Id", product.Categoryid);
            ViewBag.Categoryname = _context.Categories
       .Select(c => new SelectListItem
       {
           Value = c.Id.ToString(),           // القيمة التي سترجع عند الاختيار
           Text = c.Gategoryname              // الاسم المعروض في القائمة
       })
       .ToList();
            return View(product);
        }

        // GET: Products/Delete/5
        public async Task<IActionResult> Delete(decimal? id)
        {
            if (id == null || _context.Products == null)
            {
                return NotFound();
            }

            var product = await _context.Products
                .Include(p => p.Category)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (product == null)
            {
                return NotFound();
            }

            return View(product);
        }

        // POST: Products/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(decimal id)
        {
            if (_context.Products == null)
            {
                return Problem("Entity set 'ModelContext.Products'  is null.");
            }
            var product = await _context.Products.FindAsync(id);
            if (product != null)
            {
                _context.Products.Remove(product);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ProductExists(decimal id)
        {
          return (_context.Products?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
