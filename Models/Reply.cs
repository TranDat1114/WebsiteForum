using System.ComponentModel.DataAnnotations.Schema;

using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using Microsoft.EntityFrameworkCore;

namespace WebsiteForum.Models
{
    public class Reply
    {
        public int ReplyId { get; set; }
        public string? Content { get; set; }
        [ValidateNever]
        public DateTime UpdatedDate { get; set; } = DateTime.Now;
        [ValidateNever]
        public DateTime CreatedDate { get; set; } = DateTime.Now;
        public int PostId { get; set; }
        [ForeignKey("PostId")]
        [ValidateNever]
        public virtual Post Post { get; set; }
        public string UserId { get; set; }
        [ForeignKey("UserId")]
        [ValidateNever]
        [DeleteBehavior(DeleteBehavior.NoAction)]
        public virtual ApplicationUser ApplicationUser { get; set; }
    }
}
