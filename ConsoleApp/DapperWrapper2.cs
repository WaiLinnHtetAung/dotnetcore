using System;
using ConsoleApp.Models;
using Shared;

namespace ConsoleApp;

internal class DapperWrapper2
{
    private readonly string _connectionString = "Server=localhost;Database=TestDB;User Id=sa;Password=YourStrong!Passw0rd;";
    private readonly DapperService _dapperService;

    public DapperWrapper2()
    {
        _dapperService = new DapperService(_connectionString);
    }

    public void Read()
    {
        string query = "select * from Tbl_Blog";
        var lst = _dapperService.Query<BlogDataModel>(query).ToList();

        foreach (var item in lst)
        {
            Console.WriteLine(item.BlogId);
            Console.WriteLine(item.BlogTitle);
            Console.WriteLine(item.BlogAuthor);
            Console.WriteLine(item.BlogContent);
            Console.WriteLine("-----------------------------");
        }
    }

    public void Edit(int blogId)
    {
        string query = "select * from Tbl_Blog where BlogId = @blogId";
        var parameters = new { blogId };

        var blog = _dapperService.QueryFirstOrDefault<BlogDataModel>(query, parameters);

        if (blog != null)
        {
            Console.WriteLine($"Editing Blog: {blog.BlogTitle}");
            // Perform edit operations here
        }
        else
        {
            Console.WriteLine("Blog not found.");
        }
    }

    public void Create(string blogTitle, string blogAuthor, string blogContent)
    {
        string query = @"INSERT INTO [dbo].[Tbl_Blog]
                        ([BlogTitle]
                        ,[BlogAuthor]
                        ,[BlogContent])
                    VALUES
                        (@blogTitle
                        ,@blogAuthor
                        ,@blogContent)";

        var parameters = new BlogDataModel { BlogTitle = blogTitle, BlogAuthor = blogAuthor, BlogContent = blogContent };
        int result = _dapperService.Execute(query, parameters);

        Console.WriteLine(result > 0 ? "Blog created successfully." : "Failed to create blog.");
    }
}
