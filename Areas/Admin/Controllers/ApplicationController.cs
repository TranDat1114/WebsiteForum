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
        
        public async Task<IActionResult> Delete(string? id)
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

        // POST: Admin/Posts/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            var post = await _context.ApplicationUsers.FindAsync(id);
            if (post != null)
            {
                _context.ApplicationUsers.Remove(post);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
    }
}
