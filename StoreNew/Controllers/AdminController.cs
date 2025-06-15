using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using StoreNew.Models;

namespace StoreNew.Controllers
{
    public class AdminController : Controller
    {
        private readonly ModelContext _context;

        public AdminController(ModelContext context)
        {
            _context = context;
        }
        public async Task< IActionResult> Dashpord()
        {
            //b = _context.Categories != null ?
            //            View(await _context.Categories.ToListAsync()) :
            //            Problem("Entity set 'ModelContext.Categories'  is null.");

            //number of Customer
            // ViewBag.numberofcustomer = _context.Users.Count();
            //select* from Users u join User_Login  ul on u.Id = ul.Userid where ul.Roleid = 2;
            ViewBag.numberofcustomer = await _context.Users
                .Join(_context.UserLogins,
                      u => u.Id,
                      ul => ul.Userid,
                      (u, ul) => new { User = u, Login = ul })
                .Where(x => x.Login.Roleid == 2)
                .CountAsync();
            //number of order
            ViewBag.numberoforder = _context.Shopingcarts.Count();
            //total store incoma
            ViewBag.totalstoreincoma = _context.Shopingcarts.Sum(x => x.Totalamount);
            //number of catugory
            ViewBag.numberofcategory = _context.Categories.Count();
            //number of product
            ViewBag.numberofproduct = _context.Products.Count();
            //number of feedbackstore
            ViewBag.numberfeedbackstore = _context.FeedbackStores.Count();
            //avg feedback of store 
            decimal ratsite = Convert.ToDecimal(_context.FeedbackStores.Average(x => x.Ratingsatars));
            ViewBag.avgfeedbackstore = Convert.ToDecimal(Math.Round(ratsite, 2));
            //number of feedback product
            ViewBag.numberfeedbackproduct = _context.FeedbackProducts.Count();
            //avg of feedback product
            decimal ratproduct = Convert.ToDecimal(_context.FeedbackProducts.Average(x => x.Ratingsatars));
            ViewBag.avgfeedbackproduct = Convert.ToDecimal(Math.Round(ratproduct, 2));

            return View();
  
        }
        public async Task<IActionResult> productincategory(int id)
        {
          var allproductincategory=_context.Products.Where(a=>a.Categoryid==id).ToList();
            ViewBag.categoryname=await _context.Categories.Where(a => a.Id == id).Select(a=>a.Gategoryname).FirstOrDefaultAsync();
            //check details for product found or no
            ViewBag.details = _context.Details.ToList();
            return View(allproductincategory);
        }
        
     public async Task<IActionResult> Detailsproduct(int id)
        {
            var detailproduct = _context.Details.Where(a => a.Productid == id).ToList();
            ViewBag.productname = await _context.Products.Where(a => a.Id == id).Select(a => a.Name).FirstOrDefaultAsync();
           // ViewBag.isfound = (detailproduct == null) ? "no" : "yes";
          
            return View(detailproduct);
        }

        public async Task<IActionResult>Colorproduct(int id)
        {
            var colorproduct=_context.Colors.Where(a=>a.Productid==id).ToList();
            ViewBag.productname = await _context.Products.Where(a => a.Id == id).Select(a => a.Name).FirstOrDefaultAsync();
            ViewBag.idproduct=id;
            return View(colorproduct);
        }

        public async Task<IActionResult> orders()
        {
            var orders=await _context.Shopingcarts.OrderByDescending(s=>s.Id).ToListAsync();
            ViewBag.user =await _context.Users.ToListAsync();
            return View(orders);
        }
        public async Task<IActionResult> iteminorders(int id)
        {
            var iteminorders = await _context.CartProducts.Where(c=>c.Cartid==id).OrderByDescending(s => s.Id).ToListAsync();
            ViewBag.products = await _context.Products.ToListAsync();
            return View(iteminorders);
        }

        public IActionResult change(int orderid,string currentstate)
        {
            var order = _context.Shopingcarts.Where(s=>s.Id==orderid).SingleOrDefault();
            switch (currentstate)
            {
                
                case "In Preparation":
                    order.Orderstate = "On The Road";
                    _context.Shopingcarts.Update(order);
                    _context.SaveChangesAsync();
                    Notification not = new Notification();
                    not.Userid = order.Userid;
                    not.Datenoti = DateTime.Now;
                    not.Orderstate = "your order On The Road";
                    _context.Notifications.Add(not);
                     _context.SaveChanges();
                    break;
                case "On The Road":
                    order.Orderstate = "Arrive";
                    order.Datearrive=DateTime.Now;
                    _context.Shopingcarts.Update(order);
                    _context.SaveChangesAsync();
                    Notification not1 = new Notification();
                    not1.Userid = order.Userid;
                    not1.Datenoti = DateTime.Now;
                    not1.Orderstate = "your order Arrive";
                    _context.Notifications.Add(not1);
                    _context.SaveChanges();
                    break;
            }
            return RedirectToAction("orders");
        }
    }
}
