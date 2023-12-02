using System.ComponentModel.DataAnnotations.Schema;

using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace WebsiteForum.Models
{
    public class Topic
    {
        public int TopicId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }

        [ValidateNever]
        public virtual List<Post> Posts { get; set; } = new List<Post>();
    }
}
