using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

using SellWebsite.Utility.IdentityHandler;

using WebsiteForum.Data;
using WebsiteForum.Data.DbInitializer;
using WebsiteForum.Models;

namespace WebsiteForum
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            var connectionString = builder.Configuration.GetConnectionString("DefaultConnection") ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");
            builder.Services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(connectionString));
            builder.Services.AddDatabaseDeveloperPageExceptionFilter();

            builder.Services.AddSession(op =>
            {
                op.IdleTimeout = TimeSpan.FromMinutes(60);
                op.Cookie.HttpOnly = true;
                op.Cookie.IsEssential = true;
            });
            builder.Services.AddIdentity<IdentityUser, IdentityRole>().AddEntityFrameworkStores<ApplicationDbContext>().AddDefaultTokenProviders();
            //Thêm cấu hình cho cookie và nhớ phải nằm dưới AddIdentity
            builder.Services.ConfigureApplicationCookie(options =>
            {
                options.LoginPath = "/Identity/Account/Login";
                options.LogoutPath = "/Identity/Account/Logout";
                options.AccessDeniedPath = "/Identity/Account/AccessDenied";
            });
            builder.Services.AddScoped<IDbInitializer, DbInitializer>();
            builder.Services.AddScoped<IEmailSender, EmailSender>();
            builder.Services.AddRazorPages();

            //builder.Services.AddControllersWithViews();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseMigrationsEndPoint();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthentication();

            app.UseAuthorization();

            SeedDatas();

            app.UseSession();

            app.MapRazorPages();
            app.MapControllerRoute(
                 name: "home",
                 pattern: "{area=User}/{controller=Home}/{action=Index}");

            app.Run();

            void SeedDatas()
            {
                using var scope = app.Services.CreateScope();
                var dbInitilizer = scope.ServiceProvider.GetRequiredService<IDbInitializer>();
                dbInitilizer.Initialize();
            }
        }


    }
}
