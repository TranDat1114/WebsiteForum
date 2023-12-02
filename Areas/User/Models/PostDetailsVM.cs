using WebsiteForum.Models;

namespace WebsiteForum.Areas.User.Models
{
    public class PostDetailsVM
    {
        public Post Post { get; set; } = new();
        public List<Reply> Replies { get; set; } = [];
    }
}
