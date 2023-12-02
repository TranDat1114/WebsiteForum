using Microsoft.AspNetCore.Mvc;

namespace WebsiteForum.Areas.User.Controllers
{
    [Area("User")]
    public class UserProfileController : Controller
    {
        public IActionResult UserDetails(int id)
        {
            return View();
        }
    }
}
