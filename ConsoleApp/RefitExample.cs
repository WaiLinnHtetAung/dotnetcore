using System;
using Refit;

namespace ConsoleApp;

public class RefitExample
{
    public async Task Read()
    {
        var blogApi = RestService.For<IBlogApi>("http://localhost:5243");
        var blogs = await blogApi.GetBlogs();

        foreach (var blog in blogs)
        {
            Console.WriteLine(blog.BlogId);
            Console.WriteLine(blog.BlogTitle);
            Console.WriteLine(blog.BlogAuthor);
            Console.WriteLine(blog.BlogContent);
            Console.WriteLine("-----------------------------");
        }
    }
}
