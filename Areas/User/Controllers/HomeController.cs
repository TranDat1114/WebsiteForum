using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

using WebsiteForum.Areas.User.Models;
using WebsiteForum.Data;

namespace WebsiteForum.Areas.User.Controllers
{
    [Area("User")]

    public class HomeController(ApplicationDbContext db) : Controller
    {
        private readonly ApplicationDbContext _db = db;
        public IActionResult Index()
        {
            var homeVM = new HomeVM()
            {
                Posts = [.. _db.Posts.Include(p => p.Topic).Include(p => p.User)],
                Topics = [.. _db.Topics]
            };


            return View(homeVM);
        }

        public IActionResult PostDetails(int id)
        {
            PostDetailsVM postDetailsVM = new PostDetailsVM()
            {
                Post = _db.Posts.Include(p => p.Topic).Include(p => p.User).FirstOrDefault(p => p.PostId == id)!,
                Replies = [.. _db.Replies.Include(r => r.User).Where(r => r.PostId == id)]
            };

            return View(postDetailsVM);
        }

        public IActionResult PostsInTopic(int id)
        {
            var topic = _db.Topics.Include(p => p.Posts).ThenInclude(p => p.User).FirstOrDefault(p => p.TopicId == id);
            return View(topic);
        }
    }
}
