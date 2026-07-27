using System.Data;
using Database.Models;
using Microsoft.EntityFrameworkCore;

namespace Domain.Features.Blog;

public class BlogsService
{
    private readonly AppDbContext _db = new AppDbContext();

    public List<TblBlog> GetBlogs()
    {
        return _db.TblBlogs.AsNoTracking().ToList();
    }

    public TblBlog GetBlog(int id)
    {
        var blog = _db.TblBlogs.AsNoTracking().FirstOrDefault(b => b.BlogId == id);
        return blog;
    }

    public TblBlog CreateBlog(TblBlog blog)
    {
        _db.TblBlogs.Add(blog);
        _db.SaveChanges();
        return blog;
    }

    public TblBlog UpdateBlog(int id, TblBlog blog)
    {
        var existingBlog = _db.TblBlogs.AsNoTracking().FirstOrDefault(b => b.BlogId == id);
        if (existingBlog is null) return null;

        existingBlog.BlogTitle = blog.BlogTitle;
        existingBlog.BlogAuthor = blog.BlogAuthor;
        existingBlog.BlogContent = blog.BlogContent;

        _db.TblBlogs.Update(existingBlog);
        _db.SaveChanges();

        return existingBlog;
    }

    public TblBlog PatchBlog(int id, TblBlog blog)
    {
        var existingBlog = _db.TblBlogs.AsNoTracking().FirstOrDefault(b => b.BlogId == id);
        if (existingBlog is null) return null;

        if (!string.IsNullOrEmpty(blog.BlogTitle))
            existingBlog.BlogTitle = blog.BlogTitle;

        if (!string.IsNullOrEmpty(blog.BlogAuthor))
            existingBlog.BlogAuthor = blog.BlogAuthor;

        if (!string.IsNullOrEmpty(blog.BlogContent))
            existingBlog.BlogContent = blog.BlogContent;

        _db.TblBlogs.Update(existingBlog);
        _db.SaveChanges();

        return existingBlog;
    }

    public bool DeleteBlog(int id)
    {
        var blog = _db.TblBlogs.AsNoTracking().FirstOrDefault(b => b.BlogId == id);
        if (blog is null) return false;

        _db.TblBlogs.Remove(blog);
        int result = _db.SaveChanges();

        return result > 0;
    }
}
