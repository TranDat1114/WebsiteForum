using Microsoft.AspNetCore.Mvc;

namespace WebsiteForum.Areas.Admin.Controllers
{
    public class ApplicationController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
