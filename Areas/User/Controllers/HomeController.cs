using System.Security.Claims;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

using WebsiteForum.Areas.User.Models;
using WebsiteForum.Data;
using WebsiteForum.Models;
using WebsiteForum.Shared;

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
                Posts = [.. _db.Posts.Where(p => p.Status == SD.Status_Approved).Include(p => p.Topic).Include(p => p.Replies).Include(p => p.ApplicationUser)],
                Topics = [.. _db.Topics]
            };

            return View(homeVM);
        }

        [Authorize]
        public IActionResult CreateNewPost(int? id)
        {
            var createNewPostVM = new CreateNewPostVM()
            {
                Post = new Post()
                {
                    PostId = id ?? 0
                },
                Topics = [.. _db.Topics]
            };

            return View(createNewPostVM);
        }

        [HttpPost]
        [Authorize]
        public IActionResult CreateNewPost(CreateNewPostVM createNewPostVM)
        {
            if (!ModelState.IsValid)
            {
                createNewPostVM.Topics = [.. _db.Topics];
                return View(createNewPostVM);
            }
            else
            {
                createNewPostVM.Post.CreatedDate = DateTime.Now;
                createNewPostVM.Post.UpdatedDate = DateTime.Now;

                _db.Posts.Add(createNewPostVM.Post);
                _db.SaveChanges();
                return RedirectToAction(nameof(PostDetails), new { id = createNewPostVM.Post.PostId });
            }
        }

        public IActionResult PostDetails(int id)
        {
            _db.Posts.FirstOrDefault(p => p.PostId == id)!.Views++;
            _db.SaveChanges();
            PostDetailsVM postDetailsVM = new()
            {
                Replies = [.. _db.Replies.Where(p => p.PostId == id).Include(p => p.ApplicationUser)],
                Post = _db.Posts.Include(p => p.Topic).Include(p => p.ApplicationUser).FirstOrDefault(p => p.PostId == id),
            };

            return View(postDetailsVM);
        }

        public IActionResult PostsInTopic(int id)
        {
            var topic = _db.Topics.Include(p => p.Posts).ThenInclude(p => p.ApplicationUser).FirstOrDefault(p => p.TopicId == id);
            return View(topic);
        }

        [HttpPost]
        public IActionResult ReplyPost(Reply reply)
        {
            ClaimsIdentity? claimIdentity = (ClaimsIdentity)User.Identity;
            if (claimIdentity == null)
            {
                return RedirectToAction(nameof(PostDetails));
            }

            reply.UserId = claimIdentity.FindFirst(ClaimTypes.NameIdentifier)!.Value;
            reply.CreatedDate = DateTime.Now;
            reply.UpdatedDate = DateTime.Now;

            _db.Replies.Add(reply);
            _db.SaveChanges();
            return RedirectToAction(nameof(PostDetails), new { id = reply.PostId });

        }
    }
}
