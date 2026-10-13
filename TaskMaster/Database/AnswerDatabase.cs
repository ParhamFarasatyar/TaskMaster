using TaskMaster.DataModel;
using Microsoft.Data.SqlClient;

namespace DataBase;

public static class AnswerDatabase
{
    private static string GetConnectionString(string username, string password)
    {
        return $"Server=192.168.1.138,1433;Database=TaskMaster;User Id={username};Password={password};TrustServerCertificate=True;";
    }
    
    public static void SaveAnswer(Answer answer)
    {
        (string username, string password) = SystemValidation.System.GetSqlAuthenticationInfo();

        string connectionString = GetConnectionString(username, password);

        using SqlConnection connection = new SqlConnection(connectionString);

        connection.Open();
        // enter table
        string sql = """
                     INSERT INTO (Table)
                     VALUES (@userName, @approvalStatus, @creadDate, @code, @grade, @descriptoin)
                     """;

        SqlCommand command = new SqlCommand(sql, connection);

        command.Parameters.AddWithValue("@userName", answer.UserName);
        command.Parameters.AddWithValue("@approvalStatus", answer.ApprovalStatus);
        command.Parameters.AddWithValue("@createdDate", answer.CreatedDate);
        command.Parameters.AddWithValue("@code", answer.Code);
        command.Parameters.AddWithValue("@grade", answer.Grade);
        command.Parameters.AddWithValue("@description", answer.Description);

        command.ExecuteNonQuery();

        command.Clone();
    }
}

