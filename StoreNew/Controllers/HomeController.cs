using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using StoreNew.Models;
using System.Diagnostics;

namespace StoreNew.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ModelContext _context;
        private readonly IWebHostEnvironment _webHostEnvironment;


        public HomeController(ILogger<HomeController> logger, ModelContext context, IWebHostEnvironment webHostEnvironment)
        {
            _logger = logger;
            _context = context;
            _webHostEnvironment = webHostEnvironment;
        }

        public IActionResult index()
        {
            //start check gust or customer by userid
            var user = HttpContext.Session.GetInt32("UserId");
            if (user == null)
            {
                ViewBag.type = 0;
            }
            else ViewBag.type = (int)user;
            //end check gust or customer by userid


            //start show feedback customer for store in home
            var feedback= _context.FeedbackStores.Where(f =>f.Shared =="on").OrderByDescending(f=>f.Id).ToList();
            //var feedback = _context.FeedbackStores.ToList();
            ViewBag.customers = _context.Users.ToList();
            //ViewBag.countfeedshared = _context.FeedbackStores.Where(f => f.Shared == "on").Count();
            //end show feedback customer for store in home
            ViewBag.count=feedback.Count();

            //start show notification customer in home
            ViewBag.notification = _context.Notifications.Where(n => n.Userid == user).OrderByDescending(n=>n.Id).ToList();
            ViewBag.numnot = _context.Notifications.Where(n => n.Userid == user).Count();
            //end show notification customer in home


            //start show ststistic products in home
            ViewBag.allproductsinsite = _context.Products.ToList();//products in store
            ViewBag.allcustomerssinsite = _context.Users.ToList();//customer in site


            //start show top 3 product in sale in home
            var productsale = _context.Products.ToList();
            List<Product> producttopsale = new List<Product>();//list of three product 
            int maxs = -999;
            for (int i = 0; i < 3; i++)
            {
                //find product than max sale
                foreach (var p in productsale)
                {
                    if (maxs < p.Sale)
                        maxs = (int)p.Sale;

                }
                //declare obj to set product than max sale for delete from list productsale
                Product obj = new Product();
                //add product than max sale to list producttopsale
                int count = 0;
                foreach (var p in productsale)
                {
                    if (maxs == (int)p.Sale)
                    {
                        if (count == 0)
                        {
                            producttopsale.Add(p);
                            obj = p;
                        }
                        count++;
                    }
                }
                //remove product than max sale from list productsale to find the second top product 
                productsale.Remove(obj);
                //reset value for max to again loop
                maxs = -999;
            }
            //now list producttopsale contin top three product in sale price
            ViewBag.topsaleproduct = producttopsale.ToList();
            //end show top 3 product in sale in home




            //start show top 3 product in number of order in home
            var product = _context.StatisticsProducts.ToList();//list of statistic products for all product
            List<StatisticsProduct> list1 = new List<StatisticsProduct>();//copy list of statistic products for all product
            List<StatisticsProduct> producttoppsseeling = new List<StatisticsProduct>();//list top three product in num order
            foreach (var p in product)
            {
                list1.Add(p);
            }
            int max = -999;
            for (int i = 0; i < 3; i++)
            {
                //find product than max number of order
                foreach (var p in list1)
                {
                    if (max < p.Topnumorderforproduct)
                        max = (int)p.Topnumorderforproduct;

                }
                //declare obj to set product than max num order for delete from list1
                StatisticsProduct topproduct = new StatisticsProduct();
                int count1 = 0;
                //add product than max number of order to list producttoppsseeling
                foreach (var p in list1)
                {
                    if (max == (int)p.Topnumorderforproduct)
                    {
                        if (count1 == 0)
                        {
                            producttoppsseeling.Add(p);
                            topproduct = p;
                        }
                        count1++;
                    }
                }
                //remove product than max number of order from list1 to find the second top product 
                list1.Remove(topproduct);
                //reset value for max to again loop
                max = -999;
            }
            //now list producttopsale contin top three product in number order
            ViewBag.topsnumorderproduct = producttoppsseeling.ToList();
            //end show top 3 product in number of order in home




            //start show top product in evaluate srats 
            var product1 = _context.StatisticsProducts.ToList();
            List<StatisticsProduct> list11 = new List<StatisticsProduct>();
            List<StatisticsProduct> producttoppevaluate = new List<StatisticsProduct>();
           
            foreach (var p in product1)
            {
                list11.Add(p);
            }
            int max1 = -999;
            for (int i = 0; i < 3; i++)
            {
                //fin product than max evaluate stars
                foreach (var p in list11)
                {
                    if (max1 < p.Topevaluateforproduct)
                        max1 = (int)p.Topevaluateforproduct;

                }
                //declare obj to set product than max evaluate stars for delete from list1
                StatisticsProduct topproduct2 = new StatisticsProduct();
                int count3 = 0;
                //add product than max evaluate stars  list producttoppsseeling
                foreach (var p in list11)
                {
                    if (max1 == (int)p.Topevaluateforproduct)
                    {
                        if (count3 == 0)
                        {
                            producttoppevaluate.Add(p);
                            topproduct2 = p;
                        }
                        count3++;
                    }
                }
                //remove product than mmax evaluate stars from list1 to find the second top product 
                list11.Remove(topproduct2);
                //reset value for max to again loop
                max1 = -999;
            }
            ViewBag.topsevaluateproduct = producttoppevaluate.ToList();
            //end show top product in evaluate srats 




            //end show ststistic products





            //start show ststistic customer
            //start show top customer in num order
            var customer = _context.StatisticsCustomers.ToList();
            List<StatisticsCustomer> list2 = new List<StatisticsCustomer>();
            List<StatisticsCustomer> customertopnumorder = new List<StatisticsCustomer>();
            foreach (var p in customer)
            {
                list2.Add(p);
            }
            int max3 = -999;
            for (int i = 0; i < 3; i++)
            {
                //fin product than max number of order
                foreach (var p in list2)
                {
                    if (max3 < p.Topnumorderforcustomer)
                        max3 = (int)p.Topnumorderforcustomer;

                }
                //declare obj to set product than max num order for delete from list1
                StatisticsCustomer topcustomer = new StatisticsCustomer();
                int count4 = 0;
                //add product than max number of order to list producttoppsseeling
                foreach (var p in list2)
                {
                    if (max3 == (int)p.Topnumorderforcustomer)
                    {
                        if (count4 == 0)
                        {
                            customertopnumorder.Add(p);
                            topcustomer = p;
                        }
                        count4++;

                    }

                }
                //remove product than max number of order from list1 to find the second top product 
                list2.Remove(topcustomer);
                //reset value for max to again loop
                max3 = -999;

            }
            ViewBag.topsnumordercustomer = customertopnumorder.ToList();
            //end show top customer in num order





            //start show top customer in bay 
            var customer1 = _context.StatisticsCustomers.ToList();
            List<StatisticsCustomer> list22 = new List<StatisticsCustomer>();
            List<StatisticsCustomer> customertopbay = new List<StatisticsCustomer>();
            foreach (var p in customer1)
            {
                list22.Add(p);
            }
            int max4 = -999;
            for (int i = 0; i < 3; i++)
            {
                //fin product than max evaluate stars
                foreach (var p in list22)
                {
                    if (max4 < p.Topbayforcustomer)
                        max4 = (int)p.Topbayforcustomer;
                }
                //declare obj to set product than max evaluate stars for delete from list1
                StatisticsCustomer topcustomerbay = new StatisticsCustomer();
                int count5 = 0;
                //add product than max evaluate stars  list producttoppsseeling
                foreach (var p in list22)
                {
                    if (max4 == (int)p.Topbayforcustomer)
                    {
                        if (count5 == 0)
                        {
                            customertopbay.Add(p);
                            topcustomerbay = p;
                        }
                        count5++;
                    }

                }
                //remove product than mmax evaluate stars from list1 to find the second top product 
                list22.Remove(topcustomerbay);
                //reset value for max to again loop
                max4 = -999;
            }
            ViewBag.topbaycustomer = customertopbay.ToList();
            //end show top customer in bay 



            //end show ststistic customer



            
            return View(feedback);
        }



        public async Task<IActionResult> clearnotification()
        {
            var user = HttpContext.Session.GetInt32("UserId");

            var notefications =await _context.Notifications.Where(n => n.Userid == user).ToListAsync();
            foreach (var not in notefications)
            {
                _context.Notifications.Remove(not);
                _context.SaveChangesAsync();
            }
            return RedirectToAction("index");
        }



        public  IActionResult category()
        {
            var user = HttpContext.Session.GetInt32("UserId");
            var categoree = _context.Categories.ToList();
            Homeviewmodel ob = new Homeviewmodel();//or use section
            if (user == null)
            {
                ob.Iduser = 0;
                ViewBag.typecategory = ob.Iduser;
                return View(categoree);
            }
            else {
                ob.Iduser = (int)user;
                ViewBag.typecategory = ob.Iduser;
                ViewBag.state = HttpContext.Session.GetString("order");

                return View(categoree);

                
            }



        }



        public IActionResult product(int id)
        {
            var user = HttpContext.Session.GetInt32("UserId");
            ViewBag.iduser = user;
            var product = _context.Products.Where(x => x.Categoryid == id).ToList();
            Homeviewmodel ob = new Homeviewmodel();
            if (user == null)
            {
                ob.Iduser = 0;
                ViewBag.typecategory = ob.Iduser;
                //get rating
                ViewBag.feedbcks = _context.FeedbackProducts.ToList();
                return View(product);

            }
            ob.Iduser = (int)user;
            ViewBag.typecategory = ob.Iduser;
            //get rating
            ViewBag.feedbcks = _context.FeedbackProducts.ToList();

            return View(product);
        }
        public IActionResult details(int id)
        {
            var user = HttpContext.Session.GetInt32("UserId");
            var detail = _context.Details.Where(x => x.Productid == id).ToList();
            Homeviewmodel ob = new Homeviewmodel();
            if (user == null)
            {
                ob.Iduser = 0;
                ViewBag.typeuser = ob.Iduser;
                return View(detail);

            }
            ob.Iduser = (int)user; 
            ViewBag.typeuser = ob.Iduser;
            return View(detail);
        }


        public IActionResult colors(int id)
        {
            var user = HttpContext.Session.GetInt32("UserId");
            var color = _context.Colors.Where(a => a.Productid == id).ToList();
            Homeviewmodel ob = new Homeviewmodel();

            if (user == null)
            {
                ob.Iduser = 0;
                ViewBag.typeuser = ob.Iduser;
                return View(color);
            }
            else
            {
                ob.Iduser = (int)user;
                ViewBag.typeuser = ob.Iduser;
                return View(color);
            }
        }

        public IActionResult about()
        {
            var user = HttpContext.Session.GetInt32("UserId");

            if (user == null)
            {
                return View(new Homeviewmodel { Iduser = 0 });
                //return RedirectToAction("index", "Home");
            }else
            return View(new Homeviewmodel { Iduser = (int)user });
        }
        public async Task<IActionResult> contact()
        {

            var user = HttpContext.Session.GetInt32("UserId");
            ViewBag.userid = user;
            var outgoing = _context.Contacts.Where(c => c.Userid == user).OrderByDescending(c=>c.Id).ToList();
            var username = _context.UserLogins.Where(u => u.Userid == user).Select(u => u.Username).SingleOrDefault();
            ViewBag.inpox = _context.Contactreverses.Where(c => c.Username == username).OrderByDescending(c => c.Id).ToList();
            return View(outgoing);

        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> contact(decimal? iduser, string? message)
        {

            Contact contact = new Contact();
            contact.Message = message;
            contact.Senddate = DateTime.Now;
            contact.Visiondate = null;
            contact.Userid = iduser;
            _context.Contacts.Add(contact);
           await _context.SaveChangesAsync();
            return RedirectToAction("contact");
        }

        public async Task<IActionResult> contactreverse0()
        {

            ViewBag.user = _context.Users.ToList();
            var inpox = _context.Contacts.OrderByDescending(c => c.Id).ToList();
            ViewBag.outgoing = _context.Contactreverses.OrderByDescending(c => c.Id).ToList();
            return View(inpox);

        }
        public async Task<IActionResult> contactreverse()
        {

            ViewBag.user = _context.Users.ToList();
            var inpox = _context.Contacts.OrderByDescending(c => c.Id).ToList();
            ViewBag.outgoing = _context.Contactreverses.OrderByDescending(c => c.Id).ToList();
            return View(inpox);

        }



        public async Task<IActionResult> vision(decimal? idmessage)
        {
            var message = _context.Contacts.Where(c => c.Id == idmessage).SingleOrDefault();
            message.Visiondate = DateTime.Now;
            _context.Contacts.Update(message);
            await _context.SaveChangesAsync();
            //notification
            var user = _context.Contacts.Where(c => c.Id == idmessage).Select(c => c.Userid).SingleOrDefault();
            Notification not = new Notification();
            not.Userid = user;
            not.Viewmessage = "your message view by admin";
            not.Datenoti = DateTime.Now;
            _context.Notifications.Add(not);
            await _context.SaveChangesAsync();
            return RedirectToAction("contactreverse");
        }

        public async Task<IActionResult> replay(decimal? idmessage)
        {
            var userid = _context.Contacts.Where(c => c.Id == idmessage).Select(c => c.Userid).FirstOrDefault();
            ViewBag.username = _context.UserLogins.Where(u => u.Userid == userid).Select(u => u.Username).FirstOrDefault();

            return View();

        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> replay(String? username, string? message)
        {
            Contactreverse contact = new Contactreverse();
            contact.Message = message;
            contact.Senddate = DateTime.Now;
            contact.Visiondate = null;
            contact.Username = username;
            _context.Contactreverses.Add(contact);
           await _context.SaveChangesAsync();
            //notification
            var user = _context.UserLogins.Where(u => u.Username == username).Select(u => u.Userid).FirstOrDefault();
            Notification not = new Notification();
            not.Userid = user;
            not.Message = "New message from admin ";
            not.Datenoti = DateTime.Now;
            _context.Notifications.Add(not);
           await _context.SaveChangesAsync();
            return RedirectToAction("contactreverse");
        }

        public async Task<IActionResult> feedback()
        {
            var user = HttpContext.Session.GetInt32("UserId");
            ViewBag.userid = user;
            var feedbacksstore = _context.FeedbackStores.Where(f => f.Userid == user).OrderByDescending(f=>f.Id).ToList();
            decimal ratsite = Convert.ToDecimal(_context.FeedbackStores.Select(f => f.Ratingsatars).Average());
            ViewBag.ratingsite = Convert.ToDecimal(Math.Round(ratsite, 2));
            return View(feedbacksstore);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> feedback(decimal? iduser, decimal? rating, string? message)
        {
            FeedbackStore feed = new FeedbackStore();
            feed.Userid = iduser;
            feed.Ratingdetails = message;
            feed.Ratingsatars = rating;
            //feed.Productid = 0;//اضافته بالخطأ غير مهم
            feed.Shared ="of";
            _context.FeedbackStores.Add(feed);
            await _context.SaveChangesAsync();

            return RedirectToAction("feedback");
        }

        public async Task<IActionResult> shared(decimal? id)
        {
            var feedback = _context.FeedbackStores.Where(f => f.Id == id).SingleOrDefault();
            var user = feedback.Userid;
            if (feedback.Shared == "of")
            {
                feedback.Shared = "on";
                Notification not = new Notification();
                not.Userid = user;
                not.Feedbackshare = "your feedbacks shared";
                not.Datenoti = DateTime.Now;
                _context.Notifications.Add(not);
                _context.SaveChangesAsync();
            }

            else
            {
                feedback.Shared = "of";
                Notification not = new Notification();
                not.Userid = user;
                not.Feedbackshare = "cansel shared for your feedbacks";
                not.Datenoti = DateTime.Now;

                _context.Notifications.Add(not);
                _context.SaveChangesAsync();
            }

            _context.FeedbackStores.Update(feedback);
            _context.SaveChangesAsync();

            return RedirectToAction("Index", "FeedbackStores");
        }


        public IActionResult profile()
        {
            var user = HttpContext.Session.GetInt32("UserId");


            var customer = _context.Users.Where(x => x.Id == user).SingleOrDefault();

            ViewBag.userlogin = _context.UserLogins.Where(x => x.Userid == user).ToList();
            //ViewBag.numberorder = _context.Shopingcarts.Where(x => x.UserId == user).Count();
            //new code start
            var payments = _context.Payments.ToList();
            var orders = _context.Shopingcarts.ToList();
            int count = 0;
            foreach (var pay in payments)
            {
                foreach (var order in orders)
                {
                    if (order.Userid == user && order.Id == pay.Shopingcartid)
                    {
                        count++;
                    }
                }

            }
            ViewBag.numberorder = count;
            //new code end
            ViewBag.balance = _context.Cards.Where(x => x.Userid == user).Sum(x => x.Balance);
            //ViewBag.bay = _context.Shopingcarts.Where(x => x.UserId == user).Sum(x => x.TotalAmount);
            //new code start
            decimal count2 = 0;
            foreach (var pay in payments)
            {
                foreach (var order in orders)
                {
                    if (order.Userid == user && order.Id == pay.Shopingcartid)
                    {
                        count2 += Convert.ToDecimal(order.Totalamount);
                    }
                }

            }
            ViewBag.bay = count2;
            //new code end
            return View(customer);


        }


        public async Task<IActionResult> Editprofile(decimal? id)
        {
            if (id == null || _context.Users == null)
            {
                return NotFound();
            }

            var user = await _context.Users.FindAsync(id);
            if (user == null)
            {
                return NotFound();
            }
            return View(user);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Editprofile(decimal id, [Bind("Id,Fname,Lname,Birthdate,Adress,Phonenumber,Email,Imagefile")] User user)
        {
            if (id != user.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                if (user.Imagefile != null)
                {
                    string wwwRootpath = _webHostEnvironment.WebRootPath;
                    string filename = Guid.NewGuid().ToString() + "_" + user.Imagefile.FileName;
                    string path = Path.Combine(wwwRootpath + "/Images/", filename);
                    using (var filestream = new FileStream(path, FileMode.Create))
                    {
                        await user.Imagefile.CopyToAsync(filestream);
                    }
                    user.Imagepath = filename;
                    user.Onlinee = "on";
                }
                _context.Update(user);
                await _context.SaveChangesAsync();


                return RedirectToAction(nameof(profile));
            }
            return View(user);
        }

        public IActionResult addcart()
        {
            var cardd = HttpContext.Session.GetInt32("UserId");
            var iffound = _context.Cards.Where(x => x.Userid == cardd).SingleOrDefault();
            if (iffound == null)
            {
                Card card = new Card();
                card.Userid = cardd;
                Random num = new Random();
                string randomnum = "";
                for (int i = 0; i < 12; i++)
                {
                    if (i == 11 && num.Next(0, 10).ToString() == "0")
                    {
                        randomnum += "9";

                    }
                    else
                        randomnum += num.Next(0, 10).ToString();
                }


                card.Cardnumber = Convert.ToDecimal(randomnum);
                Random num2 = new Random();
                string randomnum2 = "";
                for (int i = 0; i < 4; i++)
                {
                    if (i == 3 && num2.Next(0, 10).ToString() == "0")
                    {
                        randomnum2 += "9";

                    }
                    else
                        randomnum2 += num2.Next(0, 10).ToString();
                }

                card.Cvv = Convert.ToDecimal(randomnum2);
                card.Status = "empty ";
                card.Balance = 0;
                _context.Add(card);
                ViewBag.users = _context.Users.Where(x => x.Id == card.Userid).ToList();
                ViewBag.type = "new";
                return View(card);
            }
            else
            {
                ViewBag.users = _context.Users.Where(x => x.Id == cardd).ToList();
                ViewBag.type = "old";
                return View(iffound);
            }


        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> addcart([Bind("Id,Cardnumber,Balance,Status,Userid,Cvv")] Card cards)
        {
            if (ModelState.IsValid)
            {
                _context.Add(cards);
                await _context.SaveChangesAsync();

                return RedirectToAction("profile");
            }
            return View("addcart");
        }
        public async Task<IActionResult> Editcard(decimal? id)
        {
            if (id == null || _context.Cards == null)
            {
                return NotFound();
            }

            var card = await _context.Cards.FindAsync(id);
            if (card == null)
            {
                return NotFound();
            }
            return View(card);
        }


		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> Editcard(decimal id, [Bind("Id,Cardnumber,Cvv,Balance,Status,Userid")] Card card)
		{
			if (id != card.Id)
			{
				return NotFound();
			}

			if (ModelState.IsValid)
			{
				if (card.Balance >= 100)
					card.Status = "valiable";
				else if (card.Balance < 100 && card.Balance > 0)
					card.Status = "must be recharged";
				else
					card.Status = "empty ";
				_context.Update(card);
				await _context.SaveChangesAsync();
				//notification
				var user = card.Userid;
				Notification not = new Notification();
				not.Userid = user;
				not.Changeinfcard = "update information card done";
				not.Datenoti = DateTime.Now;
				_context.Notifications.Add(not);
				_context.SaveChangesAsync();

				return RedirectToAction(nameof(addcart));
			}
			return View(card);
		}
        public IActionResult cart(decimal? idproduct)
        {
            var product = _context.Products.Where(p => p.Id == idproduct).SingleOrDefault();
            string iffound = HttpContext.Session.GetString("productfound");
            ViewBag.productfound = iffound;
            return View(product);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> cart(decimal? productid, decimal? productcount)
        {
            HttpContext.Session.SetString("productfound", "");


            var user = HttpContext.Session.GetInt32("UserId");
            var product = _context.Products.Where(p => p.Id == productid).SingleOrDefault();
            //check the number of broduct found or not
            if (product.Stochquntity >= productcount)
            {
                var orederstat = _context.Shopingcarts.Where(s => s.Userid == user).ToList();
                //start code for creat more than order to same customer
                var paymentorder = _context.Payments.ToList();
                int count = 0;
                foreach (var item in paymentorder)
                {
                    var idorderrr = item.Shopingcartid;
                    var orderinf = _context.Shopingcarts.Where(s => s.Id == idorderrr).SingleOrDefault();
                    if (orderinf.Userid == user)
                        count++;
                }
                int countorder = orederstat.Count();
                var stateorder = HttpContext.Session.GetString("order");                //end code for creat more than order to same customer
                if ( count == countorder )
                {
                    HttpContext.Session.SetString("order", "close");
                }
                else
                    HttpContext.Session.SetString("order", "open");
                var state = HttpContext.Session.GetString("order");
                Shopingcart order = new Shopingcart();

                if (state == "close")
                {
                    order.Userid = (int)user;
                   // order.Orderdate = DateTime.Now;
                    order.Totalamount = 0;
                    order.Orderstate = "Loading Items";
                    _context.Add(order);
                    await _context.SaveChangesAsync();
                    CartProduct item = new CartProduct();
                    item.Productprice = product.Price;
                    item.Quantity = productcount;
                    item.Cartid = order.Id;
                    HttpContext.Session.SetInt32("curentorderid", (int)order.Id);
                    item.Productid = productid;
                    _context.Add(item);
                    await _context.SaveChangesAsync();
                    order.Totalamount = Convert.ToDecimal((double)order.Totalamount + (((double)product.Price - ((double)product.Price * ((double)product.Sale * 0.01))) * (double)productcount));
                    _context.Update(order);
                    await _context.SaveChangesAsync();
                   HttpContext.Session.SetString("order", "open");
                }
                else
                {
                    decimal curentordid = (decimal)HttpContext.Session.GetInt32("curentorderid");
                    CartProduct item = new CartProduct();
                    item.Productprice = product.Price;
                    item.Quantity = productcount;
                    item.Cartid = curentordid;
                    item.Productid = productid;
                    _context.Add(item);
                    await _context.SaveChangesAsync();
                    //order.TotalAmount = order.TotalAmount + (decimal)(product.Price * productcount);
                    //_context.Update(order);
                    //await _context.SaveChangesAsync();
                    //Shopingcart order = new Shopingcart();
                    var orderinformation = await _context.Shopingcarts.FindAsync(curentordid);
                    orderinformation.Totalamount = Convert.ToDecimal((double)orderinformation.Totalamount + (((double)product.Price - ((double)product.Price * ((double)product.Sale * 0.01))) * (double)productcount));
                    _context.Shopingcarts.Update(orderinformation);
                    await _context.SaveChangesAsync();
                }
                //مهم جدااااااااااااااااااااااااا
            // return RedirectToAction("cartitem");
                return Redirect("~/Home/cartitem");
            }
            else
            {
                HttpContext.Session.SetString("productfound", "the quantity is not enough");
                return RedirectToAction("cart", new { idproduct = productid });
            }
            //end check the number of broduct found or not

        }


        public IActionResult cartitem()
        {
            string state = HttpContext.Session.GetString("order");

            if (state == "open")
            {
                decimal curentordid = (decimal)HttpContext.Session.GetInt32("curentorderid");

            var items = _context.CartProducts.Where(cp => cp.Cartid == curentordid).ToList();

            ViewBag.products = _context.Products.ToList();
            ViewBag.order = curentordid;
            ViewBag.totalprice = _context.Shopingcarts.Where(sh => sh.Id == curentordid).Sum(sh => sh.Totalamount);
           ViewBag.state = HttpContext.Session.GetString("order");
           
                return View(items);
            }
            else 
            {
                return RedirectToAction("category");

            }
           
        }

        public   IActionResult deletefromcartitem(int id)
        {
            decimal curentordid = (decimal)HttpContext.Session.GetInt32("curentorderid");
            var items = _context.CartProducts.Where(c => c.Cartid == curentordid).ToList();
            var item = items.Where(i => i.Id == id).FirstOrDefault();

            if (item != null)
            {
                _context.CartProducts.Remove(item);
                 _context.SaveChanges();
            }
            var product = _context.Products.Where(p => p.Id == item.Productid).FirstOrDefault();
            var myorder = _context.Shopingcarts.Where(s => s.Id == curentordid).FirstOrDefault();
            if (myorder != null)
            {
                myorder.Totalamount = Convert.ToDecimal((double)myorder.Totalamount - (((double)product.Price - ((double)product.Price * ((double)product.Sale * 0.01))) * (double)item.Quantity));
                _context.Shopingcarts.Update(myorder);
                 _context.SaveChanges();
            }
            return RedirectToAction("cartitem");
        }
        public  IActionResult canselcartitem(decimal id)
        {
            var items = _context.CartProducts.Where(c => c.Cartid == id).ToList();
            foreach (var item in items)
            {
                _context.CartProducts.Remove(item);
                _context.SaveChanges();
            }
            var order = _context.Shopingcarts.Where(s => s.Id == id).FirstOrDefault();
            _context.Shopingcarts.Remove(order);
            _context.SaveChanges();

            HttpContext.Session.SetString("order", "close");

            return RedirectToAction("category");
        }

        public IActionResult print()
        {
            int curentordid = (int)HttpContext.Session.GetInt32("curentorderid");
            var items = _context.CartProducts.Where(s => s.Cartid == curentordid).ToList();
            ViewBag.order = _context.Shopingcarts.Where(s => s.Id == curentordid).SingleOrDefault();
            var orderr = _context.Shopingcarts.Where(s => s.Id == curentordid).SingleOrDefault(); ;
            ViewBag.customer = _context.Users.Where(c => c.Id == orderr.Userid).FirstOrDefault();
            ViewBag.ordernumber = curentordid;
            ViewBag.product = _context.Products.ToList();
            ViewBag.date = DateTime.Now;

            return View(items);
        }

        public IActionResult payment()
        {
            int curentordid = (int)HttpContext.Session.GetInt32("curentorderid");

            ViewBag.orderid = curentordid;
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> payment(decimal? CardNumber, decimal? Cvv)
        {
            int curentordid = (int)HttpContext.Session.GetInt32("curentorderid");
            var order = _context.Shopingcarts.Where(s => s.Id == curentordid).FirstOrDefault();
            var user = HttpContext.Session.GetInt32("UserId");
            var card = _context.Cards.Where(c => c.Userid == user).FirstOrDefault();

            if (card == null)
            {
                ViewBag.paymenterror = "you dont have a card";
                return View();
            }
            if (card.Cardnumber != CardNumber || card.Cvv != Cvv)
            {
                ViewBag.paymenterror = "error information , enter again";
                return View();
            }
            if (card.Balance < order.Totalamount)
            {
                ViewBag.paymenterror = "your balance is not enough to complete the payment";
                return View();
            }
            order.Orderdate = DateTime.Now;
            order.Orderstate = "In Preparation";
            _context.Shopingcarts.Update(order);
           await _context.SaveChangesAsync();
            Notification not = new Notification();
            not.Userid = user;
            not.Datenoti = DateTime.Now;
            not.Orderstate = "your order In Preparation";
            _context.Notifications.Add(not);
            await _context.SaveChangesAsync();
            //here the payment succss and i start to payment
            Payment payment = new Payment();
            payment.Totalamount = order.Totalamount;
            payment.Paymentdate = DateTime.Now;
            payment.Paymenttype = "visa";
            payment.Shopingcartid = order.Id;
            _context.Payments.Add(payment);
            await _context.SaveChangesAsync();
            //edit balance
            card.Balance = card.Balance - order.Totalamount;
            _context.Cards.Update(card);
           await _context.SaveChangesAsync();
            //notification
            if (card.Status == "empty")
            {
                Notification not1 = new Notification();
                not1.Userid = user;
                not1.Cardbalance = "your card is empty";
                not1.Datenoti = DateTime.Now;
                _context.Notifications.Add(not1);
              await  _context.SaveChangesAsync();
            }
            if (card.Status == "must be recharge")
            {
                Notification not1 = new Notification();
                not1.Userid = user;
                not1.Cardbalance = "your card must be recharge";
                not1.Datenoti = DateTime.Now;
                _context.Notifications.Add(not1);
              await  _context.SaveChangesAsync();
            }

           
            //start code discount from number items
            var items = _context.CartProducts.Where(c => c.Cartid == curentordid).ToList();

            foreach (var item in items)
            {
                var product = _context.Products.Where(p => p.Id == item.Productid).SingleOrDefault();
                product.Stochquntity = Convert.ToDecimal(product.Stochquntity - item.Quantity);
                if (product.Stochquntity >= 15)
                    product.Status = "Valiable";
                else if (product.Stochquntity < 15 && product.Stochquntity > 0)
                    product.Status = "must be recharged";
                else if (product.Stochquntity == 0)
                    product.Status = "empty ";
                _context.Products.Update(product);
              await  _context.SaveChangesAsync();
            }
            //end start code discount from number items
            //notification
            Notification not2 = new Notification();
            not2.Userid = user;
            not2.Payment = "payment order done";
            not2.Datenoti = DateTime.Now;
            _context.Notifications.Add(not2);
            await _context.SaveChangesAsync();
            //add to statistic product
            var alliteminorder = _context.CartProducts.Where(c => c.Cartid == curentordid).ToList();
            foreach (var item in alliteminorder)
            {
                var currentproduct = _context.StatisticsProducts.Where(t => t.Productid == item.Productid).SingleOrDefault();
                currentproduct.Topnumorderforproduct += item.Quantity;
                _context.StatisticsProducts.Update(currentproduct);
                await _context.SaveChangesAsync();
            }

            //add to statistic customer
            var customercurrent = _context.StatisticsCustomers.Where(c => c.Userid == user).SingleOrDefault();
            customercurrent.Topnumorderforcustomer++;
            customercurrent.Topbayforcustomer += order.Totalamount;
            _context.StatisticsCustomers.Update(customercurrent);
            await _context.SaveChangesAsync();

            HttpContext.Session.SetString("order", "close");

            return RedirectToAction("myorders");

        }

        public IActionResult myorders()
        {
            var user = HttpContext.Session.GetInt32("UserId");
            ViewBag.payments = _context.Payments.ToList();
            var orders = _context.Shopingcarts.Where(s => s.Userid == user).OrderByDescending(o=>o.Id).ToList();
            return View(orders);
        }
        public IActionResult itemsinorder(int id)
        {
            ViewBag.products = _context.Products.ToList();
            var items = _context.CartProducts.Where(s => s.Cartid == id).ToList();
            return View(items);
        }
       
               public IActionResult lifelocation()
        {
          
            return View();
        }
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