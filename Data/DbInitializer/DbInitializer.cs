using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

using WebsiteForum.Models;
using WebsiteForum.Shared;

namespace WebsiteForum.Data.DbInitializer
{
    public class DbInitializer : IDbInitializer
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly ApplicationDbContext _db;
        public DbInitializer(ApplicationDbContext applicationDbContext, RoleManager<IdentityRole> roleManager, UserManager<IdentityUser> userManager)
        {
            _db = applicationDbContext;
            _userManager = userManager;
            _roleManager = roleManager;
        }
        public void Initialize()
        {
            try
            {
                if (_db.Database.GetPendingMigrations().Any())
                {
                    _db.Database.Migrate();
                }
            }
            catch (Exception)
            {

            }


            if (!_roleManager.RoleExistsAsync(SD.Role_User).GetAwaiter().GetResult())
            {
                _roleManager.CreateAsync(new IdentityRole(SD.Role_User)).GetAwaiter().GetResult();
                _roleManager.CreateAsync(new IdentityRole(SD.Role_Admin)).GetAwaiter().GetResult();
                _roleManager.CreateAsync(new IdentityRole(SD.Role_Censer)).GetAwaiter().GetResult();

                //Tạo thêm một tài khoản Admin ban đầu để tránh việc không có ai để tạo người dùng
                _userManager.CreateAsync(new ApplicationUser()
                {
                    UserName = "admin@admin.com",
                    Email = "admin@admin.com",
                    PhoneNumber = "0985950723",
                    LockoutEnabled = false,
                }, @"Admin1114@").GetAwaiter().GetResult();

                ApplicationUser user = _db.ApplicationUsers.FirstOrDefault(p => p.Email == "admin@admin.com")!;

                _userManager.AddToRoleAsync(user, SD.Role_Admin).GetAwaiter().GetResult();

                _userManager.CreateAsync(new ApplicationUser()
                {
                    UserName = "jackandy249@gmail.com",
                    Email = "jackandy249@gmail.com",
                    PhoneNumber = "0985950723",
                    LockoutEnabled = false,
                }, @"Admin1114@").GetAwaiter().GetResult();

                ApplicationUser demo = _db.ApplicationUsers.FirstOrDefault(p => p.Email == "jackandy249@gmail.com")!;

                _userManager.AddToRoleAsync(demo, SD.Role_User).GetAwaiter().GetResult();


                var topics = new List<Topic>()
                {
                    new()
                    {
                        Name = "Informational",

                        Description = "I'm too lazy to write it down, I can't edit it anymore",

                    },
                    new()
                    {
                        Name = "Comics",
                          Description = "I'm too lazy to write it down, I can't edit it anymore",

                    },
                    new()
                    {
                        Name = "Entertainment",
                          Description = "These are websites that provide entertainment content, such as movie streaming websites, gaming websites, or music streaming websites.",

                    },
                    new()
                    {
                        Name = "Social-media",
                         Description = "These are websites that allow users to interact and connect with each other, such as Facebook, Twitter, or Instagram.",

                    },
                    new()
                    {
                        Name = "Blog",
                        Description = "These are personal or business websites that provide the latest posts and information on a specific topic.",

                    },
                    new()
                    {
                        Name = "Manga",
                        Description = "I'm too lazy to write it down, I can't edit it anymore",

                    },
                    new()
                    {
                        Name = "Tool",

                        Description = "These are websites that provide online tools or services for users, such as Google, Dropbox, or GitHub.",

                    },
                    new()
                    {
                        Name = "Learning",
                        Description = "These are websites that provide online courses or study materials, such as Coursera, edX, or Udemy.",
                    },
                    new()
                    {
                        Name = "Food",
                        Description = "I'm too lazy to write it down, I can't edit it anymore",

                    },
                    new()
                    {
                        Name = "Exhibition",
                        Description = "Phòng trưng bày",

                    },
                    new()
                    {
                        Name = "Travel",
                        Description = "I'm too lazy to write it down, I can't edit it anymore",
                    },
                    new()
                    {
                        Name = "Sports",
                        Description = "I'm too lazy to write it down, I can't edit it anymore",

                    },
                    new()
                    {
                        Name = "Business",
                        Description = "A huge list of the best business website templates is built to serve any company, from construction to business consulting and financial services. All templates are mobile-friendly and feature both one-page and multi-page setups. Whether you are bringing a fresh project or redesigning your current website, these templates have you covered. They are powerful enough to meet any firm and organization owner’s needs and requirements.",
                    }
                };
                _db.Topics.AddRangeAsync(topics).GetAwaiter().GetResult();
                _db.SaveChangesAsync().GetAwaiter().GetResult();

                var posts = new List<Post>()
                {
                    new()
                    {
                        Title = "Informational",
                        Content = "I'm too lazy to write it down, I can't edit it anymore",
                        TopicId = 1,
                        UserId = user.Id,
                        CreatedDate = DateTime.Now,
                        UpdatedDate = DateTime.Now,
                        PostId=0,
                    },
                    new()
                    {
                        Title = "Comics",
                        Content = "I'm too lazy to write it down, I can't edit it anymore",
                        TopicId = 2,
                        UserId = user.Id,
                        CreatedDate = DateTime.Now,
                        UpdatedDate = DateTime.Now,
                        PostId=0,
                    },
                    new()
                    {
                        Title = "Entertainment",
                        Content = "These are websites that provide entertainment content, such as movie streaming websites, gaming websites, or music streaming websites.",
                        TopicId = 3,
                        UserId = user.Id,
                        CreatedDate = DateTime.Now,
                        UpdatedDate = DateTime.Now,
                        PostId=0,
                    },
                    new()
                    {
                        Title = "Social-media",
                        Content = "These are websites that allow users to interact and connect with each other, such as Facebook, Twitter, or Instagram.",
                        TopicId = 4,
                        UserId = user.Id,
                        CreatedDate = DateTime.Now,
                        UpdatedDate = DateTime.Now,
                        PostId=0,
                    },
                    new()
                    {
                        Title = "Social-media",
                        Content = "These are websites that allow users to interact and connect with each other, such as Facebook, Twitter, or Instagram.",
                        TopicId = 4,
                        UserId = demo.Id,
                        CreatedDate = DateTime.Now,
                        UpdatedDate = DateTime.Now,
                        PostId=0,
                    },
                    new()
                    {
                        Title = "Blog",
                        Content = "These are personal or business websites that provide the latest posts and information on a specific topic.",
                        TopicId = 5,
                        UserId = demo.Id,
                        CreatedDate = DateTime.Now,
                        UpdatedDate = DateTime.Now,
                        PostId=0,
                    },
                    new()
                    {
                        Title = "Manga",
                        Content = "I'm too lazy to write it down, I can't edit it anymore",
                        TopicId = 6,
                        UserId = demo.Id,
                        CreatedDate = DateTime.Now,
                        UpdatedDate = DateTime.Now,
                        PostId=0,
                    },
                    new()
                    {
                        Title = "Tool",
                        Content = "These are websites that provide online tools or services for users, such as Google, Dropbox, or GitHub.",
                        TopicId = 7,
                        UserId = user.Id,
                        CreatedDate = DateTime.Now,
                        UpdatedDate = DateTime.Now,
                        PostId=0,
                    }
                };

                _db.Posts.AddRangeAsync(posts).GetAwaiter().GetResult();
                _db.SaveChangesAsync().GetAwaiter().GetResult();

                var replies = new List<Reply>()
                {
                    new()
                    {
                        Content="I'm too lazy to write it down, I can't edit it anymore",
                        PostId = 1,
                        UserId = user.Id,
                        CreatedDate = DateTime.Now,
                        UpdatedDate = DateTime.Now,
                    },
                    new()
                    {
                        Content="I'm too lazy to write it down, I can't edit it anymore",
                        PostId = 2,
                        UserId = user.Id,
                        CreatedDate = DateTime.Now,
                        UpdatedDate = DateTime.Now,
                    },
                    new()
                    {
                        Content="I'm too lazy to write it down, I can't edit it anymore",
                        PostId = 3,
                        UserId = demo.Id,
                        CreatedDate = DateTime.Now,
                        UpdatedDate = DateTime.Now,
                    },
                };
                _db.Replies.AddRangeAsync(replies).GetAwaiter().GetResult();
                _db.SaveChangesAsync().GetAwaiter().GetResult();

                return;
            }
        }
    }
}