using System.Data;
using System.Data.SqlClient;

namespace Shared;

public class AdoDotNetService
{
    private readonly string _connectionString;

    public AdoDotNetService(string connectionString)
    {
        _connectionString = connectionString;
    }
    public DataTable Query(string query, params SqlParameterModel[] sqlParameters)
    {
        SqlConnection connection = new SqlConnection(_connectionString);   

        connection.Open();

        SqlCommand command = new SqlCommand(query, connection);
        if (sqlParameters != null)
        {
            foreach (var param in sqlParameters)
            {
                command.Parameters.AddWithValue(param.Name, param.Value);
            }
        }

        SqlDataAdapter adapter = new SqlDataAdapter(command);
        DataTable dataTable = new DataTable();
        adapter.Fill(dataTable);

        connection.Close();

        return dataTable;
    }
    public int Execute(string query, params SqlParameterModel[] sqlParameters)
    {
        SqlConnection connection = new SqlConnection(_connectionString);   

        connection.Open();

        SqlCommand command = new SqlCommand(query, connection);
        if (sqlParameters != null)
        {
            foreach (var param in sqlParameters)
            {
                command.Parameters.AddWithValue(param.Name, param.Value);
            }
        }

        int result = command.ExecuteNonQuery();

        connection.Close();

        return result; 
    }
}

public class SqlParameterModel
{
    public string Name { get; set; }
    public object Value { get; set; }

    public SqlParameterModel(string name, object value)
    {
        Name = name;
        Value = value;
    }
}
