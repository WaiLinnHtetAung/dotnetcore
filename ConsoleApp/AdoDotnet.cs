using System.Data;
using System.Data.SqlClient;

namespace ConsoleApp
{
    public class AdoDotnet
    {

        private readonly string _connectionString = "Data Source=.;Initial Catalog=dotnet;User ID=sa;Password=saPassword1234";
        public void Read()
        {
            SqlConnection connection = new SqlConnection(_connectionString);

            connection.Open();

            string query = @"SELECT [BlogId]
                                ,[BlogTitle]
                                ,[BlogAuthor]
                                ,[BlogContent]
                            FROM [dbo].[Tbl_Blog]";

            SqlCommand command = new SqlCommand(query, connection);
            SqlDataReader reader = command.ExecuteReader();

            while (reader.Read())
            {
                Console.WriteLine(reader["BlogId"]);
                Console.WriteLine(reader["BlogTitle"]);
                Console.WriteLine(reader["BlogAuthor"]);
                Console.WriteLine(reader["BlogContent"]);
                Console.WriteLine("-----------------------------");
            }

            connection.Close();
        }

        public void Create()
        {
            Console.WriteLine("Enter Blog Title:");
            string blogTitle = Console.ReadLine();
            Console.WriteLine("Enter Blog Author:");
            string blogAuthor = Console.ReadLine();
            Console.WriteLine("Enter Blog Content:");
            string blogContent = Console.ReadLine();

            SqlConnection connection = new SqlConnection(_connectionString);

            connection.Open();

            string query = @"INSERT INTO [dbo].[Tbl_Blog]
                                ([BlogTitle]
                                ,[BlogAuthor]
                                ,[BlogContent])
                            VALUES
                                (@blogTitle
                                ,@blogAuthor
                                ,@blogContent)";

            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@blogTitle", blogTitle);
            command.Parameters.AddWithValue("@blogAuthor", blogAuthor);
            command.Parameters.AddWithValue("@blogContent", blogContent);

            int rowsAffected = command.ExecuteNonQuery();

            Console.WriteLine(rowsAffected > 0 ? "Blog post inserted successfully." : "Failed to insert blog post.");

            connection.Close();
        }

        public void Edit()
        {
            Console.WriteLine("Blog Id: ");
            int blogId = Convert.ToInt32(Console.ReadLine());

            SqlConnection connection = new SqlConnection(_connectionString);
            connection.Open();

            string query = @"SELECT [BlogId]
                                ,[BlogTitle]
                                ,[BlogAuthor]
                                ,[BlogContent]
                            FROM [dbo].[Tbl_Blog]
                            WHERE [BlogId] = @blogId";
            
            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@blogId", blogId);

            SqlDataAdapter adapter = new SqlDataAdapter(command);
            DataTable dataTable = new DataTable();
            adapter.Fill(dataTable);

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

            connection.Close();
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

            SqlConnection connection = new SqlConnection(_connectionString);
            connection.Open();

            string query = @"UPDATE [dbo].[Tbl_Blog]
                            SET [BlogTitle] = @blogTitle,
                                [BlogAuthor] = @blogAuthor,
                                [BlogContent] = @blogContent
                            WHERE [BlogId] = @blogId";

            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@blogId", blogId);
            command.Parameters.AddWithValue("@blogTitle", blogTitle);
            command.Parameters.AddWithValue("@blogAuthor", blogAuthor);
            command.Parameters.AddWithValue("@blogContent", blogContent);

            int rowsAffected = command.ExecuteNonQuery();

            Console.WriteLine(rowsAffected > 0 ? "Blog post updated successfully." : "Failed to update blog post.");

            connection.Close();
        }


    }
}