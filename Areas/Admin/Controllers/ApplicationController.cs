using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

using WebsiteForum.Areas.User.Models;
using WebsiteForum.Data;

namespace WebsiteForum.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ApplicationController(ApplicationDbContext dbContext) : Controller
    {
        private readonly ApplicationDbContext _context = dbContext;
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.ApplicationUsers;

            var applicationUserVM = new ApplicationUserVM
            {
                ApplicationUsers = await applicationDbContext.ToListAsync()
            };  

            return View(applicationUserVM);
        }
        
        public async Task<IActionResult> Ban(string? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var post = await _context.ApplicationUsers
                        .FirstOrDefaultAsync(m => m.Id == id);
            if (post == null)
            {
                return NotFound();
            }

            return View(post);
        }
        public async Task<IActionResult> UnBan(string? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var post = await _context.ApplicationUsers
                        .FirstOrDefaultAsync(m => m.Id == id);
            if (post == null)
            {
                return NotFound();
            }

            return View(post);
        }

        [HttpPost, ActionName("Ban")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> BanConfirmed(string id)
        {
            var post = await _context.ApplicationUsers.FindAsync(id);
            if (post != null)
            {
                post.IsBan = true;
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        [HttpPost, ActionName("UnBan")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> UnBanConfirmed(string id)
        {
            var post = await _context.ApplicationUsers.FindAsync(id);
            if (post != null)
            {
                post.IsBan = false;
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
    }
}
