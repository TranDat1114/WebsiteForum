using WebsiteForum.Models;

namespace WebsiteForum.Areas.User.Models
{
    public class HomeVM
    {
        public List<Post> Posts { get; set; } = [];
        public List<Topic> Topics { get; set; } = [];   
        
    }
}
