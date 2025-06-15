using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using StoreNew.Models;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Model;

namespace StoreNew.Controllers
{
	public class RigesterAndLoginController : Controller
	{
		private readonly ModelContext _context;
		private readonly IWebHostEnvironment _webHostEnvironment;
		private readonly EmailSender _emailSender;
		public RigesterAndLoginController(ModelContext context, IWebHostEnvironment webHostEnvironment)
		{
			_context = context;
			_webHostEnvironment = webHostEnvironment;
			_emailSender = new EmailSender();
		}
		public IActionResult Rigester()
		{
			return View();
		}

		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> Rigester([Bind("Id,Fname,Lname,Birthdate,Adress,Phonenumber,Email,Imagefile,Online")] User user, string username, string password)
		{
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
				}
				user.Onlinee = "of";
				_context.Add(user);
				await _context.SaveChangesAsync();
				//add userlogin details
				UserLogin ob = new UserLogin();
				ob.Roleid = 2;
				ob.Username = username;
				ob.Password = password;
				ob.Userid = user.Id;
				ob.Lastlogin = null;
				_context.Add(ob);
				await _context.SaveChangesAsync();

				//add to statistc customer
				StatisticsCustomer statisticsCustomer = new StatisticsCustomer();
				statisticsCustomer.Userid = user.Id;
				statisticsCustomer.Topnumorderforcustomer = 0;
				statisticsCustomer.Topbayforcustomer = 0;
				_context.StatisticsCustomers.Add(statisticsCustomer);
				await _context.SaveChangesAsync();

				return RedirectToAction("Login");
			}
			return View("Rigester");
		}

		public IActionResult Login()
		{
			ViewBag.error = HttpContext.Session.GetString("errorlogin");

			return View();
		}
		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> Login([Bind("Username,Password")] UserLogin userLogin)
		{
			var auth = _context.UserLogins.Where(x => x.Username == userLogin.Username && x.Password == userLogin.Password).SingleOrDefault();


			if (auth != null)
			{

				//admin

				//customer


				switch (auth.Roleid)
				{
					case 1:
						HttpContext.Session.Remove("errorlogin");
						//HttpContext.Session.SetString("Adminname", auth.Username);
						auth.Lastlogin = DateTime.Now;
						_context.Update(auth);
						await _context.SaveChangesAsync();
						//return RedirectToAction("Dashpord", "Admin");
						return Redirect("~/Admin/Dashpord");

					case 2:
						HttpContext.Session.Remove("errorlogin");

						var customer = _context.Users.Where(x => x.Id == auth.Userid).SingleOrDefault();

						auth.Lastlogin = DateTime.Now;
						_context.Update(auth);
						await _context.SaveChangesAsync();
						customer.Onlinee = "on";
						_context.Update(customer);
						await _context.SaveChangesAsync();
						HttpContext.Session.SetInt32("UserId", (int)auth.Userid);
						//ViewBag.username = HttpContext.Session.GetInt32("UserId");
						return Redirect("~/Home/Index");

				}
			}

			HttpContext.Session.SetString("errorlogin", "Faild,error information");

			return Redirect("~/RigesterAndLogin/Login");


		}

		public IActionResult logout()
		{
			var user = HttpContext.Session.GetInt32("UserId");
			var customer = _context.Users.Where(x => x.Id == user).SingleOrDefault();
			//customer.Onlinee = "of";
			//         _context.Update(customer);
			//          _context.SaveChangesAsync();
			//         return RedirectToAction("login");
			return View(customer);
		}
		[HttpPost, ActionName("logout")]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> logout(decimal id)
		{

			var customer = _context.Users.Where(x => x.Id == id).SingleOrDefault();
			customer.Onlinee = "of";
			HttpContext.Session.Remove("UserId");
			_context.Update(customer);
			_context.SaveChangesAsync();
			return RedirectToAction("login");
		}
        public IActionResult Forget()
        {

            return View();
        }
        public IActionResult Forgetbyemail()
		{

			return View();
		}
		[HttpPost]
		public async Task<IActionResult> Forgetbyemail(ForgetPasswordAddByMe model)
		{
			if (!ModelState.IsValid)
				return View("Forget", model);

			// تحقق من وجود المستخدم
			var user = await _context.Users.Where(u=>u.Email==model.Email).SingleOrDefaultAsync();
			if (user == null)
			{
				//تستحدم لتخزين الاخطاء نضع اسم الخطأ في اول دبل كوتيشن ورسالة الخطأ في الثانية 
				//اذ لم نضه اسم للخطأ ننادي عليه ب 
				// asp-validation-sammary="All"
				ModelState.AddModelError("Email", "email not found");
				return View("Forget", model);
			}

			// توليد رمز تحقق
			var code = new Random().Next(100000, 999999).ToString();

			// تخزين الرمز مؤقتاً
			//tempData تشبه الجلسات سكشن ولكن الفرق انها تحدف بعد القرأة مباشرة بشكل الي عكس 
			//الجلسات وتعتبر مناسبة لتخلزين الرساءل المؤقتة مثل اشعار نجاح
			TempData["ResetCode"] = code;
			TempData["Email"] = model.Email;

			// إرسال الرمز بالإيميل ← (الخطوة 4 أدناه 👇)
			await _emailSender.SendEmailAsync(model.Email, "رمز إعادة تعيين كلمة المرور", $"اهلا بك في متجرنا ,رمزك هو : {code}");

			return RedirectToAction("VerifyCode");
		}

		public IActionResult ForgetbyPhonenumber()
		{

			return View();
		}
		[HttpPost]
		public async Task<IActionResult> ForgetbyPhonenumber(ForgetPasswordAddByMe model)
		{
			if (!ModelState.IsValid)
				return View("Forget", model);

			// تحقق من وجود المستخدم
			var user = await _context.Users.Where(u => u.Phonenumber == model.PhoneNumber).SingleOrDefaultAsync();
			if (user == null)
			{
				//تستحدم لتخزين الاخطاء نضع اسم الخطأ في اول دبل كوتيشن ورسالة الخطأ في الثانية 
				//اذ لم نضه اسم للخطأ ننادي عليه ب 
				// asp-validation-sammary="All"
				ModelState.AddModelError("PhoneNumber", "PhoneNumber not found");
				return View("Forget", model);
			}

			// توليد رمز تحقق
			var code = new Random().Next(100000, 999999).ToString();

			// تخزين الرمز مؤقتاً
			//tempData تشبه الجلسات سكشن ولكن الفرق انها تحدف بعد القرأة مباشرة بشكل الي عكس 
			//الجلسات وتعتبر مناسبة لتخلزين الرساءل المؤقتة مثل اشعار نجاح
			TempData["ResetCode"] = code;
			TempData["PhoneNumber"] = model.PhoneNumber;

			// إرسال الرمز بالإيميل ← (الخطوة 4 أدناه 👇)
		//await _smsSender.SendSmsAsync(model.PhoneNumber, "رمز إعادة تعيين كلمة المرور", $"اهلا بك في متجرنا ,رمزك هو : {code}");
			return RedirectToAction("VerifyCode");
		}
		public IActionResult VerifyCode()
		{

			return View();
		}
		[HttpPost]
		public IActionResult VerifyCode(VerifyCode model)
		{
			var expectedCode = TempData["ResetCode"] as string;
			var email = TempData["Email"] as string;

			if (model.Code == expectedCode)
			{
				TempData["Email"] = email; // نعيد تخزينه للخطوة التالية
				return RedirectToAction("ResetPassword");
			}

			ModelState.AddModelError("Code", "error code");
			TempData["ResetCode"] = expectedCode;
			return View();
		}


		public IActionResult ResetPassword()
		{

			return View();
		}
		[HttpPost]
		public IActionResult ResetPassword(string newpass)
		{
			int count = 0;
			if (string.IsNullOrEmpty(newpass)) {
				ModelState.AddModelError("newpass", "enter new pass");
				return View();
			}


			// 1. الطول على الأقل 8
			if (newpass.Length < 8) { 
				ModelState.AddModelError("newpass", "less than 8 char");
				count++;
			}


			// 2. على الأقل 5 حروف
			int letterCount = newpass.Count(char.IsLetter);
			if (letterCount < 5) { 
				ModelState.AddModelError("", "contains less than 5 char");
				count++;
			}


			// 3. أول حرف يجب أن يكون حرف كبير
			if (!char.IsUpper(newpass[0])) { 
				ModelState.AddModelError("newpass", "started not capital");
				count++;
			}

			// 4. يجب أن تحتوي على حرف صغير واحد على الأقل
			if (!newpass.Any(char.IsLower))
			{
				ModelState.AddModelError("newpass", "no found any small char");
				count++;
			}


			// 5. يجب أن تحتوي على رقم
			if (!newpass.Any(char.IsDigit)) { 
				ModelState.AddModelError("newpass", "no fount any number");
				count++;
			}

			// 6. يجب أن تحتوي على رمز خاص (غير حرف أو رقم)
			if (!newpass.Any(ch => !char.IsLetterOrDigit(ch))) { 
				ModelState.AddModelError("newpass", "no fount any sembol");
				count++;
			}

			if (count > 0) { return View(); } else {
				var email = TempData["Email"] as string;
				var user = _context.Users.Where(u => u.Email == email).FirstOrDefault();
				var userlogin = _context.UserLogins.Where(ul => ul.Userid == user.Id).FirstOrDefault();
				userlogin.Password = newpass;
				_context.UserLogins.Update(userlogin);
				_context.SaveChanges();
				return RedirectToAction("Login");
			}

			
		}
	}

	

}