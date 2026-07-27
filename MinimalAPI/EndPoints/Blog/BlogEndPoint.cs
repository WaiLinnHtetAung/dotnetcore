
namespace MinimalAPI.EndPoints.Blog;

public static class BlogEndPoint
{
    public static void MapBlogEndPoints(this IEndpointRouteBuilder app)
    {
        app.MapGet("/blogs", () =>
        {
            AppDbContext dbContext = new AppDbContext();
            var blogs = dbContext.TblBlogs.AsNoTracking().ToList();
            return Results.Ok(blogs);
        })
        .WithName("GetBlogs");

        app.MapGet("/blogs/{id}", (int id) =>
        {
            AppDbContext dbContext = new AppDbContext();
            var blog = dbContext.TblBlogs.AsNoTracking().FirstOrDefault(b => b.BlogId == id);
            if (blog == null)
            {
                return Results.NotFound();
            }
            return Results.Ok(blog);
        })
        .WithName("GetBlogById");

        app.MapPost("/blogs", (TblBlog blog) =>
        {
            AppDbContext dbContext = new AppDbContext();
            dbContext.TblBlogs.Add(blog);
            dbContext.SaveChanges();
            return Results.Created($"/blogs/{blog.BlogId}", blog);
        }).WithName("CreateBlog");

        app.MapPut("/blogs/{id}", (int id, TblBlog updatedBlog) =>
        {
            AppDbContext dbContext = new AppDbContext();
            var blog = dbContext.TblBlogs.FirstOrDefault(b => b.BlogId == id);
            if (blog == null)
            {
                return Results.NotFound();
            }
            blog.BlogTitle = updatedBlog.BlogTitle;
            blog.BlogAuthor = updatedBlog.BlogAuthor;
            blog.BlogContent = updatedBlog.BlogContent;

            dbContext.SaveChanges();
            return Results.NoContent();
        }).WithName("UpdateBlog");

        app.MapDelete("/blogs/{id}", (int id) =>
        {
            AppDbContext dbContext = new AppDbContext();
            var blog = dbContext.TblBlogs.FirstOrDefault(b => b.BlogId == id);
            if (blog == null)
            {
                return Results.NotFound();
            }
            
            dbContext.TblBlogs.Remove(blog);
            dbContext.SaveChanges();
            return Results.NoContent();
        }).WithName("DeleteBlog");
    }
}
