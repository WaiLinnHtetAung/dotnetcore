using System;
using Database.Models;
using Domain.Features.Blog;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;


namespace WebAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class BlogServiceController : ControllerBase
{
    private readonly BlogsService _blogService;
    public BlogServiceController(BlogsService service)
    {
        _blogService = service;
    }
    
    [HttpGet]
    public IActionResult GetBlogs()
    {
        var blogs = _blogService.GetBlogs();

        return Ok(blogs);
    }

    [HttpGet("{id}")]
    public IActionResult GetBlog(int id)
    {
        var blog = _blogService.GetBlog(id);

        if (blog == null) return NotFound();

        return Ok(blog);
    }

    [HttpPost]
    public IActionResult CreateBlog(TblBlog blog)
    {
        var createdBlog = _blogService.CreateBlog(blog);
        return Ok(createdBlog);
    }

    [HttpPut("{id}")]
    public IActionResult UpdateBlog(int id, TblBlog blog)
    {
        var item = _blogService.UpdateBlog(id, blog);
        if (item is null) return NotFound();
        return Ok(item);
    }

    [HttpPatch("{id}")]
    public IActionResult PatchBlog(int id, TblBlog blog)
    {
        var item = _blogService.PatchBlog(id, blog);
        if (item is null) return NotFound();
        return Ok(item);
    }

    [HttpDelete("{id}")]
    public IActionResult DeleteBlog(int id)
    {
        var item = _blogService.DeleteBlog(id);
        if (!item) return NotFound();
        return Ok();
    }
}
