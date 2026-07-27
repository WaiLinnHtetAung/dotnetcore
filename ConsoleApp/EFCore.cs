
using ConsoleApp.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace ConsoleApp
{
    public class EFCore
    {
        public void Read()
        {
            AppDbContext dbContext = new AppDbContext();
            var blogs = dbContext.Blogs.ToList();

            foreach (var blog in blogs)
            {
                Console.WriteLine(blog.BlogId);
                Console.WriteLine(blog.BlogTitle);
                Console.WriteLine(blog.BlogAuthor);
                Console.WriteLine(blog.BlogContent);
                Console.WriteLine("-----------------------------");
            }
        }

        public void Create(string blogTitle, string blogAuthor, string blogContent)
        {
            AppDbContext dbContext = new AppDbContext();

            BlogDataModel newBlog = new BlogDataModel
            {
                BlogTitle = blogTitle,
                BlogAuthor = blogAuthor,
                BlogContent = blogContent
            };

            dbContext.Blogs.Add(newBlog);
            int result = dbContext.SaveChanges();

            Console.WriteLine(result > 0 ? "Blog created successfully." : "Failed to create blog.");
        }

        public void Edit(int blogId)
        {
            AppDbContext dbContext = new AppDbContext();
            var blog = dbContext.Blogs.FirstOrDefault(b => b.BlogId == blogId);

            if (blog != null)
            {
                Console.WriteLine("Blog found:");
                Console.WriteLine(blog.BlogId);
                Console.WriteLine(blog.BlogTitle);
                Console.WriteLine(blog.BlogAuthor);
                Console.WriteLine(blog.BlogContent);
            }
            else
            {
                Console.WriteLine("Blog not found.");
            }
        }

        public void Update(int blogId, string newTitle, string newAuthor, string newContent)
        {
            AppDbContext dbContext = new AppDbContext();
            var blog = dbContext.Blogs.AsNoTracking().FirstOrDefault(b => b.BlogId == blogId);

            if (blog != null)
            {
                if (!string.IsNullOrEmpty(newTitle)) blog.BlogTitle = newTitle;
                if (!string.IsNullOrEmpty(newAuthor)) blog.BlogAuthor = newAuthor;
                if (!string.IsNullOrEmpty(newContent)) blog.BlogContent = newContent;

                dbContext.Blogs.Update(blog);
                int result = dbContext.SaveChanges();

                Console.WriteLine(result > 0 ? "Blog updated successfully." : "Failed to update blog.");
            }
            else
            {
                Console.WriteLine("Blog not found.");
            }
        }

        public void Delete(int blogId)
        {
            AppDbContext dbContext = new AppDbContext();
            var blog = dbContext.Blogs.FirstOrDefault(b => b.BlogId == blogId);

            if (blog != null)
            {
                dbContext.Blogs.Remove(blog);
                int result = dbContext.SaveChanges();

                Console.WriteLine(result > 0 ? "Blog deleted successfully." : "Failed to delete blog.");
            }
            else
            {
                Console.WriteLine("Blog not found.");
            }
        }
    }
}