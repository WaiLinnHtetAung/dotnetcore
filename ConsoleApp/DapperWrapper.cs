using System.Data.SqlClient;
using System.Data;
using Dapper;
using ConsoleApp.Models;

namespace ConsoleApp
{
    public class DapperWrapper
    {
        private readonly string _connectionString = "Data Source=.;Initial Catalog=dotnet;User ID=sa;Password=saPassword1234";
       
        public void Read()
        {
            using(IDbConnection db = new SqlConnection(_connectionString))
            {
                string query = "select * from Tbl_Blog";
                var lst = db.Query<BlogDataModel>(query).ToList();

                foreach (var item in lst)
                {
                    Console.WriteLine(item.BlogId);
                    Console.WriteLine(item.BlogTitle);
                    Console.WriteLine(item.BlogAuthor);
                    Console.WriteLine(item.BlogContent);
                    Console.WriteLine("-----------------------------");
                }
            }
        }

        public void Create(string blogTitle, string blogAuthor, string blogContent)
        {

            using(IDbConnection db = new SqlConnection(_connectionString))
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
                int result = db.Execute(query, parameters);

                Console.WriteLine(result > 0 ? "Blog created successfully." : "Failed to create blog.");
            }
        }

        public void Edit(int blogId)
        {
            using(IDbConnection db = new SqlConnection(_connectionString))
            {
                string query = "select * from Tbl_Blog where BlogId = @blogId";
                var parameters = new { blogId };

                var blog = db.Query<BlogDataModel>(query, parameters).FirstOrDefault();

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
        }
        
    }
}