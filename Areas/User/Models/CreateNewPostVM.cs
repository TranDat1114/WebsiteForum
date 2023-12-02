using WebsiteForum.Models;

namespace WebsiteForum.Areas.User.Models
{
    public class CreateNewPostVM
    {
        public Post Post { get; set; } = new Post();
        public IEnumerable<Topic> Topics { get; set; } = new List<Topic>();
    }
}
