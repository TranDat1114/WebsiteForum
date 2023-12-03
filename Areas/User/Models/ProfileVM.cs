using WebsiteForum.Models;

namespace WebsiteForum.Areas.User.Models
{
    public class ProfileVM
    {
        public ApplicationUser applicationUser { get; set; }
        public List<Post> posts { get; set; }
    }
}
