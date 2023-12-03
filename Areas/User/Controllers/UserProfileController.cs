using System.Security.Claims;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

using WebsiteForum.Areas.User.Models;
using WebsiteForum.Data;
using WebsiteForum.Models;
using WebsiteForum.Shared;

namespace WebsiteForum.Areas.User.Controllers
{
    [Area("User")]
    public class UserProfileController(ApplicationDbContext db, IWebHostEnvironment webHost) : Controller
    {
        private ApplicationDbContext _db = db;

        private readonly IWebHostEnvironment _webHostEnvironment = webHost;

        public IActionResult Profile(string id)
        {
            var user = _db.ApplicationUsers.Find(id);

            if (user == null)
            {
                return NotFound();
            }
            else
            {
                var profileVM = new ProfileVM
                {
                    applicationUser = user,
                    posts = [.. _db.Posts.Where(p => p.ApplicationUser.Id == id && p.Status == SD.Status_Approved).Include(p => p.Topic).Include(p => p.Replies).Include(p => p.ApplicationUser)],
                };

                return View(profileVM);
            }
        }
    }
}