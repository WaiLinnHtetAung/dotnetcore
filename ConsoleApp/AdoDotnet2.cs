using System;
using System.Data;
using Shared;

namespace ConsoleApp;

internal class AdoDotnet2
{
    private readonly string _connectionString = "Server=localhost;Database=TestDB;User Id=sa;Password=your_password;";
    private readonly AdoDotNetService _adoDotNetService;

    public AdoDotnet2()
    {
        _adoDotNetService = new AdoDotNetService(_connectionString);
    }

    public void Read()
    {
        string query = @"SELECT [BlogId]
                        ,[BlogTitle]
                        ,[BlogAuthor]
                        ,[BlogContent]
                    FROM [dbo].[Tbl_Blog]";
        
        var dataTable = _adoDotNetService.Query(query);

        foreach (DataRow row in dataTable.Rows)
        {
            Console.WriteLine(row["BlogId"]);
            Console.WriteLine(row["BlogTitle"]);
            Console.WriteLine(row["BlogAuthor"]);
            Console.WriteLine(row["BlogContent"]);
            Console.WriteLine("-----------------------------");
        }
    }

    public void Edit()
    {
        Console.WriteLine("Blog Id: ");
        int blogId = Convert.ToInt32(Console.ReadLine());

        string query = @"SELECT [BlogId]
                                ,[BlogTitle]
                                ,[BlogAuthor]
                                ,[BlogContent]
                            FROM [dbo].[Tbl_Blog]
                            WHERE [BlogId] = @blogId";

        var dataTable = _adoDotNetService.Query(query, new SqlParameterModel("@blogId", blogId));

        if (dataTable.Rows.Count > 0)
        {
            DataRow row = dataTable.Rows[0];
            Console.WriteLine(row["BlogId"]);
            Console.WriteLine(row["BlogTitle"]);
            Console.WriteLine(row["BlogAuthor"]);
            Console.WriteLine(row["BlogContent"]);
        }
        else
        {
            Console.WriteLine("Blog post not found.");
        }
    }

    public void Create()
    {
        Console.WriteLine("Enter Blog Title:");
        string blogTitle = Console.ReadLine();
        Console.WriteLine("Enter Blog Author:");
        string blogAuthor = Console.ReadLine();
        Console.WriteLine("Enter Blog Content:");
        string blogContent = Console.ReadLine();

       
        string query = @"INSERT INTO [dbo].[Tbl_Blog]
                            ([BlogTitle]
                            ,[BlogAuthor]
                            ,[BlogContent])
                        VALUES
                            (@blogTitle
                            ,@blogAuthor
                            ,@blogContent)";

        int rowsAffected = _adoDotNetService.Execute(query,
            new SqlParameterModel("@blogTitle", blogTitle),
            new SqlParameterModel("@blogAuthor", blogAuthor),
            new SqlParameterModel("@blogContent", blogContent));

        Console.WriteLine(rowsAffected > 0 ? "Blog post inserted successfully." : "Failed to insert blog post.");
    }

    public void Update()
    {
        Console.WriteLine("Blog Id: ");
        int blogId = Convert.ToInt32(Console.ReadLine());
        Console.WriteLine("Enter new Blog Title:");
        string blogTitle = Console.ReadLine();
        Console.WriteLine("Enter new Blog Author:");
        string blogAuthor = Console.ReadLine();
        Console.WriteLine("Enter new Blog Content:");
        string blogContent = Console.ReadLine();


        string query = @"UPDATE [dbo].[Tbl_Blog]
                        SET [BlogTitle] = @blogTitle,
                            [BlogAuthor] = @blogAuthor,
                            [BlogContent] = @blogContent
                        WHERE [BlogId] = @blogId";

        int rowsAffected = _adoDotNetService.Execute(query,
            new SqlParameterModel("@blogId", blogId),
            new SqlParameterModel("@blogTitle", blogTitle),
            new SqlParameterModel("@blogAuthor", blogAuthor),
            new SqlParameterModel("@blogContent", blogContent));

        Console.WriteLine(rowsAffected > 0 ? "Blog post updated successfully." : "Failed to update blog post.");
    }
}
