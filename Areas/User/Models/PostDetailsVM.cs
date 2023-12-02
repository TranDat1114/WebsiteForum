using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

using WebsiteForum.Models;

namespace WebsiteForum.Areas.User.Models
{
    public class PostDetailsVM
    {
        public Reply Reply { get; set; } = new();

        [ValidateNever]
        public Post Post { get; set; } = new();
        [ValidateNever]
        public List<Reply> Replies { get; set; } = [];
    }
}
