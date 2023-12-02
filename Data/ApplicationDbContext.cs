using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

using WebsiteForum.Models;

namespace WebsiteForum.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        public DbSet<ApplicationUser> ApplicationUsers  { get; set; }
        public DbSet<Post> Posts { get; set; }
        public DbSet<Reply> Replies { get; set; }
        public DbSet<Topic> Topics { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.Entity<Post>()
                .HasOne(p => p.Topic)
                .WithMany(t => t.Posts)
                .HasForeignKey(p => p.TopicId);
            builder.Entity<Reply>()
                .HasOne(r => r.Post)
                .WithMany(p => p.Replies)
                .HasForeignKey(r => r.PostId);
            builder.Entity<Reply>()
                .HasOne(r => r.User)
                .WithMany(u => u.Replies)
                .HasForeignKey(r => r.UserId);
            builder.Entity<Post>()
                .HasOne(p => p.User)
                .WithMany(u => u.Posts)
                .HasForeignKey(p => p.UserId);

            //builder.Entity<Topic>().HasData(
            //    new Topic
            //    {
            //        TopicId = 1,
            //        Name = "First Topic",
            //        Description = "This is the first topic"
            //    },
            //    new Topic
            //    {
            //        TopicId = 2,
            //        Name = "Second Topic",
            //        Description = "This is the second topic"
            //    }
            //    );

            //builder.Entity<Post>().HasData(
            //    new Post
            //    {
            //        PostId = 1,
            //        Title = "First Post",
            //        Content = "This is the first post",
            //        TopicId = 1,
            //        UserId = "1"
            //    },
            //    new Post
            //    {
            //        PostId = 2,
            //        Title = "Second Post",
            //        Content = "This is the second post",
            //        TopicId = 1,
            //        UserId = "2"
            //    }
            //    );

            //builder.Entity<Reply>().HasData(
            //    new Reply
            //    {
            //        ReplyId = 1,
            //        Content = "This is the first reply",
            //        PostId = 1,
            //        UserId = "1"
            //    }
            //    );

        }
    }
}
