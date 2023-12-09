using System.ComponentModel.DataAnnotations.Schema;

using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace WebsiteForum.Models
{
    public class ApplicationUser : IdentityUser
    {
        [NotMapped]
        public string Role { get; set; }
        [ValidateNever]
        public DateOnly JoinDate { get; set; } = DateOnly.FromDateTime(DateTime.Now);
        public string ProfilePicture { get; set; } = "default.png";

        public bool IsBan { get; set; } = false;

        [ValidateNever]
        public virtual List<Post> Posts { get; set; } = new List<Post>();
        [ValidateNever]
        public virtual List<Reply> Replies { get; set; } = new List<Reply>();
    }
}
