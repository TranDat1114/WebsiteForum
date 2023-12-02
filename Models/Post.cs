using System.ComponentModel.DataAnnotations.Schema;

using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using Microsoft.EntityFrameworkCore;

namespace WebsiteForum.Models
{
    public class Post
    {
        public int PostId { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public int Views { get; set; } = 0;
        [ValidateNever]
        public DateTime UpdatedDate { get; set; }
        [ValidateNever]
        public DateTime CreatedDate { get; set; }
        public int TopicId { get; set; }
        [ForeignKey("TopicId")]
        [ValidateNever]
        public virtual Topic Topic { get; set; }
        public string UserId { get; set; }
        [ForeignKey("UserId")]
        [ValidateNever]
        [DeleteBehavior(DeleteBehavior.Cascade)]
        public virtual ApplicationUser ApplicationUser { get; set; }

        [ValidateNever]
        public virtual List<Reply> Replies { get; set; } = new List<Reply>();

    }
}
