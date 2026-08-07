using Database.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BlogsController : ControllerBase
    {
        private readonly AppDbContext _dbContext;

        public BlogsController(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        [HttpGet]
        public IActionResult GetBlogs()
        {
            var blogs = _dbContext.TblBlogs.AsNoTracking().ToList();
            
            return Ok(blogs);
        }

        [HttpGet("{id}")]
        public IActionResult GetBlog(int id)
        {
            var blog = _dbContext.TblBlogs.AsNoTracking().FirstOrDefault(b => b.BlogId == id);
            
            if (blog == null) return NotFound();

            return Ok(blog);
        }

        [HttpPost]
        public IActionResult CreateBlog(TblBlog blog)
        {
            _dbContext.TblBlogs.Add(blog);
            _dbContext.SaveChanges();
            return Ok(blog);
        }   

        [HttpPut("{id}")]
        public IActionResult UpdateBlog(int id, TblBlog blog)
        {
            var existingBlog = _dbContext.TblBlogs.AsNoTracking().FirstOrDefault(b => b.BlogId == id);
            if (existingBlog == null) return NotFound();

            existingBlog.BlogTitle = blog.BlogTitle;
            existingBlog.BlogAuthor = blog.BlogAuthor;
            existingBlog.BlogContent = blog.BlogContent;

            _dbContext.TblBlogs.Update(existingBlog);
            _dbContext.SaveChanges();

            return Ok(existingBlog);
        }

        [HttpPatch("{id}")]
        public IActionResult PatchBlog(int id, TblBlog blog)
        {
            var existingBlog = _dbContext.TblBlogs.AsNoTracking().FirstOrDefault(b => b.BlogId == id);
            if (existingBlog == null) return NotFound();

            if (!string.IsNullOrEmpty(blog.BlogTitle))
                existingBlog.BlogTitle = blog.BlogTitle;
            if (!string.IsNullOrEmpty(blog.BlogAuthor))
                existingBlog.BlogAuthor = blog.BlogAuthor;
            if (!string.IsNullOrEmpty(blog.BlogContent))
                existingBlog.BlogContent = blog.BlogContent;

            _dbContext.TblBlogs.Update(existingBlog);
            _dbContext.SaveChanges();

            return Ok(existingBlog);
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteBlog(int id)
        {
            var existingBlog = _dbContext.TblBlogs.AsNoTracking().FirstOrDefault(b => b.BlogId == id);
            if (existingBlog == null) return NotFound();

            _dbContext.TblBlogs.Remove(existingBlog);
            _dbContext.SaveChanges();

            return Ok();
        }
    }
}
