using Microsoft.EntityFrameworkCore;
using StoreNew.Models;
using Microsoft.AspNetCore.Mvc.Razor.RuntimeCompilation;
namespace StoreNew
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddControllersWithViews();
            //connectiob batabase
            builder.Services.AddDbContext<ModelContext>(options => options.UseOracle(builder.Configuration.GetConnectionString("DefaultConnection")));
            //runtimecompilation تعديل بدون رن جديد
            builder.Services.AddControllersWithViews().AddRazorRuntimeCompilation();
			//for login
			builder.Services.AddDistributedMemoryCache();
			builder.Services.AddSession(options => { options.IdleTimeout = TimeSpan.FromMinutes(60); });

            //builder.WebHost.UseUrls("http://0.0.0.0:5058");
            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            //use seesion
            app.UseSession();

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();
            
            app.UseAuthorization();

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");

            app.Run();
        }
    }
}